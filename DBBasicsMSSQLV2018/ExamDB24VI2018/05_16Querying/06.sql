SELECT 
CASE WHEN MiddleName IS NOT NULL THEN CONCAT(FirstName, ' ', MiddleName, ' ', LastName) 
ELSE CONCAT(FirstName, ' ', LastName)
END AS [Full Name],
YEAR(BirthDate) AS BirthYear
FROM Accounts
WHERE YEAR(BirthDate) > '1991'
ORDER BY BirthYear DESC, [Full Name]