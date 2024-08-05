CREATE DATABASE Hotel
--It must be pasted in Judge without this above

CREATE TABLE Employees 
(
Id INT NOT NULL PRIMARY KEY,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Title NVARCHAR(255) NOT NULL,
Notes NTEXT
) 

INSERT INTO Employees(Id, FirstName, LastName, Title) VALUES
(1, 'Ivan', 'Penchev', 'Guard'),
(2, 'Tanya', 'Petrova', 'Waitress'),
(3, 'Gergo', 'Tomov', 'Barman')

CREATE TABLE Customers
(
AccountNumber INT NOT NULL PRIMARY KEY,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
PhoneNumber VARCHAR(50),
EmergencyName NVARCHAR(50) NOT NULL,
EmergencyNumber VARCHAR(50) NOT NULL,
Notes NTEXT 
)

INSERT INTO Customers(AccountNumber, FirstName, LastName, EmergencyName, EmergencyNumber) VALUES
(1, 'Sarah', 'Riley', 'Danielle Howland', '112'),
(2, 'Adam', 'Winrow', 'Rob Fuller', '112'),
(3, 'Nicke', 'Eede', 'Andrew Arnison', '918')

CREATE TABLE RoomStatus 
(
RoomStatus VARCHAR(50) NOT NULL PRIMARY KEY,
Notes NTEXT
)

INSERT INTO RoomStatus(RoomStatus) VALUES
('Free'),
('Reserved'),
('Occupied')

CREATE TABLE RoomTypes 
(
RoomType VARCHAR(50) NOT NULL PRIMARY KEY,
Notes NTEXT
)

INSERT INTO RoomTypes(RoomType) VALUES
('Luxury'),
('Normal'),
('Poor')

CREATE TABLE BedTypes 
(
BedType VARCHAR(50) NOT NULL PRIMARY KEY,
Notes NTEXT
)

INSERT INTO BedTypes(BedType) VALUES
('Single'),
('Double'),
('Huge')

CREATE TABLE Rooms
(
RoomNumber INT NOT NULL PRIMARY KEY,
RoomType VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES RoomTypes(RoomType),
BedType VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES BedTypes(BedType),
Rate INT NOT NULL,
RoomStatus VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES RoomStatus(RoomStatus),
Notes NTEXT
)

INSERT INTO Rooms(RoomNumber, RoomType, BedType, Rate, RoomStatus) VALUES
(1, 'Luxury', 'Huge', 100, 'Free'),
(2, 'Normal', 'Double', 80, 'Reserved'),
(3, 'Poor', 'Single', 50, 'Occupied')

CREATE TABLE Payments 
(
Id INT NOT NULL PRIMARY KEY,
EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
PaymentDate DATE NOT NULL,
AccountNumber INT NOT NULL FOREIGN KEY REFERENCES Customers(AccountNumber),
FirstDateOccupied DATE NOT NULL,
LastDateOccupied DATE NOT NULL,
TotalDays INT NOT NULL,
AmountCharged DECIMAL(15, 2) NOT NULL,
TaxRate DECIMAL(15, 2) NOT NULL,
TaxAmount DECIMAL(15, 2) NOT NULL,
PaymentTotal DECIMAL(15, 2) NOT NULL,
Notes NTEXT
)

ALTER TABLE Payments
ADD CONSTRAINT ch_TotalDays CHECK(DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied) = TotalDays)

ALTER TABLE Payments
ADD CONSTRAINT ch_TaxAmount CHECK(TotalDays * TaxRate = TaxAmount)

INSERT INTO Payments(Id, EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, 
AmountCharged, TaxRate, TaxAmount, PaymentTotal) VALUES
(1, 2, '08-07-2018', 1, '08-07-2018', '08-09-2018', 2, 475.99, 50, 100, 475.99),
(2, 3, '08-09-2018', 2, '08-10-2018', '08-11-2018', 1, 100, 38.68, 38.68, 100),
(3, 1, '08-01-2018', 3, '08-01-2018', '08-11-2018', 10, 1475.83, 10, 100, 1475.83)

CREATE TABLE Occupancies 
(
Id INT NOT NULL PRIMARY KEY,
EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
DateOccupied DATE NOT NULL,
AccountNumber INT NOT NULL FOREIGN KEY REFERENCES Customers(AccountNumber),
RoomNumber INT NOT NULL FOREIGN KEY REFERENCES Rooms(RoomNumber),
RateAplied INT,
PhoneCharge VARCHAR(50) NOT NULL,
Notes NTEXT
)

INSERT INTO Occupancies(Id, EmployeeId, DateOccupied, AccountNumber, RoomNumber, PhoneCharge) VALUES
(1, 2, '08-07-2018', 1, 1, '0888 09 08 01'),
(2, 3, '08-10-2018', 2, 2, '0888 09 08 02'),
(3, 1, '08-01-2018', 3, 3, '0888 09 08 03')
