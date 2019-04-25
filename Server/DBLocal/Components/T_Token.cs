using System;
using System.Linq;
using Tool;

namespace Server.DBLocal
{
    public class T_Token : DBComponent
    {
        public bool CheckToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            return Context(db =>
            {
                return !db.STokens.Where(i => i.Token.Equals(token)).FirstOrDefault().Invalid;
            });
        }

        public SToken Add(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            return Context(db =>
            {
                SToken sToken = new SToken
                {
                    Systime = DateTime.Now,
                    Createtime = DateTime.Now,
                    Invalid = false,
                    Operator = "hjh",
                    Token = token,
                    Tid = TRandom.Instance.GetRandomString(20)
                };

                db.STokens.InsertOnSubmit(sToken);

                db.SubmitChanges();

                return sToken;
            });
        }
    }
}
