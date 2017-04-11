
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/11/2017 16:29:30
-- Generated from EDMX file: D:\repository\net_proj\ProjectPurple\DataAccessLayer\HotelDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HotelDataDevelop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstLine] nvarchar(max)  NOT NULL,
    [APT] nvarchar(max)  NOT NULL,
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
    [User_Id] int  NOT NULL,
    [BillingInfo_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [ReservationId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'BillingInfoes'
CREATE TABLE [dbo].[BillingInfoes] (
    [Id] uniqueidentifier  NOT NULL,
    [PhoneNumbers_Id] uniqueidentifier  NOT NULL,
    [Emails_Id] int  NOT NULL,
    [Addresses_Id] int  NOT NULL,
    [Customer_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'PhoneNumbers'
CREATE TABLE [dbo].[PhoneNumbers] (
    [Id] uniqueidentifier  NOT NULL
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

-- Creating table 'Users_Staff'
CREATE TABLE [dbo].[Users_Staff] (
    [Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
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

-- Creating primary key on [Id] in table 'Users_Staff'
ALTER TABLE [dbo].[Users_Staff]
ADD CONSTRAINT [PK_Users_Staff]
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

-- Creating foreign key on [PhoneNumbers_Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [FK_BillingInfoPhoneNumber]
    FOREIGN KEY ([PhoneNumbers_Id])
    REFERENCES [dbo].[PhoneNumbers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInfoPhoneNumber'
CREATE INDEX [IX_FK_BillingInfoPhoneNumber]
ON [dbo].[BillingInfoes]
    ([PhoneNumbers_Id]);
GO

-- Creating foreign key on [Emails_Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [FK_BillingInfoEmail]
    FOREIGN KEY ([Emails_Id])
    REFERENCES [dbo].[Emails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInfoEmail'
CREATE INDEX [IX_FK_BillingInfoEmail]
ON [dbo].[BillingInfoes]
    ([Emails_Id]);
GO

-- Creating foreign key on [Addresses_Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [FK_BillingInfoAddress]
    FOREIGN KEY ([Addresses_Id])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInfoAddress'
CREATE INDEX [IX_FK_BillingInfoAddress]
ON [dbo].[BillingInfoes]
    ([Addresses_Id]);
GO

-- Creating foreign key on [Customer_Id] in table 'BillingInfoes'
ALTER TABLE [dbo].[BillingInfoes]
ADD CONSTRAINT [FK_CustomerBillingInfo]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerBillingInfo'
CREATE INDEX [IX_FK_CustomerBillingInfo]
ON [dbo].[BillingInfoes]
    ([Customer_Id]);
GO

-- Creating foreign key on [ReservationId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_ReservationCustomer]
    FOREIGN KEY ([ReservationId])
    REFERENCES [dbo].[Reservations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationCustomer'
CREATE INDEX [IX_FK_ReservationCustomer]
ON [dbo].[Customers]
    ([ReservationId]);
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

-- Creating foreign key on [Id] in table 'Users_Staff'
ALTER TABLE [dbo].[Users_Staff]
ADD CONSTRAINT [FK_Staff_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------