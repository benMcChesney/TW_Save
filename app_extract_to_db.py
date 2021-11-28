import os 
import pandas as pd 
import xml.etree.ElementTree as ET
import shutil
import configparser
import time
import datetime
import sys
import esf_utils

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
cwd = os.getcwd()

econ_array = []
army_array = [] 
regions_array = [] 
characters_array = [] 

max = len(campaign_files)-1
i = 0 
for s in campaign_files:

    save_file_clean = esf_utils.clean_filename( s )
    #'extract\\$filename_extract'

    out = output_folder
    templated = out.replace( '[$filename]', save_file_clean )
    
    full_path = os.path.join( save_folder , s )
    #output_dir =  f"{save_file_clean}_extract"
    file_modstamp = os.path.getmtime(full_path)
    ts = datetime.datetime.fromtimestamp( file_modstamp )
    unix_timestamp = int(time.mktime(ts.timetuple()))
    #print()
    
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
    
    #try :
    session_id = esf_utils.get_session_guid( extracted_output )[1]
    turn_num = esf_utils.get_turn_number( extracted_output )

    # for faction economics at a high KPI level
    new_array = []
    esf_utils.parse_extracted_factions_folder( extracted_output, new_array ) 
    for r in new_array:
        r["session"] = session_id
        r["turn_num"] = turn_num
        r["modifiedOn"] = unix_timestamp
        econ_array.append( r ) 
    econ_df = pd.DataFrame( econ_array )
    econ_df.to_csv('export_faction_economy.csv' , index=False )

    # parse army information
    new_array = []
    #session_id, session_guid, turn_num = 
    esf_utils.parse_extracted_armies_folder( extracted_output, new_array ) 

    # write in all data universal to this save file
    for r in new_array:
        r["session"] = session_id
        r["turn_num"] = turn_num
        r["modifiedOn"] = unix_timestamp
        army_array.append( r )
    army_df = pd.DataFrame( army_array )
    army_df.to_csv('export_army_unit.csv' , index=False )

    # parse region information
    new_array = []
    #session_id, session_guid, turn_num = 
    esf_utils.parse_extracted_region_folder( extracted_output, new_array ) 

    # write in all data universal to this save file
    for r in new_array:
        r["session"] = session_id
        r["turn_num"] = turn_num
        r["modifiedOn"] = unix_timestamp
        regions_array.append( r )
    region_df = pd.DataFrame( regions_array )
    region_df.to_csv('export_region.csv' , index=False )

    # parse province information - only need to run 1x 
    if i == 0 :
        new_array = []
        province_array = [] 
        #session_id, session_guid, turn_num = 
        esf_utils.parse_province_campaign_data( extracted_output, new_array ) 

        # write in all data universal to this save file
        for r in new_array:
            province_array.append( r )
        province_df = pd.DataFrame( province_array )
        province_df.to_csv('export_province.csv', index=False )


     # parse character information
    new_array = []
    esf_utils.parse_character_folder( extracted_output, new_array ) 

    # write in all data universal to this save file
    for r in new_array:
        r["session"] = session_id
        r["turn_num"] = turn_num
        r["modifiedOn"] = unix_timestamp
        characters_array.append( r )
    characters_df = pd.DataFrame( characters_array )
    characters_df.to_csv('export_chracters.csv' , index=False )

    #except:
    #    print("An exception occurred while extracting")
   
    print( f"@ { i }  / { max } folders loaded and exported")
    i += 1



print('end of run...')


# name 

#faction_name = root[0]'rec']['asc']

