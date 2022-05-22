--{{ config(alias='Fact_ArmyPath', materialized='table' ) }}

/*

Scenarios to consider : 

- should be joining on the character lords name
- the army "belongs" the the lords
*/
        
with max_cte AS 
(
    select  turn_num, max(modifiedOn) as maxModified
    FROM tw_characters
    group by turn_num

), 
clean_cte AS
(
    select  
         b.* 
    FROM max_cte as a
    inner join tw_characters as b
    ON a.turn_num = b.turn_num
        AND a.maxModified = b.modifiedOn

),

lords_cte AS (

	select 
		c.*
	FROM clean_cte as c 
	where 1=1
	AND faction = 'wh2_main_skv_clan_skyre'
	And [type] in ( 'general' ) 
	 
) ,

max_cte_unts AS 
(
    select  turn_num, max(modifiedOn) as maxModified
    FROM tw_army_units
    group by turn_num

)
,clean_units_cte AS
(
    select  
         b.* 

    FROM max_cte_unts as a
    inner join tw_army_units as b
    ON a.turn_num = b.turn_num
		AND a.maxModified = b.modifiedOn
)
, army_status_cte AS 
(
	select 
		* 
		, 
	case 
		when localized_status LIKE '%garrison%'
		then 'Garrison Army'
		else 
			'Standing Army'
	end as army_status
	FROM clean_units_cte
)

select 
	
	strength 
	,locX
	,locY
	,turn_num
	,nameMapping_id
	,dim_f.id as faction_id
FROM
(
	select 
	
		a.turn_num
		,d_nm.id as nameMapping_id
		,SUM( strength ) as strength 
		, AVG( lords.[loc.x] ) as locX
		, AVG( lords.[loc.y] ) as locY
		, MAX( lords.faction ) as faction 
	FROM army_status_cte as a 
	INNER JOIN Dim_NameMapping as d_nm
	ON a.commander_nk = d_nm.name_nk

	INNER JOIN lords_cte as lords 
	ON a.commander_nk = lords.name_nk
	AND a.turn_num = lords.turn_num 

	-- shortcut to test out just Ikit Claw
	--where d_nm.name_nk = 'names_name_1400581194_names_name_1574593534'
	GROUP BY d_nm.id, a.turn_num
) as a 
INNER JOIN Dim_Factions as dim_f
ON dim_f.faction_nk = a.faction

order by nameMapping_id , turn_num 
