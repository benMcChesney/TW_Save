{{ config(alias='Dim_Factions') }}

-- Dim_Factions
with dFactions_cte AS
(
    select 
        distinct( faction_name ) as faction_name
        FROM tw_economy
)
, faction_split_cte AS 
(
     select 
        * 
        , SPLIT_PART( faction_name , '_', 1) as game_source
        , SPLIT_PART( faction_name , '_', 2) as dlc_source
        , SPLIT_PART( faction_name , '_', 3) as faction
        -- unfortunately some names like 'high_elves' for names breaks the ease of using split_part so we work with this after
        , SPLIT_PART( faction_name , '_', 4) as lord_us
    FROM dFactions_cte
)

select 
    -- *
    ROW_NUMBER() OVER( ORDER BY faction_name ) as id 
    , faction_name as faction_nk  
    , game_source
    , dlc_source
    , faction
    , SUBSTRING( faction_name , strpos( faction_name , lord_us ) ) as lord 
FROM faction_split_cte