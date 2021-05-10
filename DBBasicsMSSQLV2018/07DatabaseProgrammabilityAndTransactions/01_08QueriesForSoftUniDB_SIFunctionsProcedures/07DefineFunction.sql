CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX))
RETURNS BIT AS
BEGIN
  DECLARE @result BIT = 0
  DECLARE @isCounted BIT = 0
  DECLARE @wordIndex INT = 1
  DECLARE @setOfLettersIndex INT = 1
  DECLARE @counter INT = 0
  WHILE(@wordIndex <= LEN(@word))
   BEGIN
    WHILE(@setOfLettersIndex <= LEN(@setOfLetters))
     BEGIN
     IF (SUBSTRING(@word, @wordIndex, 1) = SUBSTRING(@setOfLetters, @setOfLettersIndex, 1)
	 AND @isCounted = 0)
     BEGIN
      SET @counter += 1
	  SET @isCounted = 1	  	      
     END	  
     SET @setOfLettersIndex += 1
    END
	SET @isCounted = 0     
    SET @wordIndex += 1
    SET @setOfLettersIndex = 1
   END  
   IF (@counter = LEN(@word))
   SET @result = 1
  RETURN @result
 END

--Only one query must be paste in Judge
GO

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX))
RETURNS BIT AS
BEGIN  
  DECLARE @index INT = 1  
  WHILE(@index <= LEN(@word))
   BEGIN
    DECLARE @letter CHAR(1) = SUBSTRING(@word, @index, 1)
    IF (CHARINDEX(@letter, @setOfLetters) <= 0)
     BEGIN
      RETURN 0	  	      
     END 
	 SET @index += 1
   END    
  RETURN 1
 END

 --In Judge must be paste without this below
 GO
 DROP FUNCTION dbo.ufn_IsWordComprised

 GO
 SELECT dbo.ufn_IsWordComprised('gug', 'Guy') AS Result
