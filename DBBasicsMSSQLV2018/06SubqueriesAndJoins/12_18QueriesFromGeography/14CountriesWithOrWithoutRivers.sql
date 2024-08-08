SELECT TOP(5) c.CountryName, r.RiverName
FROM Countries AS c
LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode 
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName

--Only one query must be pasted in Judge

SELECT TOP(5) c.CountryName, r.RiverName
FROM Countries AS c
LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode 
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
WHERE c.ContinentCode = (SELECT ContinentCode FROM Continents WHERE ContinentName = 'Africa')
ORDER BY c.CountryName
