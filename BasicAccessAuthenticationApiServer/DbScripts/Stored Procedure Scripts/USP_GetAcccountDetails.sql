use AccountDB

IF OBJECT_ID('DBO.USP_GetAcccountDetails','P') IS NOT NULL
BEGIN
 DROP PROCEDURE DBO.USP_GetAcccountDetails
 END
 GO
 Create Procedure USP_GetAcccountDetails
As
Begin
select ai.AccountId,
		ai.AccountHolderName,
		ai.AccountHolderAddress,
		ai.PanNumber,
		ai.AdharNumber,
		ai.AcountHolderEmailId,
		g.GenderType as Gender
from dbo.AccountInformation ai
left join 
dbo.Gender g
on g.GenderId=ai.GenderId
order by ai.AccountHolderName asc
End