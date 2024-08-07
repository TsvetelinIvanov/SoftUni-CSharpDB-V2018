CREATE VIEW V_EmployeesHiredAfter2000 AS 
SELECT FirstName, LastName
FROM Employees
WHERE HireDate > '2000-12-31'

--In Judge must be pasted without this below

GO

SELECT * FROM V_EmployeesHiredAfter2000 
