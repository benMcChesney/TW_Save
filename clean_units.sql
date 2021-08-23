--select distinct(turn_num)
--FROM export_army_unit_TK
--group border by turn_num ASC


with max_cte AS 
(
	select  turn_num, max(modifiedOn) as maxModified
	FROM export_army_unit_TK
	group by turn_num
	
), 
clean_cte AS
(
	select  
		  [unit_name]
		  ,[strength]
		  ,[max_strength]
		  ,[army]
		  ,[faction]
		  ,[garrison_a]
		  ,[garrison_b]
		  ,[campaign_localized]
		  ,[session_id]
		  ,a.[turn_num] as turn_num
		  ,[modifiedOn]
	FROM max_cte as a
	inner join export_army_unit_TK as b
	ON a.turn_num = b.turn_num
		AND a.maxModified = b.modifiedOn

)

select * 
FROM clean_cte
order by turn_num ASC