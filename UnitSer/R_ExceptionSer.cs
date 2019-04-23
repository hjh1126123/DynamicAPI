using HDBChartModel.ServiceModel;
using System;
using System.Data;
using System.Text;
using WEB.Plugin.CommonHelper;

namespace HDBChartService
{
    public class R_ExceptionSer
    {
        public DataTable GetTotal(string dateStart, string dateEnd, string realStatisDays, string createCenterCode)
        {
            try
            {
                if (HelperMediator.GetInstance().stringHelper.CheckSpecialCharacters(dateStart, dateEnd))
                {
                    return null;
                }

                R_LogisticsExceptionReportDetail rpt_LogisticsExceptionReportDetail1 = new R_LogisticsExceptionReportDetail()
                {
                    ModifyTimeStart = dateStart,
                    ModifyTimeEnd = dateEnd,
                    CreateCenterCodeIn = createCenterCode,
                    STTypeIn = "5,6",
                    DelState = "0",
                    RealStatisDays = realStatisDays
                };

                R_LogisticsExceptionReportDetail rpt_LogisticsExceptionReportDetail2 = new R_LogisticsExceptionReportDetail()
                {
                    ModifyTimeStart = dateStart,
                    ModifyTimeEnd = dateEnd,
                    CreateCenterCodeIn = createCenterCode,
                    STTypeIn = "4",
                    DelState = "0",
                    RealStatisDays = "1"
                };

                R_LogisticsExceptionReportDetail rpt_LogisticsExceptionReportDetail3 = new R_LogisticsExceptionReportDetail()
                {
                    ModifyTimeStart = dateStart,
                    ModifyTimeEnd = dateEnd,
                    CreateCenterCodeIn = createCenterCode,
                    STTypeIn = "2,3",
                    DelState = "0",
                    RealStatisDays = realStatisDays
                };

                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	COUNT(*) AS Total ");
                sql.AppendLine(",[T].[STType] ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM		[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE		1 = 1 ");
                sql.AppendLine("*[T]*{0}  ");
                sql.AppendLine(") T ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	[BattleCode] ");
                sql.AppendLine("FROM		[dbo].[G_Battle_Box_T] ");
                sql.AppendLine("WHERE		[DelState] = 0 ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("ON [T].[BillCode] = [T1].[BattleCode] ");
                sql.AppendLine("GROUP BY	[T].[STType] ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	COUNT(*) AS Total ");
                sql.AppendLine(",'4' AS [STType] ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM		[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE		1 = 1 ");
                sql.AppendLine("*[T]*{1}  ");
                sql.AppendLine(") T ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	[BattleCode] ");
                sql.AppendLine("FROM		[dbo].[G_Battle_Box_T] ");
                sql.AppendLine("WHERE		[DelState] = 0 ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("ON [T].[BillCode] = [T1].[BattleCode] ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	COUNT(*) AS Total ");
                sql.AppendLine(",[T].[STType] ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM		[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE		1 = 1 ");
                sql.AppendLine("*[T]*{2}  ");
                sql.AppendLine(") T ");
                sql.AppendLine("GROUP BY	[T].[STType] ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	SUM([HKRKJL]) AS [Total] ");
                sql.AppendLine(",'7' AS [STType] ");
                sql.AppendLine("FROM		RT_SpecialExcpStatistics ");
                sql.AppendLine("WHERE 1=1 ");
                sql.AppendLine($"AND [DayReport] >= N'{dateStart}' ");
                sql.AppendLine($"AND [DayReport] <= N'{dateEnd}' ");
                sql.AppendLine("GROUP BY CONVERT(varchar(100), [DayReport], 111) ");
                sql.AppendLine(") T ");
                sql.AppendLine("ORDER BY T.[STType];  ");                

                DataTable serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(
                       sql.ToString()
                       , out string checkSql
                       , rpt_LogisticsExceptionReportDetail1
                       , rpt_LogisticsExceptionReportDetail2
                       , rpt_LogisticsExceptionReportDetail3);

                Console.WriteLine(checkSql);

                return serDt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable GetChartData(string dateStart, string dateEnd, string createCenterCode, string exType)
        {
            string STType = "1";
            string realStatisDays = "3";
            try
            {
                if (HelperMediator.GetInstance().stringHelper.CheckSpecialCharacters(dateStart, dateEnd, createCenterCode, exType))
                {
                    return null;
                }

                switch (exType)
                {
                    case "4":
                        realStatisDays = "1";
                        break;

                    case "7":
                        realStatisDays = "5";
                        break;

                    case "8":
                        STType = "8";
                        realStatisDays = "7";
                        break;
                }

                R_LogisticsExceptionReportDetail rpt_LogisticsExceptionReportDetail0 = new R_LogisticsExceptionReportDetail()
                {
                    ModifyTimeStart = dateStart,
                    ModifyTimeEnd = dateEnd,
                    CreateCenterCodeIn = createCenterCode,
                    STTypeIn = "2,3",
                    RealStatisDays = realStatisDays
                };

                R_LogisticsExceptionReportDetail rpt_LogisticsExceptionReportDetail1 = new R_LogisticsExceptionReportDetail()
                {
                    ModifyTimeStart = dateStart,
                    ModifyTimeEnd = dateEnd,
                    CreateCenterCodeIn = createCenterCode,
                    STTypeIn = "4",
                    RealStatisDays = "1"
                };

                R_LogisticsExceptionReportDetail rpt_LogisticsExceptionReportDetail2 = new R_LogisticsExceptionReportDetail()
                {
                    ModifyTimeStart = dateStart,
                    ModifyTimeEnd = dateEnd,
                    CreateCenterCodeIn = createCenterCode,
                    STTypeIn = "5,6",
                    RealStatisDays = realStatisDays
                };

                R_LogisticsExceptionReportDetail rpt_LogisticsExceptionReportDetail3 = new R_LogisticsExceptionReportDetail()
                {
                    ModifyTimeStart = dateStart,
                    ModifyTimeEnd = dateEnd,
                    CreateCenterCodeIn = createCenterCode,
                    STType = exType,
                    RealStatisDays = realStatisDays
                };

                StringBuilder sql = new StringBuilder();

                sql.AppendLine("DECLARE	@SQL NVARCHAR(500);    ");
                sql.AppendLine("DECLARE	@ExType INT;    ");
                sql.AppendLine($"SET @ExType = {exType};   ");
                sql.AppendLine("IF ( ");
                sql.AppendLine("@ExType = 1 ");
                sql.AppendLine("OR @ExType = 8 ");
                sql.AppendLine(") ");
                sql.AppendLine("BEGIN    ");
                sql.AppendLine("SELECT	SUM([T].[Total]) AS [Total] ");
                sql.AppendLine(",CONVERT(VARCHAR(10), [T].[ModifyTime], 23) AS ModifyTime ");
                sql.AppendLine($",'{STType}' AS [STType] ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	COUNT(*) AS [Total] ");
                sql.AppendLine(",[ModifyTime] ");
                sql.AppendLine("FROM		[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE		1 = 1 ");
                sql.AppendLine("*[T]*{0}  ");
                sql.AppendLine("GROUP BY	[ModifyTime] ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	COUNT(*) AS [Total] ");
                sql.AppendLine(",[T].[ModifyTime] ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	[T].[BillCode] ");
                sql.AppendLine(",[T].[CreateCenterCode] ");
                sql.AppendLine(",CONVERT(VARCHAR(100), [T].[ModifyTime], 23) AS [ModifyTime] ");
                sql.AppendLine("FROM		[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE		1 = 1 ");
                sql.AppendLine("*[T]*{1}  ");
                sql.AppendLine(") T ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	[BattleCode] ");
                sql.AppendLine("FROM		[dbo].[G_Battle_Box_T] ");
                sql.AppendLine("WHERE		[DelState] = 0 ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("ON [T].[BillCode] = [T1].[BattleCode] ");
                sql.AppendLine("GROUP BY	[T].[ModifyTime] ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	COUNT(*) AS [Total] ");
                sql.AppendLine(",[T].[ModifyTime] ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	[T].[BillCode] ");
                sql.AppendLine(",[T].[CreateCenterCode] ");
                sql.AppendLine(",CONVERT(VARCHAR(100), [T].[ModifyTime], 23) AS [ModifyTime] ");
                sql.AppendLine("FROM		[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE		1 = 1 ");
                sql.AppendLine("*[T]*{2}  ");
                sql.AppendLine(") T ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	[BattleCode] ");
                sql.AppendLine("FROM		[dbo].[G_Battle_Box_T] ");
                sql.AppendLine("WHERE		[DelState] = 0 ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("ON [T].[BillCode] = [T1].[BattleCode] ");
                sql.AppendLine("GROUP BY	[T].[ModifyTime] ");
                sql.AppendLine("UNION ");
                //sql.AppendLine("SELECT	COUNT(*) AS [Total] ");
                //sql.AppendLine(",GETDATE() AS ModifyTime ");
                //sql.AppendLine("FROM		( ");
                //sql.AppendLine("SELECT	* ");
                //sql.AppendLine("FROM		RT_RouteExcpStatistics ");
                //sql.AppendLine("WHERE		ISNULL(HKSHRK, 0) + ISNULL(HKRKDB, 0) ");
                //sql.AppendLine("+ ISNULL(HKDBJJ, 0) + ISNULL(HKJJJL, 0) ");
                //sql.AppendLine("+ ISNULL(JLFH, 0) > 3 ");
                //sql.AppendLine("AND IsEnd = 0 ");
                //sql.AppendLine("AND DelState = 0 ");
                //sql.AppendLine($"AND CreateCenterCode IN ({createCenterCode}) ");
                //sql.AppendLine(") T ");
                sql.AppendLine("SELECT	SUM([HKRKJL]) AS [Total] ");
                sql.AppendLine(",CONVERT(varchar(100), [DayReport], 23) AS [ModifyTime] ");
                sql.AppendLine("FROM		RT_SpecialExcpStatistics ");
                sql.AppendLine("WHERE 1=1 ");
                sql.AppendLine($"AND [DayReport] >= N'{dateStart}' ");
                sql.AppendLine($"AND [DayReport] <= N'{dateEnd}' ");
                sql.AppendLine($"AND [CreateCenterCode] IN ({createCenterCode}) ");
                sql.AppendLine("GROUP BY CONVERT(varchar(100), [DayReport], 23) ");
                sql.AppendLine(") T ");
                sql.AppendLine("GROUP BY CONVERT(VARCHAR(10), [T].[ModifyTime], 23);    ");
                sql.AppendLine("END; ");
                sql.AppendLine("ELSE ");
                sql.AppendLine("IF ( ");
                sql.AppendLine("@ExType > 1 ");
                sql.AppendLine("AND @ExType <= 3 ");
                sql.AppendLine(") ");
                sql.AppendLine("BEGIN    ");
                sql.AppendLine("SELECT	COUNT(*) AS [Total] ");
                sql.AppendLine(",CONVERT(VARCHAR(100), [T].[ModifyTime], 23) AS [ModifyTime] ");
                sql.AppendLine(",[T].[STType] ");
                sql.AppendLine("FROM	[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE	1 = 1 ");
                sql.AppendLine("*[T]*{3}  ");
                sql.AppendLine("GROUP BY CONVERT(VARCHAR(100), [T].[ModifyTime], 23) ");
                sql.AppendLine(",[T].[STType];    ");
                sql.AppendLine("END;    ");
                sql.AppendLine("ELSE ");
                sql.AppendLine("IF ( ");
                sql.AppendLine("@ExType > 3 ");
                sql.AppendLine("AND @ExType <= 6 ");
                sql.AppendLine(") ");
                sql.AppendLine("BEGIN    ");
                sql.AppendLine("SELECT	COUNT(*) AS [Total] ");
                sql.AppendLine(",[T].[ModifyTime] ");
                sql.AppendLine(",[T].[STType] ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	[T].[BillCode] ");
                sql.AppendLine(",[T].[CreateCenterCode] ");
                sql.AppendLine(",CONVERT(VARCHAR(100), [T].[ModifyTime], 23) AS [ModifyTime] ");
                sql.AppendLine(",[T].[STType] ");
                sql.AppendLine("FROM		[dbo].[rpt_LogisticsExceptionReportDetail] T ");
                sql.AppendLine("WHERE		1 = 1 ");
                sql.AppendLine("*[T]*{3}  ");
                sql.AppendLine(") T ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	[BattleCode] ");
                sql.AppendLine("FROM	[dbo].[G_Battle_Box_T] ");
                sql.AppendLine("WHERE	[DelState] = 0 ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("ON [T].[BillCode] = [T1].[BattleCode] ");
                sql.AppendLine("GROUP BY [T].[ModifyTime] ");
                sql.AppendLine(",[T].[STType];    ");
                sql.AppendLine("END;    ");
                sql.AppendLine("ELSE ");
                sql.AppendLine("IF @ExType = 7 ");
                sql.AppendLine("BEGIN    ");
                sql.AppendLine("SELECT	SUM([HKRKJL]) AS [Total] ");
                sql.AppendLine(",CONVERT(varchar(100), [DayReport], 23) AS [ModifyTime] ");
                sql.AppendLine(",'7' AS [STType] ");
                sql.AppendLine("FROM		RT_SpecialExcpStatistics ");
                sql.AppendLine("WHERE 1=1 ");
                sql.AppendLine($"AND [DayReport] >= N'{dateStart}' ");
                sql.AppendLine($"AND [DayReport] <= N'{dateEnd}' ");
                sql.AppendLine($"AND [CreateCenterCode] IN ({createCenterCode}) ");
                sql.AppendLine("GROUP BY CONVERT(varchar(100), [DayReport], 23) ");
                sql.AppendLine("END;    ");


                DataTable serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(
                       sql.ToString()
                       , out string checkSql
                       , rpt_LogisticsExceptionReportDetail0
                       , rpt_LogisticsExceptionReportDetail1
                       , rpt_LogisticsExceptionReportDetail2
                       , rpt_LogisticsExceptionReportDetail3);

                Console.WriteLine(checkSql);

                return serDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAllWarehouse()
        {
            string[] objs = new string[0];

            R_LogisticsExceptionReport rpt_LogisticsExceptionReport = new R_LogisticsExceptionReport()
            {
                NotInCenterCode = "'','HKDB','QDDB','JPSFDB'"
            };

            return ServiceConfig.GetInstance().GetOperation().SimpleGroupQuery(
                rpt_LogisticsExceptionReport
                , "CreateCenterCode"
                , "CreateCenterCode");
        }

        public DataTable GetAllST()
        {
            string[] objs = new string[0];

            R_LogisticsExceptionReport rpt_LogisticsExceptionReport = new R_LogisticsExceptionReport()
            {
                NotInSTType = "1,8"
            };

            DataTable dataTable = ServiceConfig.GetInstance().GetOperation().SimpleGroupQuery(
                rpt_LogisticsExceptionReport
                , "STName, STType"
                , "STName, STType");

            return dataTable;
        }
    }
}
