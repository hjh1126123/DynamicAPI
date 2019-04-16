using Data.Local;
using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var _u = new U_User();


            var users = _u.CheckUser("hjh","123456");

            Console.Read();
        }
    }
}
