CREATE TRIGGER TR_CancelTrip ON Trips
INSTEAD OF DELETE AS
BEGIN
 UPDATE Trips
 SET CancelDate = GETDATE()
 WHERE Id IN (SELECT Id FROM deleted WHERE CancelDate IS NULL)
END

--In Judge must be pasted without this below

DELETE FROM Trips
WHERE Id IN (48, 49, 50)
