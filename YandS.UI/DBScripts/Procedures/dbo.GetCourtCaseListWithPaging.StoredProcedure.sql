SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetCourtCaseListWithPaging]
@Location varchar(1),
@Pageno INT=1,
@filter NVARCHAR(500)='',
@pagesize INT=20,  
@Sorting NVARCHAR(500)='CC.OfficeFileNo',
@SortOrder NVARCHAR(500)='desc',
@DataFor nvarchar(15),
@CaseLevelFilter nvarchar(50) = null,
@DateFrom date,
@DateTo date,
@CallerName varchar(10),
@EnfCourtLocation varchar(10) = null,
@EnfGovernorate varchar(10) = null,
@EnfClientCode varchar(10) = null,
@EnfStage varchar(10) = null,
@EnfAuctionCode varchar(10) = null

AS
BEGIN
SET NOCOUNT ON;
DECLARE @SqlCount INT
DECLARE @From INT = @pageno
DECLARE @SQLQuery NVARCHAR(MAX)
DECLARE @SQLSummaryQuery NVARCHAR(MAX)
DECLARE @FinalQuery NVARCHAR(MAX)
DECLARE @OrderByClause NVARCHAR(MAX)
DECLARE @FilterText NVARCHAR(MAX) = ''
DECLARE @DataForFilter NVARCHAR(500) = ''
DECLARE @MXDate		   DATETIME2 = '1700-01-01'
DECLARE @LCMXDate	DATETIME2 = '1700-01-01'
IF(@Sorting ='CC.OfficeFileNo')
BEGIN
 SET @Sorting = 'dbo.fnMixSort(CC.OfficeFileNo)'
END

IF(@DataFor in ('GENERAL','CASEREP','CLOSEREP','CASEREP_BC','CASEREP_ENF','CASEREP_CONS','CASEREP_AR'))
	BEGIN
	 SET @DataForFilter = ' AND 1=1 '
	END
ELSE IF (CHARINDEX('ENF-', @DataFor) > 0)
	BEGIN
	 SET @DataForFilter = ' AND 1=1 '
	END
ELSE IF (@DataFor = 'BEFORECOURT')
	BEGIN
	 SET @DataForFilter = ' AND CC.CaseLevelCode = ''2'' AND CC.CaseStatus = ''1'' '
	END
ELSE IF (@DataFor = 'PRIMARY')
	BEGIN
	 SET @DataForFilter = ' AND CC.CaseLevelCode = ''3'' AND CC.CaseStatus = ''1'' '
	END
ELSE IF (@DataFor = 'APPEAL')
	BEGIN
	 SET @DataForFilter = ' AND CC.CaseLevelCode = ''4'' AND CC.CaseStatus = ''1'' '
	END
ELSE IF (@DataFor = 'SUPREME')
	BEGIN
	 SET @DataForFilter = ' AND CC.CaseLevelCode = ''5'' AND CC.CaseStatus = ''1'' '
	END
ELSE IF (@DataFor = 'ENFORCEMENT')
	BEGIN
	 SET @DataForFilter = ' AND CC.CaseLevelCode = ''6'' AND CC.CaseStatus = ''1'' '
	END
ELSE IF (@DataFor = 'TOBEREG')
	BEGIN
	 SET @DataForFilter = ' AND CC.CaseLevelCode = ''1'' AND CC.CaseStatus = ''1'' '
	END

IF(@filter !='')
BEGIN
	SET @FilterText = ' and OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							or ClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
							or DefClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%''
							or AccountContractNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
							or ClientFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
							or CourtRefNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
							or COURT like ''%'+CONVERT(NVARCHAR,@filter)+'%''
							or case when CaseStatus = ''2'' then ''CLOSE'' else CaseLevelName end like ''%'+CONVERT(NVARCHAR,@filter)+'%''
							or CaseStatusName like ''%'+CONVERT(NVARCHAR,@filter)+'%''
						   '

END

IF (@DataFor = 'GENERAL')
	BEGIN
	 CREATE TABLE #T1 (
					CaseId int,OfficeFileNo varchar(10),ClientName varchar(2000),DefClientName varchar(200),AgainstName varchar(50),
					ReceptionDate DATETIME2,AccountContractNo varchar(1000),ClientFileNo varchar(1000),CaseTypeName varchar(1000),
					CaseLevelCode varchar(10),CaseLevelName varchar(1000),CaseStatus varchar(10),CaseStatusName varchar(1000),FSort int,
					SSort int,ToBeRegisterDays int,CourtRefNo varchar(1000),COURT varchar(100),CurrentHearingDate DATETIME2,CourtDecision nvarchar(max),
					NextHearingDate DATETIME2,FileStatusName nvarchar(1000)
				 )

		SET @SQLQuery = 'SELECT CC.CaseId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,AgainstMas.Mst_Desc as AgainstName
						,CC.ReceptionDate,CC.AccountContractNo,CC.ClientFileNo,CaseTypeMas.Mst_Desc as CaseTypeName,CC.CaseLevelCode
						,CaseLevelMas.Mst_Desc as CaseLevelName,CC.CaseStatus,CaseStatusMas.Mst_Desc as CaseStatusName,
						case when CC.CaseStatus = ''2'' then 6 else
									case 
										when CC.CaseLevelCode = ''1'' then 1
										when CC.CaseLevelCode = ''3'' then 2
										when CC.CaseLevelCode = ''4'' then 3
										when CC.CaseLevelCode = ''5'' then 4
										when CC.CaseLevelCode = ''2'' then 5
										when CC.CaseLevelCode = ''6'' then 7
										when CC.CaseLevelCode = ''7'' then 6
										else 8
									end
								end as FSort,
								case when CC.CaseStatus = ''2'' then 1 else
									case 
										when CC.CaseLevelCode = ''1'' AND DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) > 40 then 0
										else 1
									end
								end as SSort,case when CC.CaseLevelCode = ''1'' AND CC.CaseStatus = ''1'' then DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) else 0 end as ToBeRegisterDays,
								null as CourtRefNo,null as COURT,CC.CurrentHearingDate,CC.CourtDecision,CC.NextHearingDate
						,case when ( CC.OfficeFileStatus = ''0'' OR CC.OfficeFileStatus is null) then NULL else FileStatus.Mst_Desc end as FileStatusName
						from CourtCases as CC
						join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
						join MASTER_S AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
						join MASTER_S CaseTypeMas on CC.CaseTypeCode = CaseTypeMas.Mst_Value and CaseTypeMas.MstParentId = 14
						join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
						join MASTER_S CaseStatusMas on CC.CaseStatus = CaseStatusMas.Mst_Value and CaseStatusMas.MstParentId = 252 
						Left  join MASTER_S FileStatus on CC.OfficeFileStatus = FileStatus.Mst_Value and FileStatus.MstParentId = 1573
						where CC.CaseStatus != ''-1'' 
						and   CC.CaseTypeCode != ''6'' 
						' + @DataForFilter + '
						and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')'

		SET @OrderByClause = ' order by ISNULL(CurrentHearingDate,''' + CAST(@MXDate AS VARCHAR(30)) + ''') OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

		-- SET ANSI_WARNINGS OFF (FOR CHECKING TRUNCATION
			INSERT into #T1 execute (@SQLQuery )
		-- SET ANSI_WARNINGS ON

			UPDATE  #T1 
			SET		COURT = CourtCasesDetailVW.CourtLocation
			FROM	#T1  
			JOIN    CourtCasesDetailVW on CourtCasesDetailVW.CaseId = #T1.CaseId and CourtCasesDetailVW.CaseLevelCode = #T1.CaseLevelCode
			COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)

			UPDATE  #T1 
			SET		CourtRefNo = CaseNosVW.CASE_NO
			FROM	#T1  
			JOIN    CaseNosVW on CaseNosVW.CaseId = #T1.CaseId
			--COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)


		SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
			, count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
			from #T1
			where CaseStatus != ''-1'' 
			' + @DataForFilter + '
			and (LEFT(OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

		SET @FinalQuery = 'SELECT *,count(*) OVER() AS TotalRecords	FROM #T1 where 1=1 ' + @FilterText + @OrderByClause

		print @FinalQuery
		exec (@FinalQuery)

		print @SQLSummaryQuery
		exec (@SQLSummaryQuery)

		drop table #T1

	END
ELSE IF (@DataFor = 'TRANSELATION')
	BEGIN
	 CREATE TABLE #T9 (
					CaseId int,
					OfficeFileNo varchar(10),
					ClientName nvarchar(2000),
					DefClientName nvarchar(2000),
					ReceptionDate DATETIME2,
					CurrentHearingDate DATETIME2,
					CourtDecision nvarchar(max),
					CaseLevelCode nvarchar(10),
					CaseLevelName nvarchar(1000),
					DaysCounter int,
					CourtRefNo varchar(1000),
					COURT nvarchar(1000),
					FileStatusName nvarchar(1000)
				 )

			IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (
											OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or DefClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CourtDecision like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											)
										   '

				END
		SET @SQLQuery = 'SELECT 
						CC.CaseId,
						CC.OfficeFileNo,
						ClientMas.Mst_Desc as ClientName,
						CC.Defendant as DefClientName,
						CC.ReceptionDate,
						CC.CurrentHearingDate,
						CC.CourtDecision,
						CC.CaseLevelCode,
						CaseLevelMas.Mst_Desc as CaseLevelName,
						case when CC.CurrentHearingDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) end as DaysCounter,
						null as CourtRefNo,
						null as COURT,
						case when ISNULL(CC.OfficeFileStatus,''0'') = ''0'' then null else FileStatus.Mst_Desc end as FileStatusName
						from CourtCases as CC
						join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
						join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
						left join MASTER_S as FileStatus on FileStatus.Mst_Value = CC.OfficeFileStatus and FileStatus.MstParentId = 1573
						where CC.CaseStatus = ''1'' 
						and   CC.Translation = ''1'' 
						and   exists (select * from DecisionTranslations as DT where DT.CaseId = CC.CaseId AND DT.TranslationDone = 0)
						and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')'

		SET @OrderByClause = ' order by 10 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

		-- SET ANSI_WARNINGS OFF (FOR CHECKING TRUNCATION
			INSERT into #T9 execute (@SQLQuery )
		-- SET ANSI_WARNINGS ON

			UPDATE  #T9 
			SET		COURT = CourtCasesDetailVW.CourtLocation
			FROM	#T9  
			JOIN    CourtCasesDetailVW on CourtCasesDetailVW.CaseId = #T9.CaseId and CourtCasesDetailVW.CaseLevelCode = #T9.CaseLevelCode
			COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)

			UPDATE  #T9 
			SET		CourtRefNo = CaseNosVW.CASE_NO
			FROM	#T9  
			JOIN    CaseNosVW on CaseNosVW.CaseId = #T9.CaseId
			--COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)


		SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
			, count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
			from #T9
			where (LEFT(OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

		SET @FinalQuery = 'SELECT *,count(*) OVER() AS TotalRecords	FROM #T9 where 1=1 ' + @FilterText + @OrderByClause

		print @FinalQuery
		exec (@FinalQuery)

		print @SQLSummaryQuery
		exec (@SQLSummaryQuery)

		drop table #T9

	END
ELSE IF (@DataFor = 'CORPORATE')
	BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CC.Subject like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CC.CorporateWorkDetail like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											or FileStatus.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CC.CorporateFee like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT  CC.CaseId,
						CC.OfficeFileNo,
						ClientMas.Mst_Desc as ClientName,
						CC.Defendant as DefClientName,
						CC.ReceptionDate,
						case when CC.ReceptionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) end as DaysCounter,
						CC.Subject,CC.CorporateWorkDetail,CC.CorporateFee,CC.OfficeFileStatus as FileStatus,
						case when ISNULL(CC.OfficeFileStatus,''0'') = ''0'' then null else FileStatus.Mst_Desc end as FileStatusName,
						count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				left join MASTER_S as FileStatus on FileStatus.Mst_Value = CC.OfficeFileStatus and FileStatus.MstParentId = 1573 and FileStatus.Remarks like ''%CORP%''
				where CC.CaseStatus = ''1''
				AND   CC.CaseTypeCode = ''6''
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 6 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
			SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
			, count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
			from CourtCases as CC
			join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
			left join MASTER_S as FileStatus on FileStatus.Mst_Value = CC.OfficeFileStatus and FileStatus.MstParentId = 1573 and FileStatus.Remarks like ''%CORP%''
			where CC.CaseStatus = ''1''
			AND   CC.CaseTypeCode = ''6''
			and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
	END
ELSE IF (@DataFor = 'PRIMARY' OR @DataFor = 'APPEAL' OR @DataFor = 'SUPREME' OR @DataFor = 'ENFORCEMENT')
	BEGIN
	 	 CREATE TABLE #T2 (
					CaseId int,OfficeFileNo varchar(10),ClientName nvarchar(2000),DefClientName nvarchar(2000),AgainstName nvarchar(500),
					ReceptionDate DATETIME2,AccountContractNo varchar(1000),ClientFileNo varchar(1000),CaseTypeName nvarchar(1000),
					CaseLevelCode nvarchar(10),CaseLevelName nvarchar(1000),CaseStatus varchar(10),CaseStatusName nvarchar(1000),FSort int,
					SSort int,ToBeRegisterDays int,CourtRefNo nvarchar(1000),COURT varchar(1000),CurrentHearingDate DATETIME2,CourtDecision nvarchar(max),
					NextHearingDate DATETIME2,FileStatusName nvarchar(1000)
				 )

		SET @SQLQuery = 'SELECT CC.CaseId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,AgainstMas.Mst_Desc as AgainstName
						,CC.ReceptionDate,CC.AccountContractNo,CC.ClientFileNo,CaseTypeMas.Mst_Desc as CaseTypeName,CC.CaseLevelCode
						,CaseLevelMas.Mst_Desc as CaseLevelName,CC.CaseStatus,CaseStatusMas.Mst_Desc as CaseStatusName,
						case when CC.CourtDecision is null then 0 else 1 end as FSort,
						case when CC.NextHearingDate is null then 0 else 1 end as SSort,
						case when CC.CurrentHearingDate is not null then DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) else 0 end as ToBeRegisterDays,
						null as CourtRefNo,null as COURT,CC.CurrentHearingDate,CC.CourtDecision,CC.NextHearingDate
						,case when ( CC.OfficeFileStatus = ''0'' OR CC.OfficeFileStatus is null) then NULL else FileStatus.Mst_Desc end as FileStatusName
						from CourtCases as CC
						join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
						join MASTER_S AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
						join MASTER_S CaseTypeMas on CC.CaseTypeCode = CaseTypeMas.Mst_Value and CaseTypeMas.MstParentId = 14
						join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
						join MASTER_S CaseStatusMas on CC.CaseStatus = CaseStatusMas.Mst_Value and CaseStatusMas.MstParentId = 252 
						Left  join MASTER_S FileStatus on CC.OfficeFileStatus = FileStatus.Mst_Value and FileStatus.MstParentId = 1573 
						where CC.CaseStatus != ''-1'' 
						' + @DataForFilter + '
						and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')'

		SET @OrderByClause = ' order by FSort,SSort,ToBeRegisterDays desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

		-- SET ANSI_WARNINGS OFF (FOR CHECKING TRUNCATION
			INSERT into #T2 execute (@SQLQuery )
		-- SET ANSI_WARNINGS ON

			
			
			UPDATE  #T2 
			SET		COURT = CourtCasesDetailVW.CourtLocation
			FROM	#T2  
			JOIN    CourtCasesDetailVW on CourtCasesDetailVW.CaseId = #T2.CaseId and CourtCasesDetailVW.CaseLevelCode = #T2.CaseLevelCode
			COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)

			UPDATE  #T2 
			SET		CourtRefNo = CaseNosVW.CASE_NO
			FROM	#T2  
			JOIN    CaseNosVW on CaseNosVW.CaseId = #T2.CaseId

		SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(CC.OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
			, count(case when left(CC.OfficeFileNo,1) = ''S'' then 1 end) SLLRecords
			from #T2 as CC
			where CC.CaseStatus != ''-1'' 
			' + @DataForFilter + '
			and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

		SET @FinalQuery = 'SELECT *,count(*) OVER() AS TotalRecords	FROM #T2 where 1=1 ' + @FilterText + @OrderByClause

		print @FinalQuery
		exec (@FinalQuery)

		print @SQLSummaryQuery
		exec (@SQLSummaryQuery)

		drop table #T2


	END
ELSE IF (@DataFor = 'TOBEREG')
	BEGIN
		IF(@filter !='')
		BEGIN
			SET @FilterText = ' and OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or ClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or DefClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or AccountContractNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or ClientFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or FileStatus like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or case when CaseStatus = ''2'' then ''CLOSE'' else CaseLevelName end like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or CaseStatusName like ''%'+CONVERT(NVARCHAR,@filter)+'%''

								   '

		END

	 CREATE TABLE #T3 (
					CaseId int,OfficeFileNo varchar(10),ClientName varchar(200),DefClientName varchar(200),AgainstName varchar(50),
					ReceptionDate DATETIME2,AccountContractNo varchar(100),ClientFileNo varchar(100),CaseTypeName varchar(100),
					CaseLevelCode varchar(10),CaseLevelName varchar(100),CaseStatus varchar(10),CaseStatusName varchar(100),
					FSort int,ToBeRegisterDays int,FileStatus varchar(100),ActionDate DATETIME2
				 )

		SET @SQLQuery = 'SELECT CC.CaseId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,AgainstMas.Mst_Desc as AgainstName
						,CC.ReceptionDate,CC.AccountContractNo,CC.ClientFileNo,CaseTypeMas.Mst_Desc as CaseTypeName,CC.CaseLevelCode
						,CaseLevelMas.Mst_Desc as CaseLevelName,CC.CaseStatus,CaseStatusMas.Mst_Desc as CaseStatusName,
						case when CC.CaseLevelCode = ''1'' AND CC.CaseStatus = ''1'' AND DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) >= 60 then 1 else 0 end as FSort,
						case when CC.CaseLevelCode = ''1'' AND CC.CaseStatus = ''1'' then DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) else 0 end as ToBeRegisterDays,
						null as FileStatus,null as ActionDate
						from CourtCases as CC
						join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
						join MASTER_S AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
						join MASTER_S CaseTypeMas on CC.CaseTypeCode = CaseTypeMas.Mst_Value and CaseTypeMas.MstParentId = 14
						join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
						join MASTER_S CaseStatusMas on CC.CaseStatus = CaseStatusMas.Mst_Value and CaseStatusMas.MstParentId = 252 
						where CC.CaseStatus != ''-1'' 
						' + @DataForFilter + '
						and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')'

		SET @OrderByClause = ' order by 14 desc,ReceptionDate OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

		-- SET ANSI_WARNINGS OFF (FOR CHECKING TRUNCATION
			INSERT into #T3 execute (@SQLQuery )
		-- SET ANSI_WARNINGS ON

			UPDATE  #T3 
			SET		FileStatus = CaseRegistrationsVW.FileStatusName, ActionDate = CaseRegistrationsVW.ActionDate
			FROM	#T3  
			JOIN    CaseRegistrationsVW on CaseRegistrationsVW.CaseId = #T3.CaseId
			--COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)

		SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(CC.OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
			, count(case when left(CC.OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
			from #T3 as CC
			where CC.CaseStatus != ''-1'' 
			' + @DataForFilter + '
			and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

		SET @FinalQuery = 'SELECT *,count(*) OVER() AS TotalRecords	FROM #T3 where 1=1 ' + @FilterText + @OrderByClause

		print @FinalQuery
		exec (@FinalQuery)

		print @SQLSummaryQuery
		exec (@SQLSummaryQuery)

		drop table #T3

	END
ELSE IF (@DataFor in ('CASEREP','CLOSEREP','CASEREP_BC','CASEREP_ENF','CASEREP_CONS','CASEREP_AR'))
	BEGIN
	DECLARE @StatusCode VARCHAR(1) = '1'
		
		IF(@DataFor ='CLOSEREP')
		BEGIN
			SET @StatusCode= '2'
		END

		IF(@filter !='')
		BEGIN
		  IF(@DataFor ='CLOSEREP')
			BEGIN
				SET @FilterText = ' and OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or ClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or DefClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or CaseLevelName  like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									'
			END
		  ELSE IF(@DataFor ='CASEREP_ENF')
			BEGIN
				SET @FilterText = ' and OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or ClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or DefClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or COURT like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									or CurrentEnforcementLevel like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									'
			END
		  ELSE
			BEGIN
				SET @FilterText = ' and OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or ClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
									or DefClientName like ''%'+CONVERT(NVARCHAR,@filter)+'%''
									'
			END
		END

	 CREATE TABLE #T5 (
					CaseId int,OfficeFileNo varchar(10),ClientName varchar(200),DefClientName varchar(200),AgainstName varchar(50),
					ReceptionDate DATETIME2,AccountContractNo varchar(100),ClientFileNo varchar(100),CaseTypeName varchar(100),
					CaseLevelCode varchar(10),CaseLevelName varchar(100),CaseStatus varchar(10),CaseStatusName varchar(100),FSort int,
					SSort int,ToBeRegisterDays int,CurrentHearingDate DATETIME2,CourtDecision nvarchar(max),COURT varchar(100),
					ClosureDate DATETIME2,CurrentEnforcementLevel nvarchar(100)
				 )

		SET @SQLQuery = 'SELECT CC.CaseId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,AgainstMas.Mst_Desc as AgainstName
						,CC.ReceptionDate,CC.AccountContractNo,CC.ClientFileNo,CaseTypeMas.Mst_Desc as CaseTypeName,CC.CaseLevelCode
						,CaseLevelMas.Mst_Desc as CaseLevelName,CC.CaseStatus,CaseStatusMas.Mst_Desc as CaseStatusName,
						case when CC.CaseStatus = ''2'' then 6 else
									case 
										when CC.CaseLevelCode = ''1'' then 1
										when CC.CaseLevelCode = ''3'' then 2
										when CC.CaseLevelCode = ''4'' then 3
										when CC.CaseLevelCode = ''5'' then 4
										when CC.CaseLevelCode = ''2'' then 5
										when CC.CaseLevelCode = ''6'' then 7
										when CC.CaseLevelCode = ''7'' then 6
										else 8
									end
								end as FSort,
								case when CC.CaseStatus = ''2'' then 1 else
									case 
										when CC.CaseLevelCode = ''1'' AND DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) > 40 then 0
										else 1
									end
								end as SSort,case when CC.CaseLevelCode = ''1'' AND CC.CaseStatus = ''1'' then DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) else 0 end as ToBeRegisterDays,
								CC.CurrentHearingDate,CC.CourtDecision,null as COURT, CC.ClosureDate, null as CurrentEnforcementLevel
						from CourtCases as CC
						join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
						join MASTER_S AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
						join MASTER_S CaseTypeMas on CC.CaseTypeCode = CaseTypeMas.Mst_Value and CaseTypeMas.MstParentId = 14
						join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
						join MASTER_S CaseStatusMas on CC.CaseStatus = CaseStatusMas.Mst_Value and CaseStatusMas.MstParentId = 252 
						where CC.CaseStatus = ''' + @StatusCode + '''
						and   (CC.CaseLevelCode = ''' + @CaseLevelFilter + ''' OR ''' + @CaseLevelFilter + ''' = ''0'')
						' + @DataForFilter + '
						and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')'

		SET @OrderByClause = ' order by 14,ISNULL(CurrentHearingDate,''' + CAST(@MXDate AS VARCHAR(30)) + ''') OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

		-- SET ANSI_WARNINGS OFF (FOR CHECKING TRUNCATION
			INSERT into #T5 execute (@SQLQuery )
		-- SET ANSI_WARNINGS ON

			UPDATE  #T5 
			SET		COURT = CourtCasesDetailVW.CourtLocation, CurrentEnforcementLevel = CourtCasesDetailVW.CurrentEnforcementLevel
			FROM	#T5  
			JOIN    CourtCasesDetailVW on CourtCasesDetailVW.CaseId = #T5.CaseId and CourtCasesDetailVW.CaseLevelCode = #T5.CaseLevelCode
			COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)

		SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
			, count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
			from #T5
			where CaseStatus = ''' + @StatusCode + '''
			' + @DataForFilter + '
			and (LEFT(OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

		SET @FinalQuery = 'SELECT *,count(*) OVER() AS TotalRecords	FROM #T5 where 1=1 ' + @FilterText + @OrderByClause

		print @FinalQuery
		exec (@FinalQuery)

		print @SQLSummaryQuery
		exec (@SQLSummaryQuery)

		drop table #T5

	END
ELSE IF (CHARINDEX('ENF-', @DataFor) > 0)
	BEGIN
	 IF (@DataFor = 'ENF-GENERAL') 
		BEGIN

			IF (@EnfGovernorate = '99')
				BEGIN SET @DataForFilter = ' AND CC.GovernorateId in (''3'',''4'') ' END
			ELSE
				BEGIN SET @DataForFilter = ' AND (CC.GovernorateId = ''' + @EnfGovernorate + ''' OR ''' + @EnfGovernorate + ''' = ''0'') ' END

			SET @DataForFilter += ' AND (CCE.CourtLocationid = ''' + @EnfCourtLocation + ''' OR ''' + @EnfCourtLocation + ''' = ''0'') AND (CC.OfficeFileStatus = ''' + @EnfStage + ''' OR ''' + @EnfStage + ''' = ''0'') AND (CC.ClientCode = ''' + @EnfClientCode + ''' OR ''' + @EnfClientCode + ''' = ''0'') '

		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ENFLVL.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											or GOV.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CC.AccountContractNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CC.ClientFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT  CC.CaseId,
						CC.OfficeFileNo,
						ClientMas.Mst_Desc as ClientName,
						CC.Defendant as DefClientName,
						case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
						CCE.EnforcementNo as CourtRefNo,
						case when CC.OfficeFileStatus = ''0'' then null else ENFLVL.Mst_Desc end as CurrentEnforcementLevel,
						case when CC.CurrentHearingDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) end as DaysCounter,
						CC.CourtDecision,
						CC.AccountContractNo,
						CC.ClientFileNo,
						Case 
							When CCE.EnforcementlevelId = ''OFS-39'' Then 1
							When CCE.EnforcementlevelId = ''OFS-22'' Then 2
							When CCE.EnforcementlevelId = ''OFS-23'' Then 3
							When CCE.EnforcementlevelId = ''OFS-24'' Then 4
							When CCE.EnforcementlevelId = ''OFS-26'' Then 5
							When CCE.EnforcementlevelId = ''OFS-30'' Then 6
							When CCE.EnforcementlevelId = ''OFS-28'' Then 7
							When CCE.EnforcementlevelId = ''OFS-25'' Then 8
							When CCE.EnforcementlevelId = ''OFS-27'' Then 9
							else 10
						End  as fSort,
						CC.GovernorateId,
						case when ISNULL(CC.GovernorateId,''0'') = ''0'' then null else GOV.Mst_Desc end as GovernorateName,
						count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573 and ENFLVL.Remarks like ''%ENF%''
				left join MASTER_S as GOV on GOV.Mst_Value = CC.GovernorateId and GOV.MstParentId = 1153
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 12,8 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
										, count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
										from CourtCases as CC
											join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
											join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
											left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
											join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573 and ENFLVL.Remarks like ''%ENF%''
											left join MASTER_S as GOV on GOV.Mst_Value = CC.GovernorateId and GOV.MstParentId = 1153
											where CC.CaseStatus = ''1''
											AND   CC.CaseLevelCode = ''6''
											' + @DataForFilter + '
											and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-ONLINEREG') 
		BEGIN

		  SET @DataForFilter += ' AND (CC.GovernorateId = ''' + @EnfGovernorate + ''' OR ''' + @EnfGovernorate + ''' = ''0'') AND (CCE.CourtLocationid = ''' + @EnfCourtLocation + ''' OR ''' + @EnfCourtLocation + ''' = ''0'') AND (CC.ClientCode = ''' + @EnfClientCode + ''' OR ''' + @EnfClientCode + ''' = ''0'') '

		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.RegistrationDate like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,
				case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,
				ClientMas.Mst_Desc as ClientName,
				CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,
				CCE.RegistrationDate as ReceptionDate,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,
				case when CCE.RegistrationDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.RegistrationDate),dateadd(hour , 11,GETDATE())) end as ToBeRegisterDays,
				count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-22''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-22''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-ANN') 
		BEGIN
		  SET @DataForFilter += ' AND (CC.GovernorateId = ''' + @EnfGovernorate + ''' OR ''' + @EnfGovernorate + ''' = ''0'') AND (CCE.CourtLocationid = ''' + @EnfCourtLocation + ''' OR ''' + @EnfCourtLocation + ''' = ''0'') AND (CC.ClientCode = ''' + @EnfClientCode + ''' OR ''' + @EnfClientCode + ''' = ''0'') '

		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ANN.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,
				case when CCE.AnnouncementTypeId = ''0'' then null else ANN.Mst_Desc end as AnnouncementType,
				CCE.AnnouncementCompleteDt
				,case when DATEDIFF(DAY, CCE.AnnouncementCompleteDt,dateadd(hour , 11,GETDATE())) >= 0 then DATEDIFF(DAY, CCE.AnnouncementCompleteDt,dateadd(hour , 11,GETDATE())) else -1 end as ToBeRegisterDays
				,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as ANN on ANN.Mst_Value = CCE.AnnouncementTypeId and ANN.MstParentId = 1288
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-23''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 12 desc, 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as ANN on ANN.Mst_Value = CCE.AnnouncementTypeId and LOC.MstParentId = 1288
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-23''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-INQ') 
		BEGIN
		  SET @DataForFilter += ' AND (CC.GovernorateId = ''' + @EnfGovernorate + ''' OR ''' + @EnfGovernorate + ''' = ''0'') AND (CCE.CourtLocationid = ''' + @EnfCourtLocation + ''' OR ''' + @EnfCourtLocation + ''' = ''0'') AND (CC.ClientCode = ''' + @EnfClientCode + ''' OR ''' + @EnfClientCode + ''' = ''0'') '

		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.SubmissionDt like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.ApprovalDt like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.InquiryDt like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,CCE.SubmissionDt,CCE.ApprovalDt	,CCE.InquiryDt,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-24''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-24''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-ARREQ') 
		BEGIN

		  SET @DataForFilter += ' AND (CC.GovernorateId = ''' + @EnfGovernorate + ''' OR ''' + @EnfGovernorate + ''' = ''0'') AND (CCE.CourtLocationid = ''' + @EnfCourtLocation + ''' OR ''' + @EnfCourtLocation + ''' = ''0'') AND (CC.ClientCode = ''' + @EnfClientCode + ''' OR ''' + @EnfClientCode + ''' = ''0'') '

		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-26''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-26''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-ARORD') 
		BEGIN

		  SET @DataForFilter += ' AND (CC.GovernorateId = ''' + @EnfGovernorate + ''' OR ''' + @EnfGovernorate + ''' = ''0'') AND (CCE.CourtLocationid = ''' + @EnfCourtLocation + ''' OR ''' + @EnfCourtLocation + ''' = ''0'') AND (CC.ClientCode = ''' + @EnfClientCode + ''' OR ''' + @EnfClientCode + ''' = ''0'') '

		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.ArrestedDate like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.ArrestOrderIssueDate like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.ArrestOrderNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or POL.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				case when CCE.PoliceStationid = ''0'' then null else POL.Mst_Desc end as PoliceStationName,
				CCE.EnforcementNo as CourtRefNo,CCE.ArrestOrderNo,CCE.ArrestOrderIssueDate,CCE.ArrestedDate,
				case when CC.CurrentHearingDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) end as DaysCounter,
				CC.CourtDecision,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as POL on POL.Mst_Value = CCE.PoliceStationid and POL.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-27''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 12 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as POL on POL.Mst_Value = CCE.PoliceStationid and POL.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-27''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-JUDSALE') 
		BEGIN

		  SET @DataForFilter += ' AND (CC.GovernorateId = ''' + @EnfGovernorate + ''' OR ''' + @EnfGovernorate + ''' = ''0'') AND (CCE.CourtLocationid = ''' + @EnfCourtLocation + ''' OR ''' + @EnfCourtLocation + ''' = ''0'') AND (CC.ClientCode = ''' + @EnfClientCode + ''' OR ''' + @EnfClientCode + ''' = ''0'') AND (CCE.AuctionProcess = ''' + @EnfAuctionCode + ''' OR ''' + @EnfAuctionCode + ''' = ''0'') '

		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.JUDAuctionDate like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.JUDAuctionRepeat like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.JUDAuctionValue like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or AP.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				case when CCE.AuctionProcess = ''0'' then null else AP.Mst_Desc end as AuctionProcess,
				CCE.EnforcementNo as CourtRefNo,CCE.JUDAuctionDate,CCE.JUDAuctionRepeat,CCE.JUDAuctionValue,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,
				case when JUDAuctionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.JUDAuctionDate),dateadd(hour , 11,GETDATE())) end as ToBeRegisterDays,
				CCE.AuctionProcess as Mst_Value, count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as AP on AP.Mst_Value = CCE.AuctionProcess and AP.MstParentId = 1290
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-25''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 14 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as AP on AP.Mst_Value = CCE.AuctionProcess and AP.MstParentId = 1290
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-25''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-SUSPEND') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.SuspensionPeriod like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or DATEDIFF(DAY, dateadd(hour , 11,GETDATE()),dateadd(day , CCE.SuspensionPeriod, CCE.SuspensionStartDate)) like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or JD.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or SC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				case when CCE.JUDDecisionId = ''0'' then null else JD.Mst_Desc end as JUDDecision,
				case when CCE.SuspensionCauseId = ''0'' then null else SC.Mst_Desc end as SuspensionCause,
				case when (CCE.SuspensionPeriod = 0 OR CCE.SuspensionPeriod is null)  then 99999 else DATEDIFF(DAY, dateadd(hour , 11,GETDATE()),dateadd(day , CCE.SuspensionPeriod, CCE.ActionDate)) end as ToBeRegisterDays,
				case when CCE.SuspensionCauseId = ''1'' AND CCE.JUDDecisionId = ''0'' AND DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) >= 5 then DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) else 0 end as PausedDays,
				case when CCE.SuspensionPeriod > 0 then DATEDIFF(DAY, dateadd(hour , 11,GETDATE()),dateadd(day , CCE.SuspensionPeriod, CCE.ActionDate)) end as DaysRemaining,
				CCE.EnforcementNo as CourtRefNo,CCE.SuspensionPeriod,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				dateadd(day , CCE.SuspensionPeriod, CCE.ActionDate) as SuspensionEndDate,CC.CourtDecision,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as JD on JD.Mst_Value = CCE.JUDDecisionId and JD.MstParentId = 1291
				left join MASTER_S as SC on SC.Mst_Value = CCE.SuspensionCauseId and SC.MstParentId = 272
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-28''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				AND   ISNULL(CCE.ActionDate,''01-01-1950'') BETWEEN  ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + '''
				'

			SET @OrderByClause = ' order by 10 desc, 9 OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as JD on JD.Mst_Value = CCE.JUDDecisionId and JD.MstParentId = 1291
				left join MASTER_S as SC on SC.Mst_Value = CCE.SuspensionCauseId and SC.MstParentId = 272
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-28''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				AND   ISNULL(CCE.ActionDate,''01-01-1950'') BETWEEN  ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + ''' '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-AGAINSUR') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,
				case when CCE.RegistrationDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.RegistrationDate),dateadd(hour , 11,GETDATE())) end as ToBeRegisterDays,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.AgainstInsurance = ''2''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 8 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.AgainstInsurance = ''2''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-DEFTRANS') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or DTR.MoneyTrRequestDate like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ENFLVL.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when DTR.TransferDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, DTR.TransferDate),dateadd(hour , 11,GETDATE())) end as DaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,DTR.TransferDate,DTR.MoneyTrRequestDate,DTR.DefendentTransferId,
				case when CCE.EnforcementlevelId = ''0'' then null else ENFLVL.Mst_Desc end as CurrentEnforcementLevel,
				count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				JOIN DefendentTransfer DTR on DTR.CaseId = CC.CaseId and DTR.CaseLevelCode = CC.CaseLevelCode
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CCE.EnforcementlevelId and ENFLVL.MstParentId = 265
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   DTR.MoneyTrRequestDate is not null
				AND   DTR.MoneyTrCompleteDate is null
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				JOIN DefendentTransfer DTR on DTR.CaseId = CC.CaseId and DTR.CaseLevelCode = CC.CaseLevelCode
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CCE.EnforcementlevelId and ENFLVL.MstParentId = 265
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   DTR.MoneyTrRequestDate is not null
				AND   DTR.MoneyTrCompleteDate is null
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-DEFTRANS2') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or DTR.TransferDate like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or DTR.MoneyTrCompleteDate like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or DTR.Amount like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DTR.DefendentTransferId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,DTR.TransferDate,DTR.MoneyTrCompleteDate,DTR.Amount,
				count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				JOIN DefendentTransfer DTR on DTR.CaseId = CC.CaseId and DTR.CaseLevelCode = CC.CaseLevelCode
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   DTR.MoneyTrCompleteDate is not null
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				JOIN DefendentTransfer DTR on DTR.CaseId = CC.CaseId and DTR.CaseLevelCode = CC.CaseLevelCode
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   DTR.MoneyTrCompleteDate is not null
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
 	 ELSE IF (@DataFor = 'ENF-CLREPLY') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (
											CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.CourtLocation like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CaseLevelMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.CourtRefNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.CurrentEnforcementLevel like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											)
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				CCE.CourtLocation as COURT,
				CCE.CourtRefNo as CourtRefNo,
				case when CC.FirstEmailDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CC.FirstEmailDate),dateadd(hour , 11,GETDATE())) end as ToBeRegisterDays,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,CC.Requirements,CC.FirstEmailDate, U.[UserName] as UserNo,
				CCE.CurrentEnforcementLevel as CurrentEnforcementLevel,CC.AccountContractNo,CC.ClientFileNo,CaseLevelMas.Mst_Desc as CaseLevelName,
				count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				left join CourtCasesDetailView CCE on CCE.CaseId = CC.CaseId
				Left  join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
				left join USERS as U on U.[UserId] = ISNULL(CC.UpdatedBy,CC.CreatedBy)
				where CC.CaseStatus = ''1''
				AND   CC.ClientReply = ''1''
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 8 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				left join CourtCasesDetailView CCE on CCE.CaseId = CC.CaseId
				Left  join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
				left join USERS as U on U.[UserId] = ISNULL(CC.UpdatedBy,CC.CreatedBy)
				where CC.CaseStatus = ''1''
				AND   CC.ClientReply = ''1''
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-RECFRCOURT') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ENFLVL.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.DateOfInstruction like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LAW.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or COR.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				case when CCE.CauseOfRecoveryId = ''0'' then null else COR.Mst_Desc end as CauseOfRecovery,
				case when CCE.LawyerId = ''0'' then null else LAW.Mst_Desc end as LawyerName,
				CCE.EnforcementNo as CourtRefNo,CCE.DateOfInstruction,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				case when CC.OfficeFileStatus = ''0'' then null else ENFLVL.Mst_Desc end as CurrentEnforcementLevel,
				CC.CourtDecision,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as COR on COR.Mst_Value = CCE.CauseOfRecoveryId and COR.MstParentId = 1292
				left join MASTER_S as LAW on LAW.Mst_Value = CCE.LawyerId and LAW.MstParentId = 1408
				join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573 and ENFLVL.Remarks like ''%ENF%''
				where 1 = CASE WHEN CC.JudRecRedStamp = ''1'' THEN 1 ELSE CASE WHEN CC.CaseStatus = ''1'' AND CC.CaseLevelCode = ''6'' AND CC.OfficeFileStatus IN (''OFS-29'',''OFS-30'') AND CCE.EnforcementBy in (''0'', ''1'') THEN 1 ELSE 0 END	END
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				--AND   ISNULL(CCE.ActionDate,''01-01-1950'') BETWEEN  ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + '''
				and   (CC.OfficeFileStatus = ''' + @CallerName + ''' OR ''' + @CallerName + ''' = ''0'')

				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as COR on COR.Mst_Value = CCE.CauseOfRecoveryId and COR.MstParentId = 1292
				left join MASTER_S as LAW on LAW.Mst_Value = CCE.LawyerId and LAW.MstParentId = 1408
				join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573 and ENFLVL.Remarks like ''%ENF%''
				where 1 = CASE WHEN CC.JudRecRedStamp = ''1'' THEN 1 ELSE CASE WHEN CC.CaseStatus = ''1'' AND CC.CaseLevelCode = ''6'' AND CC.OfficeFileStatus IN (''OFS-29'',''OFS-30'') AND CCE.EnforcementBy in (''0'', ''1'') THEN 1 ELSE 0 END	END
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				--AND   ISNULL(CCE.ActionDate,''01-01-1950'') BETWEEN  ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + '''
				and   (CC.OfficeFileStatus = ''' + @CallerName + ''' OR ''' + @CallerName + ''' = ''0'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-COURTFOL') 
		BEGIN
		  IF(@filter !='')
				BEGIN
				   --insert into tbl_filter_text (filter_text) values (@filter)

					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LAW.Mst_Desc like N''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ENFLVL.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				case when CCE.LawyerId = ''0'' then null else LAW.Mst_Desc end as LawyerName,
				case when CC.CommissioningDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CC.CommissioningDate),dateadd(hour , 11,GETDATE())) end as ToBeRegisterDays,
				CCE.EnforcementNo as CourtRefNo,
				case when CC.OfficeFileStatus = ''0'' then null else ENFLVL.Mst_Desc end as CurrentEnforcementLevel,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,CC.CommissioningDate,CC.CourtFollowRequirement,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as LAW on LAW.Mst_Value = CCE.LawyerId and LAW.MstParentId = 1408
				left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573
				where CC.CaseStatus = ''1''
				AND   CC.CourtFollow = ''1''
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 8 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as LAW on LAW.Mst_Value = CCE.LawyerId and LAW.MstParentId = 1408
				left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.CourtFollow = ''1''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause
			--insert into tbl_filter_query (filter_text) values (@FinalQuery)
			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-AGNCLIENT') 
		BEGIN
		  IF(@filter !='')
				BEGIN
				   --insert into tbl_filter_text (filter_text) values (@filter)

					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ENFLVL.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,
				case when CC.OfficeFileStatus = ''0'' then null else ENFLVL.Mst_Desc end as CurrentEnforcementLevel,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CCE.EnforcementBy = ''2''
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 9 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CCE.EnforcementBy = ''2''
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause
			--insert into tbl_filter_query (filter_text) values (@FinalQuery)
			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-SEALOFJ') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				(
				 select CourtRefNo 
				 from (
						SELECT CaseId,max(CourtRefNo) as CourtRefNo FROM ( SELECT CCD.CaseId,CCD.CourtRefNo ,rank() over (partition by CCD.CaseId order by CCD.RegistrationDate desc) as rnk FROM CourtCasesDetails as CCD ) detData where rnk = 1 group by CaseId) CaseNoData
						where CaseNoData.CaseId =  CC.CaseId
				 )
				 as CourtRefNo,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,
				CCE.AnnouncementCompleteDt
				,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-56''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-56''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-FILEREV') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,
				CCE.AnnouncementCompleteDt
				,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-39''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-39''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-TOMJUDG') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT CC.CaseId,case when CCE.ActionDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CCE.ActionDate),dateadd(hour , 11,GETDATE())) end as AnotherDaysCounter,
				CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,
				case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
				CCE.EnforcementNo as CourtRefNo,
				DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) as DaysCounter,
				CC.CourtDecision,
				CCE.AnnouncementCompleteDt
				,count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-19''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 2 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = '
				SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
					 , count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   CC.OfficeFileStatus = ''OFS-19''
				AND   CCE.EnforcementBy in (''0'', ''1'')
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-CONTRESULT') 
		BEGIN
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ENFLVL.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.DEF_DateOfContact like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CALLER.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.DEF_VisitDate like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT  CC.CaseId,
						CC.OfficeFileNo,
						ClientMas.Mst_Desc as ClientName,
						CC.Defendant as DefClientName,
						case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
						CCE.EnforcementNo as CourtRefNo,
						case when CC.OfficeFileStatus = ''0'' then null else ENFLVL.Mst_Desc end as CurrentEnforcementLevel,
						CCE.DEF_DateOfContact,
						case when CCE.DEF_CallerName is null then null else CALLER.Mst_Desc end as DEF_CallerName,
						CCE.DEF_VisitDate,
						(SELECT Max(v) FROM (VALUES (CCE.DEF_DateOfContact), (CCE.DEF_VisitDate)) AS value(v)) as [MaxDate],
						CCE.DEF_Corresponding as Notes, count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573
				left join MASTER_S as CALLER on CALLER.Mst_Value = CCE.DEF_CallerName and CALLER.MstParentId = 1408
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				AND   (SELECT Max(v) FROM (VALUES (CCE.DEF_DateOfContact), (CCE.DEF_VisitDate)) AS value(v)) is not null
				AND   CCE.DEF_DateOfContact <= dateadd(day, 10 , dateadd(hour , 11,GETDATE()))
				and   (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				AND   ISNULL(CCE.DEF_DateOfContact,''01-01-1950'') BETWEEN  ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + '''
				and   (CCE.DEF_CallerName = ''' + @CallerName + ''' OR ''' + @CallerName + ''' = ''0'')
				'

			SET @OrderByClause = ' order by 11 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
										, count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
										from CourtCases as CC
											join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
											join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
											left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
											left join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573
											left join MASTER_S as CALLER on CALLER.Mst_Value = CCE.DEF_CallerName and CALLER.MstParentId = 1408
											where CC.CaseStatus = ''1''
											AND   CC.CaseLevelCode = ''6''
											AND   ISNULL(CCE.DEF_DateOfContact,''01-01-1950'') BETWEEN  ''' + CAST(@DateFrom AS VARCHAR(30)) + ''' AND ''' + CAST(@DateTo AS VARCHAR(30)) + '''
											and   (CCE.DEF_CallerName = ''' + @CallerName + ''' OR ''' + @CallerName + ''' = ''0'')
											and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	 ELSE IF (@DataFor = 'ENF-GOVERN') 
		
		BEGIN
		 IF (@CallerName = '47')
			BEGIN SET @DataForFilter = ' AND CC.GovernorateId in (''3'',''4'') ' END
		 ELSE IF (@CallerName = '48')
			BEGIN SET @DataForFilter = ' AND CCE.CourtLocationid = ''4'' ' END
		 ELSE IF (@CallerName = '29')
			BEGIN SET @DataForFilter = ' AND CC.GovernorateId = ''5'' ' END
		END
		
		BEGIN
		
		  IF(@filter !='')
				BEGIN
					SET @FilterText = ' and (CC.OfficeFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or ClientMas.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%'' 
											or CC.Defendant like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or LOC.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CCE.EnforcementNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or ENFLVL.Mst_Desc like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CC.AccountContractNo like ''%'+CONVERT(NVARCHAR,@filter)+'%''
											or CC.ClientFileNo like ''%'+CONVERT(NVARCHAR,@filter)+'%'')
										   '

				END
			
			SET @SQLQuery = '
				SELECT  CC.CaseId,
						CC.OfficeFileNo,
						ClientMas.Mst_Desc as ClientName,
						CC.Defendant as DefClientName,
						case when CCE.CourtLocationid = ''0'' then null else LOC.Mst_Desc end as COURT,
						CCE.EnforcementNo as CourtRefNo,
						case when CC.OfficeFileStatus = ''0'' then null else ENFLVL.Mst_Desc end as CurrentEnforcementLevel,
						case when CC.CurrentHearingDate is null then 0 else DATEDIFF(DAY, dateadd(hour , 11, CC.CurrentHearingDate),dateadd(hour , 11,GETDATE())) end as DaysCounter,
						CC.CourtDecision,
						CC.AccountContractNo,
						CC.ClientFileNo,
						Case 
							When CCE.EnforcementlevelId = ''OFS-39'' Then 1
							When CCE.EnforcementlevelId = ''OFS-22'' Then 2
							When CCE.EnforcementlevelId = ''OFS-23'' Then 3
							When CCE.EnforcementlevelId = ''OFS-24'' Then 4
							When CCE.EnforcementlevelId = ''OFS-26'' Then 5
							When CCE.EnforcementlevelId = ''OFS-30'' Then 6
							When CCE.EnforcementlevelId = ''OFS-28'' Then 7
							When CCE.EnforcementlevelId = ''OFS-25'' Then 8
							When CCE.EnforcementlevelId = ''OFS-27'' Then 9
							else 10
						End  as fSort,
						CC.GovernorateId,
						case when ISNULL(CC.GovernorateId,''0'') = ''0'' then null else GOV.Mst_Desc end as GovernorateName,
						count(*) OVER() AS TotalRecords
				from CourtCases as CC
				join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
				join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
				left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
				join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573 and ENFLVL.Remarks like ''%ENF%''
				left join MASTER_S as GOV on GOV.Mst_Value = CC.GovernorateId and GOV.MstParentId = 1153
				where CC.CaseStatus = ''1''
				AND   CC.CaseLevelCode = ''6''
				' + @DataForFilter + '
				and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')
				'

			SET @OrderByClause = ' order by 12,8 desc OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

			SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
										, count(case when left(OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
										from CourtCases as CC
											join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
											join CourtCasesEnforcements CCE on CCE.CaseId = CC.CaseId
											left join MASTER_S as LOC on LOC.Mst_Value = CCE.CourtLocationid and LOC.MstParentId = 4
											join MASTER_S as ENFLVL on ENFLVL.Mst_Value = CC.OfficeFileStatus and ENFLVL.MstParentId = 1573 and ENFLVL.Remarks like ''%ENF%''
											left join MASTER_S as GOV on GOV.Mst_Value = CC.GovernorateId and GOV.MstParentId = 1153
											where CC.CaseStatus = ''1''
											AND   CC.CaseLevelCode = ''6''
											' + @DataForFilter + '
											and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

			SET @FinalQuery = @SQLQuery + @FilterText + @OrderByClause

			print @FinalQuery
			exec (@FinalQuery)

			print @SQLSummaryQuery
			exec (@SQLSummaryQuery)
		END
	END
ELSE IF (@DataFor = 'BEFORECOURT')
	BEGIN
	 CREATE TABLE #T4 (
					CaseId int,OfficeFileNo varchar(10),ClientName varchar(200),DefClientName varchar(200),AgainstName varchar(50),
					ReceptionDate DATETIME2,AccountContractNo varchar(100),ClientFileNo varchar(100),CaseTypeName varchar(100),
					CaseLevelCode varchar(10),CaseLevelName varchar(100),CaseStatus varchar(10),CaseStatusName varchar(100),FSort int,
					SSort int,ToBeRegisterDays int,CourtRefNo varchar(100),COURT varchar(100),CurrentHearingDate DATETIME2,CourtDecision nvarchar(max)
				 )

		SET @SQLQuery = 'SELECT CC.CaseId,CC.OfficeFileNo,ClientMas.Mst_Desc as ClientName,CC.Defendant as DefClientName,AgainstMas.Mst_Desc as AgainstName
						,CC.ReceptionDate,CC.AccountContractNo,CC.ClientFileNo,CaseTypeMas.Mst_Desc as CaseTypeName,CC.CaseLevelCode
						,CaseLevelMas.Mst_Desc as CaseLevelName,CC.CaseStatus,CaseStatusMas.Mst_Desc as CaseStatusName,
						case when CC.CaseStatus = ''2'' then 6 else
									case 
										when CC.CaseLevelCode = ''1'' then 1
										when CC.CaseLevelCode = ''3'' then 2
										when CC.CaseLevelCode = ''4'' then 3
										when CC.CaseLevelCode = ''5'' then 4
										when CC.CaseLevelCode = ''2'' then 5
										when CC.CaseLevelCode = ''6'' then 7
										when CC.CaseLevelCode = ''7'' then 6
										else 8
									end
								end as FSort,
								case when CC.CaseStatus = ''2'' then 1 else
									case 
										when CC.CaseLevelCode = ''1'' AND DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) > 40 then 0
										else 1
									end
								end as SSort,case when CC.CaseLevelCode = ''1'' AND CC.CaseStatus = ''1'' then DATEDIFF(DAY, dateadd(hour , 11, CC.ReceptionDate),dateadd(hour , 11,GETDATE())) else 0 end as ToBeRegisterDays,
								null as CourtRefNo,null as COURT,CC.CurrentHearingDate,CC.CourtDecision
						from CourtCases as CC
						join MASTER_S ClientMas on CC.ClientCode = ClientMas.Mst_Value and ClientMas.MstParentId = 241
						join MASTER_S AgainstMas on CC.AgainstCode = AgainstMas.Mst_Value and AgainstMas.MstParentId = 12
						join MASTER_S CaseTypeMas on CC.CaseTypeCode = CaseTypeMas.Mst_Value and CaseTypeMas.MstParentId = 14
						join MASTER_S CaseLevelMas on CC.CaseLevelCode = CaseLevelMas.Mst_Value and CaseLevelMas.MstParentId = 15
						join MASTER_S CaseStatusMas on CC.CaseStatus = CaseStatusMas.Mst_Value and CaseStatusMas.MstParentId = 252 
						where CC.CaseStatus != ''-1'' 
						' + @DataForFilter + '
						and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'')'

		SET @OrderByClause = ' order by ReceptionDate OFFSET '+CONVERT(varchar,@From)+' ROWS FETCH NEXT '+CONVERT(varchar,@pagesize)+' ROWS ONLY OPTION (RECOMPILE)'

		-- SET ANSI_WARNINGS OFF (FOR CHECKING TRUNCATION
			INSERT into #T4 execute (@SQLQuery )
		-- SET ANSI_WARNINGS ON

			--UPDATE  #T4 
			--SET		CourtRefNo = CourtCasesDetailVW.CourtRefNo, COURT = CourtCasesDetailVW.CourtLocation
			--FROM	#T4  
			--JOIN    CourtCasesDetailVW on CourtCasesDetailVW.CaseId = #T4.CaseId and CourtCasesDetailVW.CaseLevelCode = #T4.CaseLevelCode
			--COLLATE DATABASE_DEFAULT --(DIFFERENT COLLATE FIX)

		SET @SQLSummaryQuery = 'SELECT COUNT(*) as recordsTotal, count(case when left(CC.OfficeFileNo,1) = ''M'' then 1 end) MCTRecords 
			, count(case when left(CC.OfficeFileNo,1) = ''S'' then 1 end) SLLRecords 
			from #T4 as CC
			where CC.CaseStatus != ''-1'' 
			' + @DataForFilter + '
			and (LEFT(CC.OfficeFileNo,1) = ''' + @Location + ''' OR ''' + @Location + ''' = ''A'') '+ @FilterText

		SET @FinalQuery = 'SELECT *,count(*) OVER() AS TotalRecords	FROM #T4 where 1=1 ' + @FilterText + @OrderByClause

		print @FinalQuery
		exec (@FinalQuery)

		print @SQLSummaryQuery
		exec (@SQLSummaryQuery)

		drop table #T4

	END
END