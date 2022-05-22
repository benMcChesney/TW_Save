{{ config(alias='Fact_RegionControl', materialized='table' ) }}

with regions_cte AS
(
    select 
        CAST ( turn_num as int ) as turn_num
        , settlement_name
        , settlement_owner 
        FROM tw_regions 
)
, pre_pivot AS
(

	select 
		--settlement_name as region_name
		--, settlement_owner as region_owner
		dim_geo.id as geographyId 
		, dim_f.id as factionId
		, a.turn_num as turn_num
		, dim_cal.turn_num as turnId
		-- ADD GEOs reference
	FROM regions_cte as a 
	--INNER JOIN {{ ref('Dim_Geography') }} as b 
	--INNER JOIN Dim_Geography as dim_geo 
	INNER JOIN {{ ref('Dim_Geography') }} as dim_geo 
	ON a.settlement_name = dim_geo.region_nk

	INNER JOIN{{ ref('Dim_Factions') }} as dim_f 
	--INNER JOIN Dim_Factions as dim_f 
	ON a.settlement_owner = dim_f.faction_nk 

	--INNER JOIN {{ ref( 'Dim_Calendar')}} as dim_cal
	INNER JOIN Dim_Calendar as dim_cal
	ON a.turn_num = dim_cal.turn_num
	WHERE 1=1 
) 

select * 
FROM pre_pivot
--ORDER BY geographyId, turn_num
/*

-- Pivot table with one row and five columns  
SELECT 'AverageCost' AS Cost_Sorted_By_Production_Days,   
  [0], [1], [2], [3], [4]  
FROM  
(
  SELECT DaysToManufacture, StandardCost   
  FROM Production.Product
) AS SourceTable  
PIVOT  
(  
  AVG(StandardCost)  
  FOR DaysToManufacture IN ([0], [1], [2], [3], [4])  
) AS PivotTable;  
  
  */
--AND settlement_owner = 'wh2_main_skv_clan_skyre'
-- Capital Skavenblight! Within Skaven control the entire campaign
--AND settlement_name = 'wh2_main_skavenblight_skavenblight'
--AND settlement_name = 'wh_main_tilea_luccini'

--order by settlement_name, a.turn_num 

