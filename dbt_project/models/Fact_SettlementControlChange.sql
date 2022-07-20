with cte as 
(
    select 
        *
        ,  CONVERT( int , turn_num ) as turnNumInt
    FROM tw_regions
--WHERE settlement_name = 'wh2_main_vampire_coast_pox_marsh'
) 

select 
    settlement_name
    , settlement_owner
    , MIN ( turnNumInt ) as minTurn
    , MAX( turnNumInt ) as maxTurn
FROM cte
GROUP BY settlement_name, settlement_owner
--ORDER BY settlement_name , minTurn ASC


--GROUP BY settlement_name

--order by owner_count DESC
