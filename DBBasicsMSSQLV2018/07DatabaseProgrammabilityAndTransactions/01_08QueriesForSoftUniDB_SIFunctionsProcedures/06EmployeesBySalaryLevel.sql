CREATE PROCEDURE usp_EmployeesBySalaryLevel (@salaryLevel CHAR(7)) AS
SELECT FirstName AS [First Name], LastName AS [Last Name]
FROM Employees
WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel

--In Judge must be paste without this below
GO

EXEC usp_EmployeesBySalaryLevel  'High'