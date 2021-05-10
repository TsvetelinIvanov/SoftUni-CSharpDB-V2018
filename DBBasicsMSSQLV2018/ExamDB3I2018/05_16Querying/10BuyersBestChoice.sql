SELECT Manufacturer, Model, SUM(CountOfOrdersById) AS TimesOrdered
FROM (
      SELECT m.Manufacturer, m.Model, COUNT(v.Id) AS CountOfOrdersById
      FROM Orders As o
      LEFT JOIN Vehicles AS v ON v.Id = o.VehicleId
      RIGHT JOIN Models AS m ON m.Id = v.ModelId
      GROUP BY m.Manufacturer, m.Model, v.Id      
      ) AS ovm
GROUP BY Manufacturer, Model
ORDER BY TimesOrdered DESC, Manufacturer DESC, Model ASC