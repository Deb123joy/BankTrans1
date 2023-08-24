USE [BankTrans]
GO

/****** Object:  StoredProcedure [dbo].[GetAccountTransactions]    Script Date: 21/8/2023 1:03:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountTransactions]
	-- Add the parameters for the stored procedure here
	(@AcctNum varchar(50))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  
      [AccountID]
      ,CAST(TrnDate AS date) Date
	  ,[Trans_ID]
      ,[TransType]
      ,[Amount]
  FROM [BankTrans].[dbo].[AccountTrans] where AccountID=@AcctNum
END
GO


