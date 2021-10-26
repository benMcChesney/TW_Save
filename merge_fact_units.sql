

-- "Merge" Dim_Factions
TRUNCATE TABLE tw_save_production..Dim_Factions ;

with step1 AS
(
    select
        distinct( faction ) 
        ,CHARINDEX( '_', faction ) as us_0
        FROM tw_save_staging..export_army_unit_tk
)
,step2 AS (
    select * , 
        CHARINDEX( '_', faction, us_0 + 1 ) as us_1
    FROM step1
),
step3 AS (
    select * , 
        CHARINDEX( '_', faction, us_1 + 1 ) as us_2
        , LEN( faction ) as us_3
    FROM step2
) 
, split_cols_cte AS ( 

    select 
    step3.faction as faction_NK
    ,SUBSTRING( faction, 0 , us_0 ) as game_source
    ,SUBSTRING( faction, us_0 + 1 , us_1 - us_0 - 1 ) as dlc_source
    ,SUBSTRING( faction, us_1 + 1 , us_2 - us_1 - 1) as faction
    ,SUBSTRING( faction, us_2 + 1 , LEN(faction) ) as lord
    FROM step3
)

INSERT INTO tw_save_production..Dim_Factions ( 
    faction_nk
    ,game_source
    ,dlc_source
    ,faction_name
    ,lord
)
select 
    faction_nk
    ,game_source
    ,dlc_source
    ,faction
    ,lord 
from split_cols_cte

-- "Merge" Dim_Armies
TRUNCATE TABLE tw_save_production..Dim_Armies ;
INSERT INTO tw_save_production..Dim_Armies (
    army_nk
    ,faction_id
)
select 
    distinct( army ) as army_nk
    ,dim_f.id as faction_id

from tw_save_staging..export_army_unit_tk as eau

INNER JOIN tw_save_production..Dim_Factions as dim_f
ON dim_f.faction_nk = eau.faction;


-- "Merge" Dim_Armies
TRUNCATE TABLE tw_save_production..Dim_Armies ;
INSERT INTO tw_save_production..Dim_Armies (
    army_nk
    ,faction_id
)
select 
    distinct( army ) as army_nk
    ,dim_f.id as faction_id

from tw_save_staging..export_army_unit_tk as eau

INNER JOIN tw_save_production..Dim_Factions as dim_f
ON dim_f.faction_nk = eau.faction;

-- "Merge" Dim_Armies

TRUNCATE TABLE tw_save_production..Dim_Armies ;

INSERT INTO tw_save_production..Dim_Armies (
    [army_nk]
    ,[faction_id]
)

select 
    distinct( army ) as army_nk
    , NULL
FROM tw_save_staging..export_army_unit_tk as eau;


-- "Merge" Fact_UnitHistory
TRUNCATE TABLE tw_save_production..Fact_UnitHistory ;

        
with max_cte AS 
(
    select  turn_num, max(modifiedOn) as maxModified
    FROM export_army_unit_TK
    group by turn_num

), 
clean_cte AS
(
    select  
            [unit_name]
            ,[strength]
            ,[max_strength]
            ,[army]
            ,[faction]
            ,[garrison_a]
            ,[garrison_b]
            ,[campaign_localized]
            ,[session_id]
            ,a.[turn_num] as turn_num
            ,[modifiedOn]
    FROM max_cte as a
    inner join export_army_unit_TK as b
    ON a.turn_num = b.turn_num
        AND a.maxModified = b.modifiedOn

)

INSERT INTO tw_save_production..Fact_UnitHistory (
    strength
    ,max_strength
    ,unit_id
    ,army_id
    ,faction_id
    ,campaign_localized
    ,turn_id
)



select 
    strength
    , max_strength
    , du.id as unit_id
    , da.id as army_id 
    , df.id as faction_id 
    , eau.campaign_localized
    , eau.turn_num as turn_id
from clean_cte as eau
INNER JOIN tw_save_production..Dim_Factions as df
ON eau.faction = df.faction_nk

INNER JOIN tw_save_production..Dim_Units as du
ON eau.unit_name = du.unit_nk

INNER JOIN tw_save_production..Dim_Armies as da
ON eau.army = da.army_nk
