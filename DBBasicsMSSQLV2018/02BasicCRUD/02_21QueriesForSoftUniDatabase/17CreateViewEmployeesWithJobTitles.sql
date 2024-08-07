CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name],
JobTitle
FROM Employees

--It must be pasted in Judge without the text below
GO

SELECT * FROM V_EmployeeNameJobTitle
