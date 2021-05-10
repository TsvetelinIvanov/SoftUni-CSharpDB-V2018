WITH CTE_CountriesInfo(CountryName, PeakName, Elevation, Mountain) AS
(
SELECT c.CountryName, p.PeakName, MAX(p.Elevation), m.MountainRange
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
LEFT JOIN Peaks AS p ON p.MountainId = mc.MountainId
GROUP BY c.CountryName, p.PeakName, m.MountainRange
)

SELECT TOP(5) e.CountryName AS Country, 
ISNULL(cci.PeakName, '(no highest peak)') AS [Highest peak Name],
ISNULL(cci.Elevation, 0) AS [Highest Peak Elevation],
ISNULL(cci.Mountain, '(no mountain)')
FROM
(
SELECT CountryName, MAX(Elevation) AS MaxElevation
FROM CTE_CountriesInfo
GROUP BY CountryName
) AS e
LEFT JOIN CTE_CountriesInfo AS cci ON cci.CountryName = e.CountryName AND
cci.Elevation = e.MaxElevation
ORDER BY e.CountryName, cci.PeakName