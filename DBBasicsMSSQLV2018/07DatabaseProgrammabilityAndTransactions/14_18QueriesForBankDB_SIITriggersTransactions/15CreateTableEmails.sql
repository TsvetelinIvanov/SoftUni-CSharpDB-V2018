CREATE TABLE NotificationEmails
(
Id INT NOT NULL IDENTITY CONSTRAINT PK_NotificationEmails PRIMARY KEY,
Recipient INT NOT NULL,
[Subject] NVARCHAR(50) NOT NULL,
Body NVARCHAR(300)
)

GO

--In Judge must be pasted only the Trigger's creation below

CREATE TRIGGER tr_Logs_NotificationEmails_AfterInsert ON Logs
FOR INSERT AS
BEGIN
 INSERT INTO NotificationEmails
 VALUES
 (
 (SELECT AccountId FROM inserted),
 CONCAT('Balance change for account: ', (SELECT AccountId FROM inserted)),
 CONCAT('On ', FORMAT(GETDATE(), 'MMM dd yyyy HH mm'), ' your balance was changed from ', 
 (SELECT OldSum FROM Logs), ' to ', (SELECT NewSum FROM Logs), '.')
 )
END

--In Judge must be pasted without this below
GO

UPDATE Accounts SET Balance -= 10 WHERE Id = 1

UPDATE Accounts SET Balance += 10 WHERE Id = 1

INSERT INTO Logs 
VALUES (1, 123.12, 123.12) 

SELECT * FROM NotificationEmails

DROP TRIGGER tr_Logs_NotificationEmails_AfterInsert
