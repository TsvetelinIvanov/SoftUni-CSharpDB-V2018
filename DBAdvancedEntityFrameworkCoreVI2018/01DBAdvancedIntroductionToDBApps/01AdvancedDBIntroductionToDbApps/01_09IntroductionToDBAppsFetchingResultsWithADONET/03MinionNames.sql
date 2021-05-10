SELECT [Name] 
FROM Villains
WHERE Id = 1

SELECT [Name], Age
FROM Minions AS m
JOIN MinionsVillains AS mv ON mv.MinionId = m.Id
WHERE mv.VillainId = 7
ORDER BY [Name]

