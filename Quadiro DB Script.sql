USE [Quadiro]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 16-Nov-2021 13:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](200) NULL,
	[ProductQuantity] [numeric](9, 3) NULL,
	[ProductPrice] [numeric](18, 3) NULL,
	[ProductImageUrl] [nvarchar](200) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOtp]    Script Date: 16-Nov-2021 13:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOtp](
	[UserID] [uniqueidentifier] NOT NULL,
	[Otp] [nvarchar](5) NULL,
	[IsVerified] [bit] NULL,
	[isExpired]  AS (case when getutcdate()>[ExpiredDateTime] then (1) else (0) end),
	[ExpiredDateTime] [datetime] NULL,
 CONSTRAINT [PK_UserOtp] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16-Nov-2021 13:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserPassword] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'f3286c65-26bf-4d58-8887-015913f66974', N'LOréal Professionnel Serie Expert Liss Unlimited Shampoo', CAST(50.000 AS Numeric(9, 3)), CAST(190.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/51D25OAW2VL._SX522_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'ee3b36ed-a104-4720-8f59-11644250a120', N'Just Herbs Ayurvedic Lipstick Micro-Mini Trial Kit 38gm ( Pack of 16)', CAST(60.000 AS Numeric(9, 3)), CAST(250.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/61Df6Ww6DaL._SX522_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'70d02dc2-eb58-454b-9e61-1eb1e5553ca0', N'Milton Bottle', CAST(20.000 AS Numeric(9, 3)), CAST(230.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/61F8zPY0uOL._SX679_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'b2786ccf-050c-4ab3-a5f6-2e4e8b6facc2', N'IKONIC PRO TITANIUM SHINE HAIR STRAIGHTNER', CAST(45.000 AS Numeric(9, 3)), CAST(240.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/61A3nxkUUYL._SX522_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'c8f29221-0dfd-40d2-bda5-38a79f1e58a5', N'SanDisk Cruzer Blade 32GB USB Flash Drive', CAST(460.000 AS Numeric(9, 3)), CAST(170.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/61H7b1hylLL._SX679_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'83efe6af-7e58-4e96-a371-422fc556eac0', N'BIOLAGE Smoothproof Deep Smoothing', CAST(285.000 AS Numeric(9, 3)), CAST(620.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/71RZIHChBuL._SX522_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'afb55e0d-79f4-4388-bfbf-8b1c98f8ca4e', N'Schwarzkopf Professional Bc Keratin Smooth Perfect Treatment', CAST(21.000 AS Numeric(9, 3)), CAST(720.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/41brFiZ5uUS._SX522_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'9316ca7d-acdb-4c62-8e01-996e047df982', N'Laneige Lip Sleeping Mask Berry, 8g', CAST(2.000 AS Numeric(9, 3)), CAST(850.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/51qax2UT8QL._SX522_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'0bf5f837-9a41-4745-b1e0-c426b8c45895', N'VEGA X-Glam Hair Straightening Brush With Anti-Sclad Technology & Adjustable Temperature Setting (VHSB-01), Black', CAST(5.000 AS Numeric(9, 3)), CAST(900.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/610Iq4+BiYS._SX522_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'244d64d8-b992-4662-8778-e70007d4ef85', N'LOréal Professionnel Hair Spa Deep Nourishing Shampoo for Dry Hair with Water Lily, 250 ml', CAST(90.000 AS Numeric(9, 3)), CAST(1000.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/71YjfSoe9xS._SX679_.jpg')
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductQuantity], [ProductPrice], [ProductImageUrl]) VALUES (N'aaee6f5f-150c-4f21-8714-e932f0dcf98a', N'Logitech B170 Wireless Mouse', CAST(100.000 AS Numeric(9, 3)), CAST(1050.000 AS Numeric(18, 3)), N'https://m.media-amazon.com/images/I/51kdFjyPRAL._SX679_.jpg')
GO
INSERT [dbo].[UserOtp] ([UserID], [Otp], [IsVerified], [ExpiredDateTime]) VALUES (N'84e0907a-ebe9-4d00-a723-ef6c43f2ebd9', N'10444', 0, CAST(N'2021-11-17T08:17:01.957' AS DateTime))
GO
INSERT [dbo].[Users] ([UserID], [UserName], [UserPassword]) VALUES (N'84e0907a-ebe9-4d00-a723-ef6c43f2ebd9', N'Shailam', N'5Y64zPc7ku2ZAO0tQB1Qig2RtsoSMdctV+b/XmaeD1o=')
GO
/****** Object:  StoredProcedure [dbo].[spProducts_Get]    Script Date: 16-Nov-2021 13:54:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spProducts_Get]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [ProductID],
           [ProductName],
           [ProductQuantity],
           [ProductPrice],
           [ProductImageUrl]
    FROM [dbo].[Products];
END;
GO
/****** Object:  StoredProcedure [dbo].[spUserOtp_Add]    Script Date: 16-Nov-2021 13:54:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUserOtp_Add] @UserID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Otp NVARCHAR(5);
    SELECT @Otp = LEFT(ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)), 5);

    IF NOT EXISTS
    (
        SELECT [UserID]
        FROM [dbo].[UserOtp]
        WHERE [UserID] = @UserID
    )
    BEGIN
        INSERT INTO dbo.[UserOtp]
        (
            [UserID],
            [Otp],
            [IsVerified],
            [ExpiredDateTime]
        )
        SELECT @UserID,
               @Otp,
               0 AS [IsVerified],
               DATEADD(DAY, 1, GETUTCDATE()) AS [ExpiredDateTime];
    END;
    ELSE IF EXISTS
    (
        SELECT [UserID]
        FROM [dbo].[UserOtp]
        WHERE [UserID] = @UserID
              AND [IsVerified] = 1
              AND [isExpired] = 0
    )
    BEGIN
        UPDATE [dbo].[UserOtp]
        SET [Otp] = @Otp,
            [IsVerified] = 0,
            [ExpiredDateTime] = DATEADD(DAY, 1, GETUTCDATE())
        WHERE [UserID] = @UserID;
    END;
    ELSE IF EXISTS
    (
        SELECT [UserID]
        FROM [dbo].[UserOtp]
        WHERE [UserID] = @UserID
              AND [IsVerified] = 1
              AND [isExpired] = 1
    )
    BEGIN
        UPDATE [dbo].[UserOtp]
        SET [Otp] = @Otp,
            [IsVerified] = 0,
            [ExpiredDateTime] = DATEADD(DAY, 1, GETUTCDATE())
        WHERE [UserID] = @UserID;
    END;
    ELSE IF EXISTS
    (
        SELECT [UserID]
        FROM [dbo].[UserOtp]
        WHERE [UserID] = @UserID
              AND [IsVerified] = 0
              AND [isExpired] = 1
    )
    BEGIN
        UPDATE [dbo].[UserOtp]
        SET [Otp] = @Otp,
            [IsVerified] = 0,
            [ExpiredDateTime] = DATEADD(DAY, 1, GETUTCDATE())
        WHERE [UserID] = @UserID;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[spUserOtp_Verify]    Script Date: 16-Nov-2021 13:54:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUserOtp_Verify]
    @UserID UNIQUEIDENTIFIER,
    @Otp NVARCHAR(5)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @StatusCode INT;

    IF EXISTS
    (
        SELECT [UserID]
        FROM [dbo].[UserOtp]
        WHERE [UserID] = @UserID
              AND [Otp] = @Otp
    )
    BEGIN
        IF EXISTS
        (
            SELECT [UserID]
            FROM [dbo].[UserOtp]
            WHERE [UserID] = @UserID
                  AND [Otp] = @Otp
                  AND [IsVerified] = 1
                  AND [isExpired] = 0
        )
        BEGIN
            SELECT @StatusCode = 1102;
            SELECT @StatusCode AS [StatusCode];
            RETURN;
        END;
        ELSE IF EXISTS
        (
            SELECT [UserID]
            FROM [dbo].[UserOtp]
            WHERE [UserID] = @UserID
                  AND [Otp] = @Otp
                  AND [IsVerified] = 0
                  AND [isExpired] = 1
        )
        BEGIN
            SELECT @StatusCode = 1103;
            SELECT @StatusCode AS [StatusCode];
            RETURN;
        END;
        ELSE IF EXISTS
        (
            SELECT [UserID]
            FROM [dbo].[UserOtp]
            WHERE [UserID] = @UserID
                  AND [Otp] = @Otp
                  AND [IsVerified] = 0
                  AND [isExpired] = 0
        )
        BEGIN
            UPDATE [dbo].[UserOtp]
            SET [IsVerified] = 1
            WHERE [UserID] = @UserID
                  AND [Otp] = @Otp;

            SELECT @StatusCode = 1;
            SELECT @StatusCode AS [StatusCode];
            RETURN;
        END;
    END;
    ELSE
    BEGIN
        SELECT @StatusCode = 1101;
        SELECT @StatusCode AS [StatusCode];
        RETURN;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[spUsers_Authenticate]    Script Date: 16-Nov-2021 13:54:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUsers_Authenticate]
    @UserName NVARCHAR(50),
    @UserPassword NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @StatusCode INT;
    DECLARE @UserID UNIQUEIDENTIFIER;
    DECLARE @Otp NVARCHAR(5);

    IF NOT EXISTS
    (
        SELECT [UserID]
        FROM [dbo].[Users]
        WHERE [UserName] = @UserName
    )
    BEGIN
        SELECT @StatusCode = 1101;
        SELECT @StatusCode AS [StatusCode];
        RETURN;
    END;
    ELSE IF NOT EXISTS
         (
             SELECT [UserID]
             FROM [dbo].[Users]
             WHERE [UserName] = @UserName
                   AND [UserPassword] = @UserPassword
         )
    BEGIN
        SELECT @StatusCode = 1102;
        SELECT @StatusCode AS [StatusCode];
        RETURN;
    END;
    ELSE
    BEGIN
        SELECT @UserID = [UserID]
        FROM [dbo].[Users]
        WHERE [UserName] = @UserName
              AND [UserPassword] = @UserPassword;

        EXEC [dbo].[spUserOtp_Add] @UserID;

        SELECT @Otp = [Otp]
        FROM [dbo].[UserOtp]
        WHERE [UserID] = @UserID;

        SELECT @StatusCode = 1;
    END;

    SELECT @UserID AS [UserID],
           @Otp AS [Otp],
           @StatusCode AS [StatusCode];
END;
GO
