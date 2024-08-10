SELECT [Department Name], [Category Name], [Percentage]
FROM (SELECT d.[Name] AS [Department Name], c.[Name] AS [Category Name],
      CAST(ROUND(COUNT(*) OVER(PARTITION BY c.Id) * 100.0 / COUNT(*) OVER(PARTITION BY d.Id), 0) AS INT)
      AS [Percentage]
      FROM Departments AS d
      JOIN Categories AS c ON c.DepartmentId = d.Id
      JOIN Reports AS r ON r.CategoryId = c.Id) AS fc
GROUP BY [Department Name], [Category Name], [Percentage]
--ORDER BY [Department Name], [Category Name], [Percentage]
