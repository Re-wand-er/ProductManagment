-- create database ProductManagment

use ProductManagment

create table Categories
(
	Id int identity(1,1) primary key,
	Name nvarchar(50) unique not null
)

create table Products
(
	Id int identity(1,1) primary key,
	CategoryId int,
	Name nvarchar(100) not null,
	Description nvarchar(255),
	Cost decimal(10, 3) not null,
	GeneralNote nvarchar(255),
	SpecialNote nvarchar(100),
	
--	CreatedAt datetime2 not null default sysdatetime(),
--	UpdatedAt datetime2 null

	foreign key (CategoryId) References Categories(id) on delete Cascade,
	constraint CK_Product_Cost Check(Cost > 0)
)

create table Roles
(
	Id int identity(1,1) primary key,
	Name nvarchar(50) unique not null
)

create table Users
(
	Id int identity(1,1) primary key,
	RoleId int not null,
	Login nvarchar(100) not null,
	PasswordHash nvarchar(255) not null,
	Email nvarchar(100) unique not null,

--	CreatedAt datetime2 not null default sysdatetime(),
--	UpdatedAt datetime2 null

	foreign key (RoleID) References Roles(id)
)

ALTER TABLE Users
ADD IsBlocked bit NOT NULL DEFAULT(0);

Alter Table Users
add constraint Login_Unique unique(Login);
go

-- create trigger Product_Update
-- on Products
-- after update
-- as 
-- update prod
-- set prod.UpdatedAt = sysdatetime()
-- from Products as prod
-- join inserted i on prod.id = i.id
-- end;
-- go

-- create trigger User_Update
-- on Users
-- after update
-- as 
-- update u
-- set u.UpdatedAt = sysdatetime()
-- from Users as u
-- join inserted i on u.id = i.id
-- end;
-- go
