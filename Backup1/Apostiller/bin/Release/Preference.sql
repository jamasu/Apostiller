USE [Apostiller]
GO
/****** Object:  Table [dbo].[preference]    Script Date: 06/25/2010 12:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[preference](
	[id] [int] NOT NULL,
	[pref_capacity] [nvarchar](65) NULL,
	[pref_doc] [nvarchar](65) NULL,
	[pref_office] [nvarchar](65) NULL,
	[pref_county] [nvarchar](150) NULL,
	[pref_city] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
