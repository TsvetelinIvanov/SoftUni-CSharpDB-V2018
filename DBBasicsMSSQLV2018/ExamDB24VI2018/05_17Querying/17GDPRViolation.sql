SELECT t.Id, 
CONCAT(a.FirstName, ' ' +  a.MiddleName, ' ', a.LastName) AS [Full Name],
cA.[Name] AS [From],
cH.[Name] AS [To], 
CASE
    WHEN t.CancelDate IS NOT NULL THEN 'Canceled'
    ELSE CONCAT(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate), ' days')
END AS Duration
FROM Trips AS t
JOIN AccountsTrips AS [at] ON [at].TripId = t.Id
JOIN Accounts AS a ON a.Id = [at].AccountId
JOIN Cities AS cA ON cA.Id = a.CityId
JOIN Rooms AS r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
JOIN Cities AS cH ON cH.Id = h.CityId
ORDER BY [Full Name], t.Id
