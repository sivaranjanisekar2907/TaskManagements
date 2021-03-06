USE [nozomi-pf-japan-stagingdb-qa-01_BKP_26May]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 2021-06-07 3:47:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 2021-06-07 3:47:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] NOT NULL,
	[TaskName] [varchar](50) NULL,
	[Hours] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskManagement]    Script Date: 2021-06-07 3:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NULL,
	[StaffName] [varchar](50) NULL,
	[TaskId] [int] NULL,
	[TaskName] [varchar](50) NULL,
	[WeekId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeTracker]    Script Date: 2021-06-07 3:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeTracker](
	[StaffId] [int] NULL,
	[WeekId] [int] NULL,
	[Hours] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeekDays]    Script Date: 2021-06-07 3:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekDays](
	[Id] [int] NOT NULL,
	[Days] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
