using ServerApp.Command;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using System.Windows.Controls;

namespace ServerApp.Model
{
    public class MSQLMaintenance
    {
        public struct BoxItem
        {
            private int id;
            private string text;

            public int Id { get => id; set => id = value; }
            public string Text { get => text; set => text = value; }
        }

        private IEnumerable<BoxItem> sqlGroup;
        private IEnumerable<BoxItem> sqlActive;
        private string sql;

        public IEnumerable<BoxItem> SqlGroup { get => sqlGroup; set => sqlGroup = value; }
        public IEnumerable<BoxItem> SqlActive { get => sqlActive; set => sqlActive = value; }
        public string Sql { get => sql; set => sql = value; }

        public MSQLMaintenance(IEnumerable<BoxItem> sqlType, IEnumerable<BoxItem> sqlActive, string sql)
        {
            this.sqlGroup = sqlType;
            this.sqlActive = sqlActive;
            this.sql = sql;
        }

        public ICommand ChangeGroupAction
        {
            get
            {
                return new AnotherCommandImplementation(o => ChangeGroup(o));
            }
        }
        private void ChangeGroup(object o)
        {
            Console.WriteLine(o);
        }

        public ICommand ChangeActiveAction
        {
            get
            {
                return new AnotherCommandImplementation(o => ChangeActive(o));
            }
        }
        private void ChangeActive(object o)
        {
            Console.WriteLine(o);
        }
    }
}
