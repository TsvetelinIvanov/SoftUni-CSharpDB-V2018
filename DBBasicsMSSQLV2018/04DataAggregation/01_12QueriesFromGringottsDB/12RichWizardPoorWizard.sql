SELECT SUM(d.Diff) AS SumDifference 
FROM (SELECT DepositAmount - (SELECT DepositAmount FROM WizzardDeposits WHERE Id = Host.Id + 1) AS Diff
FROM WizzardDeposits AS Host) AS d

--In Judge must be paste only one query 

SELECT SUM(wd.Diff)
FROM (SELECT FirstName, DepositAmount, 
LEAD(FirstName) OVER(ORDER BY Id) AS Wizard,
LAG(FirstName) OVER(ORDER BY Id) AS NextWizard,
LEAD(DepositAmount) OVER(ORDER BY Id) AS Deposit,
LAG(DepositAmount) OVER(ORDER BY Id) AS NextDeposit,
DepositAmount - LEAD(DepositAmount) OVER(ORDER BY Id) AS Diff
FROM WizzardDeposits)
AS wd