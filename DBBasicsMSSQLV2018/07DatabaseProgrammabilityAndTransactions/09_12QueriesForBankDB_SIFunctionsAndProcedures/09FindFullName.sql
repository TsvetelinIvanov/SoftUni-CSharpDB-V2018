CREATE PROCEDURE usp_GetHoldersFullName AS
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM AccountHolders

--In Judge must be paste without this below
GO

EXECUTE usp_GetHoldersFullName