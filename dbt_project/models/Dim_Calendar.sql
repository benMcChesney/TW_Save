{{ config(alias='Dim_Calendar', materialized='table' ) }}

with window_cte AS 
(
    select 
        * 
        , ROW_NUMBER() OVER ( PARTITION BY  turn_num, modifiedOn ORDER BY modifiedOn DESC ) as saveModifiedRank 
        FROM tw_regions
), 
chaos_turns_cte AS 
(
    select
        faction
        ,  MIN( turn_num )  as min_turn_num 
        ,  MAX(  turn_num ) as max_turn_num
    FROM tw_army_units
	where faction = 'wh_main_chs_chaos'
    GROUP BY faction
)


-- use the regions file ( smallest ) to generate turn_num 
select
    ROW_NUMBER() OVER ( ORDER BY turn_num )as Id 
    , modifiedOn  
	, turn_num
    , CASE 
        WHEN turn_num < min_turn_num
            THEN 'Before Chaos Invasion'
        WHEN turn_num >= min_turn_num AND turn_num <= max_turn_num
            THEN 'Chaos Invasion'
        WHEN turn_num > max_turn_num 
            THEN 'Chaos Defeated'
        END as IsChaosInvasion
FROM window_cte 
INNER JOIN chaos_turns_cte 
ON 1=1 
where saveModifiedRank = 1 
--order by turnNumInt 

