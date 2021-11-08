{{ config(alias='Dim_Geography', materialized='table' ) }}

with union_cte AS
(

    select 
        province_name 
        , settlement_key as region_name
    FROM tw_province

    -- add a ROW entry for each indiviudal province with no region selected  
    UNION ALL 

    select 
        distinct(province_name) as province_name
        , NULL as region_name  
    FROM tw_province
)
select  
    ROW_NUMBER() OVER( ORDER BY province_name ASC , region_name DESC ) as id
    , * 
FROM union_cte
