SELECT TOP(1) c.Id
FROM Towns AS t
JOIN Countries AS c ON c.Id = t.CountryCode
WHERE c.[Name] = 'Bulgaria'

UPDATE Towns
SET [Name] = UPPER([Name])
WHERE CountryCode = 1

SELECT [Name] FROM  Towns WHERE CountryCode = 1