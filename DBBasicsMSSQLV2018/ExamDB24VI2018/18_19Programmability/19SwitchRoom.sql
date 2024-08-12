CREATE PROCEDURE usp_SwitchRoom @TripId INT, @TargetRoomId INT 
AS
BEGIN  

  DECLARE @targetRoom_HotelId INT = (SELECT HotelId FROM Rooms WHERE Id = @TargetRoomId)
  DECLARE @trip_RoomId INT = (SELECT RoomId	FROM Trips WHERE Id = @TripId)
  DECLARE @trip_Rooms_HotelId INT = (SELECT HotelId FROM Rooms WHERE Id = @trip_RoomId)  
  IF (@targetRoom_HotelId <> @trip_Rooms_HotelId)
  BEGIN   
   RAISERROR('Target room is in another hotel!', 16, 1)  
  END
  
  DECLARE @targetRoom_Beds INT = (SELECT Beds FROM Rooms WHERE Id = @TargetRoomId)
  DECLARE @trip_AccountsCount INT = (SELECT COUNT(AccountId) FROM AccountsTrips WHERE TripId = @TripId)
  IF (@targetRoom_Beds < @trip_AccountsCount)
  BEGIN   
   RAISERROR('Not enough beds in target room!', 16, 1)  
  END  

  UPDATE Trips
  SET RoomId = @TargetRoomId
  WHERE Id = @TripId
END

GO
--In judje must be pasted only one query, but this query below does not work good - 4/7 in Judge, because 
--exception 'Target room is in another hotel!' doesn't work in transaction - I don't know way!

CREATE PROCEDURE usp_SwitchRoom @TripId INT, @TargetRoomId INT 
AS
BEGIN    
 BEGIN TRANSACTION 

  UPDATE Trips
  SET RoomId = @TargetRoomId
  WHERE Id = @TripId

  DECLARE @targetRoom_HotelId INT = (SELECT HotelId FROM Rooms WHERE Id = @TargetRoomId)
  DECLARE @trip_RoomId INT = (SELECT RoomId	FROM Trips WHERE Id = @TripId)
  DECLARE @trip_Rooms_HotelId INT = (SELECT HotelId FROM Rooms WHERE Id = @trip_RoomId) 
  IF (@targetRoom_HotelId <> @trip_Rooms_HotelId)
  BEGIN
   ROLLBACK
   RAISERROR('Target room is in another hotel!', 16, 1)
   RETURN
  END
  
  DECLARE @targetRoom_Beds INT = (SELECT Beds FROM Rooms WHERE Id = @TargetRoomId)
  DECLARE @trip_AccountsCount INT = (SELECT COUNT(AccountId) FROM AccountsTrips WHERE TripId = @TripId)
  IF (@targetRoom_Beds < @trip_AccountsCount)
  BEGIN
   ROLLBACK
   RAISERROR('Not enough beds in target room!', 16, 1)
   RETURN
  END  

 COMMIT
END

--In judje must be pasted only one query, but this below is autor's solution:

CREATE PROC usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
  BEGIN
    DECLARE @SourceHotelId INT = (SELECT H.Id FROM Hotels H
                                  JOIN Rooms R on H.Id = R.HotelId
                                  JOIN Trips T on R.Id = T.RoomId
                                  WHERE T.Id = @TripId)

    DECLARE @TargetHotelId INT = (SELECT H.Id FROM Hotels H
                                  JOIN Rooms R on H.Id = R.HotelId
                                  WHERE R.Id = @TargetRoomId)

    IF (@SourceHotelId <> @TargetHotelId)
        THROW 50013, 'Target room is in another hotel!', 1

    DECLARE @PeopleCount INT = (SELECT COUNT(*) FROM AccountsTrips
                                WHERE TripId = @TripId)

    DECLARE @TargetRoomBeds INT = (SELECT Beds FROM Rooms
                                   WHERE Id = @TargetRoomId)

    IF (@PeopleCount > @TargetRoomBeds)
        THROW 50013, 'Not enough beds in target room!', 1

    UPDATE Trips
    SET RoomId = @TargetRoomId
    WHERE Id = @TripId
  END

--In Judge must be pasted without this below

EXEC usp_SwitchRoom 10, 11

SELECT RoomId FROM Trips WHERE Id = 10

EXEC usp_SwitchRoom 10, 7

EXEC usp_SwitchRoom 10, 8

DROP PROCEDURE usp_SwitchRoom
