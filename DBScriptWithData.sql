USE [PetHotelDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GalleryImages]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalleryImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[HotelID] [int] NOT NULL,
 CONSTRAINT [PK_GalleryImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Hotels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageTags]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageTags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[GalleryImageId] [int] NULL,
 CONSTRAINT [PK_ImageTags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pets]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Age] [int] NOT NULL,
	[Alergies] [nvarchar](max) NOT NULL,
	[PetTypeID] [int] NOT NULL,
	[UserID] [nvarchar](450) NOT NULL,
	[HotelID] [int] NOT NULL,
 CONSTRAINT [PK_Pets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PetTypes]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PetTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CostPerDay] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PetTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 17.12.2022 г. 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PetID] [int] NOT NULL,
	[PetName] [nvarchar](max) NOT NULL,
	[AdmissionDate] [datetime2](7) NOT NULL,
	[DepartureDate] [datetime2](7) NOT NULL,
	[HotelID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221217093655_initial', N'6.0.9')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6d5800ce-d123-4fc8-83d9-d6b3ac1f591e', N'Administrator', N'ADMINISTRATOR', N'5d882835-b584-42a8-abe6-0abda94ccf7f')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'dea12856-c321-4129-b3f3-b893d8395082', N'User', N'USER', N'ad180d22-5499-4801-bb4c-4790e0cb2c83')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'df93e740-41a6-474c-ba20-5505df682ae4', N'Employee', N'EMPLOYEE', N'd832a928-b694-4f62-b282-f263eaffbaec')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6d5800ce-d726-4fc8-83d9-d6b3ac1f591e', N'6d5800ce-d123-4fc8-83d9-d6b3ac1f591e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd98aa74c-51b3-4480-a52d-da662c39713d', N'dea12856-c321-4129-b3f3-b893d8395082')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dea12856-c198-4129-b3f3-b893d8395082', N'dea12856-c321-4129-b3f3-b893d8395082')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a841631e-8b01-4884-a23e-c0232e24aa29', N'df93e740-41a6-474c-ba20-5505df682ae4')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6d5800ce-d726-4fc8-83d9-d6b3ac1f591e', N'Admin', N'User', 1, N'Admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAELHyHgzFk2/2yR7woApaKIiik2dW+rEiafASK8q5/HEAvgCUbHGhYL27hpRJYzQ9JA==', N'aef34771-01bd-4618-9c84-46546ec066eb', N'96b4e877-633a-4558-b6a4-03e0029e7b2e', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a841631e-8b01-4884-a23e-c0232e24aa29', N'Mitko', N'Mitkov', 1, N'Boss', N'BOSS', N'stenly.nedelkov@gmail.com', N'STENLY.NEDELKOV@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEOHSkxtLeWc+PXXXGP7aoy76i12HQVXBwDxUZBWXj9Itgis91zcK2QMiKhachrXpow==', N'cef3a5a0-2def-4556-996d-7e468c3f06b1', N'2f1a0d85-28cf-492a-949b-0ccfbfd14f94', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd98aa74c-51b3-4480-a52d-da662c39713d', N'stan', N'ned', 1, N'testuser', N'TESTUSER', N'stenly.nedelkov@gmail.com', N'STENLY.NEDELKOV@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEGYjaOGPpKRCEYwcy1IT0pVZJKVQwFAM4+5M2lYv5jwtP2hAG33KrVY6zLiCZJYBBg==', N'RHUVS6QYP5JOIF3YEYMZUCCW7GPLDGPY', N'a0aba705-112b-42a6-91aa-b3ca3f3c76f6', N'0889676775', 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'dea12856-c198-4129-b3f3-b893d8395082', N'Stanislav', N'Nedelkov', 1, N'Stan', N'STAN', N'stenly.nedelkov@gmail.com', N'STENLY.NEDELKOV@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEB7onMwAjfiJ1CkidrGz/hnDJ6LNfKIpMuTqGTiJIEDXrHBMFL66OJ9al7Bfvp86vw==', N'd722bb40-e134-4531-ba3c-20c891908f4a', N'c586fabb-1960-4d1a-a53c-4d1b5d73e0e7', NULL, 0, 0, NULL, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Hotels] ON 

INSERT [dbo].[Hotels] ([Id], [Name]) VALUES (1, N'Cats Dogs and Crocs Hotel')
SET IDENTITY_INSERT [dbo].[Hotels] OFF
GO
SET IDENTITY_INSERT [dbo].[Pets] ON 

INSERT [dbo].[Pets] ([Id], [Name], [Age], [Alergies], [PetTypeID], [UserID], [HotelID]) VALUES (1, N'Daemon', 5, N'Humans', 3, N'dea12856-c198-4129-b3f3-b893d8395082', 1)
INSERT [dbo].[Pets] ([Id], [Name], [Age], [Alergies], [PetTypeID], [UserID], [HotelID]) VALUES (2, N'Jack', 1, N'none', 2, N'd98aa74c-51b3-4480-a52d-da662c39713d', 1)
SET IDENTITY_INSERT [dbo].[Pets] OFF
GO
SET IDENTITY_INSERT [dbo].[PetTypes] ON 

INSERT [dbo].[PetTypes] ([Id], [Name], [CostPerDay]) VALUES (1, N'Cat', CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[PetTypes] ([Id], [Name], [CostPerDay]) VALUES (2, N'Dog', CAST(10.00 AS Decimal(18, 2)))
INSERT [dbo].[PetTypes] ([Id], [Name], [CostPerDay]) VALUES (3, N'Crocodile', CAST(40.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[PetTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([Id], [PetID], [PetName], [AdmissionDate], [DepartureDate], [HotelID], [Status]) VALUES (1, 1, N'Daemon', CAST(N'2022-12-25T00:00:00.0000000' AS DateTime2), CAST(N'2022-12-26T00:00:00.0000000' AS DateTime2), 1, N'Expecting')
INSERT [dbo].[Schedules] ([Id], [PetID], [PetName], [AdmissionDate], [DepartureDate], [HotelID], [Status]) VALUES (2, 1, N'Daemon', CAST(N'2022-12-17T00:00:00.0000000' AS DateTime2), CAST(N'2022-12-18T00:00:00.0000000' AS DateTime2), 1, N'InProgress')
INSERT [dbo].[Schedules] ([Id], [PetID], [PetName], [AdmissionDate], [DepartureDate], [HotelID], [Status]) VALUES (3, 2, N'Jack', CAST(N'2022-12-17T00:00:00.0000000' AS DateTime2), CAST(N'2022-12-22T00:00:00.0000000' AS DateTime2), 1, N'InProgress')
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[GalleryImages]  WITH CHECK ADD  CONSTRAINT [FK_GalleryImages_Hotels_HotelID] FOREIGN KEY([HotelID])
REFERENCES [dbo].[Hotels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GalleryImages] CHECK CONSTRAINT [FK_GalleryImages_Hotels_HotelID]
GO
ALTER TABLE [dbo].[ImageTags]  WITH CHECK ADD  CONSTRAINT [FK_ImageTags_GalleryImages_GalleryImageId] FOREIGN KEY([GalleryImageId])
REFERENCES [dbo].[GalleryImages] ([Id])
GO
ALTER TABLE [dbo].[ImageTags] CHECK CONSTRAINT [FK_ImageTags_GalleryImages_GalleryImageId]
GO
ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_AspNetUsers_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_AspNetUsers_UserID]
GO
ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_Hotels_HotelID] FOREIGN KEY([HotelID])
REFERENCES [dbo].[Hotels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_Hotels_HotelID]
GO
ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_PetTypes_PetTypeID] FOREIGN KEY([PetTypeID])
REFERENCES [dbo].[PetTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_PetTypes_PetTypeID]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Hotels_HotelID] FOREIGN KEY([HotelID])
REFERENCES [dbo].[Hotels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Hotels_HotelID]
GO
