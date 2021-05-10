SELECT t.[Name] AS TownName, COUNT(t.Id) AS OfficesNumber
FROM Towns AS t
JOIN Offices AS o ON o.TownId = t.Id
GROUP BY o.TownId, t.[Name]
ORDER BY COUNT(t.Id) DESC, t.[Name]
