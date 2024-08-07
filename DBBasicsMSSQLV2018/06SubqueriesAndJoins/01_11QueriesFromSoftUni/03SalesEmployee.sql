SELECT e.EmployeeID, e.FirstName, e.LastName, d.[Name] AS DepartmentName
FROM Employees AS e
JOIN Departments AS d
ON d.DepartmentID = e.DepartmentID
WHERE d.[Name] = 'Sales'
ORDER BY e.EmployeeID

--Only one query must be pasted in Judge

SELECT e.EmployeeID, e.FirstName, e.LastName, d.[Name] AS DepartmentName
FROM Employees AS e
JOIN Departments AS d
ON (d.DepartmentID = e.DepartmentID
AND d.[Name] = 'Sales')
ORDER BY e.EmployeeID
