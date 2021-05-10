CREATE PROC usp_GetEmployeesSalaryAboveNumber(@MinSalary DECIMAL(18, 4))
AS
SELECT FirstName AS [First Name], LastName AS [Last Name]
FROM Employees
WHERE Salary >= @MinSalary

--In Judge must be paste without this below
GO

EXEC usp_GetEmployeesSalaryAboveNumber 48100