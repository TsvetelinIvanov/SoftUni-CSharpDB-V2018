SELECT t.[Name] AS TownName,
      (SUM(oc.M) * 100) / (ISNULL(SUM(oc.M), 0) + ISNULL(SUM(oc.F), 0)) AS MailPercent,
      (SUM(oc.F) * 100) / (ISNULL(SUM(oc.M), 0) + ISNULL(SUM(oc.F), 0)) AS FemalePercent
FROM
	(SELECT o.TownId,
	       CASE WHEN (Gender = 'M') THEN COUNT(o.Id) ELSE NULL END AS M,
		   CASE WHEN (Gender = 'F') THEN COUNT(o.Id) ELSE NULL END AS F
	FROM Orders AS o
	JOIN Clients AS c ON c.Id = o.ClientId
	GROUP BY c.Gender, o.TownId
	) oc
JOIN Towns AS t ON t.Id = oc.TownId
GROUP BY t.[Name]