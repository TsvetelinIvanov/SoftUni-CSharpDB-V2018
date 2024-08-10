CREATE TABLE Logs
(
LogId INT NOT NULL IDENTITY CONSTRAINT PK_Logs PRIMARY KEY,
AccountId INT NOT NULL CONSTRAINT FK_Logs_Accounts FOREIGN KEY REFERENCES Accounts(Id),
OldSum MONEY NOT NULL,
NewSum MONEY NOT NULL
)

GO

--In Judge must be pasted only the Trigger's creation below

CREATE TRIGGER tr_Accounts_Logs_After_Update ON Accounts 
FOR UPDATE AS
BEGIN
 INSERT INTO Logs
 VALUES
 (
 (SELECT Id FROM deleted),
 (SELECT Balance FROM deleted),
 (SELECT Balance FROM inserted)
 )
END

--In Judge must be paste without this below
GO

UPDATE Accounts SET Balance -= 10 WHERE Id = 1

UPDATE Accounts SET Balance += 10 WHERE Id = 1

SELECT * FROM Logs
