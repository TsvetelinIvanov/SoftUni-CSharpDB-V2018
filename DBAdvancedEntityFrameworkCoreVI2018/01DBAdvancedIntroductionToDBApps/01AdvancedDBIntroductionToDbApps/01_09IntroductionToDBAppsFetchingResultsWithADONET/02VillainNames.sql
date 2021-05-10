SELECT v.[Name], COUNT(m.Id) AS [Number of Minions]
FROM Minions AS m
JOIN MinionsVillains AS mv ON mv.MinionId = m.Id
JOIN Villains AS v ON v.Id = mv.VillainId
GROUP BY v.[Name]
HAVING COUNT(m.Id) > 3
ORDER BY [Number of Minions] DESC

--Only one query must be executed

SELECT v.[Name] + ' - ' + CONVERT(VARCHAR(10), COUNT(m.Id)) AS [Number of Minions]
FROM Minions AS m
JOIN MinionsVillains AS mv ON mv.MinionId = m.Id
JOIN Villains AS v ON v.Id = mv.VillainId
GROUP BY v.[Name]
HAVING COUNT(m.Id) > 3
ORDER BY COUNT(m.Id) DESC