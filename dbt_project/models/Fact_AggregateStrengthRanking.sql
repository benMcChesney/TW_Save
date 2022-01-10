{{ config(alias='Fact_AggregateStrengthRanking', materialized='table' ) }}

with sum_per_turn_cte AS 
(
	select faction_id , turn_id , SUM( strength ) as sum_strength  
	FROM Fact_UnitHistory

	where army_status = 'Standing Army'
	GROUP BY faction_id , turn_id
) 

select * 
, ROW_NUMBER() OVER( PARTITION BY turn_id ORDER BY sum_strength DESC ) as strength_rank
FROM sum_per_turn_cte

