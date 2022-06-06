# RunningMan
DataBase
*********
use master
Go
if DB_Id('RunningMan') is not null drop database RunningMan
go
create database RunningMan
go
use RunningMan
---------------------------------
CREATE TABLE Permission
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Permission_Code VARCHAR(25) UNIQUE NOT NULL,
	Permission_Name NVARCHAR(50) NOT NULL
	

	CONSTRAINT pk_Permission PRIMARY KEY (Id)
)
CREATE TABLE PermissionDetail
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Account_Id INT NOT NULL,
	Permission_Id INT NOT NULL

	CONSTRAINT UC_PermissionDetail UNIQUE (Account_Id, Permission_Id),
	CONSTRAINT pk_PermissionDetail PRIMARY KEY (Id)
)
CREATE TABLE Roles
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Role_Code NVARCHAR(25) UNIQUE NOT NULL,
	Role_Name NVARCHAR(50) NOT NULL

	CONSTRAINT pk_Roles PRIMARY KEY (Id)
)

CREATE TABLE RolesDetail
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Account_Id INT NOT NULL,
	Roles_Id INT NOT NULL

	CONSTRAINT UC_RolesDetail UNIQUE (Account_Id, Roles_Id),
	CONSTRAINT pk_RolesDetail PRIMARY KEY (Id)
)

CREATE TABLE Account
(
	Id INT IdENTITY UNIQUE NOT NULL,
	UserName VARCHAR(50) UNIQUE NOT NULL,
	Password VARCHAR(250)  NOT NULL,
	Name NVARCHAR(100) NOT NULL,
	Email NVARCHAR(250) NOT NULL,
	Account_Status BIT NOT NULL


	CONSTRAINT pk_Account PRIMARY KEY (Id)
)

CREATE TABLE Location
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Address NVARCHAR(250) NOT NULL

	CONSTRAINT pk_Location PRIMARY KEY(Id)
)

CREATE TABLE Point
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Point INT DEFAULT 0,
	Account_Id INT,
	Team_Id INT 
	CONSTRAINT pk_Point PRIMARY KEY (Id)
)



CREATE TABLE Round
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Name NVARCHAR(250) NOT NULL,
	Location_Id INT NOT NULL,
	Account_Id INT NOT NULL,
	Bonus_Points INT NOT NULL,
	Level INT DEFAULT 1

	CONSTRAINT pk_Round PRIMARY KEY (Id)
)

CREATE TABLE RoundDetail
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Round_Id INT,
	Game_Id INT

	CONSTRAINT UC_RoundDetail UNIQUE (Round_Id, Game_Id),
	CONSTRAINT pk_RoundDetail PRIMARY KEY (Id)
)

CREATE TABLE RoundHistory
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Round_Id INT NOT NULL,
	Account_Id INT NOT NULL,
	Times INT

	CONSTRAINT UC_RoundHistory UNIQUE (Round_Id, Account_Id),
	CONSTRAINT pk_RoundHistory PRIMARY KEY (Id)
)

CREATE TABLE TeamDetail
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Team_Id INT NOT NULL,
	Account_Id INT NOT NULL,
	Team_Lead BIT Default 1 NOT NULL

	CONSTRAINT UC_TeamDetail UNIQUE (Team_Id, Account_Id),
	CONSTRAINT pk_TeamDetail PRIMARY KEY (Id)
)

CREATE TABLE Team
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Name NVARCHAR(50),
	Rank INT DEFAULT 1
	

	CONSTRAINT pk_Team PRIMARY KEY (Id)
)

CREATE TABLE Game
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Name NVARCHAR(50),
	Level INT DEFAULT 1,
	Account_Id INT ,
	Game_Type_Id INT NOT NULL,
	Game_Rules NVARCHAR(250),
	Question NVARCHAR(250),
	Answer NVARCHAR(250),
	Hint_1 NVARCHAR(250),
	Hint_2 NVARCHAR(250)

	CONSTRAINT pk_Game PRIMARY KEY (Id)

)

CREATE TABLE GameHistory
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Game_Id int NOT NULL,
	Account_Id int NOT NULL,
	Times int

	CONSTRAINT UC_GameHistory UNIQUE (Game_Id, Account_Id),
	CONSTRAINT pk_GameHistory PRIMARY KEY (Id)
)


CREATE TABLE GamePlay
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Rank INT DEFAULT 1,
	Date Date,
	Round_Id INT,
	Team_Id INT

	CONSTRAINT UC_GamePlay UNIQUE (Round_Id, Team_Id),
	CONSTRAINT pk_GamePlay PRIMARY KEY(Id)
)

CREATE TABLE GameType
(
	Id INT IdENTITY UNIQUE NOT NULL,
	Name NVARCHAR(50)

	CONSTRAINT pk_GameType PRIMARY KEY(Id)
)

CREATE TABLE UserRefreshTokens
(
	Id INT IdENTITY UNIQUE NOT NULL,
	UserName VARCHAR(50) NOT NULL,
	RefreshToken VARCHAR(500) UNIQUE NOT NULL,
	IsActive bit Default 1

	CONSTRAINT pk_UserRefreshTokens PRIMARY KEY(Id)
)

-----------Foreign Key----------------------
ALTER TABLE dbo.PermissionDetail
ADD CONSTRAINT fk_PermissionDetail_Permission FOREIGN KEY (Permission_Id) REFERENCES dbo.Permission(Id),
CONSTRAINT fk_PermissionDetail_Account FOREIGN KEY (Account_Id) REFERENCES dbo.Account(Id)

ALTER TABLE dbo.RolesDetail
ADD CONSTRAINT fk_RolesDetail_Roles FOREIGN KEY (Roles_Id) REFERENCES dbo.Roles(Id),
CONSTRAINT fk_RolesDetail_Account FOREIGN KEY (Account_Id) REFERENCES dbo.Account(Id)

ALTER TABLE dbo.Round
ADD CONSTRAINT fk_Round_Account FOREIGN KEY (Account_Id) REFERENCES dbo.Account(Id),
CONSTRAINT fk_Round_Location FOREIGN KEY (Location_Id) REFERENCES dbo.Location(Id)

ALTER TABLE dbo.RoundDetail
ADD CONSTRAINT fk_RoundDetail_Round FOREIGN KEY(Round_Id) REFERENCES dbo.Round(Id),
CONSTRAINT fk_RoundDetail_Game FOREIGN KEY(Game_Id) REFERENCES dbo.Game(Id)


ALTER TABLE dbo.TeamDetail
ADD CONSTRAINT fk_TeamDetail_Team FOREIGN KEY(Team_Id) REFERENCES dbo.Team(Id),
CONSTRAINT fk_TeamDetail_Account FOREIGN KEY(Account_Id) REFERENCES dbo.Account(Id)

ALTER TABLE dbo.Game
ADD CONSTRAINT fk_Game_Account FOREIGN KEY (Account_Id) REFERENCES dbo.Account(Id),
CONSTRAINT fk_Game_GameType FOREIGN KEY (Game_Type_Id) REFERENCES dbo.GameType(Id)

ALTER TABLE dbo.Point
ADD CONSTRAINT fk_Point_Account FOREIGN KEY (Account_Id) REFERENCES dbo.Account(Id),
CONSTRAINT fk_Point_Team FOREIGN KEY (Team_Id) REFERENCES dbo.Team(Id)

ALTER TABLE dbo.GamePlay
ADD CONSTRAINT fk_GamePlay_Round FOREIGN KEY(Round_Id) REFERENCES dbo.Round(Id),
CONSTRAINT fk_GamePlay_Team FOREIGN KEY (Team_Id) REFERENCES dbo.Team(Id)

ALTER TABLE dbo.GameHistory
ADD CONSTRAINT fk_GameHistory_Game FOREIGN KEY (Game_Id) REFERENCES dbo.Game(Id),
CONSTRAINT fk_GameHistory_Account FOREIGN KEY (Account_Id) REFERENCES dbo.Account(Id)

ALTER TABLE dbo.RoundHistory
ADD CONSTRAINT fk_RoundHistory_Account FOREIGN KEY (Account_Id) REFERENCES dbo.Account(Id)


--------Insert Data----------------------------------------------------------------------
--------Roles-----------------------------
insert Roles (Role_Code, Role_Name)
values ('ADMIN','Admin')
insert Roles (Role_Code, Role_Name)
values ('USER','User')

--------Permission------------------------
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_USER_VIEW','View User')
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_USER_CREATE','Create User')
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_USER_UPDATE','Update User')
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_USER_DELETE','Delete User')
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_TEAM_LEADER','Team Leader')
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_TEAM_MEMBER','Team Member')
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_TEAM_CREATE','Create Team')
insert Permission (Permission_Code,Permission_Name)
values ('RUNNING_MAN_TEAM_JOIN','Join Team')
-------Account---------------------------
insert Account(UserName,Password,Name,Email,Account_Status)
values ('admin','$2a$11$bg.hT4TTVApic96zipPMheQswmxBE.iTEDwnjbM0dybqLYKNMI.J2','Manager RunningMan','admin@gmail.com','true')
-------RoleDetail------------------------
insert RolesDetail(Account_Id,Roles_Id)
values (1,1)
-------PermissionDetail------------------
insert PermissionDetail(Account_Id,Permission_Id)
values(1,1)
insert PermissionDetail(Account_Id,Permission_Id)
values(1,2)
insert PermissionDetail(Account_Id,Permission_Id)
values(1,3)
insert PermissionDetail(Account_Id,Permission_Id)
values(1,4)
insert PermissionDetail(Account_Id,Permission_Id)
values(1,7)
insert PermissionDetail(Account_Id,Permission_Id)
values(1,8)

-------Game-----------------------
