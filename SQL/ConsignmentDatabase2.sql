USE [master]
GO
/****** Object:  Database [GMC Consignment Information]    Script Date: 11/22/2017 1:55:26 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](30) NOT NULL,
	[Password] [varchar](16) NOT NULL,
	[SKUMin] [int] NOT NULL,
	[SKUMax] [int] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_addUser]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_addUser] @Username varchar(30), @Password varchar(16), @SKUMin int, @SKUMax int, @Email varchar(50), @Name varchar(50)
as
insert into Users values(@Username, @Password, @SKUMin, @SKUMax, @Email, @Name)
declare @UserID int
set @UserID = (select top 1 UserID from Users where @Username = Username)
select * from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeEmail]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_changeEmail] @NewEmail varchar(50), @UserID int
as
update Users set Email = @NewEmail where UserID = @UserID
select Email from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeName]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_changeName] @NewName varchar(50), @UserID int
as
update Users set Name = @NewName where UserID = @UserID
select Name from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_changePassword]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_changePassword] @UserID int, @newPassword varchar(16)
as
update Users set Password = @newPassword where @UserID = UserID
select Password from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeSKURange]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_changeSKURange] @Min int, @Max int, @ID int
as
update Users set SKUMin = @Min, SKUMax = @Max where UserID = @ID
select SKUMin, SKUMax from Users where UserID = @ID
GO
/****** Object:  StoredProcedure [dbo].[usp_changeUsername]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_changeUsername] @UserID int, @newUsername varchar(30)
as
update Users set Username = @newUsername where @UserID = UserID
select Username from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getEmailFromID]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getEmailFromID] @UserID int
as
select Email from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getNameFromID]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getNameFromID] @UserID int
as
select Name from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getPasswordByID]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getPasswordByID] @UserID int
as
select Password from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_getSKURangeByID]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_getSKURangeByID] @UserID int
as
select SKUMin, SKUMax from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_IDtoUsername]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_IDtoUsername] @UserID int
as
select Username from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_removeUser]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_removeUser] @UserID int
as
delete from Users where @UserID = UserID
select * from Users where @UserID = UserID
GO
/****** Object:  StoredProcedure [dbo].[usp_usernameToID]    Script Date: 11/22/2017 1:55:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_usernameToID] @Username varchar(30)
as
select UserID from Users where @Username = Username
GO
USE [master]
GO
ALTER DATABASE [GMC Consignment Information] SET  READ_WRITE 
GO
