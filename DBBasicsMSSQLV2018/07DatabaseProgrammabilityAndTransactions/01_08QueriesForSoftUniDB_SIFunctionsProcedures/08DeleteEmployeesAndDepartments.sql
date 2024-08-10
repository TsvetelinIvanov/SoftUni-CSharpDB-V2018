CREATE PROCEDURE usp_DeleteEmployeesFromDepartment(@departmentId INT) AS
BEGIN
 ALTER TABLE Employees ALTER COLUMN ManagerID INT

 ALTER TABLE Employees ALTER COLUMN DepartmentID INT

 UPDATE Employees
 SET DepartmentID = NULL
 WHERE DepartmentID = @departmentId
 --WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

 UPDATE Employees
 SET ManagerID = NULL
 WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

 ALTER TABLE Departments ALTER COLUMN ManagerID INT

 UPDATE Departments 
 SET ManagerID = NULL WHERE DepartmentID = @departmentId

 DELETE FROM Departments WHERE DepartmentID = @departmentId

 DELETE FROM EmployeesProjects 
 WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

 DELETE FROM Employees WHERE DepartmentID = @departmentId

 SELECT COUNT(*) FROM Employees WHERE DepartmentID = @departmentId
END

--In Judge must be paste without this below
EXEC usp_DeleteEmployeesFromDepartment 3



/* *****************************************************
     ***** NOT FOR JUDGE ***** NOT FOR JUDGE *****
/* *****************************************************
	Understanding from the old exercise explanation
/* *****************************************************
	Problem 8.1. * Delete Employees and Departments
********************************************************/

CREATE PROCEDURE usp_DeleteEmployeesFromDepartment(@departmentName NVARCHAR(50)) AS
BEGIN
  ALTER TABLE Employees ALTER COLUMN ManagerID INT;

  ALTER TABLE Employees ALTER COLUMN DepartmentID INT;

  UPDATE Employees
  SET DepartmentID = NULL WHERE EmployeeID IN 
  (
  (
    SELECT e.EmployeeID FROM Employees AS e
    JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
    WHERE d.Name = @departmentName
  )
  );

  UPDATE Employees
  SET ManagerID = NULL WHERE ManagerID IN
  (
  (
    SELECT e.EmployeeID FROM Employees AS e
    JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
    WHERE d.Name = @departmentName
  )
  );

  ALTER TABLE Departments ALTER COLUMN ManagerID INT;
  UPDATE Departments
  SET ManagerID = NULL
  WHERE Name = @departmentName;

  DELETE FROM Departments
  WHERE Name = @departmentName;

  DELETE FROM EmployeesProjects
  WHERE EmployeeID IN
  (
  (
    SELECT e.EmployeeID FROM Employees AS e
    JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
    WHERE d.Name = @departmentName
  )
  );

  DELETE FROM Employees
  WHERE EmployeeID IN
  (
  (
    SELECT e.EmployeeID FROM Employees AS e
    JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
    WHERE d.Name = @departmentName
  )
  );

END;
         
BEGIN TRANSACTION;

EXECUTE usp_DeleteEmployeesFromDepartment 'Production';

EXECUTE usp_DeleteEmployeesFromDepartment 'Production Control';

ROLLBACK;

/* *****************************************************
     ***** NOT FOR JUDGE ***** NOT FOR JUDGE *****
/* *****************************************************
	Understanding from the old exercise explanation
/* *****************************************************
	Option without procedure 
********************************************************/
ALTER TABLE Employees ALTER COLUMN ManagerID INT;

ALTER TABLE Employees ALTER COLUMN DepartmentID INT;

UPDATE Employees
SET DepartmentID = NULL
WHERE EmployeeID IN
(
(
  SELECT e.EmployeeID FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
  WHERE d.Name IN('Production', 'Production Control')
)
);

UPDATE Employees
SET  ManagerID = NULL
WHERE ManagerID IN
(
(
  SELECT e.EmployeeID FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
  WHERE d.Name IN('Production', 'Production Control')
)
);

ALTER TABLE Departments ALTER COLUMN ManagerID INT;

UPDATE Departments
SET ManagerID = NULL
WHERE Name IN('Production', 'Production Control');

DELETE FROM Departments
WHERE Name IN('Production', 'Production Control');

DELETE FROM EmployeesProjects
WHERE EmployeeID IN
(
(
  SELECT e.EmployeeID
  FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
  WHERE d.Name IN('Production', 'Production Control')
)
);

DELETE FROM Employees
WHERE EmployeeID IN
(
(
  SELECT e.EmployeeID
  FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
  WHERE d.Name IN('Production', 'Production Control')
)
);

/* *****************************************************
 ****** FOR JUDGE ***** FOR JUDGE ***** FOR JUDGE *****
/* *****************************************************
	NEW exercise explanation
/* *****************************************************
	Problem 8.2. * Delete Employees and Departments
********************************************************/

CREATE PROCEDURE usp_DeleteEmployeesFromDepartment(@departmentId INT) AS
BEGIN
  ALTER TABLE Employees ALTER COLUMN ManagerID INT;

  ALTER TABLE Employees ALTER COLUMN DepartmentID INT;

  UPDATE Employees
  SET DepartmentID = NULL
  WHERE EmployeeID IN
  (
  (
    SELECT EmployeeID FROM Employees
    WHERE DepartmentID = @departmentId
  )
  );

  UPDATE Employees
  SET ManagerID = NULL
  WHERE ManagerID IN
  (
  (
    SELECT EmployeeID FROM Employees
    WHERE DepartmentID = @departmentId
  )
  );

  ALTER TABLE Departments ALTER COLUMN ManagerID INT;

  UPDATE Departments
  SET ManagerID = NULL
  WHERE DepartmentID = @departmentId;

  DELETE FROM Departments
  WHERE DepartmentID = @departmentId;

  DELETE FROM EmployeesProjects
  WHERE EmployeeID IN
  (
  (
    SELECT EmployeeID FROM Employees
    WHERE DepartmentID = @departmentId
  )
  );

  DELETE FROM Employees
  WHERE DepartmentID = @departmentId;

  SELECT COUNT(*)
  FROM Employees
  WHERE DepartmentID = @departmentId;
END;	 
