CREATE TRIGGER tr_Orders_MoveTheTally ON Orders 
FOR UPDATE 
AS
BEGIN
 DECLARE @newTotalMileage INT = (SELECT TotalMileage FROM inserted)
 DECLARE @oldTotalMileage INT = (SELECT TotalMileage FROM deleted)
 DECLARE @vehicleId INT = (SELECT VehicleId FROM inserted) 

 IF (@oldTotalMileage IS NULL AND @vehicleId IS NOT NULL)
 BEGIN
  UPDATE Vehicles
  SET Mileage += @newTotalMileage
  WHERE Id = @vehicleId
 END
END

--In Judge must be paste without this below

UPDATE Orders
SET
TotalMileage = 100
WHERE Id = 40
