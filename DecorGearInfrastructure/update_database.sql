IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Brand] (
    [BrandID] int NOT NULL IDENTITY,
    [BrandName] nvarchar(255) NOT NULL,
    [Description] nvarchar(100) NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY ([BrandID])
);
GO

CREATE TABLE [Category] (
    [CategoryID] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(255) NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryID])
);
GO

CREATE TABLE [Role] (
    [RoleID] int NOT NULL IDENTITY,
    [RoleName] nvarchar(max) NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleID])
);
GO

CREATE TABLE [Sale] (
    [SaleID] int NOT NULL IDENTITY,
    [SaleName] nvarchar(255) NOT NULL,
    [SalePercent] int NOT NULL,
    [Status] int NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Sale] PRIMARY KEY ([SaleID])
);
GO

CREATE TABLE [VerificationCodePws] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [Expiration] datetime2 NOT NULL,
    CONSTRAINT [PK_VerificationCodePws] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [VerificationCodes] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [ExpirationTime] datetime2 NOT NULL,
    CONSTRAINT [PK_VerificationCodes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Voucher] (
    [VoucherID] int NOT NULL IDENTITY,
    [VoucherName] nvarchar(max) NOT NULL,
    [VoucherPercent] int NOT NULL,
    [expiry] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Voucher] PRIMARY KEY ([VoucherID])
);
GO

CREATE TABLE [SubCategory] (
    [SubCategoryID] int NOT NULL IDENTITY,
    [SubCategoryName] nvarchar(255) NOT NULL,
    [CategoryID] int NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_SubCategory] PRIMARY KEY ([SubCategoryID]),
    CONSTRAINT [FK_SubCategory_Category_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Category] ([CategoryID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [User] (
    [UserID] int NOT NULL IDENTITY,
    [RoleID] int NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserID]),
    CONSTRAINT [FK_User_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Role] ([RoleID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product] (
    [ProductID] int NOT NULL IDENTITY,
    [SaleID] int NULL,
    [BrandID] int NOT NULL,
    [SubCategoryID] int NOT NULL,
    [ProductName] nvarchar(255) NOT NULL,
    [Price] float NOT NULL,
    [View] int NOT NULL,
    [Quantity] int NOT NULL,
    [Weight] float NOT NULL,
    [Description] nvarchar(100) NULL,
    [AvatarProduct] nvarchar(100) NOT NULL,
    [Size] nvarchar(10) NOT NULL,
    [BatteryCapacity] int NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductID]),
    CONSTRAINT [FK_Product_Brand_BrandID] FOREIGN KEY ([BrandID]) REFERENCES [Brand] ([BrandID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Product_Sale_SaleID] FOREIGN KEY ([SaleID]) REFERENCES [Sale] ([SaleID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Product_SubCategory_SubCategoryID] FOREIGN KEY ([SubCategoryID]) REFERENCES [SubCategory] ([SubCategoryID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Cart] (
    [CartID] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [TotalQuantity] int NOT NULL,
    [TotalAmount] float NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY ([CartID]),
    CONSTRAINT [FK_Cart_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Member] (
    [MemberID] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [Points] int NOT NULL,
    [ExpiryDate] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY ([MemberID]),
    CONSTRAINT [FK_Member_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Order] (
    [OrderID] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [VoucherID] int NULL,
    [totalQuantity] int NOT NULL,
    [totalPrice] float NOT NULL,
    [Status] int NOT NULL,
    [paymentMethod] nvarchar(100) NOT NULL,
    [size] nvarchar(max) NOT NULL,
    [weight] real NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([OrderID]),
    CONSTRAINT [FK_Order_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Order_Voucher_VoucherID] FOREIGN KEY ([VoucherID]) REFERENCES [Voucher] ([VoucherID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [VoucherUser] (
    [VoucherUserId] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [VoucherID] int NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_VoucherUser] PRIMARY KEY ([VoucherUserId]),
    CONSTRAINT [FK_VoucherUser_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_VoucherUser_Voucher_VoucherID] FOREIGN KEY ([VoucherID]) REFERENCES [Voucher] ([VoucherID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Favorite] (
    [FavoriteID] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [ProductID] int NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Favorite] PRIMARY KEY ([FavoriteID]),
    CONSTRAINT [FK_Favorite_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Favorite_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE CASCADE
);
GO

CREATE TABLE [FeedBack] (
    [FeedBackID] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [ProductID] int NOT NULL,
    [Comment] nvarchar(500) NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_FeedBack] PRIMARY KEY ([FeedBackID]),
    CONSTRAINT [FK_FeedBack_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FeedBack_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [KeyboardDetail] (
    [KeyboardDetailID] int NOT NULL IDENTITY,
    [ProductID] int NOT NULL,
    [Color] nvarchar(100) NOT NULL,
    [Layout] nvarchar(100) NOT NULL,
    [Case] nvarchar(100) NOT NULL,
    [Switch] nvarchar(100) NOT NULL,
    [SwitchLife] int NULL,
    [Led] nvarchar(100) NULL,
    [KeycapMaterial] nvarchar(100) NULL,
    [SwitchMaterial] nvarchar(100) NULL,
    [SS] nvarchar(100) NULL,
    [Stabilizes] nvarchar(100) NULL,
    [PCB] nvarchar(100) NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_KeyboardDetail] PRIMARY KEY ([KeyboardDetailID]),
    CONSTRAINT [FK_KeyboardDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [MouseDetail] (
    [MouseDetailID] int NOT NULL IDENTITY,
    [ProductID] int NOT NULL,
    [Color] nvarchar(100) NOT NULL,
    [DPI] int NOT NULL,
    [Connectivity] nvarchar(100) NOT NULL,
    [Dimensions] nvarchar(100) NOT NULL,
    [Material] nvarchar(100) NOT NULL,
    [EyeReading] nvarchar(100) NULL,
    [Button] int NULL,
    [LED] nvarchar(100) NULL,
    [SS] nvarchar(100) NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_MouseDetail] PRIMARY KEY ([MouseDetailID]),
    CONSTRAINT [FK_MouseDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CartDetail] (
    [CartDetailID] int NOT NULL IDENTITY,
    [ProductID] int NOT NULL,
    [CartID] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] float NOT NULL,
    [TotalPrice] float NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_CartDetail] PRIMARY KEY ([CartDetailID]),
    CONSTRAINT [FK_CartDetail_Cart_CartID] FOREIGN KEY ([CartID]) REFERENCES [Cart] ([CartID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CartDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OrderDetail] (
    [OrderDetailId] int NOT NULL IDENTITY,
    [OrderID] int NOT NULL,
    [ProductID] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] float NOT NULL,
    CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([OrderDetailId]),
    CONSTRAINT [FK_OrderDetail_Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Order] ([OrderID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ImageList] (
    [ImageListID] int NOT NULL IDENTITY,
    [ProductID] int NULL,
    [ImagePath] nvarchar(max) NOT NULL,
    [Description] nvarchar(500) NULL,
    [KeyboardDetailID] int NULL,
    [MouseDetailID] int NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NOT NULL,
    [CreatedTime] datetimeoffset NOT NULL,
    CONSTRAINT [PK_ImageList] PRIMARY KEY ([ImageListID]),
    CONSTRAINT [FK_ImageList_KeyboardDetail_KeyboardDetailID] FOREIGN KEY ([KeyboardDetailID]) REFERENCES [KeyboardDetail] ([KeyboardDetailID]),
    CONSTRAINT [FK_ImageList_MouseDetail_MouseDetailID] FOREIGN KEY ([MouseDetailID]) REFERENCES [MouseDetail] ([MouseDetailID]),
    CONSTRAINT [FK_ImageList_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BrandID', N'BrandName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Brand]'))
    SET IDENTITY_INSERT [Brand] ON;
INSERT INTO [Brand] ([BrandID], [BrandName], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Description], [ModifiedBy], [ModifiedTime])
VALUES (1, N'Razer', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Thương hiệu gaming gear được tin dùng các proplayer', NULL, '0001-01-01T00:00:00.0000000+00:00'),
(2, N'Aula', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Một thương hiệu bàn phím đã quá quen thuộc với một số ae', NULL, '0001-01-01T00:00:00.0000000+00:00'),
(3, N'Rainy', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Thương hiệu bàn phím  với một số ae', NULL, '0001-01-01T00:00:00.0000000+00:00'),
(4, N'Logitech', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Một thương gaming gear quá quen thuộc với các proplayer', NULL, '0001-01-01T00:00:00.0000000+00:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BrandID', N'BrandName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Brand]'))
    SET IDENTITY_INSERT [Brand] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryID', N'CategoryName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Category]'))
    SET IDENTITY_INSERT [Category] ON;
INSERT INTO [Category] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime])
VALUES (1, N'Chuột', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00'),
(2, N'Bàn Phím', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryID', N'CategoryName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Category]'))
    SET IDENTITY_INSERT [Category] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role] ([RoleID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [RoleName])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Admin'),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'User');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SaleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SaleName', N'SalePercent', N'Status') AND [object_id] = OBJECT_ID(N'[Sale]'))
    SET IDENTITY_INSERT [Sale] ON;
INSERT INTO [Sale] ([SaleID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [SaleName], [SalePercent], [Status])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Giảm giá mùa hè', 100, 1),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Giảm giá cuối năm', 200, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SaleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SaleName', N'SalePercent', N'Status') AND [object_id] = OBJECT_ID(N'[Sale]'))
    SET IDENTITY_INSERT [Sale] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VoucherID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'Status', N'VoucherName', N'VoucherPercent', N'expiry') AND [object_id] = OBJECT_ID(N'[Voucher]'))
    SET IDENTITY_INSERT [Voucher] ON;
INSERT INTO [Voucher] ([VoucherID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [Status], [VoucherName], [VoucherPercent], [expiry])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 1, N'Giảm giá 30%', 30, '2024-11-05T00:00:00.0000000'),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 2, N'Giảm giá 50%', 50, '2024-11-05T00:00:00.0000000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VoucherID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'Status', N'VoucherName', N'VoucherPercent', N'expiry') AND [object_id] = OBJECT_ID(N'[Voucher]'))
    SET IDENTITY_INSERT [Voucher] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SubCategoryID', N'CategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SubCategoryName') AND [object_id] = OBJECT_ID(N'[SubCategory]'))
    SET IDENTITY_INSERT [SubCategory] ON;
INSERT INTO [SubCategory] ([SubCategoryID], [CategoryID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [SubCategoryName])
VALUES (1, 1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Chuột Razer'),
(2, 1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Chuột logitech'),
(3, 2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Bàn Phím Aula'),
(4, 2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Bàn Phím Rainy');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SubCategoryID', N'CategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SubCategoryName') AND [object_id] = OBJECT_ID(N'[SubCategory]'))
    SET IDENTITY_INSERT [SubCategory] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Email', N'ModifiedBy', N'ModifiedTime', N'Name', N'Password', N'PhoneNumber', N'RefreshToken', N'RoleID', N'Status', N'UserName') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([UserID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Email], [ModifiedBy], [ModifiedTime], [Name], [Password], [PhoneNumber], [RefreshToken], [RoleID], [Status], [UserName])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'admin@example.com', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Admin', N'6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=', N'0123456789', NULL, 1, 0, N'admin'),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'jane@example.com', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Jane Hangminton', N'e8KBt/ULqlM1FYiO/+NpJsDBO+4H3X1XYQIcbFcH5oU=', N'0987654321', NULL, 2, 0, N'user2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Email', N'ModifiedBy', N'ModifiedTime', N'Name', N'Password', N'PhoneNumber', N'RefreshToken', N'RoleID', N'Status', N'UserName') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'TotalAmount', N'TotalQuantity', N'UserID') AND [object_id] = OBJECT_ID(N'[Cart]'))
    SET IDENTITY_INSERT [Cart] ON;
INSERT INTO [Cart] ([CartID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [TotalAmount], [TotalQuantity], [UserID])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 0.0E0, 0, 1),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 0.0E0, 0, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'TotalAmount', N'TotalQuantity', N'UserID') AND [object_id] = OBJECT_ID(N'[Cart]'))
    SET IDENTITY_INSERT [Cart] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MemberID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ExpiryDate', N'ModifiedBy', N'ModifiedTime', N'Points', N'UserID') AND [object_id] = OBJECT_ID(N'[Member]'))
    SET IDENTITY_INSERT [Member] ON;
INSERT INTO [Member] ([MemberID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ExpiryDate], [ModifiedBy], [ModifiedTime], [Points], [UserID])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', '2024-11-12T00:00:00.0000000', NULL, '0001-01-01T00:00:00.0000000+00:00', 100, 1),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', '2025-10-03T00:00:00.0000000', NULL, '0001-01-01T00:00:00.0000000+00:00', 200, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MemberID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ExpiryDate', N'ModifiedBy', N'ModifiedTime', N'Points', N'UserID') AND [object_id] = OBJECT_ID(N'[Member]'))
    SET IDENTITY_INSERT [Member] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'OrderDate', N'Status', N'UserID', N'VoucherID', N'paymentMethod', N'size', N'totalPrice', N'totalQuantity', N'weight') AND [object_id] = OBJECT_ID(N'[Order]'))
    SET IDENTITY_INSERT [Order] ON;
INSERT INTO [Order] ([OrderID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [OrderDate], [Status], [UserID], [VoucherID], [paymentMethod], [size], [totalPrice], [totalQuantity], [weight])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', '2024-09-09T00:00:00.0000000', 6, 1, 1, N'Credit Card', N'L', 0.0E0, 5, CAST(1.5 AS real)),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', '2024-09-09T00:00:00.0000000', 6, 2, NULL, N'Cash', N'LF', 0.0E0, 3, CAST(2 AS real));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'OrderDate', N'Status', N'UserID', N'VoucherID', N'paymentMethod', N'size', N'totalPrice', N'totalQuantity', N'weight') AND [object_id] = OBJECT_ID(N'[Order]'))
    SET IDENTITY_INSERT [Order] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'AvatarProduct', N'BatteryCapacity', N'BrandID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime', N'Price', N'ProductName', N'Quantity', N'SaleID', N'Size', N'SubCategoryID', N'View', N'Weight') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] ([ProductID], [AvatarProduct], [BatteryCapacity], [BrandID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Description], [ModifiedBy], [ModifiedTime], [Price], [ProductName], [Quantity], [SaleID], [Size], [SubCategoryID], [View], [Weight])
VALUES (1, N'/media/product/250-6041-1.jpg', NULL, 1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'chiếc chuột siêu bổ rẻ ', NULL, '0001-01-01T00:00:00.0000000+00:00', 405.80000000000001E0, N'Chuột gaming Razer death adder v3', 100, 1, N'M', 1, 1000, 500.0E0),
(2, N'/media/product/250-58-700c523eec2d560efd44f277bf6559ac.jpg', NULL, 1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Chiếc chuột được nhiều tuyển thủ chuyên nghiệp tin dùng', NULL, '0001-01-01T00:00:00.0000000+00:00', 2000000.0E0, N'Chuột gaming Razor mini pro 1', 100, NULL, N'M', 1, 1000, 350.0E0),
(3, N'/media/product/250-6152-untitled-28_upscayl_2x_realesrgan-x4plus.png', NULL, 2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Một chiếc bàn phím cơ mỳ ăn liền với 3mode hotswap tầm giá 1 củ mà bạn không nên bỏ qua', NULL, '0001-01-01T00:00:00.0000000+00:00', 1000000.0E0, N'Bàn phím cơ AulaF75', 100, NULL, N'M', 3, 8000, 400.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'AvatarProduct', N'BatteryCapacity', N'BrandID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime', N'Price', N'ProductName', N'Quantity', N'SaleID', N'Size', N'SubCategoryID', N'View', N'Weight') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartDetailID', N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'Quantity', N'TotalPrice', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[CartDetail]'))
    SET IDENTITY_INSERT [CartDetail] ON;
INSERT INTO [CartDetail] ([CartDetailID], [CartID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [ProductID], [Quantity], [TotalPrice], [UnitPrice])
VALUES (9, 1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 2, 0.0E0, 50.0E0),
(10, 2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 3, 0.0E0, 40.0E0),
(11, 2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 1, 0.0E0, 75.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartDetailID', N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'Quantity', N'TotalPrice', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[CartDetail]'))
    SET IDENTITY_INSERT [CartDetail] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FavoriteID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[Favorite]'))
    SET IDENTITY_INSERT [Favorite] ON;
INSERT INTO [Favorite] ([FavoriteID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [ProductID], [UserID])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 1),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 2),
(3, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FavoriteID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[Favorite]'))
    SET IDENTITY_INSERT [Favorite] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FeedBackID', N'Comment', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[FeedBack]'))
    SET IDENTITY_INSERT [FeedBack] ON;
INSERT INTO [FeedBack] ([FeedBackID], [Comment], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [ProductID], [UserID])
VALUES (1, N'Sản phẩm rất tốt!', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 1),
(2, N'Chất lượng bình thường.', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 1),
(3, N'Giao hàng nhanh, sản phẩm đẹp.', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, '0001-01-01T00:00:00.0000000+00:00', 3, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FeedBackID', N'Comment', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[FeedBack]'))
    SET IDENTITY_INSERT [FeedBack] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageListID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ImagePath', N'KeyboardDetailID', N'ModifiedBy', N'ModifiedTime', N'MouseDetailID', N'ProductID') AND [object_id] = OBJECT_ID(N'[ImageList]'))
    SET IDENTITY_INSERT [ImageList] ON;
INSERT INTO [ImageList] ([ImageListID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Description], [ImagePath], [KeyboardDetailID], [ModifiedBy], [ModifiedTime], [MouseDetailID], [ProductID])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Hình ảnh của sản phẩm aulaf75', N'/images/aulaf75_img2.jpg', NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, 2),
(2, NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Hình ảnh của sản phẩm razer deadth addzer v3', N'/images/rzdav3_img2.jpg', NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', NULL, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageListID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ImagePath', N'KeyboardDetailID', N'ModifiedBy', N'ModifiedTime', N'MouseDetailID', N'ProductID') AND [object_id] = OBJECT_ID(N'[ImageList]'))
    SET IDENTITY_INSERT [ImageList] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KeyboardDetailID', N'Case', N'Color', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'KeycapMaterial', N'Layout', N'Led', N'ModifiedBy', N'ModifiedTime', N'PCB', N'ProductID', N'SS', N'Stabilizes', N'Switch', N'SwitchLife', N'SwitchMaterial') AND [object_id] = OBJECT_ID(N'[KeyboardDetail]'))
    SET IDENTITY_INSERT [KeyboardDetail] ON;
INSERT INTO [KeyboardDetail] ([KeyboardDetailID], [Case], [Color], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [KeycapMaterial], [Layout], [Led], [ModifiedBy], [ModifiedTime], [PCB], [ProductID], [SS], [Stabilizes], [Switch], [SwitchLife], [SwitchMaterial])
VALUES (1, N'Nhôm', N'Red', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'PBT', N'80%', N'RGB', NULL, '0001-01-01T00:00:00.0000000+00:00', N'PCB Hot-swap', 3, N'QMK', N'Stabilizer', N'Cherry MX Red', 50000000, N'Kim loại'),
(2, N'Nhựa', N'Black', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'ABS', N'75%', N'Đơn sắc', NULL, '0001-01-01T00:00:00.0000000+00:00', N'PCB tiêu chuẩn', 3, N'VIA', N'Không', N'Gateron Brown', 60000000, N'Nhựa');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KeyboardDetailID', N'Case', N'Color', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'KeycapMaterial', N'Layout', N'Led', N'ModifiedBy', N'ModifiedTime', N'PCB', N'ProductID', N'SS', N'Stabilizes', N'Switch', N'SwitchLife', N'SwitchMaterial') AND [object_id] = OBJECT_ID(N'[KeyboardDetail]'))
    SET IDENTITY_INSERT [KeyboardDetail] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MouseDetailID', N'Button', N'Color', N'Connectivity', N'CreatedBy', N'CreatedTime', N'DPI', N'Deleted', N'DeletedBy', N'DeletedTime', N'Dimensions', N'EyeReading', N'LED', N'Material', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'SS') AND [object_id] = OBJECT_ID(N'[MouseDetail]'))
    SET IDENTITY_INSERT [MouseDetail] ON;
INSERT INTO [MouseDetail] ([MouseDetailID], [Button], [Color], [Connectivity], [CreatedBy], [CreatedTime], [DPI], [Deleted], [DeletedBy], [DeletedTime], [Dimensions], [EyeReading], [LED], [Material], [ModifiedBy], [ModifiedTime], [ProductID], [SS])
VALUES (1, 6, N'Đen', N'USB', NULL, '0001-01-01T00:00:00.0000000+00:00', 16000, CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'120mm x 60mm x 40mm', N'1000Hz', N'RGB', N'Nhựa', NULL, '0001-01-01T00:00:00.0000000+00:00', 1, N'Razer Synapse'),
(2, 5, N'Trắng', N'Bluetooth', NULL, '0001-01-01T00:00:00.0000000+00:00', 12000, CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'115mm x 58mm x 38mm', N'500Hz', N'Đơn sắc', N'Kim loại', NULL, '0001-01-01T00:00:00.0000000+00:00', 2, N'Logitech G HUB');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MouseDetailID', N'Button', N'Color', N'Connectivity', N'CreatedBy', N'CreatedTime', N'DPI', N'Deleted', N'DeletedBy', N'DeletedTime', N'Dimensions', N'EyeReading', N'LED', N'Material', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'SS') AND [object_id] = OBJECT_ID(N'[MouseDetail]'))
    SET IDENTITY_INSERT [MouseDetail] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderDetailId', N'OrderID', N'ProductID', N'Quantity', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[OrderDetail]'))
    SET IDENTITY_INSERT [OrderDetail] ON;
INSERT INTO [OrderDetail] ([OrderDetailId], [OrderID], [ProductID], [Quantity], [UnitPrice])
VALUES (1, 1, 3, 10, 10000000.0E0),
(2, 1, 3, 1, 1000000.0E0),
(3, 2, 1, 1000, 1000000000.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderDetailId', N'OrderID', N'ProductID', N'Quantity', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[OrderDetail]'))
    SET IDENTITY_INSERT [OrderDetail] OFF;
GO

CREATE UNIQUE INDEX [IX_Cart_UserID] ON [Cart] ([UserID]);
GO

CREATE INDEX [IX_CartDetail_CartID] ON [CartDetail] ([CartID]);
GO

CREATE INDEX [IX_CartDetail_ProductID] ON [CartDetail] ([ProductID]);
GO

CREATE INDEX [IX_Favorite_ProductID] ON [Favorite] ([ProductID]);
GO

CREATE INDEX [IX_Favorite_UserID] ON [Favorite] ([UserID]);
GO

CREATE INDEX [IX_FeedBack_ProductID] ON [FeedBack] ([ProductID]);
GO

CREATE INDEX [IX_FeedBack_UserID] ON [FeedBack] ([UserID]);
GO

CREATE INDEX [IX_ImageList_KeyboardDetailID] ON [ImageList] ([KeyboardDetailID]);
GO

CREATE INDEX [IX_ImageList_MouseDetailID] ON [ImageList] ([MouseDetailID]);
GO

CREATE INDEX [IX_ImageList_ProductID] ON [ImageList] ([ProductID]);
GO

CREATE INDEX [IX_KeyboardDetail_ProductID] ON [KeyboardDetail] ([ProductID]);
GO

CREATE UNIQUE INDEX [IX_Member_UserID] ON [Member] ([UserID]);
GO

CREATE INDEX [IX_MouseDetail_ProductID] ON [MouseDetail] ([ProductID]);
GO

CREATE INDEX [IX_Order_UserID] ON [Order] ([UserID]);
GO

CREATE INDEX [IX_Order_VoucherID] ON [Order] ([VoucherID]);
GO

CREATE INDEX [IX_OrderDetail_OrderID] ON [OrderDetail] ([OrderID]);
GO

CREATE INDEX [IX_OrderDetail_ProductID] ON [OrderDetail] ([ProductID]);
GO

CREATE INDEX [IX_Product_BrandID] ON [Product] ([BrandID]);
GO

CREATE INDEX [IX_Product_SaleID] ON [Product] ([SaleID]);
GO

CREATE INDEX [IX_Product_SubCategoryID] ON [Product] ([SubCategoryID]);
GO

CREATE INDEX [IX_SubCategory_CategoryID] ON [SubCategory] ([CategoryID]);
GO

CREATE INDEX [IX_User_RoleID] ON [User] ([RoleID]);
GO

CREATE INDEX [IX_VoucherUser_UserID] ON [VoucherUser] ([UserID]);
GO

CREATE INDEX [IX_VoucherUser_VoucherID] ON [VoucherUser] ([VoucherID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241210085946_updateImage', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ImageList] DROP CONSTRAINT [FK_ImageList_KeyboardDetail_KeyboardDetailID];
GO

ALTER TABLE [ImageList] DROP CONSTRAINT [FK_ImageList_MouseDetail_MouseDetailID];
GO

DROP INDEX [IX_ImageList_KeyboardDetailID] ON [ImageList];
GO

DROP INDEX [IX_ImageList_MouseDetailID] ON [ImageList];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ImageList]') AND [c].[name] = N'KeyboardDetailID');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ImageList] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ImageList] DROP COLUMN [KeyboardDetailID];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ImageList]') AND [c].[name] = N'MouseDetailID');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ImageList] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ImageList] DROP COLUMN [MouseDetailID];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241210091047_updateImage2', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'Size');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Product] ALTER COLUMN [Size] nvarchar(max) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'Description');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Product] ALTER COLUMN [Description] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'AvatarProduct');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Product] ALTER COLUMN [AvatarProduct] nvarchar(max) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'paymentMethod');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Order] ALTER COLUMN [paymentMethod] nvarchar(max) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MouseDetail]') AND [c].[name] = N'SS');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [MouseDetail] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [MouseDetail] ALTER COLUMN [SS] nvarchar(max) NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MouseDetail]') AND [c].[name] = N'Material');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [MouseDetail] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [MouseDetail] ALTER COLUMN [Material] nvarchar(max) NOT NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MouseDetail]') AND [c].[name] = N'LED');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [MouseDetail] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [MouseDetail] ALTER COLUMN [LED] nvarchar(max) NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MouseDetail]') AND [c].[name] = N'EyeReading');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [MouseDetail] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [MouseDetail] ALTER COLUMN [EyeReading] nvarchar(max) NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MouseDetail]') AND [c].[name] = N'Dimensions');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [MouseDetail] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [MouseDetail] ALTER COLUMN [Dimensions] nvarchar(max) NOT NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MouseDetail]') AND [c].[name] = N'Connectivity');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [MouseDetail] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [MouseDetail] ALTER COLUMN [Connectivity] nvarchar(max) NOT NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'SwitchMaterial');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [SwitchMaterial] nvarchar(max) NULL;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'Switch');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [Switch] nvarchar(max) NOT NULL;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'Stabilizes');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [Stabilizes] nvarchar(max) NULL;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'SS');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [SS] nvarchar(max) NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'PCB');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [PCB] nvarchar(max) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'Led');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [Led] nvarchar(max) NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'Layout');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [Layout] nvarchar(max) NOT NULL;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'KeycapMaterial');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [KeycapMaterial] nvarchar(max) NULL;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'Color');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [Color] nvarchar(max) NOT NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KeyboardDetail]') AND [c].[name] = N'Case');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [KeyboardDetail] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [KeyboardDetail] ALTER COLUMN [Case] nvarchar(max) NOT NULL;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Brand]') AND [c].[name] = N'Description');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Brand] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Brand] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Brand]') AND [c].[name] = N'BrandName');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Brand] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Brand] ALTER COLUMN [BrandName] nvarchar(max) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241218032950_update-database', N'8.0.8');
GO

COMMIT;
GO

