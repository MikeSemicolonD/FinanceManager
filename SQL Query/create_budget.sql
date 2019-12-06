--Budget 1:1 Frequency
drop table if exists [UserInfo].[dbo].[Frequency]

--User 1:m budget
drop table if exists [userInfo].[dbo].[User_Budget];

--User 1:1 Budget
drop table if exists [UserInfo].[dbo].[Budget];

create table [UserInfo].[dbo].[Budget](
    ID int identity primary key not null,
    Category_ID int,
    Description varchar(128),
    Account_ID INT not null,
    Amount decimal (16,2),
    Frequency_ID int,
);

create table [UserInfo].[dbo].[User_Budget](
    UID nvarchar(128),
    Budget_ID int,
    constraint FK_Budget_UID foreign key (UID) references [AspNetUsers](Id) on delete cascade,
    constraint FK_Budget foreign key (Budget_ID) references [Budget](ID) on delete cascade
);


create table [UserInfo].[dbo].[Frequency](
    ID int identity primary key not null,
    Frequency varchar(64) not null
);

--define frequency
insert into [UserInfo].[dbo].[Frequency] (Frequency)
values ('Per Year'), ('Per Month'), ('Per Week'), ('Per Day');