/*
\# Examples for Fact\_Region Control



the granularity ( specificity of each row ) of this table is one settlement per change in ownership.



Included in this is the default
*/

select 
    '---' as fact 
    , fact.* 
    , '---' as dim_geo 
    , dim_geo.* 
    , '---' as dim_f 
    , dim_f.* 
FROM public."Fact_RegionControl" as fact 

JOIN public."Dim_Geography" as dim_geo
ON dim_geo.id = fact.geographyId 

JOIN public."Dim_Factions" as dim_f
ON dim_f.id = fact.factionId