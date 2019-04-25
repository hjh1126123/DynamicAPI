using System;
using System.Linq;

namespace Server.DBLocal
{
    public class Theme
    {
        private Boolean dark;
        private string primary;
        private string accent;

        public bool Dark { get => dark; set => dark = value; }
        public string Primary { get => primary; set => primary = value; }
        public string Accent { get => accent; set => accent = value; }
    }

    public class U_Theme : DBComponent
    {
        public UTheme Select()
        {
            return Context(db =>
            {
                return db.UThemes.FirstOrDefault();
            });
        }

        public bool Update(Theme theme)
        {
            return Context(db =>
            {
                UTheme ut = db.UThemes.Where(i => i.Id == 1).FirstOrDefault();

                ut.Isdark = theme.Dark;

                ut.Accent = string.IsNullOrWhiteSpace(theme.Accent) ? ut.Accent : theme.Accent;

                ut.Primary = string.IsNullOrWhiteSpace(theme.Primary) ? ut.Primary : theme.Primary;

                db.SubmitChanges();

                return true;
            });
        }
    }
}
