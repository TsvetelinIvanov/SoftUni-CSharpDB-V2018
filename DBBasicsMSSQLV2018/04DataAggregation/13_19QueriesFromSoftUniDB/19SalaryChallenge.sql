SELECT TOP(10) FirstName, LastName, DepartmentID
FROM Employees AS e
WHERE Salary > 
(
SELECT AVG(Salary) 
FROM Employees AS e1
WHERE e.DepartmentID = e1.DepartmentID
GROUP BY DepartmentID
)