USE [master]
GO
/****** Object:  Database [LS_Security]    Script Date: 10/26/2016 9:06:58 AM ******/
CREATE DATABASE [LS_Security]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LS_Security', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\LS_Security.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LS_Security_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\LS_Security_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LS_Security] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LS_Security].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LS_Security] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LS_Security] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LS_Security] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LS_Security] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LS_Security] SET ARITHABORT OFF 
GO
ALTER DATABASE [LS_Security] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LS_Security] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LS_Security] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LS_Security] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LS_Security] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LS_Security] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LS_Security] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LS_Security] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LS_Security] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LS_Security] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LS_Security] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LS_Security] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LS_Security] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LS_Security] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LS_Security] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LS_Security] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LS_Security] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LS_Security] SET RECOVERY FULL 
GO
ALTER DATABASE [LS_Security] SET  MULTI_USER 
GO
ALTER DATABASE [LS_Security] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LS_Security] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LS_Security] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LS_Security] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [LS_Security] SET DELAYED_DURABILITY = DISABLED 
GO
USE [LS_Security]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NULL,
 CONSTRAINT [PK_IdentityRoleClaim<string>] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_IdentityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_IdentityUserClaim<string>] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_IdentityUserLogin<string>] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_IdentityUserRole<string>] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
 CONSTRAINT [PK_ApplicationUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [nvarchar](128) NOT NULL,
	[Secret] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ApplicationType] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[RefreshTokenLifeTime] [int] NOT NULL,
	[AllowedOrigin] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[ProfileId] [bigint] IDENTITY(1,1) NOT NULL,
	[AspNetUserId] [nvarchar](450) NULL,
	[RelationshipId] [int] NULL,
	[ProfileTypeId] [int] NOT NULL,
	[FamilyId] [bigint] NULL,
	[DOB] [datetime2](7) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[HomePhone] [nvarchar](20) NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[Gender] [int] NOT NULL,
	[FirstName] [nvarchar](25) NOT NULL,
	[LastName] [nvarchar](25) NOT NULL,
	[FamilyName] [nvarchar](25) NULL,
	[HomeAddressLine1] [nvarchar](250) NULL,
	[HomeAddressLine2] [nvarchar](250) NULL,
	[City] [nvarchar](50) NULL,
	[StateId] [int] NULL,
	[Zip] [nvarchar](10) NULL,
	[EmployeeName] [nvarchar](100) NULL,
	[Occupation] [nvarchar](100) NULL,
	[OfficeAddressLine1] [nvarchar](250) NULL,
	[OfficeAddressLine2] [nvarchar](250) NULL,
	[OfficeCity] [nvarchar](50) NULL,
	[OfficeStateId] [int] NULL,
	[OfficeZip] [nvarchar](10) NULL,
	[OfficePhone] [nvarchar](10) NULL,
	[OfficeEmail] [nvarchar](100) NULL,
	[IsCompleted] [bit] NOT NULL,
	[IsValidProfile] [bit] NOT NULL,
	[ImageId] [nvarchar](100) NULL,
	[IsSync] [bit] NULL,
	[SyncId] [bigint] NULL,
	[SyncDate] [datetime] NULL,
	[QboDisplayName] [nvarchar](100) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[ProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProfileTypeMaster]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileTypeMaster](
	[ProfileTypeMasterId] [int] NOT NULL,
	[ProfileType] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_ProfileTypeMaster] PRIMARY KEY CLUSTERED 
(
	[ProfileTypeMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [nvarchar](128) NOT NULL,
	[Subject] [nvarchar](50) NOT NULL,
	[ClientId] [nvarchar](50) NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ProtectedTicket] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RelationshipMaster]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationshipMaster](
	[RelationshipMasterId] [int] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_RelationshipMaster] PRIMARY KEY CLUSTERED 
(
	[RelationshipMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateMaster]    Script Date: 10/26/2016 9:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateMaster](
	[StateMasterId] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_StateMaster] PRIMARY KEY CLUSTERED 
(
	[StateMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF__Profile__Gender__52593CB8]  DEFAULT ((2)) FOR [Gender]
GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF__Profile__IsCompl__534D60F1]  DEFAULT ((0)) FOR [IsCompleted]
GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF__Profile__IsValid__5441852A]  DEFAULT ((1)) FOR [IsValidProfile]
GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF_Profile_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_OfficeState_Profile] FOREIGN KEY([OfficeStateId])
REFERENCES [dbo].[StateMaster] ([StateMasterId])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_OfficeState_Profile]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_AspNetUsers] FOREIGN KEY([AspNetUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_AspNetUsers]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_ProfileTypeMaster_Profile] FOREIGN KEY([ProfileTypeId])
REFERENCES [dbo].[ProfileTypeMaster] ([ProfileTypeMasterId])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_ProfileTypeMaster_Profile]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_RelationshipMaster_Profile] FOREIGN KEY([RelationshipId])
REFERENCES [dbo].[RelationshipMaster] ([RelationshipMasterId])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_RelationshipMaster_Profile]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_StateMaster_Profile] FOREIGN KEY([StateId])
REFERENCES [dbo].[StateMaster] ([StateMasterId])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_StateMaster_Profile]
GO
USE [master]
GO
ALTER DATABASE [LS_Security] SET  READ_WRITE 
GO
