SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AFTER_JUDGMENT_PROCEEDINGS-TO_RECEIVE_JUDGEMENT]
@Pageno				INT=1,
@filter				NVARCHAR(500)='',
@pagesize			INT=20,  
@Sorting			NVARCHAR(500)='CC.OfficeFileNo',
@SortOrder			NVARCHAR(500)='desc',
@UserName			NVARCHAR(5)=null,
@DataFor			NVARCHAR(25)=null,
@Location			NVARCHAR(1)=null
AS
DECLARE
@FilterText			NVARCHAR(MAX) = '',
@FinalQuery			NVARCHAR(MAX),
@FinalSummaryQuery	NVARCHAR(MAX),
@FetchLimit			NVARCHAR(MAX),
@TableColumn		NVARCHAR(MAX)  = ' OfficeFileNo,FJDCounter,SessionLevelName,CountLocationName,CourtRefNo,SessionRollClientName,SessionRollDefendentName,IsFavorable,AgainstName,DisplaySentence,CaseId,SessionRollId,InvestmentTypeName,InvestmentType,SessionNote_Remark,FileStatusName,FSort ',
@TableQuery			NVARCHAR(MAX),
@AdditionalWhere	NVARCHAR(MAX) = ' where 1=1',
@QueryFirstPart		NVARCHAR(MAX),
@SelectColumns		NVARCHAR(MAX) = '
			,sortDate
			FROM (
				  SELECT
				  CC.OfficeFileNo,
				  DATEDIFF(DAY, dateadd(hour , 11, LJD.JudgementsDate),dateadd(hour , 11,GETDATE())) AS FJDCounter,
				  SessLevel.Mst_Desc as SessionLevelName,
				  CountLocation.Mst_Desc as CountLocationName,
				  CaseNosVW.CASE_NO as CourtRefNo,
				  case when CC.SessionClientId = ''0'' then null else SessClient.Mst_Desc end as SessionRollClientName,
				  CC.SessionRollDefendentName,
				  REPLACE(LTRIM(RTRIM(AgainstMas.Mst_Desc)), ''AGAINST '', '''') as AgainstName,
				  JDV.Judgement as DisplaySentence, 
				  JDV.CaseId,
				  LJD.SessionRollId,
				  JDV.IsFavorable,
				  LJD.JudgementsDate as sortDate,
				  case when convert(int,CR.DepartmentType) > 0 then DepType.Mst_Desc end as InvestmentTypeName,
				  CR.DepartmentType as InvestmentType,
				  SR.SessionNote_Remark,
				  FileStatus.Mst_Desc as FileStatusName,
				  Case When ISNULL(JDV.IsFavorable,''N'') = ''N'' then 1 else 0 end as FSort
				  FROM JudgementDetailView AS JDV
				  ',

@SummaryQuery		NVARCHAR(MAX) = '
									SELECT
									COUNT(*) as recordsTotal,
									count(case when left(CC.OfficeFileNo,1) = ''M'' then 1 end) MCTRecords,
									count(case when left(CC.OfficeFileNo,1) = ''S'' then 1 end) SLLRecords
									from JudgementDetailView AS JDV
									',
@JoinTables			NVARCHAR(MAX) = ' 
									Inner Join LastJudgementDateVW LJD on JDV.CaseId = LJD.CaseId and JDV.JudgementsDate = LJD.JudgementsDate
									Inner Join CourtCases AS CC on JDV.CaseId = CC.CaseId
									Inner Join MASTER_S as SessLevel on SessLevel.MstParentId = 15 and SessLevel.Mst_Value = JDV.CaseLevelCode
									Left  Join MASTER_S as CountLocation on CountLocation.MstParentId = 4 and CountLocation.Mst_Value = JDV.CourtLocationid
									Left  Join CaseNosVW on CaseNosVW.CaseId = JDV.CaseId
									Left  Join MASTER_S as SessClient on SessClient.MstParentId = 913 and SessClient.Mst_Value = CC.SessionClientId
									Left  Join MASTER_S as AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
									Left  Join CaseRegistrations CR on CR.CaseId = JDV.CaseId AND CR.IsDeleted = 0
									Left  Join MASTER_S DepType on CR.DepartmentType = DepType.Mst_Value and DepType.MstParentId = 822
									Inner Join SessionsRolls SR on SR.SessionRollId = LJD.SessionRollId
									Inner join MASTER_S FileStatus on CC.OfficeFileStatus = FileStatus.Mst_Value and FileStatus.MstParentId = 1573 
									',
@Where				NVARCHAR(MAX) = ' 
									where CC.CaseStatus = ''1''
									AND   JDV.JudgementReceive IS NULL
									AND	  LJD.JudgementsDate >= ''2023-06-01''
									AND   (LEFT(CC.OfficeFileNo,1) = '''+@Location+''' OR '''+@Location+''' = ''A'')
									'

BEGIN


	SET @QueryFirstPart = '
SELECT '+@TableColumn+' 
 FROM 
  ( 
   SELECT ROW_NUMBER() OVER (order by FSort DESC,FJDCounter desc) as RowNum, * 
    from 
	  ( 
	   SELECT '
	SET @TableQuery  = @SelectColumns+@JoinTables+@Where

	IF(@filter !='')
	BEGIN
		SET @FilterText = '
						AND (
							CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR SessLevel.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CC.SessionRollDefendentName like N''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CountLocation.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR JDV.Judgement like N''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							OR CaseNosVW.CASE_NO like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							OR SessClient.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							OR SR.SessionNote_Remark like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							OR FileStatus.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
							)
							'
	END


	IF(@pagesize > 0)
	BEGIN
		SET @pagesize = @pagesize + @Pageno
		SET @Pageno = @Pageno + 1
		SET @FetchLimit = 'WHERE RowNum >= '+CONVERT(varchar,@Pageno)+' AND RowNum <= '+CONVERT(varchar,@pagesize)
	END
	ELSE
	BEGIN	
		SET @FetchLimit = ' ' 
	END

BEGIN	
	SET @FinalQuery = @QueryFirstPart + @TableColumn + @TableQuery + @FilterText + 
			' ) AS DAT ' + 
			  @AdditionalWhere + 
	  ' ) AS RowConstrainedResult 
	) AS FINAL ' + @FetchLimit+' ORDER BY RowNum '
	
	SET @FinalSummaryQuery = @SummaryQuery+@JoinTables+@Where+@FilterText
END

print @FinalQuery
exec (@FinalQuery)
print @FinalSummaryQuery
exec (@FinalSummaryQuery)

END