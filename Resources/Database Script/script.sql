USE [master]
GO
/****** Object:  Database [ContactsManager]    Script Date: 4/22/2021 6:27:28 PM ******/
CREATE DATABASE [ContactsManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ContactsManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ContactsManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ContactsManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ContactsManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ContactsManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContactsManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ContactsManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ContactsManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ContactsManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ContactsManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ContactsManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [ContactsManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ContactsManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ContactsManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ContactsManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ContactsManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ContactsManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ContactsManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ContactsManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ContactsManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ContactsManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ContactsManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ContactsManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ContactsManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ContactsManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ContactsManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ContactsManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ContactsManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ContactsManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ContactsManager] SET  MULTI_USER 
GO
ALTER DATABASE [ContactsManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ContactsManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ContactsManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ContactsManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ContactsManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ContactsManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ContactsManager] SET QUERY_STORE = OFF
GO
USE [ContactsManager]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 4/22/2021 6:27:28 PM ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NT AUTHORITY\NETWORK SERVICE]    Script Date: 4/22/2021 6:27:28 PM ******/
CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [D1]    Script Date: 4/22/2021 6:27:28 PM ******/
CREATE USER [D1] FOR LOGIN [D1] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_datareader] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_owner] ADD MEMBER [D1]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [D1]
GO
/****** Object:  Table [dbo].[ContactInfo]    Script Date: 4/22/2021 6:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](100) NULL,
	[LastName] [nchar](100) NULL,
	[Email] [nchar](300) NULL,
	[PhoneNumber] [nchar](10) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddNewContactDetails]    Script Date: 4/22/2021 6:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[AddNewContactDetails]  
(  
   @FName varchar (100),  
   @LName varchar (100),  
   @Email varchar (300),  
   @PhoneNumber varchar (50),
   @Active bit
)  
as  
begin  
   Insert into ContactInfo values(@FName,@LName,@Email,@PhoneNumber,@Active)  
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteContactById]    Script Date: 4/22/2021 6:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DeleteContactById]  
(  
   @Id int  
)  
as   
begin  
   Delete from ContactInfo where Id=@Id  
End 
GO
/****** Object:  StoredProcedure [dbo].[GetContactDetails]    Script Date: 4/22/2021 6:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetContactDetails]  
as  
begin  
   select *from ContactInfo  
End 
GO
/****** Object:  StoredProcedure [dbo].[UpdateContactDetails]    Script Date: 4/22/2021 6:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateContactDetails]  
(  
   @Id int,
   @FName varchar (100),  
   @LName varchar (100),  
   @Email varchar (100),  
   @PhoneNumber varchar (50),
   @Active bit 
)  
as  
begin  
   Update ContactInfo   
   set FirstName=@FName,  
   LastName=@LName,  
   Email=@Email,
   PhoneNumber=@PhoneNumber,
   Active=@Active
   where Id=@Id
   End
GO
USE [master]
GO
ALTER DATABASE [ContactsManager] SET  READ_WRITE 
GO
