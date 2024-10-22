--create the database with name  AccountDB
use AccountDB

Create table UserRole(
RoleId int not null primary key,
RoleType varchar(50) not null
)

Create table UserDetails(
UserId int not null primary key,
RoleId int not null foreign key(RoleId) references UserRole(RoleId), 
UserName varchar(100) not null,
UserEmailId varchar(100) not null,
UserPassword varchar(100)
)

Create Table Gender(
GenderId int not null primary key,
GenderType varchar(5) not null
)

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
