{{ config(alias='Dim_Calendar', materialized='table' ) }}

with window_cte AS 
(
    select 
        * 
        , CAST( turn_num as int ) as turnNumInt 
        , ROW_NUMBER() OVER ( PARTITION BY  turn_num, modifiedOn ORDER BY modifiedOn DESC ) as saveModifiedRank 
        FROM tw_regions
), 
chaos_turns_cte AS 
(
    select
        faction
        , CAST( MIN( turn_num ) as int ) as min_turn_num 
        , CAST( MAX( turn_num ) as int ) as max_turn_num
    from public.tw_army_units
    where faction = 'wh_main_chs_chaos'
    GROUP BY faction
)
-- use the regions file ( smallest ) to generate turn_num 
select
    ROW_NUMBER() OVER ( ORDER BY turnNumInt )as Id 
    , turnNumInt as turn_Num 
    , modifiedOn  
    , CASE 
        WHEN turnNumInt < min_turn_num
            THEN 'Before Chaos Invasion'
        WHEN turnNumInt >= min_turn_num AND turnNumInt <= max_turn_num
            THEN 'Chaos Invasion'
        WHEN turnNumInt > max_turn_num 
            THEN 'Chaos Defeated'
        END as IsChaosInvasion
FROM window_cte 
INNER JOIN chaos_turns_cte 
ON 1=1 
where saveModifiedRank = 1 
order by turn_num 

