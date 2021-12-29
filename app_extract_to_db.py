import os 
import pandas as pd 
import xml.etree.ElementTree as ET
import shutil
import configparser
import time
import datetime
import sys
import esf_utils
import pyodbc
import sqlalchemy
from sqlalchemy import create_engine

def create_data_output( label , destination ) :
    
    obj = { 
        "array" : []
        , "destination" : destination
    }
    return obj 

def apply_metadata( df, session_id, turn_num , modifiedOn ):
    df[ 'session_guid_id' ] = session_id 
    df[ 'turn_num' ] = turn_num 
    df[ 'modifiedOn' ] = modifiedOn
    df.columns = df.columns.astype(str)
    return df 

def parse_args():
    
    
    print('Number of arguments:', len(sys.argv))
    print('Argument List:', str(sys.argv))

    if len(sys.argv) < 2 :
        print('not enough args. using default file...')
        return 'skaven_clan_skryre_me.txt'
    else:
        return sys.argv[1]

campaign_files = [ ]

campaign_esf_paths_file = parse_args()
print('loading esf paths from file @ ', campaign_esf_paths_file )

file = open(campaign_esf_paths_file, 'r') 
lines = file.readlines()
max_file = len( lines ) -  1 
i = 0 
for line in lines: 
    # remove front quote and \n character
    if i < max_file:
        campaign_files.append( line[1:-2] ) 
    else:
        campaign_files.append( line[1:-1] ) 
    i += 1

config = configparser.ConfigParser()
config.read('config.ini')
#print ( config['dependencies']['esf2xml'])
#print ( config['paths']['save_game_folder'])
#print ( config['paths']['output_folder']) 

esf2xml_dir =  config['dependencies']['esf2xml']
save_folder = config['paths']['save_game_folder']
output_folder = config['paths']['output_folder']
extracted_subfolder = config['paths']['extracted_subfolder']

c = config['output_db']
conn_string = f"mssql+pyodbc://{c['user']}:{c['pwd']}@{c['server']}/{c['database']}?driver={c['driver']}&trusted_connection=Yes"
engine = sqlalchemy.create_engine( conn_string , echo=False) 
engine.connect() ; 



cwd = os.getcwd()

'''
econ_array = []
army_array = [] 
regions_array = [] 
characters_array = [] 
battle_result_array = [] 
diplomacy_array = [] 
'''

# setup output for data 
econ = create_data_output( "economy" , "tw_economy")
army = create_data_output( "army" , "tw_army_units")
region = create_data_output( "region" , "tw_regions")
characters = create_data_output( "characters" , "tw_characters")
battle_result = create_data_output( "battle_result" , "tw_battle_result" )
diplomacy = create_data_output( "diplomacy" , "tw_diplomacy" )

outputs = [ econ, army, region, characters]
sql_scripts = """
SELECT
  TABLE_NAME
FROM
  tw_save.INFORMATION_SCHEMA.TABLES
where table_name like 'tw_%'
"""
# create sqlalchemy connection 
conn = engine.raw_connection()

tables_df = pd.read_sql( sql_scripts , conn )
tables = list(tables_df["TABLE_NAME"])
for o in outputs : 
    dest = o["destination"]
    if dest in tables :
        cmd = f'DROP TABLE {dest}' 
        conn.execute( cmd )
        # print ( cmd )

conn = engine.connect() 
max = len(campaign_files)-1
i = 0 
indexOffset = 0 ; 
for s in campaign_files:

    
    if i >= indexOffset :    
        save_file_clean = esf_utils.clean_filename( s )
        out = output_folder
        templated = out.replace( '[$filename]', save_file_clean )
        full_path = os.path.join( save_folder , s )
        file_modstamp = os.path.getmtime(full_path)
        ts = datetime.datetime.fromtimestamp( file_modstamp )
        unix_timestamp = int(time.mktime(ts.timetuple()))
        
        try:
            dat1 = esf_utils.extract_save_file( 
            save_folder 
            , s
            ,templated
            ,config 
            )
        except:
            print("An exception occurred while extracting")
        

        extracted_output = os.path.join(templated, extracted_subfolder)
        
        try :
            session_id = esf_utils.get_session_guid( extracted_output )[1]
            turn_num = esf_utils.get_turn_number( extracted_output )
                        
            # for faction economics at a high KPI level
            new_array = []
            esf_utils.parse_extracted_factions_folder( extracted_output, new_array ) 

            econ_df = pd.DataFrame( new_array )
            econ_df = apply_metadata( econ_df , session_id , turn_num , unix_timestamp )
            dest = econ["destination"]
            if i == 0 :
                print ( 'init econ db ')
                # quick cheat - select top(0) into table for blank table with the right cols
                top_df = econ_df.head(0) 
                top_df.to_sql(  name=dest , con=conn, if_exists="replace", index=False ) 

            econ_df.to_sql( name=dest , con=conn, index=False, if_exists="append" )

            # parse army information
            new_array = []

            esf_utils.parse_extracted_armies_folder( extracted_output, new_array ) 
            army_df = pd.DataFrame( new_array )
            army_df = apply_metadata( army_df , session_id , turn_num , unix_timestamp )
            dest = army["destination"]
            if i == 0 :
                print ( 'init army db ')
                # quick cheat - select top(0) into table for blank table with the right cols
                top_df = army_df.head(0) 
                top_df.to_sql(  name=dest , con=conn, if_exists="replace", index=False ) 

            army_df.to_sql( name=dest , con=conn, index=False, if_exists="append" )
            
            # parse region information
            new_array = []
            #session_id, session_guid, turn_num = 
            esf_utils.parse_extracted_region_folder( extracted_output, new_array ) 

            region_df = pd.DataFrame( new_array )
            region_df = apply_metadata( region_df , session_id , turn_num , unix_timestamp )
            dest = region["destination"]
            if i == 0 :
                print ( 'init region db ')
                # quick cheat - select top(0) into table for blank table with the right cols
                top_df = region_df.head(0) 
                top_df.to_sql(  name=dest , con=conn, if_exists="replace", index=False ) 

            region_df.to_sql( name=dest , con=conn, index=False, if_exists="append" )
            # parse province information - only need to run 1x 
            if i == 0 :
                new_array = []
                print ( 'init province db ')
                #session_id, session_guid, turn_num = 
                esf_utils.parse_province_campaign_data( extracted_output, new_array ) 
                province_df = pd.DataFrame( new_array )
                province_df.to_sql(  name='tw_province' , con=conn, if_exists="replace", index=False )
            
        
            # parse character information
            new_array = []
            esf_utils.parse_character_folder( extracted_output, new_array )
            characters_df = pd.DataFrame( new_array  )
            characters_df = apply_metadata( characters_df , session_id , turn_num , unix_timestamp )
            dest = characters["destination"]
            if i == 0 :
                print ( 'init character db ')
                # quick cheat - select top(0) into table for blank table with the right cols
                top_df = characters_df.head(0) 
                top_df.to_sql(  name=dest , con=conn, if_exists="replace", index=False ) 

            characters_df.to_sql( name=dest , con=conn, index=False, if_exists="append" )
            #characters_df.to_csv('export_chracters.csv' , index=False )
        
            new_array = []
            esf_utils.parse_campaigndata_battle_result( extracted_output, new_array ) 

            battle_result_df = pd.DataFrame( new_array  )
            battle_result_df = apply_metadata( battle_result_df , session_id , turn_num , unix_timestamp )
            dest = battle_result["destination"]
            if i == 0 :
                print ( 'init battle_result db ')
                # quick cheat - select top(0) into table for blank table with the right cols
                top_df = battle_result_df.head(0) 
                top_df.to_sql(  name=dest , con=conn, if_exists="replace", index=False ) 

            battle_result_df.to_sql( name=dest , con=conn, index=False, if_exists="append" )
            # battle_result_df.to_csv('export_battle_result.csv' , index=False )
            
            new_array = []
            dest = diplomacy["destination"]
            esf_utils.parse_diplomacy_from_factions_folder( extracted_output, new_array ) 
            diplomacy_array_df = pd.DataFrame( new_array )
            diplomacy_array_df = apply_metadata( diplomacy_array_df , session_id , turn_num , unix_timestamp )
            
            if i == 0 :
                print ( 'init diplomacy db ')
                # quick cheat - select top(0) into table for blank table with the right cols
                top_df = diplomacy_array_df.head(0) 
                top_df.to_sql(  name=dest , con=conn, if_exists="replace", index=False ) 

            diplomacy_array_df.to_sql( name=dest , con=conn, index=False, if_exists="append" )
            
        except:
            print("An exception occurred while extracting")
    
        print( f"@ { i }  / { max } folders loaded and exported")

    else : 
        print ( f"skipping #{i} ")
    i += 1



print('end of run...')


# name 

#faction_name = root[0]'rec']['asc']

