{{ config(alias='Fact_UnitHistory', materialized='table' ) }}

        
with max_cte AS 
(
    select  turn_num, max(modifiedOn) as maxModified
    FROM public."tw_army_units"
    group by turn_num

), 
clean_cte AS
(
    select  
            unit_name
            ,strength
            ,max_strength
            ,army
            ,faction
            ,garrison_a
            ,garrison_b
            ,campaign_localized
            ,a.turn_num as turn_num
            ,modifiedOn
    FROM max_cte as a
    inner join public."tw_army_units" as b
    ON a.turn_num = b.turn_num
        AND a.maxModified = b.modifiedOn

)
select 
    strength
    , max_strength
    , dim_u.id as unit_id
    , dim_a.id as army_id 
    , dim_f.id as faction_id 
    , units.campaign_localized
    , units.turn_num as turn_id
from clean_cte as units
INNER JOIN {{ ref('Dim_Factions') }}  as dim_f
ON units.faction = dim_f.faction_nk

INNER JOIN {{ ref('Dim_Units')}} as dim_u 
ON units.unit_name = dim_u.unit_nk

INNER JOIN  {{ ref('Dim_Armies') }}  as dim_a
ON units.army = dim_a.army
AND (units.turn_num >= dim_a.validFrom 
AND units.turn_num < dim_a.validTo)


/*
with regions_cte AS
(
    select 
        CAST ( turn_num as int ) as turn_num
        , settlement_name
        , settlement_owner 
        FROM public.tw_regions 
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
*/

/*

select 
    strength
    , max_strength
    , du.id as unit_id
    , da.id as army_id 
    , df.id as faction_id 
    , eau.campaign_localized
    , eau.turn_num as turn_id
from clean_cte as eau
INNER JOIN {{ ref('Dim_Factions') }}  as df
ON eau.faction = df.faction_nk

INNER JOIN tw_save_production..Dim_Units as du
ON eau.unit_name = du.unit_nk

INNER JOIN tw_save_production..Dim_Armies as da
ON eau.army = da.army_nk
*/
/*
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
--INNER JOIN public."Dim_Geography" as dim_geo 
INNER JOIN {{ ref('Dim_Geography') }} as dim_geo 
ON a.settlement_name = dim_geo.region_name

INNER JOIN{{ ref('Dim_Factions') }} as dim_f 
ON a.settlement_owner = dim_f.faction_nk 

INNER JOIN {{ ref( 'Dim_Calendar')}} as dim_cal
ON a.turn_num = dim_cal.turn_num 
WHERE 1=1 

--AND settlement_owner = 'wh2_main_skv_clan_skyre'
-- Capital Skavenblight! Within Skaven control the entire campaign
--AND settlement_name = 'wh2_main_skavenblight_skavenblight'
--AND settlement_name = 'wh_main_tilea_luccini'

order by settlement_name, a.turn_num */

