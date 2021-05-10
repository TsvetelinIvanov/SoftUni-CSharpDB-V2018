SELECT TOP(30) CountryName, [Population]
FROM Countries
WHERE ContinentCode = 'EU'
ORDER BY [Population] DESC, CountryName

--In Judge must be paste only one query

SELECT TOP(30) CountryName, [Population]
FROM Countries
WHERE ContinentCode IN 
(SELECT ContinentCode FROM Continents WHERE ContinentName = 'Europe')
ORDER BY [Population] DESC, CountryName