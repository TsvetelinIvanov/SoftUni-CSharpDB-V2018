CREATE PROCEDURE usp_AssignProject(@EmployeeID INT, @ProjectID INT) AS
BEGIN
 DECLARE @maxEmployeeProgectsCount INT = 3
 DECLARE @employeeProjectsCount INT
 SET @employeeProjectsCount = (SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @EmployeeID)
 BEGIN TRANSACTION
  INSERT INTO EmployeesProjects (EmployeeID, ProjectID)
  VALUES (@EmployeeID, @ProjectID)
  IF (@employeeProjectsCount >= @maxEmployeeProgectsCount)
  BEGIN
   RAISERROR('The employee has too many projects!', 16, 1)
   ROLLBACK
   RETURN
  END

 COMMIT
END