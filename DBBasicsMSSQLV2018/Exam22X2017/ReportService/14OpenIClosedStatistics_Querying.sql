SELECT e.FirstName + ' ' + e.LastName AS [Name],        
CONCAT(ISNULL(rC.ReportSum, 0), '/', ISNULL(rO.ReportSum, 0))
       AS [Closed Open Reports]
FROM Employees AS e
JOIN (SELECT EmployeeId, COUNT(*) AS ReportSum
      FROM Reports WHERE YEAR(OpenDate) = 2016
	  GROUP BY EmployeeId) AS rO ON rO.EmployeeId = e.Id
LEFT JOIN (SELECT EmployeeId, COUNT(*) AS ReportSum
           FROM Reports WHERE YEAR(CloseDate) = 2016
		   GROUP BY EmployeeId) AS rC  ON rC.EmployeeId = e.Id
ORDER BY [Name], e.Id

--Only one query must be paste in Judge

SELECT e.Firstname + ' ' + e.Lastname AS [Name], 
	   ISNULL(CONVERT(varchar, CC.ReportSum), '0') + '/' +        
       ISNULL(CONVERT(varchar, OC.ReportSum), '0') AS [Closed Open Reports]
FROM Employees AS e
JOIN (SELECT EmployeeId,  COUNT(*) AS ReportSum
	  FROM Reports R
	  WHERE  YEAR(OpenDate) = 2016
	  GROUP BY EmployeeId) AS OC ON OC.Employeeid = E.Id
LEFT JOIN (SELECT EmployeeId,  COUNT(*) AS ReportSum
	       FROM Reports R
	       WHERE  YEAR(CloseDate) = 2016
	       GROUP BY EmployeeId) AS CC ON CC.Employeeid = E.Id
ORDER BY [Name], e.Id