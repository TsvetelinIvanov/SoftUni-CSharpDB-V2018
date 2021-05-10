SELECT mc.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Mountains AS m
JOIN MountainsCountries AS mc ON (mc.MountainId = m.Id AND mc.CountryCode = 'BG')
JOIN Peaks AS p ON (p.MountainId = m.Id AND p.Elevation > 2835)
ORDER BY p.Elevation DESC
