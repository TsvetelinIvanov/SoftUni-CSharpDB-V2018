SELECT DISTINCT c.[Name] AS [Category Name]
FROM Categories AS c
JOIN Reports AS r ON r.CategoryId = c.Id
JOIN Users AS u ON (u.Id = r.UserId AND DAY(u.BirthDate) = DAY(r.OpenDate) 
AND MONTH(u.BirthDate) = MONTH(r.OpenDate))
ORDER BY c.[Name]

--Only one query must be paste in Judge

SELECT DISTINCT c.[Name] AS [Category Name]
FROM Categories AS c
JOIN Reports AS r ON r.CategoryId = c.Id
JOIN Users AS u ON u.Id = r.UserId 
WHERE DAY(u.BirthDate) = DAY(r.OpenDate) AND MONTH(u.BirthDate) = MONTH(r.OpenDate)
ORDER BY c.[Name]