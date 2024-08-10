CREATE PROCEDURE usp_AssignEmployeeToReport(@employeeId INT, @reportId INT) AS
BEGIN
 BEGIN TRANSACTION
  DECLARE @reportCategoryId INT = (SELECT CategoryId FROM Reports WHERE Id = @reportId)
  DECLARE @employeeDepartmentId INT = (SELECT DepartmentId FROM Employees WHERE Id = @employeeId)
  DECLARE @reportCategoryDepartmentId INT = (SELECT DepartmentId FROM Categories WHERE Id = @reportCategoryId)

  UPDATE Reports
  SET EmployeeId = @employeeId WHERE Id = @reportId

  IF (@employeeDepartmentId <> @reportCategoryDepartmentId)
  BEGIN
   ROLLBACK
   RAISERROR('Employee doesn''t belong to the appropriate department!', 16, 1 )
   RETURN
  END

 COMMIT
END

--In Judge must be pasted without this below

EXEC usp_AssignEmployeeToReport 17, 2
SELECT EmployeeId FROM Reports WHERE Id = 2
