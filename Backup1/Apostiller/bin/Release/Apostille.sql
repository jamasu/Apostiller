USE [Apostiller]
GO
/****** Object:  Table [dbo].[apostille]    Script Date: 06/25/2010 12:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[apostille](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](65) NULL,
	[date] [datetime] NULL,
	[title] [nvarchar](65) NULL,
	[capacity] [nvarchar](35) NULL,
	[doc] [nvarchar](20) NULL,
	[about] [nvarchar](65) NULL,
	[sig] [nvarchar](6) NULL,
	[country] [nvarchar](50) NULL,
	[office] [ntext] NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
