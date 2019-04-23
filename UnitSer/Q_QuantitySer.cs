using System;
using System.Text;
using System.Data;
using HDBChartModel.ServiceModel;

namespace HDBChartService
{
    public class Q_QuantitySer
    {
        public DataTable MonthlyQuantity(string start, string end, string l_start, string l_end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	T1.CenterName ");
                sql.AppendLine(",T1.InCountThisYear ");
                sql.AppendLine(",[T2].[InCountLastYear] ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	SUBSTRING(T1.Name, 0, 3) + '仓' CenterName ");
                sql.AppendLine(",COUNT(T1.Id) InCountThisYear ");
                sql.AppendLine("FROM		B_BussCenter T1 ");
                sql.AppendLine("LEFT JOIN E_OverseasExpress T2 ");
                sql.AppendLine("ON T1.CenterCode = T2.CenterCode ");
                sql.AppendLine($"AND T2.InDate >= '{start}' ");
                sql.AppendLine($"AND T2.InDate < '{end}' ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T2.State = 1 ");
                sql.AppendLine("WHERE T1.CenterCode IN ( 'USDB', 'JPDB', 'JPSFDB', 'AUDB', ");
                sql.AppendLine("'GERDB','DEDB', 'UKDB', 'ZTDB' ) ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', ");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY SUBSTRING(T1.Name, 0, 3) ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("LEFT JOIN ( ");
                sql.AppendLine("SELECT	SUBSTRING(T1.Name, 0, 3) + '仓' CenterName ");
                sql.AppendLine(",COUNT(T1.Id) InCountLastYear ");
                sql.AppendLine("FROM	B_BussCenter T1 ");
                sql.AppendLine("LEFT JOIN V_E_OverseasExpress T2 ");
                sql.AppendLine("ON T1.CenterCode = T2.CenterCode ");
                sql.AppendLine($"AND T2.InDate >= '{l_start}' ");
                sql.AppendLine($"AND T2.InDate < '{l_end}' ");
                sql.AppendLine("AND T2.DelState = 0 ");
                sql.AppendLine("AND T2.State = 1 ");
                sql.AppendLine("WHERE	T1.CenterCode IN ( 'USDB', 'JPDB', 'JPSFDB', 'AUDB', ");
                sql.AppendLine("'GERDB', 'DEDB', 'UKDB', 'ZTDB' ) ");
                sql.AppendLine("AND T2.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU',");
                sql.AppendLine("'APWHW', 'BUUBXA' ) ");
                sql.AppendLine("GROUP BY SUBSTRING(T1.Name, 0, 3) + '仓' ");
                sql.AppendLine(") T2 ");
                sql.AppendLine("ON T1.CenterName = T2.[CenterName]; ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        public DataTable QuantityProportion(string start, string end, string l_start, string l_end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	CONVERT(VARCHAR(4), T1.InDate, 23) InYear ");
                sql.AppendLine(",DATEPART(M, T1.InDate) InMonth ");
                sql.AppendLine(",COUNT(T1.Id) InCount ");
                sql.AppendLine("FROM		E_OverseasExpress T1 ");
                sql.AppendLine("WHERE		T1.DelState = 0 ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', 'APWHW', ");
                sql.AppendLine("'BUUBXA' ) ");
                sql.AppendLine("AND T1.[CenterCode] IN ('AUDB','JPDB','USDB','JPSFDB','UKDB','DEDB','ZTDB','GERDB') ");
                sql.AppendLine($"AND T1.[CreateDate] >= '{start}' ");
                sql.AppendLine($"AND T1.[CreateDate] < '{end}' ");
                sql.AppendLine("GROUP BY	CONVERT(VARCHAR(4), T1.InDate, 23) ");
                sql.AppendLine(",DATEPART(M, T1.InDate) ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	CONVERT(VARCHAR(4), T1.InDate, 23) InYear ");
                sql.AppendLine(",DATEPART(M, T1.InDate) InMonth ");
                sql.AppendLine(",COUNT(T1.Id) InCount ");
                sql.AppendLine("FROM		v_E_OverseasExpress T1 ");
                sql.AppendLine("WHERE		T1.DelState = 0 ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', 'APWHW', ");
                sql.AppendLine("'BUUBXA' ) ");
                sql.AppendLine("AND T1.[CenterCode] IN ('AUDB','JPDB','USDB','JPSFDB','UKDB','DEDB','ZTDB','GERDB') ");
                sql.AppendLine($"AND T1.[CreateDate] >= '{l_start}' ");
                sql.AppendLine($"AND T1.[CreateDate] < '{l_end}' ");
                
                sql.AppendLine("GROUP BY	CONVERT(VARCHAR(4), T1.InDate, 23) ");
                sql.AppendLine(",DATEPART(M, T1.InDate) ");
                sql.AppendLine(") AS sel PIVOT ( SUM(InCount) FOR InYear IN ( [2010], [2011], [2012], ");
                sql.AppendLine("[2013], [2014], [2015], ");
                sql.AppendLine("[2016], [2017], [2018], ");
                sql.AppendLine("[2019], [2020], [2021], ");
                sql.AppendLine("[2022], [2023], [2024], ");
                sql.AppendLine("[2025], [2026], [2027], ");
                sql.AppendLine("[2028], [2029], [2030] ) ) AS pvt ");
                sql.AppendLine("ORDER BY InMonth;    ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        public DataTable MonthlyWarehouseVolume(string centerCode, string start, string end, string l_start, string l_end)
        {
            DataTable serDt = null;
            try
            {
                Q_Quantity q_Quantity = new Q_Quantity()
                {
                    CenterCodeIn = centerCode
                };

                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	* ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT	CONVERT(VARCHAR(4), T1.InDate, 23) InYear ");
                sql.AppendLine(",DATEPART(M, T1.InDate) InMonth ");
                sql.AppendLine(",COUNT(T1.Id) InCount ");
                sql.AppendLine("FROM		E_OverseasExpress T1 ");
                sql.AppendLine("WHERE		T1.DelState = 0 ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', 'APWHW', ");
                sql.AppendLine("'BUUBXA' ) ");
                sql.AppendLine($"AND T1.[CreateDate] >= '{start}' ");
                sql.AppendLine($"AND T1.[CreateDate] < '{end}' ");
                sql.AppendLine("*[T1]*{0} ");
                sql.AppendLine("GROUP BY	CONVERT(VARCHAR(4), T1.InDate, 23) ");
                sql.AppendLine(",DATEPART(M, T1.InDate) ");
                sql.AppendLine("UNION ");
                sql.AppendLine("SELECT	CONVERT(VARCHAR(4), T1.InDate, 23) InYear ");
                sql.AppendLine(",DATEPART(M, T1.InDate) InMonth ");
                sql.AppendLine(",COUNT(T1.Id) InCount ");
                sql.AppendLine("FROM		v_E_OverseasExpress T1 ");
                sql.AppendLine("WHERE		T1.DelState = 0 ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.UserCode NOT IN ( 'CCCCC', 'BBBBB', 'DDDDD', ");
                sql.AppendLine("'HHHHH', 'RJFSU', 'APWHW', ");
                sql.AppendLine("'BUUBXA' ) ");
                sql.AppendLine($"AND T1.[CreateDate] >= '{l_start}' ");
                sql.AppendLine($"AND T1.[CreateDate] < '{l_end}' ");
                sql.AppendLine("*[T1]*{0} ");
                sql.AppendLine("GROUP BY	CONVERT(VARCHAR(4), T1.InDate, 23) ");
                sql.AppendLine(",DATEPART(M, T1.InDate) ");
                sql.AppendLine(") AS sel PIVOT ( SUM(InCount) FOR InYear IN ( [2013], [2014], [2015], ");
                sql.AppendLine("[2016], [2017], [2018], ");
                sql.AppendLine("[2019], [2020], [2021], ");
                sql.AppendLine("[2022], [2023], [2024], ");
                sql.AppendLine("[2025], [2026], [2027], ");
                sql.AppendLine("[2028], [2029], [2030] ) ) AS pvt ");
                sql.AppendLine("ORDER BY InMonth;    ");

                serDt = ServiceConfig.GetInstance().GetOperation().CusQuery(sql.ToString(), q_Quantity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serDt;
        }

        public DataTable OfflineAQuantity(string start, string end)
        {
            DataTable serDt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT	MONTH(T1.InDate) InDate ");
                sql.AppendLine(",SUM(USXX) USXX ");
                sql.AppendLine(",SUM(JPXX) JPXX ");
                sql.AppendLine(",SUM(HGXX) HGXX ");
                sql.AppendLine(",SUM(AUXX) AUXX ");
                sql.AppendLine("FROM	( ");
                sql.AppendLine("SELECT DISTINCT ");
                sql.AppendLine("T1.InDate ");
                sql.AppendLine(",T3.BoxCode ");
                sql.AppendLine(",MAX(CASE WHEN T3.LineCode = 'USXX' THEN 1 ");
                sql.AppendLine("ELSE 0 ");
                sql.AppendLine("END) AS 'USXX' ");
                sql.AppendLine(",MAX(CASE WHEN T3.LineCode = 'JPXX' ");
                sql.AppendLine("OR T3.UserCode IN ( 'NSAMS', 'KZBZM' ) ");
                sql.AppendLine("THEN 1 ");
                sql.AppendLine("ELSE 0 ");
                sql.AppendLine("END) AS 'JPXX' ");
                sql.AppendLine(",MAX(CASE WHEN T3.LineCode = 'HGXX' THEN 1 ");
                sql.AppendLine("ELSE 0 ");
                sql.AppendLine("END) AS 'HGXX' ");
                sql.AppendLine(",MAX(CASE WHEN T3.LineCode = 'AUXX' THEN 1 ");
                sql.AppendLine("ELSE 0 ");
                sql.AppendLine("END) AS 'AUXX'						 ");
                sql.AppendLine("FROM		E_OverseasExpress T1 ");
                sql.AppendLine("LEFT JOIN R_Express_Box T2 ");
                sql.AppendLine("ON T1.ExpressCode = T2.ExpressCode ");
                sql.AppendLine("LEFT JOIN P_Box T3 ");
                sql.AppendLine("ON T2.BoxCode = T3.BoxCode ");
                sql.AppendLine($"WHERE		T1.InDate >= '{start}' ");
                sql.AppendLine($"AND T1.InDate < '{end}' ");
                sql.AppendLine("AND T1.State = 1 ");
                sql.AppendLine("AND T1.DelState = 0 ");
                sql.AppendLine("AND ( ");
                sql.AppendLine("T3.LineCode IN ( 'USXX', 'JPXX', 'HGXX', 'AUXX' ) ");
                sql.AppendLine("OR T3.UserCode IN ( 'NSAMS', 'KZBZM' ) ");
                sql.AppendLine(") ");
                sql.AppendLine("AND T3.DelState = 0 ");
                sql.AppendLine("AND T3.IsBox = 0 ");
                sql.AppendLine("AND T3.StateWH = 0 ");
                sql.AppendLine("GROUP BY	T1.InDate ");
                sql.AppendLine(",T3.BoxCode ");
                sql.AppendLine(") T1 ");
                sql.AppendLine("GROUP BY MONTH(T1.InDate) ");
                sql.AppendLine("ORDER BY MONTH(T1.InDate);  ");

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
