SELECT m.Model, m.Seats, v.Mileage
FROM Models AS m
JOIN Vehicles AS v ON v.ModelId = m.Id
WHERE v.Id NOT IN 
(SELECT o.VehicleId FROM Orders AS o WHERE o.ReturnDate IS NULL)
ORDER BY v.Mileage ASC, m.Seats DESC, m.Id ASC