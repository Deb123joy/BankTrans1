USE [BankTrans]
GO

/****** Object:  StoredProcedure [dbo].[AddINterestRule]    Script Date: 21/8/2023 1:01:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddINterestRule]
	-- Add the parameters for the stored procedure here
	( @IntDate DateTime, @RuleID varchar(50), @Rate float)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DEclare @IntRateExists int;
	Set @IntRateExists=(Select count(*) from [dbo].[InterestRules]  where InterestDate=@IntDate);
	if(@IntRateExists=0)
	Begin
    -- Insert statements for procedure here
	INSERT INTO [dbo].[InterestRules]           
           ([InterestDate]
           ,[RuleID]
           ,[Rate])
		   
     VALUES
           (@IntDate, @RuleID, @Rate)
		   End
		   if(@IntRateExists>0)
		   Begin
		   Update [dbo].[InterestRules] set RuleID=@RuleID, Rate=@Rate where InterestDate=@IntDate
		   End

END
GO


