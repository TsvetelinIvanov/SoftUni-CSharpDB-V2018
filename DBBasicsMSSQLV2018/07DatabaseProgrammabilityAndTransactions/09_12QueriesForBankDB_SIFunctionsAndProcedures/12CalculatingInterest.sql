CREATE PROCEDURE usp_CalculateFutureValueForAccount(@AccountId INT, @YearlyInterestRate FLOAT) AS
BEGIN
  SELECT a.Id AS [Account Id], ah.FirstName AS [First Name], ah.LastName AS [Last Name], 
  a.Balance AS [Current balance], 
  dbo.ufn_CalculateFutureValue(a.Balance, @YearlyInterestRate, 5) AS [Balance in 5 years] 
  FROM Accounts AS a
  JOIN AccountHolders AS ah ON ah.Id = a.AccountHolderId
  WHERE a.Id = @AccountId
END

--In Judge must be pasted without this below
GO

EXECUTE dbo.usp_CalculateFutureValueForAccount 1, 0.1
