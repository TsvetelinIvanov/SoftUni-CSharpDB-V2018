CREATE FUNCTION ufn_CalculateFutureValue(@InitialSum DECIMAL(15, 4), @YearlyInterestRate FLOAT, 
@NumberOfYears INT) 
RETURNS DECIMAL(15, 4)
AS
BEGIN
  DECLARE @futureValue DECIMAL(15, 4)
  SET @futureValue = @InitialSum * (POWER((1 + @YearlyInterestRate), @NumberOfYears))
  RETURN @futureValue
END

--In Judge must be pasted without this below
GO

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)
