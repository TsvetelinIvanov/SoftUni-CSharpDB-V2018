--The query below causes a compile time error in Jugge, but it works correctly in the database!!!
CREATE FUNCTION udf_GetReportsCount(@employeeId INT, @statusId INT) 
RETURNS INT AS
BEGIN
 DECLARE @reportsCount INT

 SET @reportsCount = (SELECT COUNT(r.StatusId) FROM Employees AS e
                      JOIN Reports AS r ON r.EmployeeId = e.Id
		      WHERE e.Id = @employeeId AND r.StatusId = @statusId
		      GROUP BY r.EmployeeId)

 RETURN ISNULL(@reportsCount, 0)
END
--The query above causes a compile time error in Jugge, but it works correctly in the database!!!

--In Judge must be pasted only one query

CREATE FUNCTION udf_GetReportsCount(@employeeId INT, @statusId INT) 
RETURNS INT AS
BEGIN
 DECLARE @reportsCount INT

 SET @reportsCount = (SELECT COUNT(*) FROM Reports 
		      WHERE EmployeeId = @employeeId AND StatusId = @statusId)					  

 RETURN @reportsCount
END

--In Judge must be pasted without this below
GO

SELECT Id, FirstName, Lastname, dbo.udf_GetReportsCount(Id, 2) AS ReportsCount
FROM Employees
ORDER BY Id

DROP FUNCTION udf_GetReportsCount
