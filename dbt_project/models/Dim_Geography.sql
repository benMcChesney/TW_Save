{{ config(alias='Dim_Geography') }}

select 
    province_name 
    , settlement_key as region_name
FROM public.tw_province