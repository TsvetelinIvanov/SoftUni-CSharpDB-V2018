SELECT Names, Class
FROM (
      SELECT c.FirstName + ' ' + c.LastName AS Names, m.Class,
      RANK() OVER (PARTITION BY (c.FirstName + ' ' + c.LastName) ORDER BY COUNT(m.Class) DESC) AS [Rank]
      FROM Orders AS o
      JOIN Clients AS c ON c.Id = o.ClientId
      JOIN Vehicles AS v ON v.Id = o.VehicleId
      JOIN Models AS m ON m.Id = v.ModelId
      GROUP BY c.FirstName + ' ' + c.LastName, m.Class
	  ) AS ocvm
WHERE [Rank] = 1
ORDER BY Names, Class