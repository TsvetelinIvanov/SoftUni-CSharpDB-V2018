SELECT [Name] 
FROM Towns
WHERE [Name] LIKE '_____' OR [Name] LIKE '______'
ORDER BY [Name]

--In Judge must be paste one query

SELECT [Name] 
FROM Towns
WHERE LEN([Name]) = 5 OR LEN([Name]) = 6
ORDER BY [Name]