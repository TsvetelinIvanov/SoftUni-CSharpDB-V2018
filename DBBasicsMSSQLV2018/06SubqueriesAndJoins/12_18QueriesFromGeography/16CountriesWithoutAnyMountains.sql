SELECT COUNT(*) AS CountryCode
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId IS NULL

--Only one query must be pasted in Judge

SELECT COUNT(*) 
FROM Countries 
WHERE CountryCode NOT IN (SELECT CountryCode FROM MountainsCountries)
