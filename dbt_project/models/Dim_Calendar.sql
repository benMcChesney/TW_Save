{{ config(alias='Dim_Calendar', materialized='table' ) }}

with window_cte AS 
(
    select 
        CONVERT( int ,turn_num ) as turn_num
		, DATEADD(MONTH, 3 * CONVERT( int ,turn_num ) , '1900/01/01') as date 
		, tw_regions.settlement_name
        , ROW_NUMBER() OVER ( PARTITION BY  turn_num, modifiedOn ORDER BY modifiedOn DESC ) as saveModifiedRank 
        FROM tw_regions
), 
chaos_turns_cte AS 
(
    select
        faction
        ,  MIN( CONVERT( int ,turn_num ) )  as min_turn_num 
        ,  MAX(  CONVERT( int ,turn_num ) ) as max_turn_num
    FROM tw_army_units
	where faction = 'wh_main_chs_chaos'
    GROUP BY faction
)


-- use the regions file ( smallest ) to generate turn_num 
select
    CONVERT( date, [date] ) as date  
	, CONVERT( varchar(10) , date , 112 ) as datekey 
	, turn_num as id 
	,turn_num 
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


