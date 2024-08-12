CREATE FUNCTION udf_CheckForVehicle(@townName VARCHAR(50), @seatsNumber INT) 
RETURNS NVARCHAR(MAX)
AS
BEGIN
 DECLARE @result NVARCHAR(100) = (SELECT TOP(1) CONCAT(o.[Name], ' - ', m.Model)
                                  FROM Towns AS t
				  JOIN Offices AS o ON o.TownId = t.Id
				  JOIN Vehicles AS v ON v.OfficeId = o.Id
				  JOIN Models AS m ON m.Id = v.ModelId
				  WHERE t.[Name] = @townName AND m.Seats = @seatsNumber
				  ORDER BY o.[Name])
 IF (@result IS NULL)
     RETURN 'NO SUCH VEHICLE FOUND'

 RETURN @result
END

--In Judge must be pasted without this below

SELECT dbo.udf_CheckForVehicle ('La Escondida', 9)
