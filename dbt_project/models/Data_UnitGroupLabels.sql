{{ config(alias='Data_UnitGroupLabels', materialized='table' ) }}



select 
'art' as unit_group, 'Artillery' as unitLabel
UNION ALL 

select 'cav' ,'Cavalry'
UNION ALL 

select 'cha' , 'Character'
UNION ALL 

select 'feral' , 'Feral'
UNION ALL 

select 'forest' , 'Forest'
UNION ALL 

select 'inf' , 'Infantry'
UNION ALL 

select 'mon' , 'Monster'
UNION ALL 

--only remap, only one unique with 'peasant' type edge case
select 'peasant' , 'Infantry'
UNION ALL 

select 'veh' , 'Vehicle'

