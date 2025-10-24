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

	foreign key (RoleID) References Roles(id)
)
