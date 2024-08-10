--Creating trigger
CREATE TRIGGER tr_UserGameItems_LevelRestriction ON UserGameItems
INSTEAD OF INSERT
AS
INSERT INTO UserGameItems
SELECT ItemId, UserGameId FROM inserted
WHERE ItemId IN 
(SELECT Id FROM Items WHERE MinLevel <= (SELECT [Level] FROM UsersGames WHERE Id = UserGameId))

--Warning for adding bonus!!!
--The bonus must be added only one time (50000 or more) - using one from updating queries or 
--creating and executing procedure usp_GiveBonusInCash(executing five times with 50000 or with @bonusCash)

--Adding bonus cash as updating table (it must be execute only this query below or usp_GiveBonusInCash)
UPDATE ug SET ug.Cash += 50000 
FROM UsersGames AS ug
JOIN Users AS u ON u.Id = ug.UserId
JOIN Games AS g ON g.Id = ug.GameId
WHERE g.[Name] = 'Bali' AND 
u.Username IN('baleremuda', 'loosenoise','inguinalself','buildingdeltoid','monoxidecos')

GO

CREATE PROCEDURE usp_GiveBonusInCash (@GameName NVARCHAR(50), @Username NVARCHAR(50), 
@CashAmount MONEY) AS
BEGIN
 UPDATE ug SET ug.Cash += @CashAmount
 FROM UsersGames AS ug
 JOIN Users AS u ON u.Id = ug.UserId
 JOIN Games AS g ON g.Id = ug.GameId
 WHERE g.[Name] = @GameName AND 
 u.Username = @Username
 --u.Username IN('baleremuda', 'loosenoise','inguinalself','buildingdeltoid','monoxidecos')
END

EXECUTE usp_GiveBonusInCash 'Bali', 'baleremuda', 50000
EXECUTE usp_GiveBonusInCash 'Bali', 'loosenoise', 50000
EXECUTE usp_GiveBonusInCash 'Bali', 'inguinalself', 50000
EXECUTE usp_GiveBonusInCash 'Bali', 'buildingdeltoid', 50000
EXECUTE usp_GiveBonusInCash 'Bali', 'monoxidecos', 50000

--Another adding bonus cash as updating table, because
--according to exercise conditions (problem 19 - item 2) bonus is 50000, 
--but in some solutions exist additions to this bonus
--(it must be execute only this query below or usp_GiveBonusInCash)
UPDATE UsersGames 
SET Cash += 50000 + (SELECT SUM(i.Price) FROM Items AS i JOIN UserGameItems AS ugi ON ugi.ItemId = i.Id
        WHERE ugi.UserGameId = UsersGames.Id)
WHERE UserId IN (SELECT Id FROM Users WHERE Username IN('baleremuda', 'loosenoise','inguinalself','buildingdeltoid','monoxidecos'))
      AND GameId = (SELECT Id FROM Games WHERE [Name] = 'Bali')

DECLARE @bonusCash MONEY = 50000 + (SELECT SUM(i.Price) FROM Items AS i JOIN UserGameItems AS ugi ON ugi.ItemId = i.Id JOIN UsersGames AS ug ON ug.Id = ugi.UserGameId)									 

EXECUTE usp_GiveBonusInCash 'Bali', 'baleremuda', @bonusCash
EXECUTE usp_GiveBonusInCash 'Bali', 'loosenoise', @bonusCash
EXECUTE usp_GiveBonusInCash 'Bali', 'inguinalself', @bonusCash
EXECUTE usp_GiveBonusInCash 'Bali', 'buildingdeltoid', @bonusCash
EXECUTE usp_GiveBonusInCash 'Bali', 'monoxidecos', @bonusCash

--Buying the items
GO

CREATE PROCEDURE usp_BuyingItem(@GameName NVARCHAR(50), @Username NVARCHAR(50), @ItemId INT) AS
BEGIN
 INSERT INTO UserGameItems
 VALUES
 (@ItemId, (SELECT ug.Id FROM UsersGames AS ug
 JOIN Users AS u ON u.Id = ug.UserId
 JOIN Games AS g ON g.Id = ug.GameId
 WHERE g.[Name] = @GameName AND u.Username = @Username))
 
 UPDATE ug SET ug.Cash -= (SELECT Price FROM Items AS i WHERE i.Id = @ItemId)
 FROM UsersGames AS ug
 JOIN Users AS u ON u.Id = ug.UserId
 JOIN Games AS g ON g.Id = ug.GameId
 WHERE g.[Name] = @GameName AND u.Username = @Username
END

GO

CREATE PROCEDURE usp_BuyingItems(@GameName NVARCHAR(50), @Username NVARCHAR(50), 
@StartItemId INT, @EndItemId INT) AS
BEGIN
 DECLARE @currentItemId INT = @StartItemId
 WHILE(@currentItemId <= @EndItemId)
 BEGIN
  EXECUTE usp_BuyingItem @GameName, @Username, @currentItemId
  SET @currentItemId += 1
 END
END

EXECUTE usp_BuyingItems 'Bali', 'baleremuda', 251, 299
EXECUTE usp_BuyingItems 'Bali', 'loosenoise', 251, 299
EXECUTE usp_BuyingItems 'Bali', 'inguinalself', 251, 299
EXECUTE usp_BuyingItems 'Bali', 'buildingdeltoid', 251, 299
EXECUTE usp_BuyingItems 'Bali', 'monoxidecos', 251, 299

EXECUTE usp_BuyingItems 'Bali', 'baleremuda', 501, 539
EXECUTE usp_BuyingItems 'Bali', 'loosenoise', 501, 539
EXECUTE usp_BuyingItems 'Bali', 'inguinalself', 501, 539
EXECUTE usp_BuyingItems 'Bali', 'buildingdeltoid', 501, 539
EXECUTE usp_BuyingItems 'Bali', 'monoxidecos', 501, 539

--Selecting users and thier items 
SELECT u.Username, g.[Name], ug.Cash, i.[Name] AS [Item Name]
FROM Users AS u
JOIN UsersGames AS ug ON ug.UserId = u.Id
JOIN Games AS g ON g.Id = ug.GameId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
WHERE g.[Name] = 'Bali'
ORDER BY u.Username, i.[Name]

--This below is another solution

--The Database Diablo must be dropt and create for receiving the correct result from exercises
GO

CREATE TRIGGER tr_UserGameItems_LevelRestriction ON UserGameItems
INSTEAD OF INSERT
AS
INSERT INTO UserGameItems
SELECT ItemId, UserGameId FROM inserted
WHERE ItemId IN (SELECT Id FROM Items WHERE MinLevel <= (SELECT [Level] FROM UsersGames WHERE Id = UserGameId))

UPDATE UsersGames 
SET Cash += 50000 + (SELECT SUM(i.Price) FROM Items AS i JOIN UserGameItems AS ugi ON ugi.ItemId = i.Id WHERE ugi.UserGameId = UsersGames.Id)
WHERE UserId IN (SELECT Id FROM Users WHERE Username IN ('baleremuda', 'loosenoise','inguinalself','buildingdeltoid','monoxidecos'))
      AND GameId = (SELECT Id FROM Games WHERE [Name] = 'Bali')

INSERT INTO UserGameItems(UserGameId, ItemId)
SELECT UsersGames.Id, i.Id FROM UsersGames, Items AS i
WHERE UserId IN (SELECT Id FROM Users WHERE Username IN('baleremuda', 'loosenoise','inguinalself','buildingdeltoid','monoxidecos'))
AND GameId = (SELECT Id FROM Games WHERE [Name] = 'Bali') 
AND ((i.Id > 250 AND i.Id < 300) OR (i.Id > 500 AND i.Id < 540))

UPDATE UsersGames
SET Cash -= (SELECT SUM(i.Price) FROM Items AS i JOIN UserGameItems AS ugi ON ugi.ItemId = i.Id WHERE ugi.UserGameId = UsersGames.Id)
WHERE UserId IN (SELECT Id FROM Users WHERE Username IN ('baleremuda', 'loosenoise','inguinalself','buildingdeltoid','monoxidecos'))
      AND GameId = (SELECT Id FROM Games WHERE [Name] = 'Bali')

SELECT u.Username, g.[Name], ug.Cash, i.[Name] AS [Item Name]
FROM Users AS u
JOIN UsersGames AS ug ON ug.UserId = u.Id
JOIN Games AS g ON g.Id = ug.GameId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
WHERE g.[Name] = 'Bali'
ORDER BY u.Username, i.[Name]
