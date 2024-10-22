IF OBJECT_ID('DBO.USP_IsValidUser','P') IS NOT NULL
BEGIN
DROP PROCEDURE DBO.USP_IsValidUser
END
GO
CREATE procedure USP_IsValidUser
@UserName varchar(100),
@Password varchar(100),
@IsValid bit output
AS
BEGIN
IF EXISTS (
SELECT 1
        FROM UserDetails
        WHERE UserName=@UserName 
		and 
		UserPassword=@Password
)
Begin
set @IsValid=1;
end
else
begin
  set @IsValid=1;
end

  
END