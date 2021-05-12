SELECT t.Id AS TripId,
SUM([at].Luggage) AS Luggage,
CONCAT('$', CASE WHEN SUM([at].Luggage) > 5 THEN SUM([at].Luggage) * 5 ELSE 0 END) AS Fee
FROM Trips AS t
JOIN AccountsTrips AS [at] ON [at].TripId = t.Id
GROUP BY t.Id
HAVING SUM([at].Luggage) > 0
ORDER BY Luggage DESC