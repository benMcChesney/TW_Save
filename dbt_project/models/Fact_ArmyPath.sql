--{{ config(alias='Fact_ArmyPath', materialized='table' ) }}

/*

Scenarios to consider : 

- should be joining on the character lords name
- the army "belongs" the the lords
*/
        
with max_cte AS 
(
    select  turn_num  , max(modifiedOn) as maxModified
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
	--AND faction = 'wh2_main_skv_clan_skyre'
	And [type] in ( 'general' ) 
	 
) ,

max_cte_unts AS 
(
    select  turn_num , max(modifiedOn) as maxModified
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
, turn_loc_cte AS
(
	select 
		strength 
		,locX
		,locY
		,CONVERT( float , a.turn_num ) as turn_num 
		, nameMappingId
		, dim_f.faction_nk
		--, dim_f.id
		,dim_f.id as faction_id
	FROM
	(
		select 
			CONVERT( float , a.turn_num ) as turn_num  
			,d_nm.id as nameMappingId
			,SUM( strength ) as strength 
			, AVG( CAST( lords.[loc.x] as float) ) as locX
			, AVG( CAST( lords.[loc.y] as float) ) as locY
			, MAX( lords.faction ) as faction 

		FROM army_status_cte as a 
		INNER JOIN Dim_NameMapping as d_nm
		ON a.commander_nk = d_nm.name_nk

		INNER JOIN lords_cte as lords 
		ON a.commander_nk = lords.name_nk
		AND a.turn_num = lords.turn_num 

		where army_status = 'Standing Army'
		-- shortcut to test out just Ikit Claw
		--where d_nm.name_nk = 'names_name_1400581194_names_name_1574593534'
		GROUP BY d_nm.Id , a.turn_num
	) as a 
	INNER JOIN Dim_Factions as dim_f
	ON dim_f.faction_nk = a.faction
) 

, agg_loc_cte AS
(
	select 
		MIN(  turn_num )  as minTurnNum 
		, MAX( turn_num ) as maxTurnNum 
		, nameMappingId
	FROM turn_loc_cte
	GROUP BY nameMappingId
	
)


select a.* , b.minTurnNum, b.maxTurnNum
FROM turn_loc_cte as a 
INNER JOIN agg_loc_cte as b
ON b.nameMappingId = a.nameMappingId
--ORDER BY minTurnNum ASC, a.turn_num , a.nameMappingId ASC 
--where dim_f.id = 337 
--order by nm_name ASC , turn_num ASC
