CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan(@balance MONEY) AS
SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
FROM AccountHolders AS ah
JOIN Accounts AS a ON a.AccountHolderId = ah.Id
GROUP BY ah.FirstName, ah.LastName
HAVING SUM(a.Balance) > @balance

--In Judge must be paste without this below
GO

EXECUTE usp_GetHoldersWithBalanceHigherThan 1234.5667