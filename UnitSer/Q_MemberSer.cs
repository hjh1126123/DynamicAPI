using System;
using System.Data;
using System.Text;

using HDBChartModel.ServiceModel;

namespace HDBChartService
{
    public class Q_MemberSer
    {
        public DataTable TicketMong(string countries, string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                var centerCode = string.Empty;
                switch (countries)
                {
                    case "美国":
                        centerCode = "'USDB'";
                        break;

                    case "日本":
                        centerCode = "'JPDB','JPSFDB'";
                        break;

                    case "澳洲":
                        centerCode = "'AUDB'";
                        break;

                    case "德国":
                        centerCode = "'GERDB','DEDB'";
                        break;
                }

                sql.AppendLine($"SELECT	'{countries}' AS name ");
                sql.AppendLine(",[T].[ticket] ");
                sql.AppendLine(",[T].[total] ");
                sql.AppendLine("FROM	( ");

                sql.AppendLine("SELECT	'1200票以上' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",1 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A >= '1200' ");

                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'600-1119票' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",2 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A BETWEEN 600 AND 1119 ");

                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'360-599票' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",3 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A BETWEEN 360 AND 599 ");

                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'120-359票' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",4 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A BETWEEN 120 AND 359 ");

                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'91-119票' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",5 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A BETWEEN 91 AND 119 ");

                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'61-90票' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",6 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A BETWEEN 61 AND 90 ");

                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'31-60票' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",7 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A BETWEEN 31 AND 60 ");

                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'1-30票' AS ticket ");
                sql.AppendLine(",COUNT(*) AS total ");
                sql.AppendLine(",8 weight ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	UserCode ");
                sql.AppendLine(",COUNT(ExpressId) A ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		CenterCode IN ({centerCode}) ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine($"AND InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY	UserCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("WHERE		T1.A BETWEEN 1 AND 30 ");

                sql.AppendLine(") AS T ");

                sql.AppendLine("order by T.weight; ");


                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        public DataTable MonthlyMember(string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	YEAR(CreateDate) AS [Year] ");
                sql.AppendLine(",MONTH(CreateDate) AS [Month] ");
                sql.AppendLine(",COUNT(UserCode) CreateCount ");
                sql.AppendLine("FROM	U_UserRegIps ");
                sql.AppendLine($"WHERE	CreateDate >= '{start}' ");
                sql.AppendLine($"AND CreateDate < '{end}' ");
                sql.AppendLine("GROUP BY YEAR(CreateDate) ");
                sql.AppendLine(",MONTH(CreateDate) ");
                sql.AppendLine("ORDER BY [Year],[Month]; ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        public DataTable MemberActivity(string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	COUNT(*) AS Total ");
                sql.AppendLine(",MONTH(T.[InDate]) CreateDate ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT DISTINCT ");
                sql.AppendLine("( UserCode ) AS [UserCode] ");
                sql.AppendLine(",CONVERT(varchar(100), InDate, 111) AS InDate ");
                sql.AppendLine("FROM		E_OverseasExpress ");
                sql.AppendLine($"WHERE		InDate >= '{start}' ");
                sql.AppendLine($"AND InDate < '{end}' ");
                sql.AppendLine("AND DelState = '0' ");
                sql.AppendLine("GROUP BY	CONVERT(varchar(100), InDate, 111) ");
                sql.AppendLine(",UserCode ");
                sql.AppendLine(") T ");
                sql.AppendLine("GROUP BY MONTH(T.[InDate]) ");
                sql.AppendLine("ORDER BY MONTH(T.[InDate]) ");


                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDt;
        }

        public DataTable MemberQuantityType(string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	'散户' UserType ");
                sql.AppendLine(",COUNT(T1.Id) InCount ");
                sql.AppendLine(",1 UserState ");
                sql.AppendLine("FROM	E_OverseasExpress T1 ");
                sql.AppendLine("LEFT JOIN U_Users T2 ");
                sql.AppendLine("ON T1.UserCode = T2.UserCode ");
                sql.AppendLine($"WHERE	1 = 1 ");
                sql.AppendLine($"AND T1.InDate < '{end}' ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND T2.UserLevelId BETWEEN 0 AND 5 ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'JWMST', 'HRSMK', 'PRPAB', 'MPERF', 'MVBVA', ");
                sql.AppendLine("'UFNNX', 'PXUYN', 'VNZWW', 'NUWFN', 'WAHMS', ");
                sql.AppendLine("'YAFKAR', 'YTHDB', 'BRUYR', 'JXXHN', 'FUFTZM', ");
                sql.AppendLine("'RNWUR', 'WFKNH', 'YRZMT', 'YPAUR', 'HJPRZ', ");
                sql.AppendLine("'PYUYH', 'TFSSE', 'ZZFJU', 'MMFMX', 'RRVKB', ");
                sql.AppendLine("'TZKTM', 'UFFYF', 'EMRMK', 'BAWJP', 'RWPAT', ");
                sql.AppendLine("'XXABE', 'BRRKJ', 'EYNYX', 'HRKJU', 'KKKKK', ");
                sql.AppendLine("'WRNUM', 'NSAMS', 'WUBBU', 'CCCCC', 'BBBBB', 'DDDDD','HHHHH', 'RJFSU', 'APWHW','BUUBXA' ) ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'贩子' UserType ");
                sql.AppendLine(",COUNT(T1.Id) InCount ");
                sql.AppendLine(",2 UserState ");
                sql.AppendLine("FROM	E_OverseasExpress T1 ");
                sql.AppendLine("LEFT JOIN U_Users T2 ");
                sql.AppendLine("ON T1.UserCode = T2.UserCode ");
                sql.AppendLine($"WHERE	1 = 1 ");
                sql.AppendLine($"AND T1.InDate < '{end}' ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND T2.UserLevelId BETWEEN 6 AND 15 ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'JWMST', 'HRSMK', 'PRPAB', 'MPERF', 'MVBVA', ");
                sql.AppendLine("'UFNNX', 'PXUYN', 'VNZWW', 'NUWFN', 'WAHMS', ");
                sql.AppendLine("'YAFKAR', 'YTHDB', 'BRUYR', 'JXXHN', 'FUFTZM', ");
                sql.AppendLine("'RNWUR', 'WFKNH', 'YRZMT', 'YPAUR', 'HJPRZ', ");
                sql.AppendLine("'PYUYH', 'TFSSE', 'ZZFJU', 'MMFMX', 'RRVKB', ");
                sql.AppendLine("'TZKTM', 'UFFYF', 'EMRMK', 'BAWJP', 'RWPAT', ");
                sql.AppendLine("'XXABE', 'BRRKJ', 'EYNYX', 'HRKJU', 'KKKKK', ");
                sql.AppendLine("'WRNUM', 'NSAMS', 'WUBBU', 'CCCCC', 'BBBBB', 'DDDDD','HHHHH', 'RJFSU', 'APWHW','BUUBXA' ) ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'商户' UserType ");
                sql.AppendLine(",COUNT(T1.Id) InCount ");
                sql.AppendLine(",3 UserState ");
                sql.AppendLine("FROM	E_OverseasExpress T1 ");
                sql.AppendLine($"WHERE	1 = 1 ");
                sql.AppendLine($"AND T1.InDate < '{end}' ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND T1.UserCode IN ( 'JWMST', 'HRSMK', 'PRPAB', 'MPERF', 'MVBVA', ");
                sql.AppendLine("'UFNNX', 'PXUYN', 'VNZWW', 'NUWFN', 'WAHMS', ");
                sql.AppendLine("'YAFKAR', 'YTHDB', 'BRUYR', 'JXXHN', 'FUFTZM', ");
                sql.AppendLine("'RNWUR', 'WFKNH', 'YRZMT', 'YPAUR', 'HJPRZ', ");
                sql.AppendLine("'PYUYH', 'TFSSE', 'ZZFJU', 'MMFMX', 'RRVKB', ");
                sql.AppendLine("'TZKTM', 'UFFYF', 'EMRMK', 'BAWJP', 'RWPAT', ");
                sql.AppendLine("'XXABE', 'BRRKJ', 'EYNYX', 'HRKJU', 'KKKKK', ");
                sql.AppendLine("'WRNUM', 'NSAMS', 'WUBBU' ) ");
                sql.AppendLine("ORDER BY UserState; ");


                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDt;
        }

        public DataTable MemberType(string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	'贩子' AS [Name] ");
                sql.AppendLine(",COUNT(T.UserCode) [Total] ");
                sql.AppendLine(",2 UserState ");
                sql.AppendLine("FROM	U_Users T ");
                sql.AppendLine("LEFT JOIN U_UserRegIps T1 ");
                sql.AppendLine("ON T1.[UserCode] = T.[UserCode] ");
                sql.AppendLine("WHERE	T.UserLevelId BETWEEN 6 AND 15 ");
                sql.AppendLine("AND T.UserCode NOT IN ( 'JWMST', 'HRSMK', 'PRPAB', 'MPERF', 'MVBVA', ");
                sql.AppendLine("'UFNNX', 'PXUYN', 'VNZWW', 'NUWFN', 'WAHMS', ");
                sql.AppendLine("'YAFKAR', 'YTHDB', 'BRUYR', 'JXXHN', 'FUFTZM', ");
                sql.AppendLine("'RNWUR', 'WFKNH', 'YRZMT', 'YPAUR', 'HJPRZ', ");
                sql.AppendLine("'PYUYH', 'TFSSE', 'ZZFJU', 'MMFMX', 'RRVKB', ");
                sql.AppendLine("'TZKTM', 'UFFYF', 'EMRMK', 'BAWJP', 'RWPAT', ");
                sql.AppendLine("'XXABE', 'BRRKJ', 'EYNYX', 'HRKJU', 'KKKKK', ");
                sql.AppendLine("'WRNUM', 'NSAMS', 'WUBBU' ) ");
                sql.AppendLine($"AND 1 = 1 ");
                sql.AppendLine($"AND [T1].[CreateDate] <= '{end}' ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'散户' AS [name] ");
                sql.AppendLine(",COUNT(T.UserCode) [total] ");
                sql.AppendLine(",1 UserState ");
                sql.AppendLine("FROM	U_Users T ");
                sql.AppendLine("LEFT JOIN U_UserRegIps T1 ");
                sql.AppendLine("ON T1.[UserCode] = T.[UserCode] ");
                sql.AppendLine("WHERE	[T].[UserLevelId] BETWEEN 0 AND 5 ");
                sql.AppendLine("AND [T].[UserCode] NOT IN ( 'JWMST', 'HRSMK', 'PRPAB', 'MPERF', ");
                sql.AppendLine("'MVBVA', 'UFNNX', 'PXUYN', 'VNZWW', ");
                sql.AppendLine("'NUWFN', 'WAHMS', 'YAFKAR', 'YTHDB', ");
                sql.AppendLine("'BRUYR', 'JXXHN', 'FUFTZM', 'RNWUR', ");
                sql.AppendLine("'WFKNH', 'YRZMT', 'YPAUR', 'HJPRZ', ");
                sql.AppendLine("'PYUYH', 'TFSSE', 'ZZFJU', 'MMFMX', ");
                sql.AppendLine("'RRVKB', 'TZKTM', 'UFFYF', 'EMRMK', ");
                sql.AppendLine("'BAWJP', 'RWPAT', 'XXABE', 'BRRKJ', ");
                sql.AppendLine("'EYNYX', 'HRKJU', 'KKKKK', 'WRNUM', ");
                sql.AppendLine("'NSAMS', 'WUBBU' ) ");
                sql.AppendLine($"AND 1 = 1 ");
                sql.AppendLine($"AND [T1].[CreateDate] <= '{end}' ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	'商户' AS [name] ");
                sql.AppendLine(",38 AS [total] ");
                sql.AppendLine(",3 UserState ");
                sql.AppendLine("ORDER BY UserState;  ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDt;
        }

        #region 监控

        /// <summary>
        /// 月结客户数
        /// </summary>
        /// <returns></returns>
        public DataTable MemberMonthOk()
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("select UserCode from  U_Users where PayType=1 and DelState=0 and IsUsed='T'");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDt;
        }

        /// <summary>
        /// 手工录入月结客户数
        /// </summary>
        /// <returns></returns>
        public DataTable MemberMonthOKLastTime()
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT [UserCode] from dbo.B_MonthlyClient WHERE [DelState] = 0");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDt;
        }

        #endregion

        #region 点击量

        public DataTable Hits(string PositionType)
        {
            DataTable serDt = null;

            try
            {
                StringBuilder sql = new StringBuilder();

                H_HDBMember h_HDBMember = new H_HDBMember
                {
                    PositionType = PositionType
                };

                if (string.IsNullOrWhiteSpace(PositionType))
                {
                    sql.AppendLine("SELECT	[PositionType] ");
                    sql.AppendLine(",COUNT(*) AS [Total] ");
                    sql.AppendLine("FROM	U_UserKeyPositionRecord T1 ");
                    sql.AppendLine("WHERE	DATEDIFF(DD, [CreateDate], GETDATE()) <= 30 ");
                    sql.AppendLine("AND [PositionType] <> 9 {0} ");
                    sql.AppendLine("GROUP BY [PositionType] ");
                    sql.AppendLine("ORDER BY Total DESC;  ");
                }
                else
                {
                    sql.AppendLine("SELECT	[PositionType] ");
                    sql.AppendLine(",[PositinDesc] ");
                    sql.AppendLine(",COUNT(*) AS [Total] ");
                    sql.AppendLine("FROM	U_UserKeyPositionRecord T1 ");
                    sql.AppendLine("WHERE	DATEDIFF(DD, [CreateDate], GETDATE()) <= 30 ");
                    sql.AppendLine("AND [PositionType] <> 9 {0} ");
                    sql.AppendLine("GROUP BY [PositionType] ");
                    sql.AppendLine(",[PositinDesc] ");
                    if (PositionType.IndexOf("\'9\'") > 0)
                    {
                        sql.AppendLine("UNION ");
                        sql.AppendLine("SELECT	[PositionType] ");
                        sql.AppendLine(",'公告' ");
                        sql.AppendLine(",COUNT(*) AS [Total] ");
                        sql.AppendLine("FROM	U_UserKeyPositionRecord ");
                        sql.AppendLine("WHERE	DATEDIFF(DD, [CreateDate], GETDATE()) <= 30 ");
                        sql.AppendLine("AND PositionType = 9 ");
                        sql.AppendLine("GROUP BY [PositionType] ");
                    }
                    sql.AppendLine("ORDER BY Total DESC; ");
                }

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString(), h_HDBMember);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDt;
        }

        #endregion
    }
}
