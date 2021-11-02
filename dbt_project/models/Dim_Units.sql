{{ config(alias='Dim_Units', materialized='table' ) }}

with distinct_units_cte AS
(
    select 
        distinct( unit_name ) as unit_nk
        FROM public.tw_army_units 
), 

split_CTE as 
(
	select 
    *
	 , SPLIT_PART( unit_nk , '_', 1) as game_source
     , SPLIT_PART( unit_nk , '_', 2) as dlc_source
     , SPLIT_PART( unit_nk , '_', 3) as faction
	 , SPLIT_PART( unit_nk , '_', 4) as unit_category
	 , SPLIT_PART( unit_nk , '_', 5) as units_us
  	FROM distinct_units_cte
)
	
select 
    unit_nk 
	, game_source
	, dlc_source
	, faction
	, unit_category
	, SUBSTRING( unit_nk, strpos( unit_nk , units_us ) ) as units 
    , ROW_NUMBER() OVER( ORDER BY unit_nk ASC ) as id 
FROM split_CTE

