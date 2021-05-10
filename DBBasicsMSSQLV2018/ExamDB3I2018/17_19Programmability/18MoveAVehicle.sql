CREATE PROCEDURE usp_MoveVehicle @vehicleId INT, @officeId INT 
AS
BEGIN
 BEGIN TRANSACTION 
  UPDATE Vehicles
  SET OfficeId = @officeId
  WHERE Id = @vehicleId

  DECLARE @countVehiclesById INT = (SELECT COUNT(v.Id) FROM Vehicles AS v WHERE v.OfficeId = @officeId)
  DECLARE @parkingPlaces INT = (SELECT ParkingPlaces FROM Offices WHERE Id = @officeId)

  IF (@parkingPlaces < @countVehiclesById)
  BEGIN
   ROLLBACK
   RAISERROR('Not enough room in this office!', 16, 1)
   RETURN
  END

 COMMIT
END

--In Judge must be paste without this below

EXEC usp_MoveVehicle 7, 32;

SELECT OfficeId FROM Vehicles WHERE Id = 7
