CREATE PROCEDURE usp_WithdrawMoney @AccountId INT, @MoneyAmount MONEY AS
BEGIN
 IF (@MoneyAmount < 0)
 BEGIN
  RAISERROR('Cannot withdraw negative value!', 16, 1)
 END
 ELSE
 BEGIN
  IF (@AccountId IS NULL OR @MoneyAmount IS NULL)
  RAISERROR('Missing value!', 16, 1)
 END

 BEGIN TRANSACTION
 UPDATE Accounts SET Balance -= @MoneyAmount WHERE Id = @AccountId
 IF (@@ROWCOUNT <> 1)
 BEGIN
  ROLLBACK
  RAISERROR('Invalid account!', 16, 1)
  RETURN
 END
 ELSE
 BEGIN
  IF ((SELECT Balance FROM Accounts WHERE Id = @AccountId) < 0)
  BEGIN
   ROLLBACK
   RAISERROR('The money in this account are not enough!', 16, 1)
   RETURN
  END
 END

 COMMIT
END

--In Judge must be pasted without this below
GO

EXECUTE usp_WithdrawMoney 5, 25

EXECUTE usp_DepositMoney 5, 25

SELECT * FROM Accounts
