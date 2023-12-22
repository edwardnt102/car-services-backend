IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[ufnSplitString]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].[ufnSplitString]
GO 
CREATE FUNCTION [dbo].[ufnSplitString](
	@string NVARCHAR(MAX),
	@delimiter CHAR(1)
)
	RETURNS @output TABLE (splitdata NVARCHAR(MAX))
AS
BEGIN
	DECLARE @start INT, @end INT
	SELECT @start = 1, @end = CHARINDEX(@delimiter, @string)
	WHILE @start < LEN(@string) + 1 BEGIN
		IF @end = 0
			SET @end = LEN(@string) + 1

			INSERT INTO @output (splitdata)
			VALUES(SUBSTRING(@string, @start, @end  - @start)) 
			SET @start = @end + 1
			SET @end = CHARINDEX(@delimiter, @string, @start)
		END
		RETURN
END
GO
