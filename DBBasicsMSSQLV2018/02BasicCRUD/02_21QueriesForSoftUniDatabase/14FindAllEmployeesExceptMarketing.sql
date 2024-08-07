SELECT FirstName, LastName 
FROM Employees 
--WHERE DepartmentId <> 4
WHERE DepartmentId != 4

--In Judge must be pasted only one query

SELECT FirstName, LastName 
FROM Employees 
WHERE DepartmentId IN 
(SELECT DepartmentID FROM Departments WHERE [Name] <> 'Marketing' )
