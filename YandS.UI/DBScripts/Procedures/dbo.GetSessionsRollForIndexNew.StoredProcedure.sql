SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetSessionsRollForIndexNew]
@UserName varchar(5),
@DataFor varchar(15),
@Location varchar(1),
@SessionLevel varchar(2),
@DateFrom date,
@DateTo date,
@CountLocationId varchar(5),
@LawyerId varchar(5),
@ClientCode varchar(10) = null,
@Pageno INT=1,
@filter NVARCHAR(500)='',
@pagesize INT=20,  
@Sorting VARCHAR(4000)='dat.NextHearingDate',
@SortOrder VARCHAR(500)='desc' 
AS
DECLARE 
@SelectColumns	NVARCHAR (MAX),
@JoinTables		NVARCHAR (MAX),
@Where			NVARCHAR (MAX) = ' where 1=1',
@FilterText		NVARCHAR (MAX) = '',
@Role			VARCHAR(100),
@LocationId		VARCHAR(5),
@UserId			INT,
@RoleCount		INT,
@OrderBy		NVARCHAR (MAX),
@FetchLimit		NVARCHAR (MAX),
@SummaryQuery	NVARCHAR (MAX),
@FinalQuery		NVARCHAR (MAX),
@SqlCount		INT,
@SQLCountQuery	NVARCHAR (MAX),
@MXDate			DATETIME2 = '9999-12-31',
@lcCaseID		INT

BEGIN
SET NOCOUNT ON;


BEGIN 
		UPDATE SessionsRolls SET ShowFollowup = 0
		from SessionsRolls SR 
		inner join CourtCases CC on CC.CaseId = SR.CaseId
		where	SR.DeletedOn is null 
		AND		SR.ShowFollowup = 1 
		AND		CC.NextHearingDate < CAST(dateadd(HOUR, 4, GETUTCDATE()) as date)

		UPDATE SessionsRolls SET ShowSuspend = 0
		from SessionsRolls SR 
		inner join CourtCases CC on CC.CaseId = SR.CaseId
		where	SR.DeletedOn is null 
		AND		SR.ShowSuspend = 1 
		AND		CC.NextHearingDate < CAST(dateadd(HOUR, 4, GETUTCDATE()) as date)

		UPDATE SessionsRolls 
		SET WorkRequired = null,SessionNotes = null,LastDate = null,FollowerId = '0',ShowFollowup = 0,
		SuspendedWorkRequired = null,SuspendedSessionNotes = null,SuspendedLastDate = null,SuspendedFollowerId= '0',ShowSuspend= 0,
		CourtFollow_LawyerId = '0'
		from SessionsRolls SR 
		inner join CourtCases CC on CC.CaseId = SR.CaseId
		where	SR.DeletedOn is null 
		AND		CC.NextHearingDate < CAST(dateadd(HOUR, 4, GETUTCDATE()) as date)

		UPDATE CourtCases 
		SET CourtFollow = null,CourtFollowRequirement = null,CommissioningDate = null
		where	NextHearingDate IS NOT NULL 
		AND		NextHearingDate < CAST(dateadd(HOUR, 4, GETUTCDATE()) as date)

END

SET @LocationId = (select LocationCode from HR_Employee_s where EmployeeNumber = @UserName)
SET @UserId = (select UserId from USERS where UserName = @UserName)
SET @RoleCount = (select count(*) from LNK_USER_ROLE where UserId = @UserId and RoleId in (4)) -- 'VoucherApproval'

DECLARE @sortingTable TABLE (sortid varchar(2), sortname varchar(4000))
insert into @sortingTable values('1','dbo.fnMixSort(dat.OfficeFN)')
								,('2','dat.NextHearingDateforSort')
								,('3','dat.SessionLevelName')
								,('4','dat.CountLocationName')
								,('5','dat.SessionRollClientName')
								,('6','dat.SessionRollDefendentName')
								,('7','dat.CourtDecision')
								,('8','dat.CaseType')
								,('9','dat.CaseTypeName')
								,('10','dat.LawyerId')
								,('11','dat.LawyerName')
								,('12','dat.SessionRemarks')
								,('13','dat.DaysCounterFollow')
								,('14','dat.LastDateforSort')
								,('15','dat.WorkRequired')
								,('16','dat.FollowerId')
								,('17','dat.CaseId')
								,('18','dat.SessionRollId')
								,('19','dat.FollowerName')
								,('20','dat.DaysCounterTobeUpdate')
								,('21','dat.FJDCounter')
								,('22','dat.LocationCode')
								,('23','dat.SupJudgDateforSort')
								,('24','dat.DifferentPanelNotes')
								,('25','dat.SessionHoldSort')
								,('26','dat.LastDateforSort')
								,('27','dat.SLastDateforSort')


IF @SortOrder = 'desc' BEGIN SET @MXDate = '1700-01-01'   END ELSE BEGIN SET @MXDate = '9999-12-31'   END


SELECT @Sorting  = sortname FROM @sortingTable WHERE sortid = @Sorting
SET @OrderBy = ' order by '+ CONVERT(VARCHAR,@Sorting) +' '+ @SortOrder

SET @SelectColumns =   'CC.OfficeFileNo AS OfficeFN,
						FORMAT (CC.NextHearingDate, ''dd/MM/yyyy'') as NextHearingDate,
						case when CC.CaseLevelCode = ''0'' then null else SessLevel.Mst_Desc end as SessionLevelName,
						case when CCD.CourtLocationid = ''0'' then null else CountLocation.Mst_Desc end as CountLocationName,
						case when CC.SessionClientId = ''0'' then null else SessClient.Mst_Desc end as SessionRollClientName,
						CC.SessionRollDefendentName,
						CC.CourtDecision,
						SR.CaseType,
						case when SR.CaseType = ''0'' then null else SessCaseType.Mst_Desc end as CaseTypeName,
						SR.LawyerId,
						case when SR.LawyerId = ''0'' then null else SessLawyer.Mst_Desc end as LawyerName,
						CC.SessionRemarks,
						DATEDIFF(DAY, dateadd(hour , 11,GETDATE()),dateadd(hour , 11, NextHearingDate)) AS DaysCounterFollow,
						FORMAT (SR.LastDate, ''dd/MM/yyyy'') as LastDate,
						SR.WorkRequired,
						SR.FollowerId,
						SR.CaseId,
						SR.SessionRollId,
						case when SR.FollowerId = ''0'' then null else SessFollower.Mst_Desc end as FollowerName, 
						DATEDIFF(DAY, dateadd(hour , 11, CC.NextHearingDate),dateadd(hour , 11,GETDATE())) AS DaysCounterTobeUpdate,
						DATEDIFF(DAY, dateadd(hour , 11, SR.UpdatedOn),dateadd(hour , 11,GETDATE())) AS FJDCounter,
						case when left(CC.OfficeFileNo,1) = ''M'' then ''Muscat'' else ''Salalah'' end as LocationCode,
						ISNULL(SR.SupremeJudgementsDate,''' + CAST(@MXDate AS VARCHAR(30)) + ''') as SupJudgDateforSort,
						SR.DifferentPanelNotes,
						ISNULL(SR.SessionOnHoldUntill,''' + CAST(@MXDate AS VARCHAR(30)) + ''') as SessionHoldSort,
						ISNULL(SR.LastDate,''' + CAST(@MXDate AS VARCHAR(30)) + ''') as LastDateforSort,
						ISNULL(SR.SuspendedLastDate,''' + CAST(@MXDate AS VARCHAR(30)) + ''') as SLastDateforSort,
						CC.AccountContractNo,
						CC.ClientFileNo,
						SR.SessionNotes,
						ISNULL(SR.EnforcementJudgements,ISNULL(SR.SupremeJudgements,ISNULL(SR.AppealJudgements,SR.PrimaryJudgements))) as DisplaySentence,
						ISNULL(SR.EnforcementIsFavorable,ISNULL(SR.AppealIsFavorable,SR.PrimaryIsFavorable)) as IsNoFavorbleDecision,
						ISNULL(CC.NextHearingDate,''' + CAST(@MXDate AS VARCHAR(30)) + ''') as NextHearingDateforSort,
						CCD.CourtRefNo,
						FORMAT (SR.SupremeJudgementsDate, ''dd/MM/yyyy'') as SupremeJudgementsDate,
						SR.SuspendedWorkRequired,SR.SuspendedSessionNotes,
						FORMAT (SR.SuspendedLastDate, ''dd/MM/yyyy'') as SuspendedLastDate, SR.SuspendedFollowerId,
						case when SR.SuspendedFollowerId = ''0'' then null else SessSusFollower.Mst_Desc end as SuspendedFollowerName,
						FORMAT (SR.SessionOnHoldUntill, ''dd/MM/yyyy'') as SessionOnHoldUntill,SR.SessionOnHold,
						case when SR.SessionOnHold = ''0'' then null else SessOnHold.Mst_Desc end as SessionOnHoldDesc,
						CC.OfficeFileStatus as SessionFileStatus,case when CC.OfficeFileStatus = ''0'' then null else SessFilestatus.Mst_Desc end as SessionFileStatusDesc
						,REPLACE(LTRIM(RTRIM(AgainstMas.Mst_Desc)), ''AGAINST '', '''') as AgainstName '
SET @JoinTables =	' 
					join CourtCases CC on CC.CaseId = SR.CaseId 
					left join CourtCasesDetailVW as CCD on CC.CaseId = CCD.CaseId and CC.CaseLevelCode = CCD.CaseLevelCode
					left join MASTER_S as CountLocation on CountLocation.MstParentId = 4 and CountLocation.Mst_Value = CCD.CourtLocationid
					left join MASTER_S as SessLevel on SessLevel.MstParentId = 15 and SessLevel.Mst_Value = CC.CaseLevelCode
					left join MASTER_S as SessCaseType on SessCaseType.MstParentId = 859 and SessCaseType.Mst_Value = SR.CaseType
					left join MASTER_S as SessLawyer on SessLawyer.MstParentId = 1408 and SessLawyer.Mst_Value = SR.LawyerId
					left join MASTER_S as SessFollower on SessFollower.MstParentId = 1408 and SessFollower.Mst_Value = SR.FollowerId
					left join MASTER_S as SessClient on SessClient.MstParentId = 913 and SessClient.Mst_Value = CC.SessionClientId
					left join MASTER_S as SessFilestatus on SessFilestatus.MstParentId = 1573 and SessFilestatus.Mst_Value = CC.OfficeFileStatus and SessFilestatus.Remarks like ''%SR%''
					left join MASTER_S as SessOnHold on SessOnHold.MstParentId = 1092 and SessOnHold.Mst_Value = SR.SessionOnHold
					left join MASTER_S as SessSusFollower on SessSusFollower.MstParentId = 1408 and SessSusFollower.Mst_Value = SR.SuspendedFollowerId
					Left join MASTER_S as AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
					'


IF(@pagesize > 0)
	BEGIN
	SET @pagesize = @pagesize + @Pageno
	SET @Pageno = @Pageno + 1
		SET @FetchLimit = 'WHERE RowNum >= '+CONVERT(varchar,@Pageno)+' AND RowNum <= '+CONVERT(varchar,@pagesize)+' ORDER BY RowNum '
	END
ELSE
	BEGIN
		SET @FetchLimit = ' '
	END
PRINT @RoleCount

BEGIN SET @Where = @Where + ' AND CC.CaseStatus = ''1'' AND SR.DeletedOn IS NULL AND  (LEFT(CC.OfficeFileNo,1) = '''+@Location+''' OR '''+@Location+''' = ''A'') ' END

IF (@RoleCount > 0)
	BEGIN
		IF (@DataFor = 'ALLSESSIONS')
		BEGIN
			 SET @Where = @Where + ' AND CC.OfficeFileStatus IN (''OFS-16'',''OFS-31'',''OFS-20'',''OFS-21'') AND 1 = case when CC.CaseLevelCode = ''5'' then 1 else case when CC.NextHearingDate >= CAST(dateadd(HOUR, 4, GETUTCDATE()) as date) then 1 else 0 end end '
		END
		ELSE IF (@DataFor = 'PRINT')
		BEGIN
			 SET @Where = @Where + ' AND CC.NextHearingDate BETWEEN ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + ''' AND ( CC.CaseLevelCode = ''' + @SessionLevel + ''' OR  ''' + @SessionLevel + ''' = ''0'') AND ( CCD.CourtLocationid = ''' + @CountLocationId + ''' OR  ''' + @CountLocationId + ''' = ''0'') AND ( SR.LawyerId = ''' + @LawyerId + ''' OR  ''' + @LawyerId + ''' = ''0'') AND ( CC.ClientCode = ''' + @ClientCode + ''' OR  ''' + @ClientCode + ''' = ''0'') '
		END
		ELSE IF (@DataFor = 'FOLLOW')
		BEGIN
			 SET @Where = @Where + ' AND SR.ShowFollowup = 1  '
		END
		ELSE IF (@DataFor = 'TOBEUPDATE')
		BEGIN
			 SET @Where = @Where + ' AND 1 = case when CC.OfficeFileStatus IN (''OFS-16'',''OFS-31'',''OFS-20'',''OFS-21'') AND CC.NextHearingDate < CAST(dateadd(HOUR, 4, GETUTCDATE()) as date) then 1 else 0 end '
		END
		ELSE IF (@DataFor = 'JUDGEMENT')
		BEGIN
 			 SET @Where = @Where + ' AND CC.OfficeFileStatus = ''OFS-17'' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryIsFavorable = ''N'') OR (SR.AppealJudgements is not null AND SR.AppealIsFavorable = ''N'') OR (SR.SupremeJudgementsDate is not null) OR (SR.EnforcementIsFavorable = ''N'')) AND (SELECT ISNULL(Max(v),''2022-11-30'') FROM (VALUES (SR.PrimaryJudgementsDate), (SR.AppealJudgementsDate), (SR.SupremeJudgementsDate),(SR.EnforcementJudgementsDate)) AS value(v)) > ''2022-11-29'' '
		END
		ELSE IF (@DataFor = 'SUSPENDED')
		BEGIN
			 SET @Where = @Where + ' AND SR.ShowSuspend = 1 AND ( SR.SuspendedFollowerId = ''' + @LawyerId + ''' OR  ''' + @LawyerId + ''' = ''0'') '
		END
		ELSE IF (@DataFor = 'DIFFERENTPANELS')
		BEGIN
			 SET @Where = @Where + ' AND SR.DifferentPanelYesSet = ''Y''  '
		END
		ELSE IF (@DataFor = 'SESONCOMPLETE')
		BEGIN
			 SET @Where = @Where + 'AND ( (SR.SessionFileStatus in (''2'',''4'') OR SR.SessionFileStatus is null) OR (1 = case when SR.SessionLevel = ''3'' then case when SR.PrimaryJDReceiveDate is not null then 1 else 0 end when SR.SessionLevel = ''4'' then case when SR.AppealJDReceiveDate is not null then 1 else 0 end when SR.SessionLevel = ''5'' then case when SR.SupremeJDReceiveDate is not null then 1 else 0 end when SR.SessionLevel = ''6'' then case when SR.EnforcementJDReceiveDate is not null then 1 else 0 end else 1 end) ) '
		END
		ELSE IF (@DataFor = 'SESONONHOLD')
		BEGIN
			 SET @Where = @Where + ' AND CC.OfficeFileStatus in (''OFS-18'',''OFS-57'') '
		END
		ELSE IF (@DataFor = 'JUDGFOLLOW')
		BEGIN
			 SET @Where = @Where + ' AND CC.OfficeFileStatus = ''OFS-17'' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null) OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null) OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null)) AND (SELECT Max(v) FROM (VALUES (SR.PrimaryJudgementsDate), (SR.AppealJudgementsDate), (SR.SupremeJudgementsDate),(SR.EnforcementJudgementsDate)) AS value(v)) > ''2022-11-29'' '
		END
		ELSE IF (@DataFor = 'JUDGCORR')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'BLUESTAMP')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'PROVTIME')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'REDSTAMP')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'CLIENTAPPROV')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'MISSINGDOCS')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'REFUND34')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
	END
ELSE
		
		IF (@DataFor = 'ALLSESSIONS')
		BEGIN
			 SET @Where = @Where + ' AND CC.OfficeFileStatus IN (''OFS-16'',''OFS-31'',''OFS-20'',''OFS-21'') AND 1 = case when CC.CaseLevelCode = ''5'' then 1 else case when CC.NextHearingDate >= CAST(dateadd(HOUR, 4, GETUTCDATE()) as date) then 1 else 0 end end '
		END
		ELSE IF (@DataFor = 'PRINT')
		BEGIN
			 SET @Where = @Where + ' AND CC.NextHearingDate BETWEEN ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + ''' AND ( CC.CaseLevelCode = ''' + @SessionLevel + ''' OR  ''' + @SessionLevel + ''' = ''0'') AND ( CCD.CourtLocationid = ''' + @CountLocationId + ''' OR  ''' + @CountLocationId + ''' = ''0'') AND ( SR.LawyerId = ''' + @LawyerId + ''' OR  ''' + @LawyerId + ''' = ''0'') AND ( CC.ClientCode = ''' + @ClientCode + ''' OR  ''' + @ClientCode + ''' = ''0'') '
		END
		ELSE IF (@DataFor = 'FOLLOW')
		BEGIN
			 SET @Where = @Where + ' AND SR.ShowFollowup = 1  '
		END
		ELSE IF (@DataFor = 'TOBEUPDATE')
		BEGIN
			 SET @Where = @Where + ' AND 1 = case when CC.OfficeFileStatus IN (''OFS-16'',''OFS-31'',''OFS-20'',''OFS-21'') AND CC.NextHearingDate < CAST(dateadd(HOUR, 4, GETUTCDATE()) as date) then 1 else 0 end '
		END
		ELSE IF (@DataFor = 'JUDGEMENT')
		BEGIN
 			 SET @Where = @Where + ' AND CC.OfficeFileStatus = ''OFS-17'' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryIsFavorable = ''N'') OR (SR.AppealJudgements is not null AND SR.AppealIsFavorable = ''N'') OR (SR.SupremeJudgementsDate is not null) OR (SR.EnforcementIsFavorable = ''N'')) AND (SELECT ISNULL(Max(v),''2022-11-30'') FROM (VALUES (SR.PrimaryJudgementsDate), (SR.AppealJudgementsDate), (SR.SupremeJudgementsDate),(SR.EnforcementJudgementsDate)) AS value(v)) > ''2022-11-29'' '
		END
		ELSE IF (@DataFor = 'SUSPENDED')
		BEGIN
			 SET @Where = @Where + ' AND SR.ShowSuspend = 1 AND ( SR.SuspendedFollowerId = ''' + @LawyerId + ''' OR  ''' + @LawyerId + ''' = ''0'') '
		END
		ELSE IF (@DataFor = 'DIFFERENTPANELS')
		BEGIN
			 SET @Where = @Where + ' AND SR.DifferentPanelYesSet = ''Y''  '
		END
		ELSE IF (@DataFor = 'SESONCOMPLETE')
		BEGIN
			 SET @Where = @Where + 'AND ( (SR.SessionFileStatus in (''2'',''4'') OR SR.SessionFileStatus is null) OR (1 = case when SR.SessionLevel = ''3'' then case when SR.PrimaryJDReceiveDate is not null then 1 else 0 end when SR.SessionLevel = ''4'' then case when SR.AppealJDReceiveDate is not null then 1 else 0 end when SR.SessionLevel = ''5'' then case when SR.SupremeJDReceiveDate is not null then 1 else 0 end when SR.SessionLevel = ''6'' then case when SR.EnforcementJDReceiveDate is not null then 1 else 0 end else 1 end) ) '
		END
		ELSE IF (@DataFor = 'SESONONHOLD')
		BEGIN
			 SET @Where = @Where + ' AND CC.OfficeFileStatus  in (''OFS-18'',''OFS-57'') '
		END
		ELSE IF (@DataFor = 'JUDGFOLLOW')
		BEGIN
			 --SET @Where = @Where + ' AND CC.OfficeFileStatus = ''OFS-17'' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) AND (SELECT Max(v) FROM (VALUES (SR.PrimaryJudgementsDate), (SR.AppealJudgementsDate), (SR.SupremeJudgementsDate),(SR.EnforcementJudgementsDate)) AS value(v)) > ''2022-11-29'' '
			   SET @Where = @Where + ' AND CC.OfficeFileStatus = ''OFS-17'' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null) OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null) OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null)) AND (SELECT Max(v) FROM (VALUES (SR.PrimaryJudgementsDate), (SR.AppealJudgementsDate), (SR.SupremeJudgementsDate),(SR.EnforcementJudgementsDate)) AS value(v)) > ''2022-11-29'' '
		END
		ELSE IF (@DataFor = 'JUDGCORR')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'BLUESTAMP')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'PROVTIME')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'REDSTAMP')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'CLIENTAPPROV')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'MISSINGDOCS')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
		ELSE IF (@DataFor = 'REFUND34')
		BEGIN
			 SET @Where = @Where + ' AND ( (SR.PrimaryJudgements is not null AND SR.PrimaryJDReceiveDate is null AND SR.PrimaryIsFavorable = ''Y'') OR (SR.AppealJudgements is not null AND SR.AppealJDReceiveDate is null AND SR.AppealIsFavorable = ''Y'') OR (SR.SupremeJudgementsDate is not null AND SR.SupremeJDReceiveDate is null) OR (SR.EnforcementJudgements is not null AND SR.EnforcementJDReceiveDate is null AND SR.EnforcementIsFavorable = ''Y'')) '
		END
	END

IF(@filter !='')
BEGIN
	SET @FilterText = '
					AND (
						CC.OfficeFileNo LIKE ''%'+CONVERT(VARCHAR,@filter)+'%'' 
						OR CC.AccountContractNo LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR CC.ClientFileNo LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR CC.NextHearingDate LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SessLevel.Mst_Desc LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR CountLocation.Mst_Desc LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SessCaseType.Mst_Desc LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SessClient.Mst_Desc LIKE N''%'+CONVERT(NVARCHAR,@filter)+'%''
						OR CC.SessionRollDefendentName LIKE N''%'+CONVERT(NVARCHAR,@filter)+'%'' 
						OR CC.CourtDecision LIKE ''%'+CONVERT(VARCHAR,@filter)+'%'' 
						OR SessLawyer.Mst_Desc LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SR.LastDate LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SR.WorkRequired LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SR.SuspendedWorkRequired LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SessFollower.Mst_Desc LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						OR SessSusFollower.Mst_Desc LIKE ''%'+CONVERT(VARCHAR,@filter)+'%''
						) '

	SET @SQLCountQuery = 'SELECT @x = COUNT(*) from SessionsRolls SR'+@JoinTables + @Where + @FilterText
END
ELSE
BEGIN
	SET @SQLCountQuery = 'SELECT @x = COUNT(*) from SessionsRolls SR'+@JoinTables + @Where
END
	exec sp_executesql @SQLCountQuery, N'@x int out', @SqlCount out



SET @FinalQuery = 'SELECT * FROM (SELECT ROW_NUMBER() OVER ('+ @OrderBy +') as RowNum, * from (Select '+@SelectColumns+','+CONVERT(VARCHAR,@SqlCount)+' as TotalRecords from SessionsRolls SR '+@JoinTables + @Where + @FilterText +' ) as dat ) AS RowConstrainedResult '+ @FetchLimit 

print @FinalQuery
exec (@FinalQuery)
