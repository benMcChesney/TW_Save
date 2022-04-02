{{ config(alias='Dim_Units', materialized='table' ) }}

with distinct_units_cte AS
(
    select 
        distinct( unit_name ) as un
        FROM tw_army_units 
		where unit_name IS NOT NULL
), 
faction_split_cte AS 
(
     select 
        * 
		, CHARINDEX( '_' , un , 0 ) as us_0
		, CHARINDEX( '_' , un , CHARINDEX( '_' , un )+1 ) as us_1
		, CHARINDEX( '_' , un , CHARINDEX( '_' , un , CHARINDEX( '_' , un )+1 )+1) as us_2
        , CHARINDEX( '_' , un , CHARINDEX( '_' , un , CHARINDEX( '_' , un , CHARINDEX( '_' , un )+1 )+1)+1) as us_3
		, CHARINDEX( '_' , REVERSE( un )) as us_5 
		, LEN( un ) as [length]
    FROM distinct_units_cte
)

, ready_cte AS
(
	select  
		ROW_NUMBER() OVER( ORDER BY un ASC ) as id 
		, SUBSTRING( un, 0 , us_0 ) as game_source
		, SUBSTRING( un, us_0+1 , us_1 - us_0 - 1) as dlc_source
		, SUBSTRING( un, us_1+1 , us_2 - us_1 - 1) as faction
		, SUBSTRING( un, us_2+1 , us_3 - us_2 - 1) as unit_group
		, SUBSTRING( un, us_3+1 , LEN(un) - us_3 ) as unit_name
		, un as unit_nk 
	FROM faction_split_cte
)

select cte.* , d_ugl.unitLabel 
FROM ready_cte as cte 
LEFT OUTER JOIN {{ ref('Data_UnitGroupLabels') }}  as d_ugl
ON cte.unit_group = d_ugl.unit_group