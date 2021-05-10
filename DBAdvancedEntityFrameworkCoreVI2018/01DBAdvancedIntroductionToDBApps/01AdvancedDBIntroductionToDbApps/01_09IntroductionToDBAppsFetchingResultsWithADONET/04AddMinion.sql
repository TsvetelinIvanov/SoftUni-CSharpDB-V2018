SELECT Id
FROM Towns
WHERE [Name] = 'Sofia'

INSERT INTO Towns ([Name]) VALUES ('Montana')

SELECT Id FROM Villains WHERE[Name] = 'Jilly'

INSERT INTO Minions ([Name], Age, TownId) VALUES ('Torn', 47, 3)

SELECT Id FROM Minions WHERE [Name] = 'Torn'

INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (1, 6)