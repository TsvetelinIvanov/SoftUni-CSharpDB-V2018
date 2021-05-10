CREATE FUNCTION ufn_CashInUsersGames(@GameName NVARCHAR(50))
RETURNS TABLE AS
RETURN
(
SELECT SUM(ncr.Cash) AS SumCash
FROM
(
SELECT g.[Name], ug.Cash, ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS RowNumber
FROM Games AS g
JOIN UsersGames AS ug ON ug.GameId = g.Id
WHERE g.[Name] = @GameName
) AS ncr 
WHERE ncr.RowNumber % 2 != 0
)

--In Judge must be paste without this below
 GO

SELECT * FROM ufn_CashInUsersGames('Lily Stargazer')

SELECT * FROM ufn_CashInUsersGames('Love in a mist')

DROP FUNCTION ufn_CashInUsersGames