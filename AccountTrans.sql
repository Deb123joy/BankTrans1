USE [BankTrans]
GO

/****** Object:  Table [dbo].[AccountTrans]    Script Date: 21/8/2023 1:05:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AccountTrans](
	[ATrans_ID] [int] IDENTITY(1,1) NOT NULL,
	[Trans_ID] [varchar](200) NULL,
	[AccountID] [varchar](50) NULL,
	[TrnDate] [datetime] NULL,
	[TransType] [nchar](1) NULL,
	[Amount] [float] NULL,
 CONSTRAINT [PK_AccountTrans_1] PRIMARY KEY CLUSTERED 
(
	[ATrans_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


