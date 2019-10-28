--User 1:M User Transactions
drop table if exists [UserInfo].[dbo].[User_Transactions];
--User Transactions M:1 Transaction
drop table if exists [UserInfo].[dbo].[Transaction];

CREATE TABLE [UserInfo].[dbo].[Transaction] (
    ID int identity PRIMARY KEY not null,
    Description varchar(50),
    IsEssential BIT,
    Category varchar(50),
    Amount decimal,
    Account_ID INT not null,
    AccountType varchar(50)
);

create table [UserInfo].[dbo].[User_Transactions](
    UID nvarchar(128),
    Transaction_ID int,
    constraint FK_Transaction_UID foreign key (UID) references [AspNetUsers](Id) on delete cascade,
    constraint FK_Transaction foreign key (Transaction_ID) references [Transaction](ID)
);