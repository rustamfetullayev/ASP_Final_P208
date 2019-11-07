create table Mark(
ID int primary key identity,
Name nvarchar(50)
)

create table Model(
ID int primary key identity,
Name nvarchar(50)
)

create table Gearbox(
ID int primary key identity,
Name nvarchar(50)
)

create table Fuel(
ID int primary key identity,
Name nvarchar(50)
)

create table City(
ID int primary key identity,
Name nvarchar(50)
)

create table CarAnnouncement(
ID int primary key identity,
[Image] nvarchar(255),
CarProductionDate datetime,
CarMotor decimal,
About nvarchar(300),
PostDate datetime,
UpdateDate datetime,
CarModelID int references Model(ID),
CarMarkID int references Mark(ID),
CarGearID int references Gearbox(ID),
CarFuelID int references Fuel(ID),
CityID int references City(ID),
IsVIP bit
)

create table News(
ID int primary key identity,
[Image] nvarchar(255),
Title nvarchar(150),
Subtitle nvarchar(300)
)

create table Color(
ID int primary key identity,
Name nvarchar(50)
)

alter table CarAnnouncement add UserID int references [User](ID)

create table [Admin](
ID int primary key identity,
Username nvarchar(60),
Password nvarchar(255)
)

create table Comment(
ID int primary key identity,
Content nvarchar(1000),
UserID int references [User](ID),
CarID int references CarAnnouncement(ID)
)