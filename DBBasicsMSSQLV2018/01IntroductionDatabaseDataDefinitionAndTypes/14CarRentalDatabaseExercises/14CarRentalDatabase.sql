CREATE DATABASE CarRental
--It must be paste in Judge without this above

CREATE TABLE Categories
(
Id INT NOT NULL PRIMARY KEY,
CategoryName NVARCHAR(50) NOT NULL,
DailyRate DECIMAL(10, 2),
WeeklyRate DECIMAL(10, 2),
MonthlyRate DECIMAL(10, 2),
WeekendRate DECIMAL(10, 2)
)

ALTER TABLE Categories
ADD CONSTRAINT tc_AtLeastOneRate CHECK((DailyRate IS NOT NULL) OR (WeeklyRate IS NOT NULL) OR 
(MonthlyRate IS NOT NULL) OR (WeekendRate IS NOT NULL))

INSERT INTO Categories(Id, CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
(1, 'Saving', 15.50, 100, 400, 22.50), 
(2, 'Normal', 20, 130, 520, 30), 
(3, 'Luxury', 30, 200, 800, 50)

CREATE TABLE Cars
(
Id INT NOT NULL PRIMARY KEY,
PlateNumber VARCHAR(50) NOT NULL,
Manufacturer NVARCHAR(50) NOT NULL,
Model NVARCHAR(50) NOT NULL,
CarYear INT NOT NULL,
CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
Doors TINYINT NOT NULL,
Picture VARBINARY(MAX),
Condition NVARCHAR(50),
Available BIT DEFAULT 1
)

INSERT INTO Cars(Id, PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Available) VALUES
(1, 'CC567839046AT', 'VW', 'Golf', 1998, 1, 2, 1),
(2, 'BA786534083VG', 'Ford', 'Escort', 1992, 3, 4, 1),
(3, 'AC437539573DS', 'Renault', 'Megane', 1996, 2, 4, 1)

CREATE TABLE Employees
(
Id INT NOT NULL PRIMARY KEY,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Title NVARCHAR(50) NOT NULL,
Notes NTEXT
)

INSERT INTO Employees(Id, FirstName, LastName, Title) VALUES
(1, 'Ivan', 'Petrov', 'Mechanic'),
(2, 'Boyan', 'Vasev', 'Engineer'),
(3, 'Tanya', 'Borisova', 'Shop Assistent')

CREATE TABLE Customers
(
Id INT NOT NULL PRIMARY KEY,
DriverLicenceNumber VARCHAR(50) NOT NULL UNIQUE,
FullName NVARCHAR(50) NOT NULL,
[Address] NVARCHAR(255),
City NVARCHAR(50),
ZIPCode NVARCHAR(50),
Notes NTEXT
)

INSERT INTO Customers(Id, DriverLicenceNumber, FullName, City) VALUES
(1, '5867786100', 'Ivan Penchev', 'Sofia'),
(2, '1234566100', 'Gergana Miteva', 'Plovdiv'),
(3, '6789016100', 'Boyko Vinogradov', 'Varna')

CREATE TABLE RentalOrders
(
Id INT NOT NULL PRIMARY KEY,
EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(Id),
CarId INT NOT NULL FOREIGN KEY REFERENCES Cars(Id),
TankLevel NUMERIC(5, 2),
KilometrageStart INT,
KilometrageEnd INT,
TotalKilometrage INT,
StartDate DATE NOT NULL,
EndDate DATE NOT NULL,
TotalDays INT NOT NULL,
--TotalDays AS DATEDIFF(DAY, StartDate, EndDate)
RateApplied DECIMAL(10, 2),
TaxRate DECIMAL(10, 2),
OrderStatus NVARCHAR(50),
Notes NTEXT
)

ALTER TABLE RentalOrders
ADD CONSTRAINT ch_TotalDays CHECK(DATEDIFF(DAY, StartDate, EndDate) = TotalDays)

INSERT INTO RentalOrders(Id, EmployeeId, CustomerId, CarId, StartDate, EndDate, TotalDays) VALUES
(1, 1, 2, 1, '01-01-2018', '01-02-2018', 1),
(2, 2, 3, 2, '01-01-2018', '01-03-2018', 2),
(3, 1, 1, 3, '01-01-2018', '01-04-2018', 3)
