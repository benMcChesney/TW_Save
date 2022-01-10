{{ config(alias='Dim_Armies', materialized='table' ) }}

with army_units_cte AS
(
    select 
        --CAST ( turn_num as int ) as turn_num
        --, CAST ( modifiedOn as int ) as modifiedOn 
		turn_num 
        , army
        , unit_name
        , faction 
		, d_nm.id as name_mapping_id
        FROM tw_army_units as u 
		INNER JOIN Dim_NameMapping as d_nm 
		ON u.commander_nk = d_nm.name_nk
)
, 
army_with_prev_cte AS
(
    select 
        * 
        , CASE 
            WHEN turn_num = 1  
                THEN NULL 
            ELSE 
                LAG ( faction, 1 ) over ( ORDER BY army, turn_num )
            END as previous_faction
        , ROW_NUMBER() OVER( PARTITION BY faction, army, turn_num ORDER BY unit_name ) as unit_row
    FROM army_units_cte
)
, army_changed_cte AS 
(
    select 
        turn_num 
        , army 
        , faction 
        , previous_faction 
		, name_mapping_id
    FROM army_with_prev_cte
    where 
        unit_row = 1 
        AND ( faction != previous_faction 
            OR previous_faction IS NULL )  
)
, max_turn AS 
(
    select MAX( turn_num ) as maxTurn
    FROM army_units_cte
)
,army_changed_range_cte AS 
(
    select 
    *
    , LEAD( turn_num , 1 ) over ( ORDER BY army, turn_num ) as nextTurn
    FROM army_changed_cte
    JOIN max_turn 
    ON 1=1 
), a as 
(
	select
        
        army 
		, name_mapping_id
        --, a.faction 
        , turn_num as validFrom
        , case 
            WHEN nextturn = 1 or nextturn IS NULL 
                THEN maxTurn 
            ELSE 
                nextTurn 
            END as validTo
        , dim_f.id as factionId  
		from army_changed_range_cte as a
		INNER JOIN {{ ref('Dim_Factions') }} as dim_f 
		--INNER JOIN Dim_Factions as dim_f
		ON a.faction = dim_f.faction_nk
        
		 
)

select 
    *
    , ROW_NUMBER() OVER( ORDER BY army, factionId, validFrom, validTo ASC ) as id 
FROM a
--where a.faction = 'wh2_main_skv_clan_skyre'
--LIMIT 100

