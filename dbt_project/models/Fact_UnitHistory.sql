{{ config(alias='Fact_UnitHistory', materialized='table' ) }}

        
with max_cte AS 
(
    select  turn_num, max(modifiedOn) as maxModified
    FROM tw_army_units
    group by turn_num

), 
clean_cte AS
(
    select  
            unit_name
            ,strength
            ,max_strength
            ,army
            ,faction
            ,case 
			when localized_status LIKE '%garrison%'
			then 'Garrison Army'
			else 
				'Standing Army'
			end as army_status
            ,a.turn_num as turn_num
            ,modifiedOn
    FROM max_cte as a
    inner join tw_army_units as b
    ON a.turn_num = b.turn_num
        AND a.maxModified = b.modifiedOn

)
select 
    strength
    , max_strength
    , dim_u.id as unit_id
    , dim_a.id as army_id 
    , dim_f.id as faction_id 
    , units.army_status
    , units.turn_num as turn_id
from clean_cte as units
INNER JOIN {{ ref('Dim_Factions') }}  as dim_f
--INNER JOIN Dim_Factions  as dim_f
ON units.faction = dim_f.faction_nk

INNER JOIN {{ ref('Dim_Units')}} as dim_u 
--INNER JOIN Dim_Units as dim_u 
ON units.unit_name = dim_u.unit_nk

INNER JOIN  {{ ref('Dim_Armies') }}  as dim_a
--INNER JOIN  Dim_Armies  as dim_a
ON units.army = dim_a.army
AND (units.turn_num >= dim_a.validFrom 
AND units.turn_num < dim_a.validTo)
