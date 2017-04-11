
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/11/2017 17:57:08
-- Generated from EDMX file: D:\repository\net_proj\ProjectPurple\DataAccessLayer\HotelDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
DROP DATABASE [HotelDataDevelop];
CREATE DATABASE [HotelDataDevelop];
USE [HotelDataDevelop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ReservationUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_ReservationUser];
GO
IF OBJECT_ID(N'[dbo].[FK_BillingInfoPhoneNumber]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillingInfoes] DROP CONSTRAINT [FK_BillingInfoPhoneNumber];
GO
IF OBJECT_ID(N'[dbo].[FK_BillingInfoEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillingInfoes] DROP CONSTRAINT [FK_BillingInfoEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_BillingInfoAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillingInfoes] DROP CONSTRAINT [FK_BillingInfoAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationBillingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_ReservationBillingInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationDailyPrice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DailyPrices] DROP CONSTRAINT [FK_ReservationDailyPrice];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationGuest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Guests] DROP CONSTRAINT [FK_ReservationGuest];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[Emails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Emails];
GO
IF OBJECT_ID(N'[dbo].[Reservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservations];
GO
IF OBJECT_ID(N'[dbo].[Guests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Guests];
GO
IF OBJECT_ID(N'[dbo].[BillingInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillingInfoes];
GO
IF OBJECT_ID(N'[dbo].[PhoneNumbers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhoneNumbers];
GO
IF OBJECT_ID(N'[dbo].[DailyPrices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DailyPrices];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [HashedPassword] nvarchar(max)  NOT NULL,
    [isStaff] bit  NOT NULL,
    [isRegistered] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstLine] nvarchar(max)  NOT NULL,
    [SecondLine] nvarchar(max)  NULL,
    [State] int  NOT NULL,
    [ZipCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Emails'
CREATE TABLE [dbo].[Emails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [Id] uniqueidentifier  NOT NULL,
    [startDate] datetime  NOT NULL,
    [endDate] datetime  NOT NULL,
    [User_Id] uniqueidentifier  NOT NULL,
    [BillingInfo_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Guests'
CREATE TABLE [dbo].[Guests] (
    [Id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Reservation_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'BillingInfoes'
CREATE TABLE [dbo].[BillingInfoes] (
    [Id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber_Id] uniqueidentifier  NOT NULL,
    [Email_Id] int  NOT NULL,
    [Addresse_Id] int  NOT NULL
);
GO

-- Creating table 'PhoneNumbers'
CREATE TABLE [dbo].[PhoneNumbers] (
    [Id] uniqueidentifier  NOT NULL,
    [Number] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DailyPrices'
CREATE TABLE [dbo].[DailyPrices] (
    [Id] uniqueidentifier  NOT NULL,
    [BillingPrice] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Reservation_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [PK_Emails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Guests'
ALTER TABLE [dbo].[Guests]
ADD CONSTRAINT [PK_Guests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [PK_BillingInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhoneNumbers'
ALTER TABLE [dbo].[PhoneNumbers]
ADD CONSTRAINT [PK_PhoneNumbers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DailyPrices'
ALTER TABLE [dbo].[DailyPrices]
ADD CONSTRAINT [PK_DailyPrices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ReservationUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationUser'
CREATE INDEX [IX_FK_ReservationUser]
ON [dbo].[Reservations]
    ([User_Id]);
GO

-- Creating foreign key on [PhoneNumber_Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [FK_BillingInfoPhoneNumber]
    FOREIGN KEY ([PhoneNumber_Id])
    REFERENCES [dbo].[PhoneNumbers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInfoPhoneNumber'
CREATE INDEX [IX_FK_BillingInfoPhoneNumber]
ON [dbo].[BillingInfoes]
    ([PhoneNumber_Id]);
GO

-- Creating foreign key on [Email_Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [FK_BillingInfoEmail]
    FOREIGN KEY ([Email_Id])
    REFERENCES [dbo].[Emails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInfoEmail'
CREATE INDEX [IX_FK_BillingInfoEmail]
ON [dbo].[BillingInfoes]
    ([Email_Id]);
GO

-- Creating foreign key on [Addresse_Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [FK_BillingInfoAddress]
    FOREIGN KEY ([Addresse_Id])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInfoAddress'
CREATE INDEX [IX_FK_BillingInfoAddress]
ON [dbo].[BillingInfoes]
    ([Addresse_Id]);
GO

-- Creating foreign key on [BillingInfo_Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ReservationBillingInfo]
    FOREIGN KEY ([BillingInfo_Id])
    REFERENCES [dbo].[BillingInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationBillingInfo'
CREATE INDEX [IX_FK_ReservationBillingInfo]
ON [dbo].[Reservations]
    ([BillingInfo_Id]);
GO

-- Creating foreign key on [Reservation_Id] in table 'DailyPrices'
ALTER TABLE [dbo].[DailyPrices]
ADD CONSTRAINT [FK_ReservationDailyPrice]
    FOREIGN KEY ([Reservation_Id])
    REFERENCES [dbo].[Reservations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationDailyPrice'
CREATE INDEX [IX_FK_ReservationDailyPrice]
ON [dbo].[DailyPrices]
    ([Reservation_Id]);
GO

-- Creating foreign key on [Reservation_Id] in table 'Guests'
ALTER TABLE [dbo].[Guests]
ADD CONSTRAINT [FK_ReservationGuest]
    FOREIGN KEY ([Reservation_Id])
    REFERENCES [dbo].[Reservations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationGuest'
CREATE INDEX [IX_FK_ReservationGuest]
ON [dbo].[Guests]
    ([Reservation_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------