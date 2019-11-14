--User Transactions M:1 User
drop table if exists [UserInfo].[dbo].[User_Transactions];

--Transaction 1:1 Category
drop table if exists [UserInfo].[dbo].[Category];

--User Transactions M:1 Transaction
drop table if exists [UserInfo].[dbo].[Transaction];


create table [UserInfo].[dbo].[Category](
    ID int,
    Category varchar
)

CREATE TABLE [UserInfo].[dbo].[Transaction] (
    ID int identity PRIMARY KEY not null,
    Category_ID int,
    Description varchar,
    Amount decimal(20,2),
    Account_ID INT,
    IsEssential BIT,
    Date date,
    constraint FK_Category foreign key (Category_ID) references [Category](ID)
);

create table [UserInfo].[dbo].[User_Transactions](
    UID nvarchar(128),
    Transaction_ID int,
    constraint FK_Transaction_UID foreign key (UID) references [AspNetUsers](Id) on delete cascade,
    constraint FK_Transaction foreign key (Transaction_ID) references [Transaction](ID)
);
