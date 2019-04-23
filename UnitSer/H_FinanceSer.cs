using System;
using System.Data;
using System.Text;

using HDBChartModel.ServiceModel;

namespace HDBChartService
{
    public class FinanceSer
    {
        public DataTable MonthlyIncome(string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	T1.PayDate ");
                sql.AppendLine(",( ISNULL(T1.TotalCharge, 0) + ISNULL(T2.TotalPurchaseCharge, 0) ) AS [TotalCharge] ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T1.PayDate, 120) PayDate ");
                sql.AppendLine(",SUM(ISNULL(T1.UseAccount, 0) + ISNULL(T1.UseAlipayAmount, ");
                sql.AppendLine("0)) TotalCharge ");
                sql.AppendLine("FROM		O_CustomerOrder T1 ");
                sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T1.OrderState = 0 ");
                sql.AppendLine("AND T1.OrderType IN ( 0, 3, 4 ) ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T1.PayDate, 120) ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	T1.Dates ");
                sql.AppendLine(",T1.TotalCharge - T2.ChargeAmt - T3.AlipayFee TotalPurchaseCharge ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T1.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.TotalCharge, 0)) TotalCharge ");
                sql.AppendLine("FROM		O_CustomerOrder T1 ");
                sql.AppendLine("LEFT JOIN O_PurchaseOrder T2 ");
                sql.AppendLine("ON T1.PayOrderCode = T2.PayOrderCode ");
                sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.OrderType = 6 ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T1.PayDate, 120) ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T1.ReturnDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.ReturnFee, 0)) ChargeAmt ");
                sql.AppendLine("FROM	O_PurchaseFeeReturn T1 ");
                sql.AppendLine("LEFT JOIN O_PurchaseOrder T2 ");
                sql.AppendLine("ON T1.PurchaseOrderCode = T2.PurchaseOrderCode ");
                sql.AppendLine($"WHERE	T1.ReturnDate >= '{start}' ");
                sql.AppendLine($"AND T1.ReturnDate < '{end}' ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND T1.AllowReturn = 1 ");
                sql.AppendLine("AND T1.ReturnState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY CONVERT(NVARCHAR(7), T1.ReturnDate, 120) ");
                sql.AppendLine(") T2 ");
                sql.AppendLine("ON T1.Dates = T2.Dates ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(AccountFee, 0) + ISNULL(AlipayFee, ");
                sql.AppendLine("0)) AlipayFee ");
                sql.AppendLine("FROM	O_FeeReturn T1 ");
                sql.AppendLine($"WHERE	PayDate >= '{start}' ");
                sql.AppendLine($"AND PayDate < '{end}' ");
                sql.AppendLine("AND PayState = 1 ");
                sql.AppendLine("AND FeeReturnType = 1 ");
                sql.AppendLine("AND Remark LIKE '采购订单%' ");
                sql.AppendLine("AND UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY CONVERT(NVARCHAR(7), PayDate, 120) ");
                sql.AppendLine(") T3 ");
                sql.AppendLine("ON T1.Dates = T3.Dates ");
                sql.AppendLine(") T2 ");
                sql.AppendLine("ON T1.PayDate = T2.Dates ");
                sql.AppendLine("ORDER BY [T1].[PayDate]; ");

                Console.WriteLine(sql.ToString());

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        public DataTable TransferGenerationBuyProportion(string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	SUM(T.TotalCharge) AS TotalCharge ");
                sql.AppendLine(",SUM(T.TotalPurchaseCharge) AS TotalPurchaseCharge ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	T1.TotalCharge ");
                sql.AppendLine(",T2.TotalPurchaseCharge ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(4), T1.PayDate, 120) PayDate ");
                sql.AppendLine(",SUM(ISNULL(T1.UseAccount, 0) ");
                sql.AppendLine("+ ISNULL(T1.UseAlipayAmount, 0)) TotalCharge ");
                sql.AppendLine("FROM		O_CustomerOrder T1 ");
                sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T1.OrderState = 0 ");
                sql.AppendLine("AND T1.OrderType IN ( 0, 3, 4 ) ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(4), T1.PayDate, 120) ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	T1.Dates ");
                sql.AppendLine(",T1.TotalCharge - T2.ChargeAmt - T3.AlipayFee TotalPurchaseCharge ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(4), T1.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.TotalCharge, 0)) TotalCharge ");
                sql.AppendLine("FROM		O_CustomerOrder T1 ");
                sql.AppendLine("LEFT JOIN O_PurchaseOrder T2 ");
                sql.AppendLine("ON T1.PayOrderCode = T2.PayOrderCode ");
                sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.OrderType = 6 ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(4), T1.PayDate, 120) ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(4), T1.ReturnDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.ReturnFee, 0)) ChargeAmt ");
                sql.AppendLine("FROM		O_PurchaseFeeReturn T1 ");
                sql.AppendLine("LEFT JOIN O_PurchaseOrder T2 ");
                sql.AppendLine("ON T1.PurchaseOrderCode = T2.PurchaseOrderCode ");
                sql.AppendLine($"WHERE		T1.ReturnDate >= '{start}' ");
                sql.AppendLine($"AND T1.ReturnDate < '{end}' ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND T1.AllowReturn = 1 ");
                sql.AppendLine("AND T1.ReturnState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', ");
                sql.AppendLine("'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', ");
                sql.AppendLine("'YYYYY', 'APWHW', ");
                sql.AppendLine("'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(4), T1.ReturnDate, 120) ");
                sql.AppendLine(") T2 ");
                sql.AppendLine("ON T1.Dates = T2.Dates ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(4), PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(AccountFee, 0) ");
                sql.AppendLine("+ ISNULL(AlipayFee, 0)) AlipayFee ");
                sql.AppendLine("FROM		O_FeeReturn T1 ");
                sql.AppendLine($"WHERE		PayDate >= '{start}' ");
                sql.AppendLine($"AND PayDate < '{end}' ");
                sql.AppendLine("AND PayState = 1 ");
                sql.AppendLine("AND FeeReturnType = 1 ");
                sql.AppendLine("AND Remark LIKE '采购订单%' ");
                sql.AppendLine("AND UserCode NOT IN ( 'CCCCC', ");
                sql.AppendLine("'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', ");
                sql.AppendLine("'YYYYY', 'APWHW', ");
                sql.AppendLine("'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(4), T1.PayDate, 120) ");
                sql.AppendLine(") T3 ");
                sql.AppendLine("ON T1.Dates = T3.Dates ");
                sql.AppendLine(") T2 ");
                sql.AppendLine("ON T1.PayDate = T2.Dates ");
                sql.AppendLine(") AS T; ");

                Console.WriteLine(sql.ToString());

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDt;
        }

        public DataTable TransferMonthly(string centerCode, string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                O_CustomerOrder o_CustomerOrder = new O_CustomerOrder()
                {
                    CenterCodeIn = centerCode
                };

                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T1.PayDate, 120) AS [PayDate] ");
                sql.AppendLine(",SUM(ISNULL(T1.UseAccount, 0) + ISNULL(T1.UseAlipayAmount, 0)) TotalCharge ");
                sql.AppendLine("FROM	O_CustomerOrder T1 ");
                sql.AppendLine("LEFT JOIN B_BussCenter T2 ");
                sql.AppendLine("ON T1.CenterCode = T2.CenterCode ");
                sql.AppendLine($"WHERE	T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T1.OrderState = 0 ");
                sql.AppendLine("AND T1.OrderType IN ( 0, 3, 4 ) ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', 'HHHHH', 'RJFSU', ");
                sql.AppendLine("'YYYYY', 'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("*[T1]*{0} ");
                sql.AppendLine("GROUP BY CONVERT(NVARCHAR(7), T1.PayDate, 120) ");
                sql.AppendLine("ORDER BY PayDate; ");                

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString(), o_CustomerOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        public DataTable GenerationBuyMonthly(string currency, string start, string end)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                O_CustomerOrder o_CustomerOrder = new O_CustomerOrder()
                {
                    CurrencyIn = currency
                };

                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	'总数' AS Currency ");
                sql.AppendLine(",T1.Dates ");
                sql.AppendLine(",T1.TotalCharge - ISNULL(T2.ChargeAmt, 0) ");
                sql.AppendLine("- ISNULL(T3.AlipayFee, 0) TotalPurchaseCharge ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T1.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.TotalCharge, 0)) TotalCharge ");
                sql.AppendLine("FROM		O_CustomerOrder T1 ");
                sql.AppendLine("LEFT JOIN O_PurchaseOrder T2 ");
                sql.AppendLine("ON T1.PayOrderCode = T2.PayOrderCode ");
                sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.OrderType = 6 ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T1.PayDate, 120) ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T2.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.ReturnFee, 0)) ChargeAmt ");
                sql.AppendLine("FROM	O_PurchaseOrder T2	 ");
                sql.AppendLine("LEFT JOIN O_PurchaseFeeReturn T1 ");
                sql.AppendLine("ON T1.PurchaseOrderCode = T2.PurchaseOrderCode ");
                sql.AppendLine($"WHERE		T2.PayDate >= '{start}' ");
                sql.AppendLine($"AND T2.PayDate < '{end}' ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND T1.AllowReturn = 1 ");
                sql.AppendLine("AND T1.ReturnState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T2.PayDate, 120) ");
                sql.AppendLine(") T2 ");
                sql.AppendLine("ON T1.Dates = T2.Dates					 ");
                sql.AppendLine(" ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T2.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(AccountFee, 0) + ISNULL(AlipayFee, ");
                sql.AppendLine("0)) AlipayFee ");
                sql.AppendLine("FROM	O_PurchaseOrder T2 ");
                sql.AppendLine("LEFT JOIN 	O_FeeReturn T1 on T2.PayOrderCode=T1.BindCode ");
                sql.AppendLine($"WHERE		T2.PayDate >= '{start}' ");
                sql.AppendLine($"AND T2.PayDate < '{end}' ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T1.FeeReturnType = 1 ");
                sql.AppendLine("AND T1.Remark LIKE '采购订单%' ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T2.PayDate, 120) ");
                sql.AppendLine(") T3 ");
                sql.AppendLine("ON T1.Dates = T3.Dates ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	isnull(T1.Currency,'代采') Currency ");
                sql.AppendLine(",T1.Dates ");
                sql.AppendLine(",T1.TotalCharge - ISNULL(T2.ChargeAmt, 0) ");
                sql.AppendLine("- ISNULL(T3.AlipayFee, 0) TotalPurchaseCharge ");
                sql.AppendLine("FROM		( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T1.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.TotalCharge, 0)) TotalCharge ");
                sql.AppendLine(",isnull(T2.Currency,'代采') Currency ");
                sql.AppendLine("FROM		O_CustomerOrder T1 ");
                sql.AppendLine("LEFT JOIN O_PurchaseOrder T2 ");
                sql.AppendLine("ON T1.PayOrderCode = T2.PayOrderCode ");
                sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.OrderType = 6 ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T1.PayDate, 120) ");
                sql.AppendLine(",T2.Currency ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T2.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.ReturnFee, 0)) ChargeAmt ");
                sql.AppendLine(",isnull(T2.Currency,'代采') Currency ");
                sql.AppendLine("FROM O_PurchaseOrder T2		 ");
                sql.AppendLine("LEFT JOIN O_PurchaseFeeReturn T1 ");
                sql.AppendLine("ON T1.PurchaseOrderCode = T2.PurchaseOrderCode ");
                sql.AppendLine($"WHERE		T2.PayDate >= '{start}' ");
                sql.AppendLine($"AND T2.PayDate < '{end}' ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND T1.AllowReturn = 1 ");
                sql.AppendLine("AND T1.ReturnState = 1 ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T2.PayDate, 120) ");
                sql.AppendLine(",T2.Currency ");
                sql.AppendLine(") T2 ");
                sql.AppendLine("ON T1.Dates = T2.Dates ");
                sql.AppendLine("AND T1.Currency = T2.Currency ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T2.PayDate, 120) Dates ");
                sql.AppendLine(",SUM(ISNULL(T1.AccountFee, 0) ");
                sql.AppendLine("+ ISNULL(T1.AlipayFee, 0)) AlipayFee ");
                sql.AppendLine(",isnull(T2.Currency,'代采') Currency ");
                sql.AppendLine("FROM	O_PurchaseOrder T2 ");
                sql.AppendLine("LEFT JOIN 	O_FeeReturn T1 ");
                sql.AppendLine("ON T1.BindCode = T2.PayOrderCode ");
                sql.AppendLine($"WHERE		T2.PayDate >= '{start}' ");
                sql.AppendLine($"AND T2.PayDate < '{end}' ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T1.FeeReturnType = 1 ");
                sql.AppendLine("AND T1.Remark LIKE '采购订单%' ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', ");
                sql.AppendLine("'DDDDD', 'HHHHH', ");
                sql.AppendLine("'RJFSU', 'YYYYY', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY	CONVERT(NVARCHAR(7), T2.PayDate, 120) ");
                sql.AppendLine(",T2.Currency ");
                sql.AppendLine(") T3 ");
                sql.AppendLine("ON T1.Dates = T3.Dates ");
                sql.AppendLine("AND T1.Currency=T3.Currency ");
                sql.AppendLine(") T ");
                sql.AppendLine("WHERE	1 = 1 ");
                sql.AppendLine("*[T]*{0} ");
                sql.AppendLine("ORDER BY [T].[Dates];  ");                
               

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString(),out string checkSql, o_CustomerOrder);

                Console.WriteLine(checkSql);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDT;
        }

        public DataTable TransferCountry(string start, string end)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("IF EXISTS ( SELECT	* ");
                sql.AppendLine("FROM	tempdb.dbo.sysobjects ");
                sql.AppendLine("WHERE	id = OBJECT_ID(N'tempdb..#tmpTf') ");
                sql.AppendLine("AND type = 'U' ) ");
                sql.AppendLine("DROP TABLE #tmpTf; ");
                sql.AppendLine("ELSE ");
                sql.AppendLine("PRINT '不存在临时表'; ");
                sql.AppendLine("");
                sql.AppendLine("");
                sql.AppendLine("SELECT	SUBSTRING(T2.Name, 0, 5) Country ");
                sql.AppendLine(",SUM(ISNULL(T1.UseAccount, 0) + ISNULL(T1.UseAlipayAmount, 0)) TotalCharge ");
                sql.AppendLine("INTO	#tmpTf ");
                sql.AppendLine("FROM	O_CustomerOrder T1 ");
                sql.AppendLine("LEFT JOIN B_BussCenter T2 ");
                sql.AppendLine("ON T1.CenterCode = T2.CenterCode ");
                sql.AppendLine($"WHERE	T1.PayDate >= '{start}' ");
                sql.AppendLine($"AND T1.PayDate < '{end}' ");
                sql.AppendLine("AND T1.PayState = 1 ");
                sql.AppendLine("AND T1.OrderState = 0 ");
                sql.AppendLine("AND T1.OrderType IN ( 0, 3, 4 ) ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', 'HHHHH', 'RJFSU', ");
                sql.AppendLine("'YYYYY', 'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY SUBSTRING(T2.Name, 0, 5); ");
                sql.AppendLine("SELECT	'德国' AS Country,SUM([TotalCharge]) AS [TotalCharge] ");
                sql.AppendLine("FROM	[#tmpTf] ");
                sql.AppendLine("WHERE [Country] IN ('德国德累','德国法兰') ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT * FROM [#tmpTf] ");
                sql.AppendLine("WHERE [Country] NOT IN ('德国德累','德国法兰') ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serDT;
        }

        public DataTable GenerationBuyCurrency(string start, string end)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("select T1.Currency,T1.TotalCharge-isnull(T2.ChargeAmt,0)-isnull(T3.AlipayFee,0) TotalPurchaseCharge from ( ");
                sql.AppendLine("select T2.Currency,SUM(isnull(T1.TotalCharge,0)) TotalCharge ");
                sql.AppendLine("from O_CustomerOrder T1 left join O_PurchaseOrder T2 on T1.PayOrderCode=T2.PayOrderCode ");
                sql.AppendLine($"where T1.PayDate>='{start}' and T1.PayDate<'{end}' and T1.OrderType=6 and T1.PayState=1 ");
                sql.AppendLine("and T2.DelState=0 group by T2.Currency ");
                sql.AppendLine(") T1 left join (select  T2.Currency,SUM(isnull(T1.ReturnFee,0)) ChargeAmt  from O_PurchaseFeeReturn T1 ");
                sql.AppendLine("left join O_PurchaseOrder T2 on T1.PurchaseOrderCode=T2.PurchaseOrderCode ");
                sql.AppendLine($"where T1.ReturnDate>='{start}' and T1.ReturnDate<'{end}' and T1.DelState=0 and T1.AllowReturn=1 and T1.ReturnState=1 and T2.DelState=0 ");
                sql.AppendLine("and T2.UserCode not in ('CCCCC' , 'BBBBB' , 'DDDDD',  'HHHHH' , 'RJFSU' ,'YYYYY','APWHW','BUUBXA') ");
                sql.AppendLine("group by T2.Currency) T2 on T1.Currency=T2.Currency ");
                sql.AppendLine("left join (select T2.Currency,SUM(isnull(T1.AccountFee,0)+isnull(T1.AlipayFee,0)) AlipayFee from O_FeeReturn T1 ");
                sql.AppendLine("left join O_PurchaseOrder T2 on T1.BindCode=T2.PayOrderCode ");
                sql.AppendLine($"where T1.PayDate>='{start}' and T1.PayDate<'{end}' and T1.PayState=1 and T1.FeeReturnType=1 and T1.Remark like '采购订单%' ");
                sql.AppendLine("and T1.UserCode not in ('CCCCC' , 'BBBBB' , 'DDDDD',  'HHHHH' , 'RJFSU' ,'YYYYY','APWHW','BUUBXA') and T2.DelState=0 and T2.PayOrderCode is not null ");
                sql.AppendLine("group by T2.Currency) T3 on T1.Currency=T3.Currency ");                

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //优惠券获取
        public DataTable DailySpendingDiscounts(string start, string end)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT convert(NVARCHAR(5),[T1].[PayDate],10) 'Dates' ");
                sql.AppendLine(",SUM(ISNULL(T1.CouponsUseAmount, 0)) 'TotalCharge' ");
                sql.AppendLine("FROM O_CustomerOrder T1 ");
                sql.AppendLine($"WHERE	T1.PayDate >= '{start}'");
                sql.AppendLine($"AND T1.PayDate <= '{end}' and OrderType in (0,3,4,6) ");
                sql.AppendLine("GROUP BY convert(NVARCHAR(5),[T1].[PayDate],10) ");
                sql.AppendLine("ORDER BY convert(NVARCHAR(5),[T1].[PayDate],10) asc; ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //优惠券使用
        public DataTable DiscountsTotal(string start, string end)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT convert(NVARCHAR(5),[T1].[InputeDate],10) Dates ");
                sql.AppendLine(",SUM(T1.ParValue) TotalCharge ");
                sql.AppendLine("FROM	U_CouponsDetail T1 ");
                sql.AppendLine($"WHERE	T1.InputeDate >= '{start}' ");
                sql.AppendLine($"AND T1.InputeDate <= '{end}' ");
                sql.AppendLine("GROUP BY convert(NVARCHAR(5),[T1].[InputeDate],10) ");
                sql.AppendLine("ORDER BY convert(NVARCHAR(5),[T1].[InputeDate],10) asc; ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //用户提现
        public DataTable DailyWithDraw(DateTime d_e)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	ISNULL(SUM(ISNULL(WithdrawAmount, 0)), 0) 提现 ");
                sql.AppendLine(",RIGHT(CONVERT(VARCHAR(10), [CreateTime], 120), 5) [CreateTime] ");
                sql.AppendLine("FROM	U_Withdraw ");
                sql.AppendLine("WHERE	DelState = 0 ");
                sql.AppendLine($"AND CreateTime >= '{d_e.AddDays(-20).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND CreateTime < '{d_e.ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("AND UserCode NOT IN ( 'BBBBB', 'APWHW', 'WNKBMU' ) ");
                sql.AppendLine("GROUP BY RIGHT(CONVERT(VARCHAR(10), [CreateTime], 120), 5); ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //财务打款
        public DataTable DailyWithGiveMoney(DateTime d_e)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	ISNULL(SUM(ISNULL(CWWithdrawAmount, 0)), 0) 财务打款金额 ");
                sql.AppendLine(",RIGHT(CONVERT(VARCHAR(10), [AuditTime], 120), 5) [AuditTime] ");
                sql.AppendLine("FROM	U_Withdraw ");
                sql.AppendLine("WHERE	State = 2 ");
                sql.AppendLine("AND DelState = 0 ");
                sql.AppendLine($"AND AuditTime >= '{d_e.AddDays(-20).ToString("yyyy-MM-dd 00:00:00")}' ");
                sql.AppendLine($"AND AuditTime < '{d_e.ToString("yyyy-MM-dd 23:59:59")}' ");
                sql.AppendLine("GROUP BY RIGHT(CONVERT(VARCHAR(10), [AuditTime], 120), 5);  ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //转运财务实收监控
        public DataTable RealTimeFinancialMonitoring(DateTime d_e)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("select  (T1.Item101Alipay+T1.Item102Alipay+Item131Alipay+T1.Item132Alipay+T1.Item12Alipay+T1.Item01Alipay+T1.Item02Alipay) 支付宝, ");
                sql.AppendLine("(T1.Item1030Alipay+T1.Item1330Alipay+T1.Item030Alipay) 微信公众平台, ");
                sql.AppendLine("(T1.Item1031Alipay+T1.Item1331Alipay+T1.Item031Alipay) 微信开发平台, ");
                sql.AppendLine("(T1.Item15Alipay) NiHaoPay   , ");
                sql.AppendLine("(T2.WXAliPay1) 财务微信公众平台, ");
                sql.AppendLine("(T2.WXAliPay2) 财务微信开放平台, ");
                sql.AppendLine("(T2.ZFBAliPay) 财务支付宝, ");
                sql.AppendLine("(T2.NHAliPay) 财务NiHaoPay ");
                sql.AppendLine("from (select * from F_DayThirdPartyPay T1 ) T1  ");
                sql.AppendLine("left join F_DayThirdPartyPayByFinanceEntry  T2 ON T1.Id=T2.DayThirdPartyPayId and T2.DelState=0  ");
                sql.AppendLine($"where T1.DayReport >= '{d_e.AddDays(-1).ToString("yyyy-MM-dd")}' ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //余额监控
        public DataTable BalanceMonitoring(DateTime d_e)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	BalanceAdd ");
                sql.AppendLine(",BalanceSub ");
                sql.AppendLine(",OpeningBalance ");
                sql.AppendLine(",ActualBalance ");
                sql.AppendLine(",TheoryBalance ");
                sql.AppendLine(",GapBalance ");
                sql.AppendLine("FROM	F_DayAccountBalance ");
                sql.AppendLine($"WHERE	[DayReport] >= '{d_e.AddDays(-1).ToString("yyyy-MM-dd")}' ");
                sql.AppendLine("AND [CoinType] = 'C0'; ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //海带币监控
        public DataTable TapeCoinMonitoring(DateTime d_e)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM	F_DayPoints ");
                sql.AppendLine($"WHERE	[DayReport] >= '{d_e.AddDays(-1).ToString("yyyy-MM-dd")}'; ");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }

        //20天海带币
        public DataTable TweentlyTapeCoin(string startTime, string endTime)
        {
            DataTable serDT = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	RIGHT(CONVERT(VARCHAR(10),[DayReport],120),5) AS [DayReport] ");
                sql.AppendLine(",PointsAdd ");
                sql.AppendLine(",PointsSub ");
                sql.AppendLine("FROM	F_DayPoints");
                sql.AppendLine($"WHERE [DayReport] BETWEEN '{startTime}' AND '{endTime}';");

                Console.WriteLine(sql.ToString());

                serDT = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDT;
        }
    }
}
