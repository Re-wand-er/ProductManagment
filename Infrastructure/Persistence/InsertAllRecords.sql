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
values(N'������� ������������'),(N'����������� ������������'),(N'�������������')
