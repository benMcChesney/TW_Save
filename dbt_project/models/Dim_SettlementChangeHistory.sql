{{ config(alias='Dim_SettlementChangeHistory', materialized='table' ) }}

-- first convert turnNum to int for proper sorting
with src_cte as 
(
    select 
        *
        ,  CONVERT( int , turn_num ) as turnNumInt
    FROM tw_regions
	-- use filter while testing 
	--WHERE settlement_name = 'wh2_main_vampire_coast_pox_marsh'
)
-- while in row by row, get previous neighbor
, with_previous AS 
(
	select 
		*
		, LAG( Settlement_owner,1 ) OVER ( PARTITION BY Settlement_name ORDER BY turnNumInt ASC ) as previousOwner
	FROM src_cte
)
-- filter down to only changes, but also include NULLs
, only_change AS
(
select * 
FROM with_previous
where settlement_owner != previousOwner
	OR ( settlement_owner IS NULL and previousOwner IS NOT NULL 
		OR settlement_owner IS NOT NULL and previousOwner IS NULL )
)
-- now the next neighbor has the information we need 
, change_range AS 
(
	select 
		*
		, LEAD( turn_num ,1 ) OVER ( PARTITION BY Settlement_name ORDER BY turnNumInt ASC ) as nextOwnerTurnNum
	FROM only_change
), max_turn AS 
(
	select MAX( CONVERT( int , turn_num ) ) as maxTurnNUm 
	FROM Dim_Calendar
)
-- replace final validTo with end of calendar 
select 
	settlement_name
	, settlement_type
	, settlement_owner
	, turnNumInt as validFrom
	, ISNULL( nextOwnerTurnNum, max_turn.maxTurnNUm ) as validTo
FROM change_range
LEFT OUTER JOIN max_turn
ON 1=1 
--ORDER BY settlement_name, validFrom
