USE [master]
GO
CREATE DATABASE [blackjack]
GO
ALTER DATABASE [blackjack] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [blackjack].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [blackjack] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [blackjack] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [blackjack] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [blackjack] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [blackjack] SET ARITHABORT OFF 
GO
ALTER DATABASE [blackjack] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [blackjack] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [blackjack] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [blackjack] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [blackjack] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [blackjack] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [blackjack] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [blackjack] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [blackjack] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [blackjack] SET  ENABLE_BROKER 
GO
ALTER DATABASE [blackjack] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [blackjack] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [blackjack] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [blackjack] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [blackjack] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [blackjack] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [blackjack] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [blackjack] SET RECOVERY FULL 
GO
ALTER DATABASE [blackjack] SET  MULTI_USER 
GO
ALTER DATABASE [blackjack] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [blackjack] SET DB_CHAINING OFF 
GO
ALTER DATABASE [blackjack] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [blackjack] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [blackjack] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'blackjack', N'ON'
GO
ALTER DATABASE [blackjack] SET QUERY_STORE = OFF
GO
USE [blackjack]
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
USE [blackjack]
GO
/****** Object:  Table [dbo].[Cards]    Script Date: 25-Sep-18 11:52:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cards](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Suit] [nvarchar](max) NOT NULL,
	[Rank] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameSessions]    Script Date: 25-Sep-18 11:52:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameSessions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History]    Script Date: 25-Sep-18 11:52:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SessionId] [bigint] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Event] [nvarchar](max) NOT NULL,
	[PlayerId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NLog]    Script Date: 25-Sep-18 11:52:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MachineName] [nvarchar](200) NULL,
	[SiteName] [nvarchar](200) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Level] [varchar](5) NOT NULL,
	[UserName] [nvarchar](200) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](300) NULL,
	[Properties] [nvarchar](max) NULL,
	[ServerName] [nvarchar](200) NULL,
	[Port] [nvarchar](100) NULL,
	[Url] [nvarchar](2000) NULL,
	[Https] [bit] NULL,
	[ServerAddress] [nvarchar](100) NULL,
	[RemoteAddress] [nvarchar](100) NULL,
	[Callsite] [nvarchar](300) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayerHands]    Script Date: 25-Sep-18 11:52:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerHands](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PlayerId] [bigint] NOT NULL,
	[CardId] [bigint] NOT NULL,
	[SessionId] [bigint] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Players]    Script Date: 25-Sep-18 11:52:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[GameSessions] ADD  DEFAULT ((1)) FOR [IsOpen]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[GameSessions] ([Id])
GO
ALTER TABLE [dbo].[PlayerHands]  WITH CHECK ADD FOREIGN KEY([CardId])
REFERENCES [dbo].[Cards] ([Id])
GO
ALTER TABLE [dbo].[PlayerHands]  WITH CHECK ADD  CONSTRAINT [FK__PlayerHan__Playe__160F4887] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Players] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlayerHands] CHECK CONSTRAINT [FK__PlayerHan__Playe__160F4887]
GO
ALTER TABLE [dbo].[PlayerHands]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[GameSessions] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[NLog_AddEntry_p]    Script Date: 25-Sep-18 11:52:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NLog_AddEntry_p] (
  @machineName nvarchar(200),
  @siteName nvarchar(200),
  @creationDate datetime,
  @level varchar(5),
  @userName nvarchar(200),
  @message nvarchar(max),
  @logger nvarchar(300),
  @properties nvarchar(max),
  @serverName nvarchar(200),
  @port nvarchar(100),
  @url nvarchar(2000),
  @https bit,
  @serverAddress nvarchar(100),
  @remoteAddress nvarchar(100),
  @callSite nvarchar(300),
  @exception nvarchar(max)
) AS
BEGIN
  INSERT INTO [dbo].[SystemLog] (
    [MachineName],
    [SiteName],
    [CreationDate],
    [Level],
    [UserName],
    [Message],
    [Logger],
    [Properties],
    [ServerName],
    [Port],
    [Url],
    [Https],
    [ServerAddress],
    [RemoteAddress],
    [CallSite],
    [Exception]
  ) VALUES (
    @machineName,
    @siteName,
    @creationDate,
    @level,
    @userName,
    @message,
    @logger,
    @properties,
    @serverName,
    @port,
    @url,
    @https,
    @serverAddress,
    @remoteAddress,
    @callSite,
    @exception
  );
END
GO
USE [master]
GO
ALTER DATABASE [blackjack] SET  READ_WRITE 
GO
