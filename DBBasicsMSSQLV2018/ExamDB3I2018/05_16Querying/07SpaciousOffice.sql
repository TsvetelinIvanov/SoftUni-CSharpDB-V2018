SELECT t.[Name] AS TownName, o.[Name] AS OfficeName, o.ParkingPlaces
FROM Towns AS t
JOIN Offices AS o ON o.TownId = t.Id
WHERE o.ParkingPlaces > 25
ORDER BY TownName, o.Id