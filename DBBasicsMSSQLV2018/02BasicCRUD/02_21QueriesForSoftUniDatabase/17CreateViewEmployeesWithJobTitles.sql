CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name],
JobTitle
FROM Employees

--It must be paste in Judge without the text down 
GO

SELECT * FROM V_EmployeeNameJobTitle