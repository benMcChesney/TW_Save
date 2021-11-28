{{ config(alias='Dim_Geography', materialized='table' ) }}

with union_cte AS
(
	select 
		province_name as province_nk
		, region_name as region_nk
		, provinceReplaced as provinceName
		, CASE 
		   WHEN settlementReplaced = '' 
			THEN TRIM( REPLACE( 
					REPLACE( 
						REPLACE( region_name , 'wh_main' , '' )  
						, 'wh2_main' , '' ) 
					, '_' , ' ' )
					)
			ELSE TRIM( settlementReplaced ) 
		     END AS settlementName
		
	FROM (
    select 
        province_name 
        , settlement_key as region_name
		, REPLACE( 
			REPLACE( 
				REPLACE ( 
					REPLACE(  settlement_key , province_name , '' ) 
				, 'wh_main' , '' ) 
			, 'wh2_main' , '' ) 
		, '_' , ' ' ) as settlementReplaced
		, REPLACE( 
			REPLACE( 
				REPLACE ( 
					province_name , 'wh_main' , '' ) 
			, 'wh2_main' , '' ) 
		, '_' , ' ' ) as provinceReplaced
    FROM tw_province
	) as a 
	--where settlementName = ''
	
)
select  
    ROW_NUMBER() OVER( ORDER BY province_nk ASC , region_nk DESC ) as id
    , * 
FROM union_cte
