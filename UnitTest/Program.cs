using Server;
using Server.Local;
using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DBKeeper.Instance.DBObject<D_MsSQL>().Update(new MsSQL {
                Id = 2,
                Operator = "hjy"
            });

            Console.Read();
        }
    }
}
