SELECT AccountId, Email, CountryCode, Trips
FROM (SELECT a.Id AS AccountId, a.Email, c.CountryCode,
      COUNT(t.Id) AS Trips,
      RANK() OVER (PARTITION BY c.CountryCode ORDER BY COUNT(t.Id) DESC, a.Id) AS CountryTripsRank
      FROM Accounts AS a
      JOIN AccountsTrips AS [at] ON [at].AccountId = a.Id
      JOIN Trips AS t ON t.Id = [at].TripId
      JOIN Rooms AS r ON r.Id = t.RoomId
      JOIN Hotels AS h ON h.Id = r.HotelId
      JOIN Cities AS c ON c.Id = h.CityId
      GROUP BY c.CountryCode, a.Email, a.Id
      ) AS TrippersPerCountry
WHERE CountryTripsRank = 1
ORDER BY Trips DESC, AccountId
