WITH cte_Rancs(ReturnOfficeId, OfficeId, Id, Manufacturer, Model)
AS
(SELECT ovm.ReturnOfficeId, ovm.OfficeId, ovm.Id, ovm.Manufacturer, ovm.Model
FROM 
	(SELECT DENSE_RANK() OVER(PARTITION BY v.Id ORDER BY o.CollectionDate DESC) AS LatestRentCarsRank,
		  o.ReturnOfficeId, v.OfficeId, v.Id, m.Manufacturer, m.Model
	FROM Orders AS o
	RIGHT JOIN Vehicles AS v ON v.Id = o.VehicleId
	JOIN Models AS m ON m.Id = v.ModelId
	) AS ovm
WHERE LatestRentCarsRank = 1)

SELECT CONCAT(Manufacturer, ' - ', Model) AS Vehicle,
      [Location] = 
	              CASE
				   WHEN(SELECT COUNT(*) FROM Orders AS o WHERE o.VehicleId = cte_Rancs.Id) = 0 THEN 'home'
				   WHEN(cte_Rancs.ReturnOfficeId IS NULL) THEN 'on a rent'
				   WHEN(cte_Rancs.OfficeId <> cte_Rancs.ReturnOfficeId) 
				   THEN (SELECT CONCAT(t.[Name], ' - ', o.[Name]) 
				   FROM Towns AS t JOIN Offices AS o ON o.TownId = t.Id
				   WHERE o.Id = cte_Rancs.ReturnOfficeId)
				  END
FROM cte_Rancs
ORDER BY Vehicle, cte_Rancs.Id