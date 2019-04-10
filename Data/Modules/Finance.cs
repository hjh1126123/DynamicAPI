using System.Collections.Generic;
using System.Text;

namespace Data.Modules
{
    public class MonthlyIncome : IData
    {
        public MonthlyIncome(Dictionary<string, List<DataModel>> @params) : base(@params)
        {
        }

        public MonthlyIncome(List<DataModel> dms) : base(dms)
        {
        }

        protected override void Build(StringBuilder sql)
        {
            sql.AppendLine("SELECT	T1.PayDate ");
            sql.AppendLine(",( ISNULL(T1.TotalCharge, 0) + ISNULL(T2.TotalPurchaseCharge, 0) ) AS [TotalCharge] ");
            sql.AppendLine("FROM	( ");
            sql.AppendLine("SELECT	CONVERT(NVARCHAR(7), T1.PayDate, 120) PayDate ");
            sql.AppendLine(",SUM(ISNULL(T1.UseAccount, 0) + ISNULL(T1.UseAlipayAmount, ");
            sql.AppendLine("0)) TotalCharge ");
            sql.AppendLine("FROM		O_CustomerOrder T1 ");

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("T1");
            //sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
            //sql.AppendLine($"AND T1.PayDate < '{end}' ");
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

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("T1");
            //sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
            //sql.AppendLine($"AND T1.PayDate < '{end}' ");
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

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("T1");
            //sql.AppendLine($"WHERE	T1.ReturnDate >= '{start}' ");
            //sql.AppendLine($"AND T1.ReturnDate < '{end}' ");
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

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("");
            //sql.AppendLine($"WHERE	PayDate >= '{start}' ");
            //sql.AppendLine($"AND PayDate < '{end}' ");
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
        }
    }

    public class TransferGenerationBuyProportion : IData
    {
        public TransferGenerationBuyProportion(Dictionary<string, List<DataModel>> @params) : base(@params)
        {
        }

        public TransferGenerationBuyProportion(List<DataModel> dms) : base(dms)
        {
        }

        protected override void Build(StringBuilder sql)
        {
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

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("T1");
            //sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
            //sql.AppendLine($"AND T1.PayDate < '{end}' ");
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
            //sql.AppendLine($"WHERE		T1.PayDate >= '{start}' ");
            //sql.AppendLine($"AND T1.PayDate < '{end}' ");

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("T1");
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

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("T1");
            //sql.AppendLine($"WHERE		T1.ReturnDate >= '{start}' ");
            //sql.AppendLine($"AND T1.ReturnDate < '{end}' ");
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
            //sql.AppendLine($"WHERE		PayDate >= '{start}' ");
            //sql.AppendLine($"AND PayDate < '{end}' ");

            sql.AppendLine("WHERE 1=1 ");
            AddWhere("");
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
        }
    }


}
