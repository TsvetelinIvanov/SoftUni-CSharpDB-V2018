SELECT c.CountryCode, COUNT(mc.MountainId) AS MountainRanges
FROM Countries AS c
JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE c.CountryCode IN('BG', 'RU', 'US')
GROUP BY c.CountryCode

--Only one query must be paste in Judge

SELECT mc.CountryCode, COUNT(m.MountainRange) AS MountainRanges
FROM Mountains AS m
JOIN MountainsCountries AS mc ON(mc.MountainId = m.Id AND mc.CountryCode IN('BG', 'RU', 'US'))
GROUP BY mc.CountryCode

--Only one query must be paste in Judge

SELECT CountryCode, COUNT(MountainId) AS MountainRanges
FROM MountainsCountries
WHERE CountryCode IN('BG', 'RU', 'US')
GROUP BY CountryCode