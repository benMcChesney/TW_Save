import os 
import pandas as pd 
import xml.etree.ElementTree as ET
import shutil
import configparser
import time
import datetime
import sys
import xml.etree.ElementTree as ET
from lxml import etree
import copy

def parse_faction_xml ( path , file, data_array ):

    full_path = os.path.join( path , file )
    #print( 'loading ' ,full_path )
    tree = ET.parse( full_path )
    root = tree.getroot()
    row_to_write = parse_economy( root )
    faction_data = parse_faction_metadata( root )
    row_to_write['faction_name'] = faction_data['faction_name']
    row_to_write['faction_id'] = faction_data['faction_id']
    data_array.append( row_to_write ) 

def parse_army_xml ( path , file, data_array ):

    full_path = os.path.join( path , file ) 
    xml = ET.parse( full_path ).getroot()
    
    
    mf_xpath = "./rec/[@type = 'MILITARY_FORCE']"   
    mf_xml = xml.findall( mf_xpath )[0]

    campaign_tags = mf_xml.findall( "./rec/rec/[@type = 'CAMPAIGN_LOCALISATION']" )[0] 
    localized = campaign_tags.findall( "asc")
    unit_campaign_name = localized[0].text

    if ( unit_campaign_name is None ):
        unit_campaign_name = 'null'

    units_arr_xpath = "./rec/ary/rec/rec/[@type = 'UNIT']"
    units_xml = mf_xml.findall( units_arr_xpath )

    faction_name_xpath = "./rec/[@type='COMMANDER_DETAILS']/asc"
    faction_commander_xml = units_xml[0].findall( faction_name_xpath )
    faction_name = faction_commander_xml[0].text
    
    mf_legacy = "./rec/rec/rec/[@type = 'MILITARY_FORCE_LEGACY_HISTORY']"
    mf_legacy_xml = xml.find( mf_legacy )
    char_path = "./rec/ary/rec/rec/asc"
    name_tags = mf_legacy_xml.findall( char_path )
    tags_list = []
    for tag in name_tags : 
        if tag.text != None :
            tags_list.append( tag.text ) 
    commander_nk = '_'.join(tags_list)
    #print('debugger')
    unit_index = 0 
    for unit in units_xml:

        unit_name = unit.findall( "./rec/[@type='UNIT_RECORD_KEY']/asc")[0].text
        
        unit_stats_tags = unit.findall( "./u")
        unit_maxStrength = int( unit_stats_tags[1].text ) 
        unit_strength = int(  unit_stats_tags[2].text ) 

        garrison_a = int( unit_stats_tags[8].text )
        garrison_b = int( unit_stats_tags[9].text )
        #print('debugger')

        file_trunc = file.replace( '.xml' , '' )
        file_trunc = file_trunc.replace( 'ARMY-' , '' )

        army_index = False
        if len( file_trunc ) < 5 :
            army_index = int( file_trunc )
        
      
        json = {
            "unit_name": unit_name
            ,"strength":unit_strength
            ,"max_strength" : unit_maxStrength
            ,'army' : file 
            ,'faction' : faction_name
            #,'army_status': army_status
            , 'army_unit_index' : unit_index
            , 'army_index' : army_index
            #, 'garrison_a' : garrison_a
            #, 'garrison_b' : garrison_b
            , "localized_status" : unit_campaign_name
            , 'commander_nk' : commander_nk
        }

        data_array.append( json ) 
        unit_index += 1
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
        faction_id = rec.findall("u[1]" )[0].text
        json = {
            "faction_name": faction_name
            ,"faction_id" : faction_id
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
    factions_dir = os.path.join( folder_path , "factions" )


    arr = os.listdir( factions_dir )
    for file in os.listdir( factions_dir ):
        #print('file', file )
        if file.endswith(".xml"):
            parse_faction_xml( factions_dir , file, data_array )


def parse_extracted_armies_folder ( folder_path , data_array ) : 
    print( 'getting armies data from ', folder_path )
    army_dir = os.path.join( folder_path ,  "army" )
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


def parse_character_folder( extracted_output, new_array ):

    print( 'getting character data from ', extracted_output )

    #print ( 'debugger')
    # get factions data
    character_dir = os.path.join( extracted_output , "character" )


    arr = os.listdir( character_dir )
    for file in os.listdir( character_dir ):
        #print('file', file )
        if file.endswith(".xml"):
            #if file.find("general") != -1:
            # parse_faction_xml( factions_dir , file, data_array )
            
            full_path = os.path.join( character_dir , file ) 
            #print(full_path )
            xml = ET.parse( full_path ).getroot()
            
            coord_xpath = "./rec/rec[@type = 'LOCOMOTABLE']/v2"   
            coords = xml.findall( coord_xpath )[0]

            x = coords.attrib['x']
            y = coords.attrib['y']
            #print( coords , x ,y ) 
            
            details_xpath = "./rec/rec[@type = 'CHARACTER_DETAILS']/asc"   
            detail_tags = xml.findall( details_xpath )
            faction = detail_tags[0].text 
            type = detail_tags[2].text 
            key = detail_tags[3].text

            name_blocks = "./rec/rec[@type = 'CHARACTER_DETAILS']/rec[@type = 'CHARACTER_NAME']/ary/rec[@type='NAMES_BLOCK']/rec/asc"  
            name_tags = xml.findall( name_blocks )
            tags_list = []
            for tag in name_tags : 
                if tag.text != None :
                    tags_list.append( tag.text ) 
            name_nk = '_'.join(tags_list)
            data_row = {
                'faction' : faction
                ,'type' : type
                ,'key' : key 
                , 'loc.x' : x
                , 'loc.y' : y  
                , 'name_nk' : name_nk 
            }
            new_array.append( data_row )
                
def parse_extracted_region_folder(  extracted_output, new_array ) : 
    print( f'out={extracted_output}')
    region_dir = os.path.join( extracted_output , 'region')
    for file in os.listdir( region_dir ):
        if file.endswith(".xml"):
            region_row_data = parse_region_xml( extracted_output , file )
            new_array.append( region_row_data )

def parse_province_campaign_data( extracted_output, new_array ):
    full_path = os.path.join( extracted_output , 'campaign_env' , 'world-0000.xml' ) 
    xml = ET.parse( full_path ).getroot()    

    # get name NK of region from file, NOT filename 
    province_xml = xml.findall( "./rec/ary/rec[@type = 'PROVINCE_ARRAY']")

    xml_index = 0 
    for province in province_xml: 

        #if xml_index == 0 :
        # parse for testing 
        province_name = province.findall( './asc')[0].text 
        #print ( 'province name is ' , province_name ) 

        region_blocks = province.findall( "./rec/rec/ary/rec/asc" )
        #print ( 'length of region blocks ', len( region_blocks ) )
        for region in region_blocks : 
            data_row = {
                'province_name' : province_name
                ,'settlement_key' : region.text
            }
            new_array.append( data_row )
    xml_index = xml_index + 1


def parse_campaigndata_battle_result( extracted_output, new_array ) :
    
    path = os.path.join( extracted_output , "./campaign_env/campaign_model-0000.xml")
    #xml_path = "./extract/ikit_claw_turn1_battle_ar.13389809406.save_extract/extract/campaign_env/campaign_model-0000.xml"
    _parser = etree.XMLParser(recover=True)
    xml = ET.parse( path , parser = _parser )

    br_xpath = "./rec/rec/[@type = 'BATTLE_RESULTS']"
    # fun fact - ending in / gets all children, not / gets all tags 
    #army_units_xpath1 = f"{br_xpath}/ary/[ @type = 'ALLIANCES']/rec/rec/ary/rec/rec/ary[ @type = 'UNITS']/rec[ @type = 'UNITS']/rec[ @type = 'BATTLE_RESULT_UNIT']
    br_army_xpath = f"{br_xpath}/ary/[ @type = 'ALLIANCES']/rec/rec/ary/rec/rec[ @type = 'BATTLE_RESULT_ARMY']"
    br_army_tags = xml.findall( br_army_xpath )

    #new_array = [] 
    # Iterate within each army - link these together somehow 
    for army in br_army_tags:

        army_json = {} 
        # BATTLE_SETUP_FACTION
        faction_xpath = f"./rec[ @type='BATTLE_SETUP_FACTION']/asc"
        faction_tags = army.findall( faction_xpath )
        faction_nk = faction_tags[0].text
        
        army_attrib_xpath =  f"./s"
        army_attrib_tags = army.findall( army_attrib_xpath )

        ui_path_xpath = f"./asc"
        ui_path_tags = army.findall( ui_path_xpath )

        army_json[ 'leader'] = army_attrib_tags[0].text 
        army_json[ 'ui_path'] =  ui_path_tags[0].text
        army_json[ 'faction_nk'] = faction_nk

        # now parse individual units
        bru_xpath = "./ary[ @type = 'UNITS']/rec[ @type = 'UNITS']/rec[ @type = 'BATTLE_RESULT_UNIT']"
        bru_tags = army.findall( bru_xpath )

        for t in bru_tags : 
            unit_data = t.findall( 'u') 

            json = copy.deepcopy(army_json) 
            if len( unit_data ) >= 6 :
                json[ 'kills'] = unit_data[4].text
                json[ 'start_num_units'] = unit_data[0].text
                json[ 'end_num_units'] = unit_data[1].text

            unit_string_data = t.findall( 's') 
            if len( unit_data ) >= 1 :
                json[ 'unit_name'] = unit_string_data[0].text 

            new_array.append ( json ) 

            #print( 'debuger')

        #print( 'debuger')


def parse_diplomacy_from_factions_folder( extracted_output, data_array ):
    factions_dir = os.path.join( extracted_output , "factions" )

    print ( 'parsing factions folder for diplomacy...')

    arr = os.listdir( factions_dir )
    for file in os.listdir( factions_dir ):
        if file.endswith(".xml"):
            # print ( 'loading file' , file )
            full_path = os.path.join( factions_dir , file )
            # full_path = "C:/lab/tw_save/extract/Clan_Skryre_attack_2.1327371162.save_extract/extract/factions/wh2_main_skv_clan_skyre.xml"
            tree = ET.parse( full_path )
            root = tree.getroot()
            row = {} 
            #xpath = f"./rec/rec/ary/rec/[@type='OLD_DIPLOMACY_RELATIONSHIP']"rec[ @type = 'BATTLE_RESULT_ARMY']
            xpath = f"./rec/[ @type = 'OLD_DIPLOMACY_MANAGER']/ary/rec[ @type='DIPLOMACY_RELATIONSHIPS_ARRAY']"        
            tags = root.findall( xpath )

            faction_data = parse_faction_metadata( root )
            

            # to do - optimize and flatten all children 
            for tag in tags :
                row = {}
                row['faction_name'] = faction_data['faction_name']
                row['faction_id'] = faction_data['faction_id']

                #c = tag.findall( "./rec/")

                u0_tag = tag.find( "./rec/u")
                row[ 'diplomacy_faction_id_nk'] = u0_tag.text
           
                asc0_tag = tag.find( "./rec/asc")  
                row[ 'diplomacy_faction_id_nk2'] = asc0_tag.text
               
                #root.getchildren()
                #for child in children:
                
                #row[ 'faction_id_nk'] = u_tags[0].text

                #attrib_lst = list(tag)
                '''
                for x in range ( 0 , len( u_tags )):
                    row[ f"u_{x}"] = u_tags[x].text 

                i_tags = tag.findall( f"./rec/i" )    
                for x in range ( 0 , len( i_tags )):
                    row[ f"i_{x}"] = i_tags[x].text 

                asc_tags = tag.findall( f"./rec/asc" )    
                for x in range ( 0 , len( asc_tags )):
                    row[ f"asc_{x}"] = asc_tags[x].text 
                '''     
                data_array.append( row )
                row = {} 
                


def clean_filename( input ):
 
    output = input.replace( "'" , "" )
    output = output.replace( " " , "_" )
    output = output.replace( "-" , "_" )
    return output 

def extract_save_esf( input_folder , file_path , output_dir ,  config ):
    cwd = os.getcwd()

    esf2xml_dir =  config['dependencies']['esf2xml']

    orig_path = file_path

    output_dir_full = os.path.join( cwd , output_dir )
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

        cmd_extract = f"7z e {xz_path} -o{os.path.join(cwd,output_dir)}" #compressed_data.esf.xz"
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
