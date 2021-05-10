SELECT * INTO RichEmployees
FROM Employees
WHERE Salary > 30000

DELETE FROM RichEmployees
WHERE ManagerID = 42

UPDATE RichEmployees
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary
FROM RichEmployees
GROUP BY DepartmentID