CREATE PROC usp_GetTownsStartingWith(@StartingPattern VARCHAR(50)) AS
SELECT [Name] AS Town
FROM Towns
WHERE [Name] LIKE @StartingPattern + '%'

--In Judge must be pasted without this below
GO

EXEC usp_GetTownsStartingWith 'B'
