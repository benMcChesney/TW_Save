select f.* , d.id as factionId , d.factionLabel , d.* 
FROM Dim_SettlementChangeHistory as f 

LEFT OUTER JOIN Dim_Factions as d 
ON f.settlement_owner = d.faction_nk

--WHERE settlement_owner = 'wh2_main_skv_clan_skyre'
--WHERE game_source IS NULL 

ORDER BY settlement_name, validFrom