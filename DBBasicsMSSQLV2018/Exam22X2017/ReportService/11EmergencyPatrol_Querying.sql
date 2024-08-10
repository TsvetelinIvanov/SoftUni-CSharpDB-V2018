SELECT r.OpenDate, r.[Description], u.Email AS [Reporter Email]
FROM Reports AS r
JOIN Users AS u ON u.Id = r.UserId
JOIN Categories AS c ON c.Id = r.CategoryId
WHERE r.CloseDate IS NULL AND LEN(r.[Description]) > 20 AND r.[Description] LIKE '%str%' AND 
      c.DepartmentId IN (1, 4, 5)     
ORDER BY r.OpenDate, [Reporter Email], r.Id

--Only one query must be pasted in Judge

SELECT r.OpenDate, r.[Description], u.Email AS [Reporter Email]
FROM Reports AS r
JOIN Users AS u ON u.Id = r.UserId
JOIN Categories AS c ON c.Id = r.CategoryId
WHERE r.CloseDate IS NULL AND LEN(r.[Description]) > 20 AND r.[Description] LIKE '%str%' AND 
      c.DepartmentId IN (SELECT Id FROM Departments WHERE [Name] IN('Infrastructure', 'Emergency', 'Roads Maintenance'))     
ORDER BY r.OpenDate, [Reporter Email], r.Id

--Only one query must be pasted in Judge

SELECT r.OpenDate, r.[Description], u.Email AS [Reporter Email]
FROM Reports AS r
JOIN Users AS u ON u.Id = r.UserId
JOIN Categories AS c ON c.Id = r.CategoryId
JOIN Departments AS d ON d.Id = c.DepartmentId
WHERE r.CloseDate IS NULL AND LEN(r.[Description]) > 20 AND r.[Description] LIKE '%str%' AND       
     d.[Name] IN ('Infrastructure', 'Emergency', 'Roads Maintenance')
ORDER BY r.OpenDate, [Reporter Email], r.Id

--Only one query must be pasted in Judge

SELECT r.OpenDate, r.[Description], u.Email AS [Reporter Email]
FROM Reports AS r
JOIN Users AS u ON u.Id = r.UserId
JOIN Categories AS c ON c.Id = r.CategoryId 
JOIN Departments AS d ON (d.Id = c.DepartmentId AND d.[Name] IN ('Infrastructure', 'Emergency', 'Roads Maintenance'))
WHERE r.CloseDate IS NULL AND LEN(r.[Description]) > 20 AND r.[Description] LIKE '%str%' 
ORDER BY r.OpenDate, [Reporter Email], r.Id
