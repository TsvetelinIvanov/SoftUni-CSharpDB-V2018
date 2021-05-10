SELECT Id, [Name], Age FROM Minions

UPDATE Minions
SET [Name] = LOWER([Name])

UPDATE Minions
SET [Name] = 'bob kevin'
WHERE Id IN(17, 13, 12)

UPDATE Minions SET Age += 1 WHERE Id = 1

SELECT [Name] FROM Minions WHERE Id = 17

UPDATE Minions SET [Name] = 'Bob Kevin' WHERE Id = 13

SELECT [Name], Age FROM Minions