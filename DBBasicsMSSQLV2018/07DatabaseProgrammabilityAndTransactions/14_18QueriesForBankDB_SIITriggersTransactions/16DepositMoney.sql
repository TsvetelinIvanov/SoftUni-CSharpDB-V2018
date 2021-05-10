CREATE PROCEDURE usp_DepositMoney(@AccountId INT, @MoneyAmount MONEY) AS
BEGIN
 IF (@MoneyAmount < 0)
 BEGIN
  RAISERROR('Cannot deposit negative value!', 16, 1)
 END
 ELSE
 BEGIN
  IF (@AccountId IS NULL OR @MoneyAmount IS NULL)
  RAISERROR('Missing value!', 16, 1)
 END

 BEGIN TRANSACTION 
 UPDATE Accounts SET Balance += @MoneyAmount WHERE Id = @AccountId
 IF (@@ROWCOUNT <> 1)
 BEGIN
  ROLLBACK
  RAISERROR('Invalid account!', 16, 1)
  RETURN
 END

 COMMIT
END

--In Judge must be paste without this below
GO

EXECUTE usp_DepositMoney 1, 10

EXECUTE usp_WithdrawMoney 1, 10

SELECT * FROM Accounts