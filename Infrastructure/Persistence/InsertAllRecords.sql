insert into Categories (Name)
values(N'Еда'), (N'Вкусности'), (N'Вода') 

select * from Categories

insert into Products(CategoryId, Name, Description, Cost, GeneralNote, SpecialNote)
values
(1, N'Селедка', N'Селедка соленая', 10.000, N'Акция', N'Пересоленая'),
(1, N'Тушенка', N'Тушенка говяжья', 20.000, N'Вкусная', N'Жилы'),
(2, N'Сгущенка', N'В банках', 30.000, N'С ключом', N'Вкусная'),
(3, N'Квас ', N'В бутылках', 15.000, N'Вятский', N'Теплый')

insert into Roles (Name)
values(N'SimpleUser'),(N'AdvancedUser'),(N'administrator')

select * from Roles

insert into Users (RoleId, Login, PasswordHash, Email)
values 
(1, N'Простой Пользователь1', N'Простой Пользователь', N'prostpolz1@example.com'),  
(2, N'Продвинутый Пользователь1', N'Продвинутый Пользователь', N'prodvpolz1@example.com'), 
(1, N'Простой Пользователь2', N'Простой Пользователь', N'prostpolz2@example.com'),    
(3, N'Администратор', N'Администратор', N'admin@example.com'),   
(2, N'Продвинутый Пользователь', N'Продвинутый Пользователь', N'prodvpolz2@example.com'); 
