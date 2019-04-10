using System;
using System.Security.Cryptography;

namespace Util
{
    public class URandom
    {
        private static readonly Lazy<URandom> lazyInstance = new Lazy<URandom>(() => new URandom());
        public static URandom Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        private readonly Random random;
        public URandom()
        {
            byte[] b = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(b);
            random = new Random(BitConverter.ToInt32(b, 0));
        }

        public string GetRandomString(int length, bool useNum = true, bool useLow = true, bool useUpp = true, bool useSpe = true)
        {
            if (random == null)
                return "随机数对象不存在";

            string tmpStr = string.Empty;
            if (useNum == true) { tmpStr += "0123456789"; }
            if (useLow == true) { tmpStr += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { tmpStr += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { tmpStr += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            string result = string.Empty;
            for (int i = 0; i < length; i++)
            {
                result += tmpStr.Substring(random.Next(0, tmpStr.Length - 1), 1);
            }
            return result;
        }
    }
}
