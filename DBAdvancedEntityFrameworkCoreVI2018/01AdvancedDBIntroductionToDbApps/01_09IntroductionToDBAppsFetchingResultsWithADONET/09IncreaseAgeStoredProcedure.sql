CREATE PROCEDURE usp_GetOlder @minionId INT
AS
BEGIN
 UPDATE Minions
 SET Age += 1
 WHERE Id = @minionId
END

EXECUTE usp_GetOlder 7

SELECT [Name], Age FROM Minions
WHERE Id = 7

DROP PROCEDURE usp_GetOlder
