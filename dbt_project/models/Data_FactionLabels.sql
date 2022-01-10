{{ config(alias='Data_FactionLabels', materialized='table' ) }}



select 
'brt' as faction, 'Bretonnia' as factionLabel
UNION ALL select 
'bst' , 'Beastman'
UNION ALL select
'chs', 'Warriors of Chaos'
UNION ALL select
'cst', 'Vampire Coast'
UNION ALL select
'def' , 'Dark Elves'
UNION ALL select
'dwf' , 'Dwarves'
UNION ALL select
'emp' , 'Empire'
UNION ALL select
'grn' , 'Greenskins'
UNION ALL select
'hef' , 'High Elves'
UNION ALL select
'ksl' , 'Kislev'
UNION ALL select
'lzd' , 'Lizardmen'
UNION ALL select
'nor' , 'Norsca'
UNION ALL select
'rogue' , 'Rogue Armies'
UNION ALL select
'skv' , 'Skaven'
UNION ALL select
-- tilea ? 
'teb' , 'The Southern Realms'
UNION ALL select
'tmb' , 'Tomb Kings'
UNION ALL select
'vmp' , 'Vampires Counts'
UNION ALL select
'wef' , 'Wood Elves' 

