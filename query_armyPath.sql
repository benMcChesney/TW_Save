select * , maxTurnNum - minTurnNum AS turnsDuration
FROM Fact_ArmyPath 
-- clan skryre
WHERE faction_id = 337 
-- order by the creation date of the army ( min turn ),maxTurn helps
ORDER BY minTurnNum ASC, turn_num , nameMappingId ASC 