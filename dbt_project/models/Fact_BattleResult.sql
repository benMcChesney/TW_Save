{{ config(alias='Fact_BattleResult', materialized='table' ) }}


with cast_cte AS
(
	SELECT 
		[leader]
       ,[kills]
       ,CONVERT( int , [start_num_units] ) as [start_num_units]
	   ,CONVERT( int , [end_num_units] ) as [end_num_units]
	   ,faction_nk
       ,[unit_name]
       ,[turn_num]
	FROM [tw_save].[dbo].[tw_battle_result]
)

SELECT 
	[leader]
	,[kills]
    ,[start_num_units]
    ,[end_num_units]
	,[start_num_units] - [end_num_units] as lost_num_units
    ,[unit_name]
    ,[turn_num]
	, dim_f.id as factionId
FROM cast_cte AS twbr
LEFT OUTER JOIN Dim_Factions as dim_f
ON twbr.faction_nk = dim_f.faction_nk