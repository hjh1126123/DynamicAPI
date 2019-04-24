using MaterialDesignThemes.Wpf;
using System;

namespace ReportApp
{
    public class Global
    {
        private static readonly Lazy<Global> lazyInstance = new Lazy<Global>(() => new Global());
        public static Global Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        /// <summary>
        /// 黑色主题
        /// </summary>
        public bool IsDark { get; set; }


        public Snackbar TheMessageBox { get; set; }
        public void ShowMessage(string message)
        {
            TheMessageBox.MessageQueue.Enqueue(message);
        }
    }
}
