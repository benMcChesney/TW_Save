DROP TABLE IF EXISTS tw_province ;

CREATE TABLE tw_province ( 
    province_name TEXT , 
    settlement_key TEXT 
);

TRUNCATE TABLE tw_province;
copy tw_province FROM 'C:\lab\tw_save\test\export_province.csv' DELIMITER ',' CSV HEADER ;


select count(*)
FROM tw_province;



DROP TABLE IF EXISTS tw_regions ;

CREATE TABLE tw_regions ( 
    settlement_name TEXT , 
    settlement_type TEXT , 
    settlement_owner TEXT , 
    settlement_government TEXT , 
    session TEXT , 
    turn_num TEXT , 
    modifiedOn TEXT 
);


TRUNCATE TABLE tw_regions;
copy tw_regions FROM 'C:\lab\tw_save\test\export_region.csv' DELIMITER ',' CSV HEADER ; 

select count(*)
from tw_regions;


DROP TABLE IF EXISTS tw_army_units; 

CREATE TABLE tw_army_units ( 
    unit_name TEXT NOT NULL 
    , strength int NOT NULL 
    , max_strength int NOT NULL 
    , army TEXT NOT NULL 
    , faction TEXT NOT NULL 
    , garrison_a int NULL 
    , garrison_b int NULL 
    , campaign_localized TEXT NULL 
    , session int NOT NULL 
    , turn_num int NOT NULL 
    , modifiedOn int NOT NULL 
);
TRUNCATE TABLE tw_army_units;
copy tw_army_units FROM 'C:\lab\tw_save\test\export_army_unit.csv' DELIMITER ',' CSV HEADER ;

select count(*)
FROM tw_army_units;

DROP TABLE IF EXISTS tw_economy ; 

CREATE TABLE tw_economy ( 
    bank_balance int NOT NULL 
    , taxes int NOT NULL 
    , army_upkeep int NOT NULL 
    , faction_name TEXT NULL 
    , session int NOT NULL 
    , turn_num int NOT NULL 
    , modifiedOn int NOT NULL 
);
TRUNCATE TABLE tw_economy;
copy tw_economy FROM 'C:\lab\tw_save\test\export_faction_economy.csv' DELIMITER ',' CSV HEADER ;

select count(*)
FROM tw_economy


