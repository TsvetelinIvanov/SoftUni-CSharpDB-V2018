SELECT e.EmployeeID, e.FirstName, 
CASE
WHEN p.StartDate >= '01/01/2005' THEN NULL
ELSE p.[Name] 
END AS ProjectName
FROM Employees AS e
JOIN EmployeesProjects AS e_p ON e_p.EmployeeID = e.EmployeeID
JOIN Projects AS p ON p.ProjectID = e_p.ProjectID
WHERE e.EmployeeID = 24