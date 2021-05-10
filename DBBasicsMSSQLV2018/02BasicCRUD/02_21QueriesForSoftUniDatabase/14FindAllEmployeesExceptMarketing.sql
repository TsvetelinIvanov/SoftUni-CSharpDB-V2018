SELECT FirstName, LastName 
FROM Employees 
--WHERE DepartmentId <> 4
WHERE DepartmentId != 4

--In Judge must be paste only one query

SELECT FirstName, LastName 
FROM Employees 
WHERE DepartmentId IN 
(SELECT DepartmentID FROM Departments WHERE [Name] <> 'Marketing' )
