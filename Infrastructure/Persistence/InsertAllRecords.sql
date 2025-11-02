insert into Categories (Name)
values(N'Åäà'), (N'Âêóñíîñòè'), (N'Âîäà') 

select * from Categories

insert into Products(CategoryId, Name, Description, Cost, GeneralNote, SpecialNote)
values
(1, N'Ñåëåäêà', N'Ñåëåäêà ñîëåíàÿ', 10.000, N'Àêöèÿ', N'Ïåðåñîëåíàÿ'),
(1, N'Òóøåíêà', N'Òóøåíêà ãîâÿæüÿ', 20.000, N'Âêóñíàÿ', N'Æèëû'),
(2, N'Ñãóùåíêà', N'Â áàíêàõ', 30.000, N'Ñ êëþ÷îì', N'Âêóñíàÿ'),
(3, N'Êâàñ ', N'Â áóòûëêàõ', 15.000, N'Âÿòñêèé', N'Òåïëûé')

insert into Roles (Name)
values(N'SimpleUser'),(N'AdvancedUser'),(N'administrator')

select * from Roles

insert into Users (RoleId, Login, PasswordHash, Email)
values     
(3, N'Admin', N'AQAAAAIAAYagAAAAEOGgQcJz5m1fovlq5QUWpg87sAHDbX3iRYSNw41TQ1jOwyd5OgZKFFaflR/CTzxqNA==', N'admin@example.com');  
-- PasswordHash: 123456789

