CREATE FUNCTION [dbo].[FnGetLawyerId](
     @UserName varchar(25)
)
RETURNS varchar(10)
AS 
BEGIN

	DECLARE @LawyerId nvarchar(10);
	BEGIN 
		SELECT @LawyerId  = [LawyerId] FROM LNK_USER_LAWYER WHERE [UserName] = @UserName   
	END 

	RETURN @LawyerId;
    
END;


GO


