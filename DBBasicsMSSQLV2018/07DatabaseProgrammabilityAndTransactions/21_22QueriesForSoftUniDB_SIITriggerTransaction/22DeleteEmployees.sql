CREATE TABLE Deleted_Employees
(
EmployeeId INT NOT NULL IDENTITY CONSTRAINT PK_Deleted_Employees PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
MiddleName VARCHAR(50),
JobTitle VARCHAR NOT NULL,
DepartmentId INT NOT NULL 
CONSTRAINT FK_Deleted_Employees_Departments FOREIGN KEY REFERENCES Departments(DepartmentID),
Salary MONEY NOT NULL
)

--In Judge must be pasted only the Trigger's creation below
GO

--The query below doesn't past in Judge - Compile time error
CREATE TRIGGER tr_Deleted_Employees_Employees_AfterFirring ON Employees
AFTER DELETE AS 
BEGIN
--by inserting in this way it may take more as one value and this causes the error
 INSERT INTO Deleted_Employees 
 VALUES
 (
 (SELECT FirstName FROM deleted),
 (SELECT LastName FROM deleted),
 (SELECT MiddleName FROM deleted),
 (SELECT JobTitle FROM deleted),
 (SELECT DepartmentID FROM deleted),
 (SELECT Salary FROM deleted)
 )
END

--Only one query must be pasted in Judge
GO

CREATE TRIGGER tr_Deleted_Employees_Employees_AfterFirring ON Employees
FOR DELETE AS 
BEGIN
 INSERT INTO Deleted_Employees
 SELECT FirstName, LastName, MiddleName, JobTitle, DepartmentID, Salary
 FROM deleted
END

--Only one query must be pasted in Judge
GO

CREATE TRIGGER tr_Deleted_Employees_Employees_AfterFirring ON Employees
AFTER DELETE AS 
BEGIN
 INSERT INTO Deleted_Employees
 SELECT FirstName, LastName, MiddleName, JobTitle, DepartmentID, Salary
 FROM deleted
END

--In Judge must be pasted without this below 

ALTER TABLE Employees ALTER COLUMN ManagerID INT

ALTER TABLE Employees ALTER COLUMN DepartmentID INT

UPDATE Employees SET DepartmentID = NULL WHERE EmployeeID = 293

UPDATE Employees SET ManagerID = NULL WHERE EmployeeID = 293

DELETE FROM EmployeesProjects WHERE EmployeeID = 293

--this query below work only when the trigger isn't created
DELETE FROM Employees WHERE EmployeeID = 293

SELECT * FROM Employees

--the table is empty because the delete query work only when the trigger isn't created
SELECT * FROM Deleted_Employees

--the commeted rows below don't work - drop Database SoftUni and create it again
--ALTER TABLE Employees ALTER COLUMN ManagerID INT NOT NULL

--ALTER TABLE Employees ALTER COLUMN DepartmentID INT NOT NULL

--INSERT INTO Employees (EmployeeID, FirstName, LastName, MiddleName, JobTitle, DepartmentID, ManagerID, HireDate, Salary, AddressID)
--VALUES (293, 'George', 'Denchev', NULL, 'Independent Java Consultant', 6, NULL, '20050301', 48000, 291)

--INSERT INTO Employees (EmployeeID, FirstName, LastName, MiddleName, JobTitle, DepartmentID, ManagerID, HireDate, Salary, AddressID)
--VALUES (292, 'Martin', 'Kulov', NULL, 'Independent .NET Consultant', 6, NULL, '20050301', 48000, 291)

--INSERT INTO Employees (EmployeeID, FirstName, LastName, MiddleName, JobTitle, DepartmentID, ManagerID, HireDate, Salary, AddressID)
--VALUES (291, 'Svetlin', 'Nakov', 'Ivanov', 'Independent Software Development  Consultant', 6, NULL, '20050301', 48000, 291)

--INSERT INTO Employees (EmployeeID, FirstName, LastName, MiddleName, JobTitle, DepartmentID, ManagerID, HireDate, Salary, AddressID)
--VALUES (290, 'Lynn', 'Tsoflias', '', 'Sales Representative', 3, 288, '20050701', 23100, 153)

DROP TRIGGER tr_Deleted_Employees_Employees_AfterFirring
