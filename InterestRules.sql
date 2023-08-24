USE [BankTrans]
GO

/****** Object:  Table [dbo].[InterestRules]    Script Date: 21/8/2023 1:05:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InterestRules](
	[Interest_ID] [int] IDENTITY(1,1) NOT NULL,
	[InterestDate] [datetime] NULL,
	[RuleID] [varchar](50) NULL,
	[Rate] [float] NOT NULL,
 CONSTRAINT [PK_InterestRules] PRIMARY KEY CLUSTERED 
(
	[Interest_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


