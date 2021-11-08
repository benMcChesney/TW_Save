{{ config(alias='Dim_Factions', materialized='table' ) }}

with dFactions_cte AS
(
    select 
        distinct( CAST( faction_name as varchar(100)) ) as fn
		, cast( faction_name as varchar(100)) as faction_nk
        FROM tw_economy
)
, faction_split_cte AS 
(
     select 
        * 
		, CHARINDEX( '_' , fn , 0 ) as us_0
		, CHARINDEX( '_' , fn , CHARINDEX( '_' , fn )+1 ) as us_1
		, CHARINDEX( '_' , fn , CHARINDEX( '_' , fn , CHARINDEX( '_' , fn )+1 )+1) as us_2
        , CHARINDEX( '_' , fn , CHARINDEX( '_' , fn , CHARINDEX( '_' , fn , CHARINDEX( '_' , fn )+1 )+1)+1) as us_3
    FROM dFactions_cte
)

select 
	ROW_NUMBER() OVER( ORDER BY fn ) as id 
	, SUBSTRING( fn, 0 , us_0 ) as game_source
	, SUBSTRING( fn, us_0+1 , us_1 - us_0 - 1) as dlc_source
	, SUBSTRING( fn, us_1+1 , us_2 - us_1 - 1) as faction
	, SUBSTRING( fn, us_2+1 , LEN(fn) - us_2 ) as lord
	, faction_nk 
FROM faction_split_cte
