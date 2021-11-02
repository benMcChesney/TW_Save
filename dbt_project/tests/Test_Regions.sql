select *
FROM {{ ref('Dim_Geography') }}
WHERE province_name IS NULL 
   --OR region_name IS NULL 