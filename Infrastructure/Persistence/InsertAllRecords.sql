insert into Categories (Name)
values(N'���'), (N'���������'), (N'����') 

select * from Categories

insert into Products(CategoryId, Name, Description, Cost, GeneralNote, SpecialNote)
values
(1, N'�������', N'������� �������', 10.000, N'�����', N'�����������'),
(1, N'�������', N'������� �������', 20.000, N'�������', N'����'),
(2, N'��������', N'� ������', 30.000, N'� ������', N'�������'),
(3, N'���� ', N'� ��������', 15.000, N'�������', N'������')

insert into Roles (Name)
values(N'SimpleUser'),(N'AdvancedUser'),(N'administrator')

select * from Roles

insert into Users (RoleId, Login, PasswordHash, Email)
values 
(1, N'������� ������������1', N'������� ������������', N'prostpolz1@example.com'),  
(2, N'����������� ������������1', N'����������� ������������', N'prodvpolz1@example.com'), 
(1, N'������� ������������2', N'������� ������������', N'prostpolz2@example.com'),    
(3, N'�������������', N'�������������', N'admin@example.com'),   
(2, N'����������� ������������', N'����������� ������������', N'prodvpolz2@example.com'); 
