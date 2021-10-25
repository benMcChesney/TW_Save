import os 
import pandas as pd 
import xml.etree.ElementTree as ET
import shutil
import configparser
import time
import datetime
import sys

def parse_faction_xml ( path , file, data_array ):

    full_path = f"{path}{file}" 
    #print( 'loading ' ,full_path )
    tree = ET.parse( full_path )
    root = tree.getroot()
    row_to_write = parse_economy( root )
    faction_data = parse_faction_metadata( root )
    row_to_write['faction_name'] = faction_data['faction_name']

    data_array.append( row_to_write ) 

def parse_army_xml ( path , file, data_array ):

    full_path = f"{path}{file}" 
    #print( 'loading ' ,full_path )
    xml = ET.parse( full_path ).getroot()

    
    mf_xpath = "./rec/[@type = 'MILITARY_FORCE']"   
    mf_xml = xml.findall( mf_xpath )[0]
    #print( 'text is ' ,mf_xml.text)

    #mf_legacy_tags = mf_xml.findall( "./rec/[@type = 'MILITARY_FORCE_LEGACY']" ) 
    campaign_tags = mf_xml.findall( "./rec/rec/[@type = 'CAMPAIGN_LOCALISATION']" )[0] 
    localized = campaign_tags.findall( "asc")
    unit_campaign_name = localized[0].text

    if ( unit_campaign_name is None ):
        unit_campaign_name = 'null'

    #print( "localized length " , len ( mf_legacy_tags ))
    #print( 'found this many tags ' , len( unit_tags ) )

    units_arr_xpath = "./rec/ary/rec/rec/[@type = 'UNIT']"
    units_xml = mf_xml.findall( units_arr_xpath )
    #print( 'armg_tags ' , units_xml[0] )
    #print( ' # unit tags - ' , len( units_xml ) )

    faction_name_xpath = "./rec/[@type='COMMANDER_DETAILS']/asc"
    faction_commander_xml = units_xml[0].findall( faction_name_xpath )
    faction_name = faction_commander_xml[0].text
    
    #unit_tags =  mf_xml.findall( units_arr_xpath )
    #print( 'found this many tags ' , len( unit_tags ) )

    #unit_container_xpath = "./rec [@type = 'UNIT_CONTAINER']"
    #unit_container_tags = mf_xml.findall( unit_container_xpath  )
    #print( 'found this many tags ' , len( unit_container_tags ) )
    
    #print('debugger')
    for unit in units_xml:

        unit_name = unit.findall( "./rec/[@type='UNIT_RECORD_KEY']/asc")[0].text
        
        unit_stats_tags = unit.findall( "./u")
        unit_maxStrength = int( unit_stats_tags[1].text ) 
        unit_strength = int(  unit_stats_tags[2].text ) 

        garrison_a = int( unit_stats_tags[8].text )
        garrison_b = int( unit_stats_tags[9].text )
        #print('debugger')
        #bank_balance = int(rec.findall("./i[1]" )[0].text)
        #taxes = int(rec.findall("./u[2]" )[0].text)
        #army_upkeep = int(rec.findall( "./u[5]" )[0].text)
        json = {
            "unit_name": unit_name
            ,"strength":unit_strength
            ,"max_strength" : unit_maxStrength
            ,'army' : file 
            ,'faction' : faction_name
            , 'garrison_a' : garrison_a
            , 'garrison_b' : garrison_b
            , "campaign_localized" : unit_campaign_name
        }

        data_array.append( json ) 

    #row_to_write = parse_economy( root )
    #faction_data = parse_faction_metadata( root )
    #row_to_write['faction_name'] = faction_data['faction_name']
    #data_array.append( row_to_write ) 


def parse_economy( xml ):

    economy_xpath = "./rec/[@type = 'FACTION_ECONOMICS']"   
    for rec in xml.findall( economy_xpath ):
        bank_balance = int(rec.findall("./i[1]" )[0].text)
        taxes = int(rec.findall("./u[2]" )[0].text)
        army_upkeep = int(rec.findall( "./u[5]" )[0].text)
        json = {
            "bank_balance": bank_balance
            ,"taxes":taxes
            ,"army_upkeep" : army_upkeep
        }
        return json

def parse_faction_metadata( xml ):

    faction_xpath = "[@type = 'FACTION']"   
    for rec in xml.findall( faction_xpath ):
        faction_name = rec.findall("asc[1]" )[0].text
        json = {
            "faction_name": faction_name
        }
        return json


def get_session_guid( folder_path ):
    xml_path = f"{folder_path}\\campaign_env\\campaign_setup-main_warhammer.xml"
    campaign_env = ET.parse( xml_path ).getroot()
    session_xpath = "[@type = 'CAMPAIGN_SETUP']" 

    session_guid = -1
    session_id = -1 
    for rec in campaign_env.findall( session_xpath ):
        asc_tags = rec.findall("./asc" )
        for x in asc_tags:
            #print ( x.text )
            session_guid = asc_tags[2].text 
       # print ( 'u-----')
        u_tags = rec.findall("./u" )

        for x in u_tags:
            #print ( x.text )
            session_id = u_tags[0].text 
            
        #print('found!', session_guid)

    return [ session_guid , session_id ]


def get_turn_number ( folder_path ):
    #"C:\lab\tw_save\test_export\extract\save_game_header"

    xml_path = f".\\{folder_path}\\save_game_header"
    
    files = os.listdir( xml_path )
    for file in os.listdir( xml_path ):
        #print('file', file )
        # file name changes based on current player
        if file.endswith(".xml"):
            save_game_record = ET.parse( os.path.join( xml_path, file )).getroot()
            save_game_XPATH = "[@type = 'SAVE_GAME_HEADER']" 

            for rec in save_game_record.findall( save_game_XPATH ):
                u_tags = rec.findall("./u" )
                for x in u_tags:
                    #print ( x.text )
                    turn_num = u_tags[0].text 
                    return turn_num



def parse_extracted_factions_folder ( folder_path , data_array ) : 
    # get sessionId ( int, and GUID ) , get TURN 

    print( 'getting economy data from ', folder_path )

    #print ( 'debugger')
    # get factions data
    factions_dir = f"{cwd}\\{folder_path}\\factions\\"


    arr = os.listdir( factions_dir )
    for file in os.listdir( factions_dir ):
        #print('file', file )
        if file.endswith(".xml"):
            parse_faction_xml( factions_dir , file, data_array )


def parse_extracted_armies_folder ( folder_path , data_array ) : 
    print( 'getting armies data from ', folder_path )
    army_dir = f"{cwd}\\{folder_path}\\army\\"
    arr = os.listdir( army_dir )
    for file in os.listdir( army_dir ):
        #print('file', file )
        if file.endswith(".xml"):
            parse_army_xml( army_dir , file, data_array )


# testing region extraction only
def parse_region_xml( extracted_output , file ):
    full_path = os.path.join( extracted_output , 'region' ,  file ) 
    xml = ET.parse( full_path ).getroot()    

    # get name NK of region from file, NOT filename 
    region_name = xml.findall( "./[@type = 'REGION']/asc")[0].text

    # noticed that the 0th index will have the data we need, can skip others
    region_slots_xpath = "./rec/ary/[@type = 'REGION_SLOT_ARRAY']/xml_include"   
    region_slots_xml = xml.findall( region_slots_xpath )
    xml_index = 0 
    for xml_include_path in region_slots_xml: 
        _path = xml_include_path.attrib['path'] 
        if xml_index == 0 :
            region_slot_xml_path = os.path.join( extracted_output, _path )
            #print( ' at [0] region_slot' , region_slot_xml_path )
            region_slot_xml = ET.parse( region_slot_xml_path ).getroot()  
            controller_xpath = "./rec/rec/rec/[@type = 'BUILDING_BASE']/asc"
            controller_info = region_slot_xml.findall( controller_xpath )
            data_row = {
                'settlement_name' : region_name
                ,'settlement_type' : controller_info[0].text 
                ,'settlement_owner' : controller_info[1].text
                ,'settlement_government' : controller_info[2].text
            }
            return data_row 
        xml_index = xml_index + 1

def parse_extracted_region_folder(  extracted_output, new_array ) : 
    print( f'out={extracted_output}')
    region_dir = os.path.join( extracted_output , 'region')
    for file in os.listdir( region_dir ):
        if file.endswith(".xml"):
            region_row_data = parse_region_xml( extracted_output , file )
            new_array.append( region_row_data )

def clean_filename( input ):
 
    output = input.replace( "'" , "" )
    output = output.replace( " " , "_" )
    output = output.replace( "-" , "_" )
    return output 

def extract_save_esf( input_folder , file_path , output_dir ,  config ):
    cwd = os.getcwd()

    esf2xml_dir =  config['dependencies']['esf2xml']

    orig_path = file_path

    output_dir_full = f"{cwd}\\{output_dir}"
    if ( os.path.exists( output_dir_full )):
        shutil.rmtree( output_dir_full ) 
        print ( 'deleting existing folder')
        
    #shutil.copyfile( f"{input_folder}\\{orig_path}", esf_path)
    os.system( "c:")
    os.system( f"cd {esf2xml_dir}" )
    #cmd1 =  f"jruby {esf2xml_dir}esf2xml {esf_path} {cwd}\\{output_dir}{extracted_subfolder}"
    cmd1 =  f"jruby {esf2xml_dir}esf2xml {os.path.join(input_folder,file_path)} {output_dir}"
    print( cmd1 )
    return os.system( cmd1 )


def extract_save_file( save_folder , path , output_dir , config ):
    
    cwd = os.getcwd()
    save_folder = config['paths']['save_game_folder']
    extracted_subfolder = config['paths']['extracted_subfolder']

    dir_exists = os.path.isdir( output_dir )
    if dir_exists == False:

        ## Step 1 - copy .save to esf and run esf2xml
        p = path 
        file_ending = p[ path.rfind('.') : ]
        if ( file_ending == '.save'):
            print('need to convert to .esf for esf2xml to work, copying file....')
            p = clean_filename( f"{path[0:-5]}.esf" )
            src = os.path.join( save_folder, path ) 
            dest =  os.path.join(cwd, p)
            print ( f" copy {src} -> {dest}")
            copy_result = shutil.copyfile( src, dest )
           
        result = extract_save_esf( cwd , p , os.path.join(cwd,output_dir) , config ) 
        
        bFileExist = os.path.isfile( p )
        os.remove( p ) 
       # print('debugger ')
        if ( result ):
            print('error!', result )

        ## Step 2 - run 7z via cmd to unzip compressed_data.esf.xz
        xz_path = os.path.join(cwd, f'{output_dir}compressed_data.esf.xz')

        cmd_extract = f"7z e {xz_path} -o{cwd}\\{output_dir}" #compressed_data.esf.xz"
        result2 = os.system( cmd_extract )

        ## step 3 - run esf2xml on the extracted data a
        esf_path = f'{output_dir}compressed_data.esf'
        result2 = extract_save_esf( cwd , esf_path , os.path.join(cwd, f"{output_dir}{extracted_subfolder}"), config )  

        print('cleanup on archives to save disk space')
        os.remove( xz_path )
        os.remove( esf_path)

    else:
        print('skipping extract...')
    
    

def parse_args():
    
    
    print('Number of arguments:', len(sys.argv))
    print('Argument List:', str(sys.argv))

    if len(sys.argv) < 2 :
        print('not enough args. using default file...')
        return 'avelorn.txt'
    else:
        return sys.argv[1]

def parse_campaign_files_txt( path ):
    campaign_files = [ ]
    print('loading esf paths from file @ ', path )

    file = open(path, 'r') 
    lines = file.readlines()
    max_file = len( lines ) -  1 
    i = 0 
    for line in lines: 
        #print( line ) 
        # remove front quote and \n character
        #print ( f"'{line}'")
        if i < max_file:
            campaign_files.append( line[1:-2] ) 
        else:
            campaign_files.append( line[1:-1] ) 
        #    print('last line!', line[1:-1] )
        i += 1

    return campaign_files


def extract_campaign_files(campaign_files):

    max = len(campaign_files)-1
    i = 0
    for s in campaign_files:

        save_file_clean = clean_filename( s )
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
            dat1 = extract_save_file( 
            save_folder 
            , s
            ,templated
            ,config 
            )
        except:
            print("An exception occurred while extracting")
        
        # extracted_output = os.path.join(templated, extracted_subfolder)
    
        print( f"@ { i }  / { max } folders loaded and exported")
    i += 1
# ,"Chevaliers de Lyonesse_Auto-save.1141885391562.save"


def parse_campaign_files(campaign_files):
        
    max = len(campaign_files)-1
    i = 0 
    for s in campaign_files:

        
        save_file_clean = clean_filename( s )
        #'extract\\$filename_extract'

        out = output_folder
        templated = out.replace( '[$filename]', save_file_clean )
        
        full_path = os.path.join( save_folder , s )
        #output_dir =  f"{save_file_clean}_extract"
        file_modstamp = os.path.getmtime(full_path)
        ts = datetime.datetime.fromtimestamp( file_modstamp )
        unix_timestamp = int(time.mktime(ts.timetuple()))
        #print()
        
        extracted_output = os.path.join(templated, extracted_subfolder)
        
        try :
            session_id = get_session_guid( extracted_output )[1]
            turn_num = get_turn_number( extracted_output )
        
            # for faction economics at a high KPI level
            new_array = []
            parse_extracted_factions_folder( extracted_output, new_array ) 
            for r in new_array:
                r["session.id"] = session_id
                r["turn_num"] = turn_num
                r["modifiedOn"] = unix_timestamp
                econ_array.append( r ) 


            # parse army information
            new_array = []
            #session_id, session_guid, turn_num = 
            parse_extracted_armies_folder( extracted_output, new_array ) 

            # write in all data universal to this save file
            for r in new_array:
                r["session.id"] = session_id
                r["turn_num"] = turn_num
                r["modifiedOn"] = unix_timestamp
                army_array.append( r )

            econ_df = pd.DataFrame( econ_array )
            army_df = pd.DataFrame( army_array )

            econ_df.to_csv('export_faction_economy.csv')
            army_df.to_csv('export_army_unit.csv')
        except:
            print("An exception occurred while extracting")
    
        print( f"@ { i }  / { max } folders loaded and exported")
        i += 1
econ_array = []
army_array = [] 

'''
campaign_esf_paths_file = parse_args()
campaign_files = parse_campaign_files_txt( campaign_esf_paths_file )


config = configparser.ConfigParser()
config.read('config.ini')

esf2xml_dir =  config['dependencies']['esf2xml']
save_folder = config['paths']['save_game_folder']
output_folder = config['paths']['output_folder']
extracted_subfolder = config['paths']['extracted_subfolder']
cwd = os.getcwd()


max = len(campaign_files)-1
i = 0 
for s in campaign_files:

    save_file_clean = clean_filename( s )
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
        dat1 = extract_save_file( 
        save_folder 
        , s
        ,templated
        ,config 
        )
    except:
        print("An exception occurred while extracting")
    

    extracted_output = os.path.join(templated, extracted_subfolder)
    
    try :
        session_id = get_session_guid( extracted_output )[1]
        turn_num = get_turn_number( extracted_output )
    
        # for faction economics at a high KPI level
        new_array = []
        parse_extracted_factions_folder( extracted_output, new_array ) 
        for r in new_array:
            r["session.id"] = session_id
            r["turn_num"] = turn_num
            r["modifiedOn"] = unix_timestamp
            econ_array.append( r ) 


        # parse army information
        new_array = []
        #session_id, session_guid, turn_num = 
        parse_extracted_armies_folder( extracted_output, new_array ) 

        # write in all data universal to this save file
        for r in new_array:
            r["session.id"] = session_id
            r["turn_num"] = turn_num
            r["modifiedOn"] = unix_timestamp
            army_array.append( r )

        econ_df = pd.DataFrame( econ_array )
        army_df = pd.DataFrame( army_array )

        econ_df.to_csv('export_faction_economy.csv')
        army_df.to_csv('export_army_unit.csv')
    except:
        print("An exception occurred while extracting")
   
    print( f"@ { i }  / { max } folders loaded and exported")
    i += 1

'''

#print('end of run...')


# name 

#faction_name = root[0]'rec']['asc']

