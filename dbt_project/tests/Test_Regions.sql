select province_name, region_name
FROM public."Dim_Geography"
WHERE province_name IS NULL 
   OR region_name IS NULL 