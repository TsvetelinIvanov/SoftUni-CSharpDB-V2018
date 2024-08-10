SELECT DISTINCT e.FirstName + ' ' + e.LastName AS [Name], COUNT(r.UserId) AS [Users Number]
FROM Employees AS e
LEFT JOIN Reports AS r ON r.EmployeeId = e.Id
GROUP BY e.FirstName + ' ' + e.LastName
ORDER BY [Users Number] DESC, [Name]

--Only one query must be pasted in Judge

SELECT e.FirstName + ' ' + e.LastName AS [Name], COUNT(DISTINCT r.UserId) AS [Users Number]
FROM Employees AS e
LEFT JOIN Reports AS r ON r.EmployeeId = e.Id
GROUP BY e.FirstName + ' ' + e.LastName
ORDER BY [Users Number] DESC, [Name]
