USE [master]
GO
/****** Object:  Database [AuthServer]    Script Date: 2017/02/13 08:13:23 AM ******/
CREATE DATABASE [AuthServer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AuthServer', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AuthServer.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AuthServer_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AuthServer_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AuthServer] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AuthServer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AuthServer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AuthServer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AuthServer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AuthServer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AuthServer] SET ARITHABORT OFF 
GO
ALTER DATABASE [AuthServer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AuthServer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AuthServer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AuthServer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AuthServer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AuthServer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AuthServer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AuthServer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AuthServer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AuthServer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AuthServer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AuthServer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AuthServer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AuthServer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AuthServer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AuthServer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AuthServer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AuthServer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AuthServer] SET  MULTI_USER 
GO
ALTER DATABASE [AuthServer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AuthServer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AuthServer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AuthServer] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AuthServer] SET DELAYED_DURABILITY = DISABLED 
GO
USE [AuthServer]
GO
/****** Object:  Table [dbo].[ClientRoles]    Script Date: 2017/02/13 08:13:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ClientRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 2017/02/13 08:13:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [varchar](50) NOT NULL,
	[Secret] [varchar](250) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ApplicationType] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[RefreshTokenLifeTime] [int] NOT NULL,
	[AllowedOrigin] [varchar](250) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Clients_1] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 2017/02/13 08:13:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClientId] [varchar](50) NOT NULL,
	[IssuedUTC] [datetime] NOT NULL,
	[ExpiresUTC] [datetime] NOT NULL,
	[ProtectedTicket] [varchar](max) NOT NULL,
	[TokenId] [varchar](250) NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2017/02/13 08:13:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleDescription] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserClientRoles]    Script Date: 2017/02/13 08:13:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserClientRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClientRoleId] [int] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_UserClientRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserClients]    Script Date: 2017/02/13 08:13:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserClients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClientId] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_UserClients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2017/02/13 08:13:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_Users_CreateDate]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NOT NULL,
	[ModifiedDate] [datetime] NULL CONSTRAINT [DF_Users_ModifiedDate]  DEFAULT (getdate()),
	[ModifiedBy] [varchar](50) NOT NULL,
	[LastLogin] [datetime] NULL CONSTRAINT [DF_Users_LastLogin]  DEFAULT (getdate()),
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Clients] ([ClientId], [Secret], [Name], [ApplicationType], [Active], [RefreshTokenLifeTime], [AllowedOrigin], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (N'ConsoleApp', N'W2CRH9ebeQHB/eP9UUXt2G39IW6EWOBW+awCG3g+bgU=', N'Console Application', 0, 1, 7200, N'*', N'DumisaneK', NULL, N'DumisaneK', NULL)
INSERT [dbo].[Clients] ([ClientId], [Secret], [Name], [ApplicationType], [Active], [RefreshTokenLifeTime], [AllowedOrigin], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (N'ngAuthApp', N'W2CRH9ebeQHB/eP9UUXt2G39IW6EWOBW+awCG3g+bgU=', N'AngularJS front-end Application', 0, 1, 7200, N'http://localhost:4810', N'DumisaneK', NULL, N'DumisaneK', NULL)
SET IDENTITY_INSERT [dbo].[RefreshTokens] ON 

INSERT [dbo].[RefreshTokens] ([Id], [UserId], [ClientId], [IssuedUTC], [ExpiresUTC], [ProtectedTicket], [TokenId]) VALUES (142, 7, N'ngAuthApp', CAST(N'2017-02-10 15:51:34.910' AS DateTime), CAST(N'2017-02-15 15:51:34.910' AS DateTime), N'k7FFPZODvAFYrx0i-6ylFSHGK4BofAia8tYia5M-DAmDvEQ6NhLM4Mqk_-5pmTA-WYRgaC-p6xAeTeDyiwEGMhOc0U1RBgvVhQ49ASqoL7bLqE81doN8ZU7T8_JbBqyPt1XfDmqwMu91vX9l6ySoSQHIjKxU89BWQDCBi9Qp1mrLjoGJpXMvS_bwuBa9VF80aneoH2qlwerYwTu5cqs7mW6ZDoBxPSW-P-7Oo--K_bkoUWMzMuoCX2POXszHk4i45AxhRdQslR-7MO2PvaT3NTcoXEX2ZOQkUhnDjYltv_xXVMG6BbcFa6cFTvQk_kAbRZaPAVxyZ0oLpY4heoDlc-q-A7qRxB5SXynUM9fxUxc', N'imBWe1/Gh4eFryypUzoY/0kXS0463ZgtG063j2SKpok=')
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [ClientId], [IssuedUTC], [ExpiresUTC], [ProtectedTicket], [TokenId]) VALUES (199, 1, N'ngAuthApp', CAST(N'2017-02-12 20:59:08.137' AS DateTime), CAST(N'2017-02-17 20:59:08.137' AS DateTime), N'NVOsfBs4QTnTGGD-JCJCKQYHHXgbeVPWAcqeOYOp99nbXK0MAvtXWKCpbBwMA8o4rGbbsKEulyslEPwafhgBQ4iluz-4mca5f27LB_L_8yfT6FYXafrxh4XIqCVaTvvzY24QJQ0KJjZTAYmP51BziYlGkZuelOYDafLetI5DT1mZipoxhpLQJ4nNO23elq0eaUZilumOL9y5m0Jbjl6uOKyzOgiehft-Vz2msWzbxyFqgqc89PKNtshkYunDNVSSKk7Aiu6q7uuD55Y1b2G_L8AsfZoBxY_D3a2aCwo1LLBupB5zzga0VJvsAiHPXrMgSFmOTFfSROPnkGdPOQfWyIc2C_p4TXeO4jUqZ0NB6mQ', N'IoCQeqOieGhC2yeiiaV8SBorGoi2wvDgjvBUAVT8t4c=')
SET IDENTITY_INSERT [dbo].[RefreshTokens] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [RoleDescription], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (1, N'User', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Roles] ([Id], [RoleDescription], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate], [IsActive]) VALUES (2, N'Manager', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[UserClients] ON 

INSERT [dbo].[UserClients] ([Id], [UserId], [ClientId], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (12, 1, N'ngAuthApp', NULL, NULL, NULL, NULL)
INSERT [dbo].[UserClients] ([Id], [UserId], [ClientId], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (13, 1, N'ConsoleApp', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserClients] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Name], [Surname], [CreateDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [LastLogin], [IsActive]) VALUES (1, N'DumisaneK', N'Dumisane', N'Kubayi', NULL, N'DumisaneK', NULL, N'DumisaneK', NULL, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Name], [Surname], [CreateDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [LastLogin], [IsActive]) VALUES (2, N'JohnTe', N'John', N'Tembe', NULL, N'DumisaneK', NULL, N'DumisaneK', NULL, 0)
INSERT [dbo].[Users] ([Id], [UserName], [Name], [Surname], [CreateDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [LastLogin], [IsActive]) VALUES (3, N'Monaiwa', N'Jabulane', N'Monaiwa', NULL, N'DumisaneK', NULL, N'DumisaneK', NULL, 0)
INSERT [dbo].[Users] ([Id], [UserName], [Name], [Surname], [CreateDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [LastLogin], [IsActive]) VALUES (7, N'ChulumancoM', N'Chulumanco', N'Mbangi', NULL, N'DumisaneK', NULL, N'DumisaneK', NULL, 0)
INSERT [dbo].[Users] ([Id], [UserName], [Name], [Surname], [CreateDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [LastLogin], [IsActive]) VALUES (8, N'OlwethuK', N'Olwethu', N'Kubayi', NULL, N'DumisaneK', NULL, N'DumisaneK', NULL, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[ClientRoles]  WITH CHECK ADD  CONSTRAINT [FK_ClientRoles_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[ClientRoles] CHECK CONSTRAINT [FK_ClientRoles_Clients]
GO
ALTER TABLE [dbo].[ClientRoles]  WITH CHECK ADD  CONSTRAINT [FK_ClientRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[ClientRoles] CHECK CONSTRAINT [FK_ClientRoles_Roles]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Clients]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users]
GO
ALTER TABLE [dbo].[UserClientRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserClientRoles_ClientRoles] FOREIGN KEY([ClientRoleId])
REFERENCES [dbo].[ClientRoles] ([Id])
GO
ALTER TABLE [dbo].[UserClientRoles] CHECK CONSTRAINT [FK_UserClientRoles_ClientRoles]
GO
ALTER TABLE [dbo].[UserClientRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserClientRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserClientRoles] CHECK CONSTRAINT [FK_UserClientRoles_Users]
GO
ALTER TABLE [dbo].[UserClients]  WITH CHECK ADD  CONSTRAINT [FK_UserClients_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[UserClients] CHECK CONSTRAINT [FK_UserClients_Clients]
GO
ALTER TABLE [dbo].[UserClients]  WITH CHECK ADD  CONSTRAINT [FK_UserClients_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserClients] CHECK CONSTRAINT [FK_UserClients_Users]
GO
USE [master]
GO
ALTER DATABASE [AuthServer] SET  READ_WRITE 
GO
