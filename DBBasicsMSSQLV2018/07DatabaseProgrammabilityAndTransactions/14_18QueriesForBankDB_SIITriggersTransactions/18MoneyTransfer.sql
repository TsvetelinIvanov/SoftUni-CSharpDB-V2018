CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @MoneyAmount MONEY) AS
BEGIN
 IF (@MoneyAmount < 0)
 BEGIN
  RAISERROR('Cannot transfer negative value!', 16, 1)
 END
 ELSE
 BEGIN
  IF (@SenderId IS NULL OR @ReceiverId IS NULL OR @MoneyAmount IS NULL)
  RAISERROR('Missing value!', 16, 1)
 END

 BEGIN TRANSACTION
 UPDATE Accounts SET Balance -= @MoneyAmount WHERE Id = @SenderId
 IF (@@ROWCOUNT <> 1)
 BEGIN
  ROLLBACK
  RAISERROR('Invalid account!', 16, 1)
  RETURN
 END

 IF ((SELECT Balance FROM Accounts WHERE Id = @SenderId) < 0)
 BEGIN
  ROLLBACK
  RAISERROR('The money in this account are not enough!', 16, 1)
  RETURN
 END

 UPDATE Accounts SET Balance += @MoneyAmount WHERE Id = @ReceiverId
 IF (@@ROWCOUNT <> 1)
 BEGIN
  ROLLBACK
  RAISERROR('Invalid account!', 16, 1)
  RETURN
 END

 COMMIT
END

--Only one query must be pasted in Judge
GO

CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @MoneyAmount MONEY) AS
BEGIN
 BEGIN TRANSACTION 

 EXECUTE usp_WithdrawMoney @SenderId, @MoneyAmount

 EXECUTE usp_DepositMoney @ReceiverId, @MoneyAmount
 
 COMMIT
END

--Only one query must be pasted in Judge
GO

CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @MoneyAmount MONEY) AS
BEGIN 

 EXECUTE usp_WithdrawMoney @SenderId, @MoneyAmount

 EXECUTE usp_DepositMoney @ReceiverId, @MoneyAmount 
 
END

--In Judge must be pasted without this below
GO

DROP PROCEDURE usp_TransferMoney

EXECUTE usp_TransferMoney 5, 1, 5000

EXECUTE usp_TransferMoney 1, 5, 5000

SELECT * FROM Accounts
