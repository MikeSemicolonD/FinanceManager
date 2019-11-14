--User 1:M User Accounts
drop table if exists [UserInfo].[dbo].[User_Accounts];

--User Accounts M:1 Account
drop table if exists [UserInfo].[dbo].[Account];

create table [UserInfo].[dbo].[Account](
    ID int identity primary key not null,
    Type varchar(128)
);


create table [UserInfo].[dbo].[User_Accounts](
    UID nvarchar(128),
    Account_ID int,
    constraint FK_Account_UID foreign key (UID) references [AspNetUsers](Id),
    constraint FK_Account foreign key (Account_ID) references [Account](ID)
);