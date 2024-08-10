SELECT d.[Name] AS [Department Name],
       ISNULL(CONVERT(VARCHAR, AVG(DATEDIFF(DAY, r.OpenDate, r.CloseDate))), 'no info') AS [Aerage Duration]
FROM Departments AS d
JOIN Categories AS c ON c.DepartmentId = d.Id
LEFT JOIN Reports AS r ON r.CategoryId = c.Id
GROUP BY d.[Name]
ORDER BY d.[Name]
