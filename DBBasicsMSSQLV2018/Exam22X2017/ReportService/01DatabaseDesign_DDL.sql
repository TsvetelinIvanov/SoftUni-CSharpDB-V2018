CREATE TABLE Users
(
Id INT NOT NULL IDENTITY CONSTRAINT PK_Users PRIMARY KEY,
Username NVARCHAR(30) UNIQUE NOT NULL,
[Password] NVARCHAR(50) NOT NULL,
[Name] NVARCHAR(50),
Gender CHAR(1) CONSTRAINT CH_Gender CHECK(Gender IN('M', 'F')),
BirthDate DATETIME, 
Age INT,
Email NVARCHAR(50) NOT NULL
)

CREATE TABLE Departments
(
Id INT NOT NULL IDENTITY CONSTRAINT PK_Departments PRIMARY KEY,
[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees
(
Id INT NOT NULL IDENTITY CONSTRAINT PK_Employees PRIMARY KEY,
FirstName NVARCHAR(25),
LastName NVARCHAR(25),
Gender CHAR(1) CHECK(Gender IN('M', 'F')),
BirthDate DATETIME,
Age INT,
DepartmentId INT NOT NULL CONSTRAINT FK_Employees_Departments FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE Categories
(
Id INT NOT NULL IDENTITY CONSTRAINT PK_Categories PRIMARY KEY, 
[Name] VARCHAR(50) NOT NULL,
DepartmentId INT CONSTRAINT FK_Categories_Departments FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE [Status]
(
Id INT NOT NULL IDENTITY CONSTRAINT PK_Status PRIMARY KEY,
Label VARCHAR(50) NOT NULL
)

CREATE TABLE Reports
(
Id INT NOT NULL IDENTITY CONSTRAINT PK_Reports PRIMARY KEY,
CategoryId INT NOT NULL CONSTRAINT FK_Reports_Categories FOREIGN KEY REFERENCES Categories(Id),
StatusId INT NOT NULL CONSTRAINT FK_Reports_Status FOREIGN KEY REFERENCES [Status](Id),
OpenDate DATETIME NOT NULL,
CloseDate DATETIME,
[Description] VARCHAR(200),
UserId INT NOT NULL CONSTRAINT FK_Reports_Users FOREIGN KEY REFERENCES Users(Id),
EmployeeId INT CONSTRAINT FK_Reports_Employees FOREIGN KEY REFERENCES Employees(Id)
)
