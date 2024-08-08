SELECT TOP(5) e.EmployeeID, e.FirstName, p.[Name] AS ProjectName
FROM Employees AS e
INNER JOIN EmployeesProjects AS e_p ON e_p.EmployeeID = e.EmployeeID
INNER JOIN Projects AS p ON p.ProjectID = e_p.ProjectID
WHERE p.StartDate > '08/13/2002' AND p.EndDate IS NULL
ORDER BY e.EmployeeID
