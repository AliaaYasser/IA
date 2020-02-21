USE [test]
GO

/****** Object:  Table [dbo].[users]    Script Date: 2/21/2020 5:41:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserPhoto] [varchar](250) NOT NULL,
	[UserExpirienceYears] [int] NOT NULL,
	[UserEmail] [varchar](250) NOT NULL,
	[UserJob_description] [text] NOT NULL,
	[UserFirstName] [varchar](30) NOT NULL,
	[UserLasttName] [varchar](30) NOT NULL,
	[UserPhone] [varchar](250) NOT NULL,
	[UserRole] [varchar](250) NOT NULL,
	[UserPassword] [varchar](250) NOT NULL,
	[NumOFProject] [int] NULL,
	[Qualifications] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




USE [test]
GO

/****** Object:  Table [dbo].[projects]    Script Date: 2/21/2020 5:41:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[projects](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](250) NOT NULL,
	[ProjectDescription] [text] NOT NULL,
	[Projectstatus] [int] NULL,
	[StartDate] [varchar](40) NULL,
	[endData] [varchar](40) NULL,
	[price] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[projects] ADD  DEFAULT ((0)) FOR [Projectstatus]
GO


USE [test]
GO

/****** Object:  Table [dbo].[userProject]    Script Date: 2/21/2020 5:41:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[userProject](
	[ProjectId] [int] NULL,
	[userId] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[userProject]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[projects] ([ProjectID])
GO

ALTER TABLE [dbo].[userProject]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([UserId])
GO





USE [test]
GO

/****** Object:  Table [dbo].[requests]    Script Date: 2/21/2020 5:41:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[requests](
	[requestID] [int] IDENTITY(1,1) NOT NULL,
	[projectid] [int] NULL,
	[FromUser] [int] NULL,
	[ToUser] [int] NULL,
	[isAccepted] [int] NULL,
	[sentAt] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[requestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[requests]  WITH CHECK ADD FOREIGN KEY([FromUser])
REFERENCES [dbo].[users] ([UserId])
GO

ALTER TABLE [dbo].[requests]  WITH CHECK ADD FOREIGN KEY([projectid])
REFERENCES [dbo].[projects] ([ProjectID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[requests]  WITH CHECK ADD FOREIGN KEY([ToUser])
REFERENCES [dbo].[users] ([UserId])
GO


USE [test]
GO

/****** Object:  Table [dbo].[feedback]    Script Date: 2/21/2020 5:41:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[feedback](
	[feedbackID] [int] IDENTITY(1,1) NOT NULL,
	[feedbackContent] [varchar](250) NULL,
	[Evaluate] [int] NULL,
	[FromUser] [int] NULL,
	[ToUser] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[feedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[feedback]  WITH CHECK ADD FOREIGN KEY([FromUser])
REFERENCES [dbo].[users] ([UserId])
GO

ALTER TABLE [dbo].[feedback]  WITH CHECK ADD FOREIGN KEY([ToUser])
REFERENCES [dbo].[users] ([UserId])
GO


USE [test]
GO

/****** Object:  Table [dbo].[comments]    Script Date: 2/21/2020 5:40:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[comments](
	[commentID] [int] IDENTITY(1,1) NOT NULL,
	[projectid] [int] NULL,
	[pmid] [int] NULL,
	[sentAt] [varchar](60) NULL,
	[content] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[commentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[comments]  WITH CHECK ADD FOREIGN KEY([pmid])
REFERENCES [dbo].[users] ([UserId])
GO

ALTER TABLE [dbo].[comments]  WITH CHECK ADD FOREIGN KEY([projectid])
REFERENCES [dbo].[projects] ([ProjectID])
ON DELETE CASCADE
GO


