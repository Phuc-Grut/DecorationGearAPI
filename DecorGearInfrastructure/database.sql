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
    [BrandName] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserID]),
    CONSTRAINT [FK_User_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Role] ([RoleID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product] (
    [ProductID] int NOT NULL IDENTITY,
    [SaleID] int NULL,
    [BrandID] int NOT NULL,
    [ProductName] nvarchar(255) NOT NULL,
    [Price] float NOT NULL,
    [View] int NOT NULL,
    [Quantity] int NOT NULL,
    [Weight] float NOT NULL,
    [Description] nvarchar(max) NULL,
    [AvatarProduct] nvarchar(max) NOT NULL,
    [Size] nvarchar(max) NOT NULL,
    [BatteryCapacity] int NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductID]),
    CONSTRAINT [FK_Product_Brand_BrandID] FOREIGN KEY ([BrandID]) REFERENCES [Brand] ([BrandID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Product_Sale_SaleID] FOREIGN KEY ([SaleID]) REFERENCES [Sale] ([SaleID]) ON DELETE NO ACTION
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [paymentMethod] nvarchar(max) NOT NULL,
    [size] nvarchar(max) NOT NULL,
    [weight] real NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
    CONSTRAINT [PK_FeedBack] PRIMARY KEY ([FeedBackID]),
    CONSTRAINT [FK_FeedBack_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_FeedBack_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ImageList] (
    [ImageListID] int NOT NULL IDENTITY,
    [ProductID] int NULL,
    [ImagePath] nvarchar(max) NOT NULL,
    [Description] nvarchar(500) NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
    CONSTRAINT [PK_ImageList] PRIMARY KEY ([ImageListID]),
    CONSTRAINT [FK_ImageList_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [KeyboardDetail] (
    [KeyboardDetailID] int NOT NULL IDENTITY,
    [ProductID] int NOT NULL,
    [Color] nvarchar(max) NOT NULL,
    [Layout] nvarchar(max) NOT NULL,
    [Case] nvarchar(max) NOT NULL,
    [Switch] nvarchar(max) NOT NULL,
    [SwitchLife] int NULL,
    [Led] nvarchar(max) NULL,
    [KeycapMaterial] nvarchar(max) NULL,
    [SwitchMaterial] nvarchar(max) NULL,
    [SS] nvarchar(max) NULL,
    [Stabilizes] nvarchar(max) NULL,
    [PCB] nvarchar(max) NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
    CONSTRAINT [PK_KeyboardDetail] PRIMARY KEY ([KeyboardDetailID]),
    CONSTRAINT [FK_KeyboardDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [MouseDetail] (
    [MouseDetailID] int NOT NULL IDENTITY,
    [ProductID] int NOT NULL,
    [Color] nvarchar(100) NOT NULL,
    [DPI] int NOT NULL,
    [Connectivity] nvarchar(max) NOT NULL,
    [Dimensions] nvarchar(max) NOT NULL,
    [Material] nvarchar(max) NOT NULL,
    [EyeReading] nvarchar(max) NULL,
    [Button] int NULL,
    [LED] nvarchar(max) NULL,
    [SS] nvarchar(max) NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
    CONSTRAINT [PK_MouseDetail] PRIMARY KEY ([MouseDetailID]),
    CONSTRAINT [FK_MouseDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ProductSubCategory] (
    [ProductID] int NOT NULL,
    [SubCategoryID] int NOT NULL,
    [CreatedBy] uniqueidentifier NULL,
    [ModifiedTime] datetimeoffset NOT NULL,
    [ModifiedBy] uniqueidentifier NULL,
    [Deleted] bit NOT NULL,
    [DeletedBy] uniqueidentifier NULL,
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
    CONSTRAINT [PK_ProductSubCategory] PRIMARY KEY ([ProductID], [SubCategoryID]),
    CONSTRAINT [FK_ProductSubCategory_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductSubCategory_SubCategory_SubCategoryID] FOREIGN KEY ([SubCategoryID]) REFERENCES [SubCategory] ([SubCategoryID]) ON DELETE CASCADE
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
    [DeletedTime] datetimeoffset NULL,
    [CreatedTime] datetimeoffset NULL,
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BrandID', N'BrandName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Brand]'))
    SET IDENTITY_INSERT [Brand] ON;
INSERT INTO [Brand] ([BrandID], [BrandName], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Description], [ModifiedBy], [ModifiedTime])
VALUES (1, N'Razer', NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Thương hiệu gaming gear được tin dùng các proplayer', NULL, '0001-01-01T00:00:00.0000000+00:00'),
(2, N'Aula', NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Một thương hiệu bàn phím đã quá quen thuộc với một số ae', NULL, '0001-01-01T00:00:00.0000000+00:00'),
(3, N'Rainy', NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Thương hiệu bàn phím  với một số ae', NULL, '0001-01-01T00:00:00.0000000+00:00'),
(4, N'Logitech', NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Một thương gaming gear quá quen thuộc với các proplayer', NULL, '0001-01-01T00:00:00.0000000+00:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BrandID', N'BrandName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Brand]'))
    SET IDENTITY_INSERT [Brand] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryID', N'CategoryName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Category]'))
    SET IDENTITY_INSERT [Category] ON;
INSERT INTO [Category] ([CategoryID], [CategoryName], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime])
VALUES (1, N'Chuột', NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00'),
(2, N'Bàn Phím', NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryID', N'CategoryName', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[Category]'))
    SET IDENTITY_INSERT [Category] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role] ([RoleID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [RoleName])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Admin'),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'User');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SaleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SaleName', N'SalePercent', N'Status') AND [object_id] = OBJECT_ID(N'[Sale]'))
    SET IDENTITY_INSERT [Sale] ON;
INSERT INTO [Sale] ([SaleID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [SaleName], [SalePercent], [Status])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Giảm giá mùa hè', 100, 1),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Giảm giá cuối năm', 200, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SaleID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SaleName', N'SalePercent', N'Status') AND [object_id] = OBJECT_ID(N'[Sale]'))
    SET IDENTITY_INSERT [Sale] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VoucherID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'Status', N'VoucherName', N'VoucherPercent', N'expiry') AND [object_id] = OBJECT_ID(N'[Voucher]'))
    SET IDENTITY_INSERT [Voucher] ON;
INSERT INTO [Voucher] ([VoucherID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [Status], [VoucherName], [VoucherPercent], [expiry])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 1, N'Giảm giá 30%', 30, '2024-11-05T00:00:00.0000000'),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 2, N'Giảm giá 50%', 50, '2024-11-05T00:00:00.0000000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VoucherID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'Status', N'VoucherName', N'VoucherPercent', N'expiry') AND [object_id] = OBJECT_ID(N'[Voucher]'))
    SET IDENTITY_INSERT [Voucher] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'AvatarProduct', N'BatteryCapacity', N'BrandID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime', N'Price', N'ProductName', N'Quantity', N'SaleID', N'Size', N'View', N'Weight') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] ([ProductID], [AvatarProduct], [BatteryCapacity], [BrandID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Description], [ModifiedBy], [ModifiedTime], [Price], [ProductName], [Quantity], [SaleID], [Size], [View], [Weight])
VALUES (1, N'/media/product/250-6041-1.jpg', NULL, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, N'chiếc chuột siêu bổ rẻ ', NULL, '0001-01-01T00:00:00.0000000+00:00', 405.80000000000001E0, N'Chuột gaming Razer death adder v3', 100, 1, N'M', 1000, 500.0E0),
(2, N'/media/product/250-58-700c523eec2d560efd44f277bf6559ac.jpg', NULL, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Chiếc chuột được nhiều tuyển thủ chuyên nghiệp tin dùng', NULL, '0001-01-01T00:00:00.0000000+00:00', 2000000.0E0, N'Chuột gaming không dây Razer mini pro 1', 100, NULL, N'M', 1000, 350.0E0),
(3, N'/media/product/250-6152-untitled-28_upscayl_2x_realesrgan-x4plus.png', NULL, 2, NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Một chiếc bàn phím cơ mỳ ăn liền với 3mode hotswap tầm giá 1 củ mà bạn không nên bỏ qua', NULL, '0001-01-01T00:00:00.0000000+00:00', 1000000.0E0, N'Bàn phím cơ AulaF75', 100, NULL, N'M', 8000, 400.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'AvatarProduct', N'BatteryCapacity', N'BrandID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ModifiedBy', N'ModifiedTime', N'Price', N'ProductName', N'Quantity', N'SaleID', N'Size', N'View', N'Weight') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SubCategoryID', N'CategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SubCategoryName') AND [object_id] = OBJECT_ID(N'[SubCategory]'))
    SET IDENTITY_INSERT [SubCategory] ON;
INSERT INTO [SubCategory] ([SubCategoryID], [CategoryID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [SubCategoryName])
VALUES (1, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Chuột Razer'),
(2, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Chuột logitech'),
(3, 2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Bàn Phím Aula'),
(4, 2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Bàn Phím Rainy'),
(5, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Chuột không dây'),
(6, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', N'Chuột gaming');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SubCategoryID', N'CategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'SubCategoryName') AND [object_id] = OBJECT_ID(N'[SubCategory]'))
    SET IDENTITY_INSERT [SubCategory] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Email', N'ModifiedBy', N'ModifiedTime', N'Name', N'Password', N'PhoneNumber', N'RefreshToken', N'RoleID', N'Status', N'UserName') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([UserID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Email], [ModifiedBy], [ModifiedTime], [Name], [Password], [PhoneNumber], [RefreshToken], [RoleID], [Status], [UserName])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, N'admin@example.com', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Admin', N'6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=', N'0123456789', NULL, 1, 0, N'admin'),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, N'jane@example.com', NULL, '0001-01-01T00:00:00.0000000+00:00', N'Jane Hangminton', N'e8KBt/ULqlM1FYiO/+NpJsDBO+4H3X1XYQIcbFcH5oU=', N'0987654321', NULL, 2, 0, N'user2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Email', N'ModifiedBy', N'ModifiedTime', N'Name', N'Password', N'PhoneNumber', N'RefreshToken', N'RoleID', N'Status', N'UserName') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'TotalAmount', N'TotalQuantity', N'UserID') AND [object_id] = OBJECT_ID(N'[Cart]'))
    SET IDENTITY_INSERT [Cart] ON;
INSERT INTO [Cart] ([CartID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [TotalAmount], [TotalQuantity], [UserID])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 0.0E0, 0, 1),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 0.0E0, 0, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'TotalAmount', N'TotalQuantity', N'UserID') AND [object_id] = OBJECT_ID(N'[Cart]'))
    SET IDENTITY_INSERT [Cart] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FavoriteID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[Favorite]'))
    SET IDENTITY_INSERT [Favorite] ON;
INSERT INTO [Favorite] ([FavoriteID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [ProductID], [UserID])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 1),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 2),
(3, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FavoriteID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[Favorite]'))
    SET IDENTITY_INSERT [Favorite] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FeedBackID', N'Comment', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[FeedBack]'))
    SET IDENTITY_INSERT [FeedBack] ON;
INSERT INTO [FeedBack] ([FeedBackID], [Comment], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [ProductID], [UserID])
VALUES (1, N'Sản phẩm rất tốt!', NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 1),
(2, N'Chất lượng bình thường.', NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 1),
(3, N'Giao hàng nhanh, sản phẩm đẹp.', NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 3, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FeedBackID', N'Comment', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'UserID') AND [object_id] = OBJECT_ID(N'[FeedBack]'))
    SET IDENTITY_INSERT [FeedBack] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageListID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ImagePath', N'ModifiedBy', N'ModifiedTime', N'ProductID') AND [object_id] = OBJECT_ID(N'[ImageList]'))
    SET IDENTITY_INSERT [ImageList] ON;
INSERT INTO [ImageList] ([ImageListID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [Description], [ImagePath], [ModifiedBy], [ModifiedTime], [ProductID])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Hình ảnh của sản phẩm aulaf75', N'/images/aulaf75_img2.jpg', NULL, '0001-01-01T00:00:00.0000000+00:00', 2),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, N'Hình ảnh của sản phẩm razer deadth addzer v3', N'/images/rzdav3_img2.jpg', NULL, '0001-01-01T00:00:00.0000000+00:00', 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ImageListID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'Description', N'ImagePath', N'ModifiedBy', N'ModifiedTime', N'ProductID') AND [object_id] = OBJECT_ID(N'[ImageList]'))
    SET IDENTITY_INSERT [ImageList] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KeyboardDetailID', N'Case', N'Color', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'KeycapMaterial', N'Layout', N'Led', N'ModifiedBy', N'ModifiedTime', N'PCB', N'ProductID', N'SS', N'Stabilizes', N'Switch', N'SwitchLife', N'SwitchMaterial') AND [object_id] = OBJECT_ID(N'[KeyboardDetail]'))
    SET IDENTITY_INSERT [KeyboardDetail] ON;
INSERT INTO [KeyboardDetail] ([KeyboardDetailID], [Case], [Color], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [KeycapMaterial], [Layout], [Led], [ModifiedBy], [ModifiedTime], [PCB], [ProductID], [SS], [Stabilizes], [Switch], [SwitchLife], [SwitchMaterial])
VALUES (1, N'Nhôm', N'Red', NULL, NULL, CAST(0 AS bit), NULL, NULL, N'PBT', N'80%', N'RGB', NULL, '0001-01-01T00:00:00.0000000+00:00', N'PCB Hot-swap', 3, N'QMK', N'Stabilizer', N'Cherry MX Red', 50000000, N'Kim loại'),
(2, N'Nhựa', N'Black', NULL, NULL, CAST(0 AS bit), NULL, NULL, N'ABS', N'75%', N'Đơn sắc', NULL, '0001-01-01T00:00:00.0000000+00:00', N'PCB tiêu chuẩn', 3, N'VIA', N'Không', N'Gateron Brown', 60000000, N'Nhựa');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KeyboardDetailID', N'Case', N'Color', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'KeycapMaterial', N'Layout', N'Led', N'ModifiedBy', N'ModifiedTime', N'PCB', N'ProductID', N'SS', N'Stabilizes', N'Switch', N'SwitchLife', N'SwitchMaterial') AND [object_id] = OBJECT_ID(N'[KeyboardDetail]'))
    SET IDENTITY_INSERT [KeyboardDetail] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MemberID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ExpiryDate', N'ModifiedBy', N'ModifiedTime', N'Points', N'UserID') AND [object_id] = OBJECT_ID(N'[Member]'))
    SET IDENTITY_INSERT [Member] ON;
INSERT INTO [Member] ([MemberID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ExpiryDate], [ModifiedBy], [ModifiedTime], [Points], [UserID])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, '2024-11-12T00:00:00.0000000', NULL, '0001-01-01T00:00:00.0000000+00:00', 100, 1),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, '2025-10-03T00:00:00.0000000', NULL, '0001-01-01T00:00:00.0000000+00:00', 200, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MemberID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ExpiryDate', N'ModifiedBy', N'ModifiedTime', N'Points', N'UserID') AND [object_id] = OBJECT_ID(N'[Member]'))
    SET IDENTITY_INSERT [Member] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MouseDetailID', N'Button', N'Color', N'Connectivity', N'CreatedBy', N'CreatedTime', N'DPI', N'Deleted', N'DeletedBy', N'DeletedTime', N'Dimensions', N'EyeReading', N'LED', N'Material', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'SS') AND [object_id] = OBJECT_ID(N'[MouseDetail]'))
    SET IDENTITY_INSERT [MouseDetail] ON;
INSERT INTO [MouseDetail] ([MouseDetailID], [Button], [Color], [Connectivity], [CreatedBy], [CreatedTime], [DPI], [Deleted], [DeletedBy], [DeletedTime], [Dimensions], [EyeReading], [LED], [Material], [ModifiedBy], [ModifiedTime], [ProductID], [SS])
VALUES (1, 6, N'Đen', N'USB', NULL, NULL, 16000, CAST(0 AS bit), NULL, NULL, N'120mm x 60mm x 40mm', N'1000Hz', N'RGB', N'Nhựa', NULL, '0001-01-01T00:00:00.0000000+00:00', 1, N'Razer Synapse'),
(2, 5, N'Trắng', N'Bluetooth', NULL, NULL, 12000, CAST(0 AS bit), NULL, NULL, N'115mm x 58mm x 38mm', N'500Hz', N'Đơn sắc', N'Kim loại', NULL, '0001-01-01T00:00:00.0000000+00:00', 2, N'Logitech G HUB');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MouseDetailID', N'Button', N'Color', N'Connectivity', N'CreatedBy', N'CreatedTime', N'DPI', N'Deleted', N'DeletedBy', N'DeletedTime', N'Dimensions', N'EyeReading', N'LED', N'Material', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'SS') AND [object_id] = OBJECT_ID(N'[MouseDetail]'))
    SET IDENTITY_INSERT [MouseDetail] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'OrderDate', N'Status', N'UserID', N'VoucherID', N'paymentMethod', N'size', N'totalPrice', N'totalQuantity', N'weight') AND [object_id] = OBJECT_ID(N'[Order]'))
    SET IDENTITY_INSERT [Order] ON;
INSERT INTO [Order] ([OrderID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [OrderDate], [Status], [UserID], [VoucherID], [paymentMethod], [size], [totalPrice], [totalQuantity], [weight])
VALUES (1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', '2024-09-09T00:00:00.0000000', 6, 1, 1, N'Credit Card', N'L', 0.0E0, 5, CAST(1.5 AS real)),
(2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', '2024-09-09T00:00:00.0000000', 6, 2, NULL, N'Cash', N'LF', 0.0E0, 3, CAST(2 AS real));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OrderID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'OrderDate', N'Status', N'UserID', N'VoucherID', N'paymentMethod', N'size', N'totalPrice', N'totalQuantity', N'weight') AND [object_id] = OBJECT_ID(N'[Order]'))
    SET IDENTITY_INSERT [Order] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'SubCategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[ProductSubCategory]'))
    SET IDENTITY_INSERT [ProductSubCategory] ON;
INSERT INTO [ProductSubCategory] ([ProductID], [SubCategoryID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime])
VALUES (1, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00'),
(1, 6, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00'),
(2, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00'),
(2, 5, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00'),
(2, 6, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'SubCategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[ProductSubCategory]'))
    SET IDENTITY_INSERT [ProductSubCategory] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartDetailID', N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'Quantity', N'TotalPrice', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[CartDetail]'))
    SET IDENTITY_INSERT [CartDetail] ON;
INSERT INTO [CartDetail] ([CartDetailID], [CartID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime], [ProductID], [Quantity], [TotalPrice], [UnitPrice])
VALUES (9, 1, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 1, 2, 0.0E0, 50.0E0),
(10, 2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 3, 0.0E0, 40.0E0),
(11, 2, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00', 2, 1, 0.0E0, 75.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartDetailID', N'CartID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime', N'ProductID', N'Quantity', N'TotalPrice', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[CartDetail]'))
    SET IDENTITY_INSERT [CartDetail] OFF;
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

CREATE INDEX [IX_ProductSubCategory_SubCategoryID] ON [ProductSubCategory] ([SubCategoryID]);
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
VALUES (N'20241218072424_update-database', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'SubCategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[ProductSubCategory]'))
    SET IDENTITY_INSERT [ProductSubCategory] ON;
INSERT INTO [ProductSubCategory] ([ProductID], [SubCategoryID], [CreatedBy], [CreatedTime], [Deleted], [DeletedBy], [DeletedTime], [ModifiedBy], [ModifiedTime])
VALUES (3, 3, NULL, NULL, CAST(0 AS bit), NULL, NULL, NULL, '0001-01-01T00:00:00.0000000+00:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductID', N'SubCategoryID', N'CreatedBy', N'CreatedTime', N'Deleted', N'DeletedBy', N'DeletedTime', N'ModifiedBy', N'ModifiedTime') AND [object_id] = OBJECT_ID(N'[ProductSubCategory]'))
    SET IDENTITY_INSERT [ProductSubCategory] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241218073404_updata', N'8.0.8');
GO

COMMIT;
GO

