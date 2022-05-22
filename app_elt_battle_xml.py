
import xml.etree.ElementTree as ET
import pandas as pd 
import copy
import xml_utils as xu
print( 'depenencies loaded')

'''
# now loading xml file directly from campaign 
tag_type_filter = [ "UNITS" , "BATTLE_RESULT_UNIT" ]
path = f".\extract\Clan_Skryre_HEROIC_VICTORY.214083363608.save_extract\extract\campaign_env\campaign_model-0000_clean.xml"
stop_climb_tag_type = "BATTLE_RESULT_ALLIANCE"

tree = ET.parse( path)  
tree.getroot()
xu.extract_and_expand_xpath( tree, 'rec' , "BATTLE_RESULT_UNIT"  ,  tag_type_filter )
print('this happened!')
'''

# file unzipped can come with bad charactesr which ET can't parse 
path = f".\extract\\battle_result.434083363608.save_extract\extract\campaign_env\campaign_model-0000.xml"

output_file = xu.clean_bad_xml_characters( path )
tree = ET.parse( output_file )  
root = tree.getroot()

stop_climb_tag_type = "BATTLE_RESULT_ALLIANCE" 
tag_type_filter = [ "UNITS" , "BATTLE_RESULT_UNIT" ]
attrib_type = "BATTLE_RESULT_UNIT"
tag_type = "rec"
#def extract_and_expand_xpath( tree , tag, attrib_type , tag_label , stop_climb_tag_type, tag_type_filter = []):

#tree , attrib_type , tag_label , stop_climb_tag_type, tag_type_filter = []
df = xu.extract_and_expand_xpath( tree , tag_type , attrib_type , stop_climb_tag_type, tag_type_filter=tag_type_filter )

# remove blank columns
df = df.dropna(axis=1, how='any', thresh=1.0)

# remove columns with only 1 unique value, there's nothing useful here for us in this one off!
for col in df.columns:
    if len(df[col].unique()) == 1:
        df.drop(col,inplace=True,axis=1)

output_path = 'battle_result_unit.csv'
df.to_csv(  output_path )
print ( f"CSV write out to {output_path}")
