CREATE PROCEDURE usp_GetEmployeesFromTown(@TownName VARCHAR(50)) AS
SELECT  e.FirstName AS [First Name], e.LastName AS [Last Name]
FROM Employees AS e
JOIN Addresses AS a ON a.AddressID = e.AddressID
JOIN Towns AS t ON t.TownID = a.TownID
WHERE t.[Name] = @TownName

--Only one query must be pasted in Judge
GO
DROP PROC usp_GetEmployeesFromTown
GO

CREATE PROCEDURE usp_GetEmployeesFromTown(@TownName VARCHAR(50)) AS
SELECT e.FirstName AS [First Name], e.LastName AS [Last Name]
FROM Employees AS e
JOIN Addresses AS a ON a.AddressID = e.AddressID
JOIN Towns AS t ON t.TownID = a.TownID
WHERE t.[Name] LIKE @TownName + '%'

--In Judge must be pasted without this below
GO

EXEC usp_GetEmployeesFromTown 'Sofia'

