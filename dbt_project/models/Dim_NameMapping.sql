{{ config(alias='Dim_NameMapping', materialized='table' ) }}

with cte as 
(
	select distinct(commander_nk ) 
	FROM tw_army_units

	UNION 

	select distinct( name_nk ) 
	FROM tw_characters
)

select 
	commander_nk as name_nk
	, ROW_NUMBER() OVER( ORDER BY commander_nk ASC ) as id
FROM cte 