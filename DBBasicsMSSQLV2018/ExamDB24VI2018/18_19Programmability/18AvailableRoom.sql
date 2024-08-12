CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
RETURNS NVARCHAR(MAX) AS
BEGIN
 DECLARE @BookedRooms TABLE(Id INT)
 INSERT INTO @BookedRooms
 SELECT DISTINCT r.Id
 FROM Rooms AS r
 LEFT JOIN Trips AS t ON t.RoomId = r.Id
 WHERE r.HotelId = @HotelId AND (@Date BETWEEN t.ArrivalDate AND t.ReturnDate) AND t.CancelDate IS NULL

 DECLARE @Rooms TABLE(Id INT, Price DECIMAL(15, 2), Type VARCHAR(20), Beds INT, TotalPrice DECIMAL(15, 2))
 INSERT INTO @Rooms 
 SELECT TOP(1) r.Id, r.Price, r.[Type], r.Beds, @People * (h.BaseRate + r.Price) AS TotalPrice
 FROM Rooms  AS r
 LEFT JOIN Hotels AS h on h.Id = r.HotelId
 WHERE r.HotelId = @HotelId AND r.Beds >= @People AND r.Id NOT IN (SELECT Id FROM @BookedRooms)
 ORDER BY TotalPrice DESC

 DECLARE @RoomCount INT = (SELECT COUNT(*) FROM @Rooms)
 IF (@RoomCount < 1)
 BEGIN
  RETURN 'No rooms available'
 END

 DECLARE @Result NVARCHAR(MAX) = (SELECT CONCAT('Room ', Id, ': ', [Type], ' (', Beds, ' beds) - ', 
                                   '$', TotalPrice) FROM @Rooms)
 
 RETURN @Result
END

--In Judge must be pasted without this below

SELECT dbo.udf_GetAvailableRoom(112, '2011-12-17', 2)

SELECT dbo.udf_GetAvailableRoom(94, '2015-07-26', 3)
