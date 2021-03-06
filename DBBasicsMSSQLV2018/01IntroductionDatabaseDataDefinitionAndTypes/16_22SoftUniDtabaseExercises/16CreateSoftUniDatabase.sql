CREATE DATABASE SoftUni

CREATE TABLE Towns
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Addresses
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
AddressText NVARCHAR(255) NOT NULL,
TownId INT NOT NULL FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
FirstName NVARCHAR(50) NOT NULL,
MiddleName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
JobTitle NVARCHAR(255) NOT NULL,
DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Departments(Id),
HireDate DATE,
Salary DECIMAL(15, 2) NOT NULL,
AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)
