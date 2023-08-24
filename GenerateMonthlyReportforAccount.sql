USE [BankTrans]
GO
/****** Object:  StoredProcedure [dbo].[GenerateMonthlyReportforAccount]    Script Date: 21/8/2023 1:03:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GenerateMonthlyReportforAccount]
	-- Add the parameters for the stored procedure here
	(@AcctNum varchar(50),@TransMonth int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @withdrawaAmount as float;
	Declare @DepositAmount as float;
	Declare @BalanceAmount as float;
	set @BalanceAmount=0;
	set @withdrawaAmount=0;
	set @DepositAmount=0;
	set @withdrawaAmount=(select Sum(Amount) from [BankTrans].[dbo].[AccountTrans]  where AccountID=@AcctNum and TransType='W' and MONTH(TrnDate)=@TransMonth);
	set @DepositAmount= (select Sum(Amount) from [BankTrans].[dbo].[AccountTrans]  where AccountID=@AcctNum and TransType='D' and MONTH(TrnDate)=@TransMonth);
	set @BalanceAmount=@DepositAmount - @withdrawaAmount;
    -- Insert statements for procedure here
SELECT  
      [AccountID]
      ,[TrnDate]
	  ,[Trans_ID]
      ,[TransType]
      ,sum(Amount) Amount
	  ,@BalanceAmount BalanceAmount
  FROM [BankTrans].[dbo].[AccountTrans]  where AccountID=@AcctNum and MONTH(TrnDate)=@TransMonth group by AccountID,TrnDate,Trans_ID,TransType,Amount 
END
