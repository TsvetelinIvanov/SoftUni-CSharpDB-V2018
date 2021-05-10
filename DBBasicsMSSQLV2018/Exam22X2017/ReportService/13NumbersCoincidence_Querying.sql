SELECT DISTINCT u.Username
FROM Users AS u
JOIN Reports AS r ON r.UserId = u.Id
WHERE (LEFT(u.Username, 1) LIKE '[0-9]' AND TRY_CAST(LEFT(u.Username, 1) AS INT) = r.CategoryId) OR 
      (RIGHT(u.Username, 1) LIKE '[0-9]' AND TRY_CAST(RIGHT(u.Username, 1) AS INT) = r.CategoryId)
ORDER BY u.Username