{{ config(alias='Fact_RegionControl', materialized='table' ) }}

with regions_cte AS
(
    select 
        CAST ( turn_num as int ) as turn_num
        , settlement_name
        , settlement_owner 
        FROM tw_regions 
), 
regions_with_prev_cte AS 
(
    select 
        * 
        , CASE 
            WHEN turn_num = 1  
                THEN NULL 
            ELSE 
                LAG ( settlement_owner, 1 ) over ( ORDER BY settlement_name, turn_num )
            END as previous_owner
    FROM regions_cte
)
, region_changed_cte AS 
(
    select * 
    FROM regions_with_prev_cte
    where settlement_owner != previous_owner 
        OR previous_owner IS NULL 
)
, max_turn AS 
(
    select MAX( turn_num ) as maxTurn
    FROM regions_cte
)
,region_changed_range_cte AS 
(
    select 
    *
    , LEAD( turn_num , 1 ) over ( ORDER BY settlement_name, turn_num ) as nextTurn
    FROM region_changed_cte
    JOIN max_turn 
    ON 1=1 
)

select 
    --settlement_name as region_name
    --, settlement_owner as region_owner
    dim_geo.id as geographyId 
    , dim_f.id as factionId
    , a.turn_num as validFrom
    , case 
        WHEN nextturn = 1 or nextturn IS NULL 
            THEN maxTurn 
        ELSE 
            nextTurn 
        END as validTo
    , dim_cal.turn_num as turnId
    -- ADD GEOs reference
FROM region_changed_range_cte as a 
--INNER JOIN {{ ref('Dim_Geography') }} as b 
--INNER JOIN Dim_Geography as dim_geo 
INNER JOIN {{ ref('Dim_Geography') }} as dim_geo 
ON a.settlement_name = dim_geo.region_nk

INNER JOIN{{ ref('Dim_Factions') }} as dim_f 
--INNER JOIN Dim_Factions as dim_f 
ON a.settlement_owner = dim_f.faction_nk 

INNER JOIN {{ ref( 'Dim_Calendar')}} as dim_cal
--INNER JOIN Dim_Calendar as dim_cal
ON a.turn_num = dim_cal.turn_num
WHERE 1=1 

--AND settlement_owner = 'wh2_main_skv_clan_skyre'
-- Capital Skavenblight! Within Skaven control the entire campaign
--AND settlement_name = 'wh2_main_skavenblight_skavenblight'
--AND settlement_name = 'wh_main_tilea_luccini'

--order by settlement_name, a.turn_num 

