USE [master]
GO
/****** Object:  Database [Futbot]    Script Date: 2014/1/29 23:58:56 ******/
CREATE DATABASE [Futbot]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Futbot', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER2012\MSSQL\DATA\Futbot.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Futbot_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER2012\MSSQL\DATA\Futbot_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Futbot] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Futbot].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Futbot] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Futbot] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Futbot] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Futbot] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Futbot] SET ARITHABORT OFF 
GO
ALTER DATABASE [Futbot] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Futbot] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Futbot] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Futbot] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Futbot] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Futbot] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Futbot] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Futbot] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Futbot] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Futbot] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Futbot] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Futbot] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Futbot] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Futbot] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Futbot] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Futbot] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Futbot] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Futbot] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Futbot] SET RECOVERY FULL 
GO
ALTER DATABASE [Futbot] SET  MULTI_USER 
GO
ALTER DATABASE [Futbot] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Futbot] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Futbot] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Futbot] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Futbot', N'ON'
GO
USE [Futbot]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 2014/1/29 23:58:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[SecurityQuestion] [nvarchar](100) NOT NULL,
	[Coin] [int] NULL,
	[ConnectionStatus] [bit] NULL,
	[SearchStatus] [bit] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CardItems]    Script Date: 2014/1/29 23:58:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardItems](
	[CardItemId] [int] IDENTITY(1,1) NOT NULL,
	[CardTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Image] [image] NULL,
 CONSTRAINT [PK_CardItems] PRIMARY KEY CLUSTERED 
(
	[CardItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cards]    Script Date: 2014/1/29 23:58:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cards](
	[CardId] [int] IDENTITY(1,1) NOT NULL,
	[CardTypeId] [int] NOT NULL,
	[ChemistryModId] [int] NULL,
	[PositionId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[BuyPrice] [int] NULL,
	[SellPrice] [int] NULL,
	[MaxPrice] [int] NULL,
	[BuyPercent] [int] NULL,
	[SellPercent] [int] NULL,
	[AverageValue] [int] NULL,
	[SearchStatus] [bit] NULL,
	[UpdateStatus] [bit] NULL,
	[ExcessivePercent] [int] NULL,
	[MaxPriceModifierPercent] [int] NULL,
 CONSTRAINT [PK_Cards] PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CardTypes]    Script Date: 2014/1/29 23:58:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardTypes](
	[CardTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CardTypes] PRIMARY KEY CLUSTERED 
(
	[CardTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScriptQueues]    Script Date: 2014/1/29 23:58:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScriptQueues](
	[ScriptQueueId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[ScriptName] [nvarchar](50) NOT NULL,
	[Priority] [int] NOT NULL,
	[RequestTime] [datetime] NOT NULL,
	[FinishTime] [datetime] NULL,
	[Status] [int] NOT NULL,
	[AgentId] [nvarchar](50) NULL,
 CONSTRAINT [PK_ScriptQueues] PRIMARY KEY CLUSTERED 
(
	[ScriptQueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CardItems]  WITH CHECK ADD  CONSTRAINT [FK_CardItems_CardTypes] FOREIGN KEY([CardTypeId])
REFERENCES [dbo].[CardTypes] ([CardTypeId])
GO
ALTER TABLE [dbo].[CardItems] CHECK CONSTRAINT [FK_CardItems_CardTypes]
GO
ALTER TABLE [dbo].[Cards]  WITH CHECK ADD  CONSTRAINT [FK_Cards_ChemistryMods] FOREIGN KEY([ChemistryModId])
REFERENCES [dbo].[CardItems] ([CardItemId])
GO
ALTER TABLE [dbo].[Cards] CHECK CONSTRAINT [FK_Cards_ChemistryMods]
GO
ALTER TABLE [dbo].[Cards]  WITH CHECK ADD  CONSTRAINT [FK_Cards_Positions] FOREIGN KEY([PositionId])
REFERENCES [dbo].[CardItems] ([CardItemId])
GO
ALTER TABLE [dbo].[Cards] CHECK CONSTRAINT [FK_Cards_Positions]
GO
ALTER TABLE [dbo].[ScriptQueues]  WITH CHECK ADD  CONSTRAINT [FK_ScriptQueues_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[ScriptQueues] CHECK CONSTRAINT [FK_ScriptQueues_Accounts]
GO
USE [master]
GO
ALTER DATABASE [Futbot] SET  READ_WRITE 
GO
