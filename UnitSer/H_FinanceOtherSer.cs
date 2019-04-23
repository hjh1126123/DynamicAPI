using System;
using System.Data;
using System.Text;

namespace HDBChartService
{
    public class H_FinanceOtherSer
    {
        public DataTable SystemRecharge(DateTime d_e)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("select Convert(nvarchar(10),AuditDate,120) AS [Date], ");
                sql.AppendLine("SUM(ISNULL(ChargeAmt,0)+ISNULL(RebateAmt,0)) AS [Total] ");
                sql.AppendLine("from U_ChargeRecord where DelState=0 and States=1 and IsAdjust=0 and BalanceType=0 ");
                sql.AppendLine($"and AuditDate>='{d_e.AddDays(-20).ToString("yyyy-MM-dd HH:mm:ss")}' and AuditDate<'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' ");
                sql.AppendLine("group by Convert(nvarchar(10),AuditDate,120) ");
                sql.AppendLine("order by Convert(nvarchar(10),AuditDate,120) ");

                Console.WriteLine(sql.ToString());

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
