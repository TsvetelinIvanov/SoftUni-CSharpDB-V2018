SELECT [Name] AS Game, 
CASE
WHEN DATEPART(HOUR, [Start]) >= 0 AND DATEPART(HOUR, [Start]) < 12 THEN 'Morning'
WHEN DATEPART(HOUR, [Start]) >= 12 AND DATEPART(HOUR, [Start]) < 18 THEN 'Afternoon'
WHEN DATEPART(HOUR, [Start]) >= 18 AND DATEPART(HOUR, [Start]) < 24 THEN 'Evening'
END AS [Part of the Day],
CASE
WHEN Duration <= 3 THEN 'Extra Short'
WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
WHEN Duration > 6 THEN 'Long'
ELSE 'Extra Long'
END AS [Duration]
FROM Games
ORDER BY Game, [Duration], [Part of the Day]

/* ******************************************
	Problem 15.	 Show All Games with Duration and Part of the Day
*********************************************/

--SELECT Name AS [Game],
--       CASE
--           WHEN DATEPART(HOUR, Start) BETWEEN 0 AND 11
--           THEN 'Morning'
--           WHEN DATEPART(HOUR, Start) BETWEEN 12 AND 17
--           THEN 'Afternoon'
--           WHEN DATEPART(HOUR, Start) BETWEEN 18 AND 23
--           THEN 'Evening'
--           ELSE 'N\A'
--       END AS [Part of the Day],
--       CASE
--           WHEN Duration <= 3
--           THEN 'Extra Short'
--           WHEN Duration BETWEEN 4 AND 6
--           THEN 'Short'
--           WHEN Duration > 6
--           THEN 'Long'
--           WHEN Duration IS NULL
--           THEN 'Extra Long'
--           ELSE 'Error - must be unreachable case'
--       END AS [Duration]
--FROM Games
--ORDER BY Name,
--         [Duration],
--         [Part of the Day]; 

