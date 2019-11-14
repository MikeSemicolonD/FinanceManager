--Budget 1:1 Frequency
drop table if exists [UserInfo].[dbo].[Frequency]

--User 1:1 Budget
drop table if exists [UserInfo].[dbo].[Budget];

create table [UserInfo].[dbo].[Budget](
    UID nvarchar(128) not null,
    Account_ID INT not null,
    Amount decimal,
    Times int,
    Frequency_ID int,
    constraint FK_Budget_UID foreign key (UID) references [AspNetUsers](Id)
);

create table [UserInfo].[dbo].[Frequency](
    ID int identity primary key not null,
    Frequency varchar(64) not null
);

--define frequency
insert into [UserInfo].[dbo].[Frequency] (Frequency)
values ('Per Year'), ('Per Month'), ('Per Day'), ('Per Hour');