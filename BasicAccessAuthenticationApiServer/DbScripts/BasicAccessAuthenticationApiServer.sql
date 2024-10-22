--create the database with name  AccountDB
use AccountDB

use BasicAccessAuthenticationApiServer

Create table UserRole(
RoleId int not null primary key,
RoleType varchar(50) not null
)

insert into UserRole values(1,'Admin');
insert into UserRole values(2,'User');
insert into UserRole values(3,'SuperAdmin');

Create table UserDetails(
UserId int not null primary key,
RoleId int not null foreign key(RoleId) references UserRole(RoleId), 
UserName varchar(100) not null,
UserEmailId varchar(100) not null,
UserPassword varchar(100)
)

insert into UserDetails values(101,1,'Rakesh','rakesh@gmail.com','Rakesh@123')
insert into UserDetails values(102,2,'User1','User1@gmail.com','User1@123')
insert into UserDetails values(103,3,'User2','User2@gmail.com','User2@123')

select * from UserDetails
select * from UserRole

-----------------
Create Table Gender(
GenderId int not null primary key,
GenderType varchar(5) not null
)

insert into Gender values(201,'M')
insert into Gender values(202,'F')
insert into Gender values(203,'N/A')

Create table AccountInformation
(
AccountId int not null primary key,
AccountHolderName varchar(200) not null,
AccountHolderAddress varchar(max) not null,
AcountHolderEmailId varchar(200) not null,
PanNumber varchar(30) not null,
AdharNumber varchar(30) not null,
GenderId int not null foreign key references Gender(GenderId)
)

insert into AccountInformation values(100001,'Ramesh','Bangalore','ramesh@gmail.com','qwert1233e','123-234-456-345',201)
insert into AccountInformation values(100002,'Rani','mysore','Rani@gmail.com','mwert134e','765-234-456-891',202)
insert into AccountInformation values(100003,'Usha','Davangere','usha@gmail.com','qw34wety','325-234-456-744',203)

select * from AccountInformation 


--select acc.AccountHolderName,acc.AccountId,g.GenderType from 
--AccountInformation acc
--left join 
--Gender g
--on acc.GenderId=g.GenderId

-----

create procedure USP_IsValidUser
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

DECLARE @IsValid BIT;

EXEC dbo.USP_IsValidUser @UserName = 'Rakesh', @Password = 'Rakesh@123', @IsValid = @IsValid OUTPUT;

SELECT @IsValid AS IsValidUser;

SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'



----------------------
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
