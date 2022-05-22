import pandas as pd 
import copy
import re
import io
from xml.etree import ElementTree as ET


def tag_get_type( element ):

    attribs = element.keys() 
    for attrib in attribs:
        if attrib.lower() == 'type':
            return element.get( attrib ) 

    return None

def flatten_xml_node_attribs( node , obj , prefix_path = '' ):
    attribs = node.keys() 
    #print ( attribs )
    for attrib in attribs:
        if attrib != '__my_parent__':
            #print ( f"{prefix_path} @{attrib} = {node.get( attrib)}"  )
            obj[ f"{prefix_path}{attrib}" ] = node.get( attrib ) 


def flatten_xml_children(  node , obj , prefix_path = '' ):
    json = {}
    ignore_tags = [ 'ary' , 'rec']
    count_tags = {} 
    for c in list(node) :
        tag = c.tag 
        #print( c.tag )
        
        if tag == 'yes' or tag == 'no':
            mapped_tag = 'bool'
            mapped_value = tag 
        
        else :
            mapped_tag = tag 
            mapped_value = c.text

        if mapped_tag in count_tags : 
            count_tags[ mapped_tag ] += 1
        else :
            count_tags[ mapped_tag ] = 0 

        curIndex = count_tags[ mapped_tag ]

        if tag not in ignore_tags : 
            obj[ f"{prefix_path}{mapped_tag}_{curIndex}" ] = mapped_value

    for c in list(node) :
        tag = c.tag 
        #print( c.tag )
        
        if tag in ignore_tags :
            if tag == 'rec' : 
                doc_path = f"{prefix_path}rec."
                if len( prefix_path ) == 0 : 
                    doc_path = f"rec."
                flatten_xml_node_attribs(  c , json , doc_path )
                flatten_xml_children( c , obj , doc_path )
            elif tag == 'ary' : 
                doc_path = f"{prefix_path}ary."
                if len( prefix_path ) == 0 : 
                    doc_path = f"ary."
            else :
                mapped_tag = tag 
                mapped_value = c.text
                
def addParentInfo(et):
    for child in et:
        child.attrib['__my_parent__'] = et
        addParentInfo(child)

def stripParentInfo(et):
    for child in et:
        child.attrib.pop('__my_parent__', 'None')
        stripParentInfo(child)

def getParent(et):
    if '__my_parent__' in et.attrib:
        return et.attrib['__my_parent__']
    else:
        return None

def flatten_xml_node(  node , obj , prefix_path = '', flatten_children = True, ignore_tag_type_array = [] ):
    json = {} 
    # these have a different behaivor and are flattened below
    flatten_ignore_tags = [ 'ary' , 'rec']
    count_tags = {} 
    for c in list(node) :
        tag = c.tag 
        mapped_tag = ""
        #print( c.tag )
        tag_type = tag_get_type(c)
        if tag == 'yes' or tag == 'no':
            mapped_tag = 'bool'
            mapped_value = tag 
        
        elif len(ignore_tag_type_array) > 0 != None and tag_type != None and tag_type not in ignore_tag_type_array : 
            fake_var = ''
            #print( f"hit flatten_xml_node bedrock '{tag} @='{tag_type}'") 
        else :
            mapped_tag = tag 
            mapped_value = c.text

        if mapped_tag in count_tags : 
            count_tags[ mapped_tag ] += 1
        else :
            count_tags[ mapped_tag ] = 0 

        curIndex = count_tags[ mapped_tag ]

        if tag not in flatten_ignore_tags : 
            obj[ f"{prefix_path}{mapped_tag}_{curIndex}" ] = mapped_value

    if flatten_children == True : 
        for c in list(node) :
            tag = c.tag 
            #print( c.tag )
            
            if tag in flatten_ignore_tags and tag not in ignore_tag_type_array :
                if tag == 'rec' : 
                    doc_path = f"{prefix_path}rec."
                    if len( prefix_path ) == 0 : 
                        doc_path = f"rec."
                    flatten_xml_node_attribs(  c , json , doc_path )
                    flatten_xml_node( c , obj , doc_path , False,  ignore_tag_type_array )

                else :
                    mapped_tag = tag 
                    mapped_value = c.text

# stop_climb_tag_type = "BATTLE_RESULT_ALLIANCE" 
# tag_label = "BATTLE_RESULT_UNIT"

def extract_and_expand_xpath( tree , tag_type , attrib_type ,  stop_climb_tag_type, tag_type_filter = []):
    
    addParentInfo( tree.getroot() )
    xpath_search = f".//*{tag_type}[@type = '{attrib_type}']" 
    tags = tree.findall( xpath_search )
    obj_array = [] 
    json = {}

    for tag in tags : 
        
        tag_obj = {} 
        flatten_xml_node_attribs( tag , tag_obj )
        flatten_xml_children( tag , tag_obj )
        parent = getParent(tag)
        parentJson = {} 
        prev_tag = copy.copy( tag ) #'rec'
        prev_type = copy.copy( attrib_type )

        # ignore for now
        while parent :
            
            if parent != None:
                pJson = {} 

                _label = f"@{prev_type}"
                # only should have to write the tag_obj 1x 
                if parentJson == {}:
                    pJson[ "child" ] = tag_obj 
                else :
                    pJson [ _label ] = parentJson 
            
                flatten_xml_node_attribs( parent, pJson, '' )
                flatten_xml_node( parent, pJson , '', True , tag_type_filter )
                
                #flatten_xml_node( parent , pJson , '' , False )
                parentJson = copy.deepcopy( pJson ) 

                #for attrib in pkeys : 
                #    if attrib == 'type':
                #        print ( f"<{parent.tag} @{attrib} = {parent.get( attrib )}/>")
                prev_tag = parent.tag

                # prevent climbing all the way to top of XML 
                if tag_get_type( parent )== stop_climb_tag_type :
                    break
            else:
                break

            parent = getParent(parent)
        obj_array.append( parentJson )
        prev_tag = {}
        prev_type = {}
    df = pd.json_normalize( obj_array )

    
    # sort columns based on alphabet
    # https://stackoverflow.com/questions/613183/how-do-i-sort-a-dictionary-by-value
    # d = {'one':1,'three':3,'five':5,'two':2,'four':4}
    # a = sorted(d.items(), key=lambda x: x[1]) 
    json = {}
    cols = df.columns
    for c in cols : 
        json[c] = len(c)
    sorted_json = sorted( json.items() , key=lambda x : x[1] )

    sorted_cols_array = [] 
    for item in sorted_json:
        sorted_cols_array.append( item[0] )

    df = df[ sorted_cols_array]
    return df
    

def clean_bad_xml_characters( src_path, output_path=None ):
    with io.open(src_path, 'r', encoding='utf-8') as f:
        contents = f.read()
        escape_illegal_xml_characters = lambda x: re.sub(u'[\x00-\x08\x0b\x0c\x0e-\x1F\uD800-\uDFFF\uFFFE\uFFFF\u0002]', '', x)
        substitute_string = escape_illegal_xml_characters(contents) #instead of ET.XML(my_xml_string)

        # append _clean.xml if no output provided 
        if output_path == None:
            output_path = src_path[0:-4] + '_clean.xml'
        with io.open(output_path, 'w', encoding='utf-8') as fw:
            fw.write( substitute_string )
        
        print(f'{src_path} -> {output_path}')
        return output_path