# TW Save

Is an application and code for parsing ESF files from Total War : Warhammer 2, and extracting CSV Files for data visualization

## Dependencies

- 7z ( https://www.7-zip.org/ )
- esf2xml ( https://github.com/taw/etwng/blob/master/esfxml/esf2xml ) 


## ETL Planning for Data Visualization

open "etl_dw_planning_sheet.ods" 

tab 'ETL_sheet' reviews the transform steps 
tab 'bus matrix' shows the overlap of common dimensions 

# Settings
change settings in config.ini for local paths 

## Usage

```bash
python app_extract_to_db.py

```

## To Do 
- add .ini file for paths
- accept command args

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)