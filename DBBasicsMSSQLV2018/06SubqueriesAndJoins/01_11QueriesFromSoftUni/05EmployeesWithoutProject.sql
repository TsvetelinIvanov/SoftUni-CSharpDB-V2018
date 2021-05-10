SELECT TOP(3) e.EmployeeID, e.FirstName
FROM Employees AS e
LEFT JOIN EmployeesProjects AS e_p ON e_p.EmployeeID = e.EmployeeID
WHERE e_p.ProjectID IS NULL
ORDER BY e.EmployeeID