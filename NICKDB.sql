USE [NickDB]
GO
/****** Object:  Table [dbo].[Incident]    Script Date: 26/01/2023 20:29:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incident](
	[IncidentCode] [nvarchar](50) NOT NULL,
	[IncidentName] [nvarchar](50) NULL,
	[IncidentDate] [datetime] NULL,
	[IncidentLocation] [nvarchar](50) NULL,
	[IncidentRecordBy] [nvarchar](50) NULL,
	[IncidentAddressed] [bit] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[IncidentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 26/01/2023 20:29:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[UserAccountCode] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[PasswordSalt] [nvarchar](50) NULL,
	[PasswordHash] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[UserAccountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
