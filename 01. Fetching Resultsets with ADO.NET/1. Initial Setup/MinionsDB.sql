use MinionsDB

create table Minions
(
	Id int primary key identity,
	Name varchar(20) not null,
	Age int not null,
	TownId varchar(30)
)

create table Towns
(
	Id int identity primary key,
	TownName varchar(30),
	CountryName varchar(35)
)

create table Villains
(
	Id int identity primary key,
	Name varchar(20),
	EvilnessFactor varchar(10) check(EvilnessFactor in ('good', 'bad', 'evil', 'super evil'))
)

create table VillainsMinions
(
	VillainId int,
	MinionId int,
	constraint PK_VillainsMinions primary key(VillainId, MinionId)
)

insert into Towns (TownName, CountryName)
values ('Sofia', 'Bulgaria'), ('Plovdiv', 'Bulgaria'), ('Berlin', 'Germany'), ('Paris', 'France'), ('Liverpool', 'England')

insert into Minions(Name, Age, TownId)
values ('Kev', 11, 1), ('Bob', 12, 2), ('Stew', 5, 3), ('Malk', 3, 5), ('Tosh', 1, 4)

insert into Villains(Name, EvilnessFactor)
values ('Gosho', 'bad'), ('Tosho', 'good'), ('Misho', 'evil'), ('Gogo', 'super evil'), ('Tiho', 'bad')

insert into VillainsMinions(VillainId, MinionId)
values (1,1), (1,2), (1,3), (1,4), (1,5), (2,2), (3,1), (3,2), (3,3), (3,4), (3,5), (4,4), (5,5)