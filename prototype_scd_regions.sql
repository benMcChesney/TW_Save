DROP TABLE IF EXISTS Test_RegionData;

CREATE TABLE Test_RegionData(

    region_name varchar(10) NOT NULL 
    , region_owner varchar( 10 ) NULL 
    , turn_num int NOT NULL 
)

INSERT INTO Test_RegionData( 
    region_name
    , region_owner 
    , turn_num 
)

    VALUES
    ( 'regionA', 'faction1' , '1' )
    , ( 'regionA', 'faction1' , '2' )
    , ( 'regionA', 'faction1' , '3' )
    , ( 'regionB', 'faction2' , '1' )
    , ( 'regionB', NULL , '2' )
    , ( 'regionB', NULL , '3' )
    , ( 'regionC', 'faction3' , '1' )
    , ( 'regionC', NULL , '2' )
    , ( 'regionC', 'faction1' , '3' )
    , ( 'regionD', NULL , '1' )
    , ( 'regionD', NULL , '2' )
    , ( 'regionD', 'faction4' , '3' )

select 
    *
FROM (
    select 
        *
        , ROW_NUMBER() OVER (PARTITION BY region_name, region_owner ORDER BY turn_num) as parition_row 
        , MIN(turn_num) over (partition by region_name, region_owner ORDER BY turn_num) as valid_from_turn
        , MAX(turn_num) over (partition by region_name, region_owner ) as valid_to_turn
    FROM Test_RegionData
) as q 
where parition_row = 1 
