CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18, 4))
RETURNS CHAR(7) AS
BEGIN
  DECLARE @salaryLevel CHAR(7)
  IF (@salary < 30000)
    SET @salaryLevel = 'Low'
  ELSE IF (@salary BETWEEN 30000 AND 50000)
    SET @salaryLevel = 'Average'
  ELSE
    SET @salaryLevel = 'High'
  
  RETURN @salaryLevel
END

--In Judge must be pasted without this below
GO

SELECT e.Salary, dbo.ufn_GetSalaryLevel(e.Salary) AS [Salary Level]
FROM Employees AS e
