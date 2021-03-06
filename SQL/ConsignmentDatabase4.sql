USE [master]
GO
/****** Object:  Database [GMC Consignment Information]    Script Date: 11/28/2017 8:40:57 PM ******/
CREATE DATABASE [GMC Consignment Information]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GMC Consignment Information', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\GMC Consignment Information.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GMC Consignment Information_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\GMC Consignment Information_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [GMC Consignment Information] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GMC Consignment Information].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GMC Consignment Information] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET ARITHABORT OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GMC Consignment Information] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GMC Consignment Information] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GMC Consignment Information] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GMC Consignment Information] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GMC Consignment Information] SET  MULTI_USER 
GO
ALTER DATABASE [GMC Consignment Information] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GMC Consignment Information] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GMC Consignment Information] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GMC Consignment Information] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GMC Consignment Information] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GMC Consignment Information] SET QUERY_STORE = OFF
GO
USE [GMC Consignment Information]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [GMC Consignment Information]
GO
/****** Object:  Table [dbo].[ConsignmentInfo]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsignmentInfo](
	[UserID] [int] NULL,
	[SKUMin] [int] NOT NULL,
	[SKUMax] [int] NOT NULL,
	[ConsignmentID] [int] IDENTITY(1,1) NOT NULL,
	[ConsignmentName] [varchar](50) NOT NULL,
	[ConsignmentStatus] [int] NOT NULL,
	[Total] [varchar](50) NOT NULL,
	[MoneyMade] [varchar](50) NOT NULL,
	[IsTest] [int] NOT NULL,
 CONSTRAINT [PK_ConsignmentInfo] PRIMARY KEY CLUSTERED 
(
	[ConsignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](30) NOT NULL,
	[Password] [varchar](16) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ConsignmentID] [int] NULL,
	[IsTest] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConsignmentInfo] FOREIGN KEY([ConsignmentID])
REFERENCES [dbo].[ConsignmentInfo] ([ConsignmentID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_ConsignmentInfo]
GO
/****** Object:  StoredProcedure [dbo].[usp_addConsignment]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_addConsignment] @SKUMin int, @SKUMax int, @ConsignmentName varchar(50), @Total varchar(50), @IsTest int
as
insert into ConsignmentInfo values(null, @SKUMin, @SKUMax, @ConsignmentName, 0, @Total, 0, @IsTest)
select * from ConsignmentInfo where @ConsignmentName = ConsignmentName
GO
/****** Object:  StoredProcedure [dbo].[usp_addUser]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_addUser] @Username varchar(50), @Password varchar(50), @Email varchar(50), @Name varchar(50), @IsTest int
as
insert into Users values (@Username, @Password, @Email, @Name, null, @IsTest)
select * from Users where @Username = Username
GO
/****** Object:  StoredProcedure [dbo].[usp_assignConsignment]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_assignConsignment] @UserID int, @ConsignmentID int
as
update Users set ConsignmentID = @ConsignmentID where @UserID = UserID
update ConsignmentInfo set UserID = @UserID where @ConsignmentID = ConsignmentID
select ConsignmentInfo.UserID, Users.ConsignmentID from Users join ConsignmentInfo on Users.UserID = @UserID and ConsignmentInfo.ConsignmentID = @ConsignmentID

GO
/****** Object:  StoredProcedure [dbo].[usp_Authenticate]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_Authenticate] @Username varchar(50), @Password varchar(50)
as
select Username, Password from Users where @Username = Username and @Password = Password
GO
/****** Object:  StoredProcedure [dbo].[usp_changeConsignmentName]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeConsignmentName] @ConsignmentID int, @NewName varchar(50)
as
update ConsignmentInfo set ConsignmentName = @NewName where @ConsignmentID = ConsignmentID
select ConsignmentName from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeConsignmentStatus]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeConsignmentStatus] @ConsignmentID int, @NewStatus int
as
update ConsignmentInfo set ConsignmentStatus = @NewStatus where ConsignmentID = @ConsignmentID
select ConsignmentStatus from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeEmail]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeEmail] @UserID int, @NewEmail varchar(50)
as
update Users set Email = @NewEmail where @UserID = UserID
select Email from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeMoneyMade]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeMoneyMade] @ConsignmentID int, @NewAmount varchar(50)
as
update ConsignmentInfo set MoneyMade = @NewAmount where @ConsignmentID = ConsignmentID
select MoneyMade from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeName]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeName] @UserID int, @NewName varchar(50)
as
update Users set Name = @NewName where @UserID = UserID
select Name from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_changePassword]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changePassword] @UserID int, @NewPassword varchar(50)
as
update Users set Password = @NewPassword where @UserID = UserID
select Password from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeSKURange]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeSKURange] @ConsignmentID int, @SKUMin int, @SKUMax int
as
update ConsignmentInfo set SKUMin = @SKUMin where @ConsignmentID = ConsignmentID
update ConsignmentInfo set SKUMax = @SKUMax where @ConsignmentID = ConsignmentID
select SKUMin, SKUMax from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeTotal]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeTotal] @ConsignmentID int, @NewTotal varchar(50)
as
update ConsignmentInfo set Total = @NewTotal where @ConsignmentID = ConsignmentID
select Total from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeUser]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_changeUser] @ConsignmentID int, @NewUserID int
as
update ConsignmentInfo set UserID = @NewUserID where ConsignmentID = @ConsignmentID
select UserID from ConsignmentID where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeUsername]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_changeUsername] @UserID int, @NewUsername varchar(50)
as
update Users set Username = @NewUsername where @UserID = UserID
select Username from Users where @UserID = UserID

GO
/****** Object:  StoredProcedure [dbo].[usp_getConsignment]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getConsignment] @UserID int
as
select ConsignmentID from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getConsignmentID]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getConsignmentID] @ConsignmentName varchar(50)
as
select ConsignmentID from ConsignmentInfo where @ConsignmentName = ConsignmentName
GO
/****** Object:  StoredProcedure [dbo].[usp_getConsignmentName]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getConsignmentName] @ConsignmentID int
as
select ConsignmentName from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_getConsignmentStatus]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_getConsignmentStatus] @ConsignmentID int
as
select ConsignmentStatus from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_getEmail]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getEmail] @UserID int
as
select Email from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getMoneyMade]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getMoneyMade] @ConsignmentID int
as
select MoneyMade from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_getName]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getName] @UserID int
as
select Name from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getPassword]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getPassword] @UserID int
as
select Password from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getSKURange]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[usp_getSKURange] @ConsignmentID int
as
select SKUMin, SKUMax from ConsignmentInfo where @ConsignmentID = ConsignmentID

GO
/****** Object:  StoredProcedure [dbo].[usp_getTestConsignmentID]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_getTestConsignmentID]
as
select top 1 ConsignmentID from ConsignmentInfo where IsTest = 1
GO
/****** Object:  StoredProcedure [dbo].[usp_getTestConsignmentPair]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_getTestConsignmentPair]
as
declare @UserID int
set @UserID = (select top 1 UserID from Users where IsTest = 1 and Users.ConsignmentID != -1)
declare @ConsignmentID int
set @ConsignmentID = (select Users.ConsignmentID from Users where @UserID = UserID)
select @UserID, @ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_getTestUserID]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_getTestUserID]
as
select top 1 UserID from Users where IsTest = 1

GO
/****** Object:  StoredProcedure [dbo].[usp_getTotal]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getTotal] @ConsignmentID int
as
select Total from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_getUser]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getUser] @ConsignmentID int
as
select UserID from ConsignmentInfo where ConsignmentID = @ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_getUserID]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getUserID] @Username varchar(50)
as
select UserID from Users where @Username = Username
GO
/****** Object:  StoredProcedure [dbo].[usp_getUsername]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getUsername] @UserID int
as
select Username from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_removeConsignment]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_removeConsignment] @ConsignmentID int
as
delete from ConsignmentInfo where @ConsignmentID = ConsignmentID
select * from ConsignmentInfo where @ConsignmentID = ConsignmentID
GO
/****** Object:  StoredProcedure [dbo].[usp_removeUser]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_removeUser] @UserID int
as
delete from Users where @UserID = UserID
select * from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_unassignConsignment]    Script Date: 11/28/2017 8:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_unassignConsignment] @UserID int, @ConsignmentID int
as
update Users set ConsignmentID = null where @UserID = UserID
update ConsignmentInfo set UserID = null where @ConsignmentID = ConsignmentID
select ConsignmentInfo.UserID, Users.ConsignmentID from Users join ConsignmentInfo on Users.UserID = @UserID and ConsignmentInfo.ConsignmentID = @ConsignmentID

GO
USE [master]
GO
ALTER DATABASE [GMC Consignment Information] SET  READ_WRITE 
GO
