using System;
using System.Data;
using System.Text;

namespace HDBChartService
{
    public class FinanceExSer
    {
        /// <summary>
        /// 对账总表异常
        /// </summary>
        /// <param name="bf_d"></param>
        /// <param name="af_d"></param>
        /// <returns></returns>
        public DataTable SimpleReconciliationEx(DateTime d_e, int bf_d, int af_d)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	[DayReport] ");
                sql.AppendLine(",'总表' AS [UserCode] ");
                sql.AppendLine(",[ExceptionLink] ");
                sql.AppendLine(",[ExceptionDescription] ");
                sql.AppendLine("FROM	Q_AcctDayBalanceReportException ");
                sql.AppendLine($"WHERE	[DayReport] BETWEEN '{d_e.AddDays(-bf_d).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND	'{d_e.AddDays(-af_d).ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("AND [DelState] = 0 ");
                sql.AppendLine("ORDER BY [DayReport] DESC; ");

                Console.WriteLine(sql.ToString());

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }
 
        /// <summary>
        /// 对账用户表异常
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable SimpleUserReconciliationEx(DateTime d_e, int dateStart, int dateEnd)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	[DayReport] ");
                sql.AppendLine(",[UserCode] ");
                sql.AppendLine(",[ExceptionLink] ");
                sql.AppendLine(",[ExceptionDescription] ");
                sql.AppendLine("FROM	[dbo].[Q_AcctDayBalanceException] ");
                sql.AppendLine($"WHERE	[DayReport] BETWEEN '{d_e.AddDays(-dateStart).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND	'{d_e.AddDays(-dateEnd).ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("AND [DelState] = 0 ");
                sql.AppendLine("ORDER BY [DayReport] DESC; ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        /// <summary>
        /// 对账异常
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable UserReconciliationEx(DateTime d_e, int dateStart, int dateEnd)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	[DayReport] ");
                sql.AppendLine(",'总表' AS [UserCode] ");
                sql.AppendLine(",[ExceptionLink] ");
                sql.AppendLine(",[ExceptionDescription] ");
                sql.AppendLine("FROM	Q_AcctDayBalanceReportException ");
                sql.AppendLine($"WHERE	[DayReport] BETWEEN '{d_e.AddDays(-dateStart).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND	'{d_e.AddDays(-dateEnd).ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("AND [DelState] = 0 ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	[DayReport] ");
                sql.AppendLine(",[UserCode] ");
                sql.AppendLine(",[ExceptionLink] ");
                sql.AppendLine(",[ExceptionDescription] ");
                sql.AppendLine("FROM	[dbo].[Q_AcctDayBalanceException] ");
                sql.AppendLine($"WHERE	[DayReport] BETWEEN '{d_e.AddDays(-dateStart).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND	'{d_e.AddDays(-dateEnd).ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("AND [DelState] = 0 ");
                sql.AppendLine("ORDER BY [DayReport] DESC; ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        /// <summary>
        /// 用户对账异常
        /// </summary>
        /// <returns></returns>
        public DataTable UserReconciliationEx(DateTime d_e, int date)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	[DayReport] ");
                sql.AppendLine(",'总表' AS [UserCode] ");
                sql.AppendLine(",[ExceptionLink] ");
                sql.AppendLine(",[ExceptionDescription] ");
                sql.AppendLine("FROM	Q_AcctDayBalanceReportException ");
                sql.AppendLine($"WHERE	[DayReport] BETWEEN '{d_e.AddDays(-date).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND	'{d_e.AddDays(-date).ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("AND [DelState] = 0 ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	[DayReport] ");
                sql.AppendLine(",[UserCode] ");
                sql.AppendLine(",[ExceptionLink] ");
                sql.AppendLine(",[ExceptionDescription] ");
                sql.AppendLine("FROM	[dbo].[Q_AcctDayBalanceException] ");
                sql.AppendLine($"WHERE	[DayReport] BETWEEN '{d_e.AddDays(-date).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND	'{d_e.AddDays(-date).ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("AND [DelState] = 0 ");
                sql.AppendLine("ORDER BY [DayReport] DESC; ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }
    }
}
