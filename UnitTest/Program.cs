using Data;
using Data.Modules;
using System.Collections.Generic;
using System.Data;
using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataTable dataTable = DataControl.Instance.Select(new MonthlyIncome(new List<DataModel>{
                    new DataModel
                    {
                        Key = "PayDate",
                        Condition = ">=",
                        Value = "2018-01-01 23:59:59"
                    }
                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Console.Read();
        }
    }
}
