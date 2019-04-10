using ServerApp.Command;
using ServerApp.ServeTemplate;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServerApp.Model
{
    public class ServerControlModel : IDisposable
    {
        bool checkState;
        public void Dispose()
        {
            checkState = false;
        }

        /// <summary>
        /// 服务卡片控件
        /// </summary>
        /// <param name="source">图片资源路径</param>
        /// <param name="playBtnToolTip">运行按钮提示</param>
        /// <param name="cardTitle">卡片标题</param>
        /// <param name="cardText">卡片内容</param>
        /// <param name="setButtonToolTip">设置按钮提示</param>
        public ServerControlModel(string source, string playBtnToolTip, string cardTitle, string cardText, string setButtonToolTip, Func<bool> CheckState, IServeTemplate serveTemplate)
        {
            Source = source;
            PlayButtonToolTip = playBtnToolTip;
            CardTitle = cardTitle;
            CardText = cardText;
            SetButtonToolTip = setButtonToolTip;
            ServeIsRun = CheckState();

            this.serveTemplate = serveTemplate;

            checkState = true;
            Task.Factory.StartNew(() =>
            {
                while (checkState)
                {
                    Thread.Sleep(300);
                    ServeIsRun = CheckState();
                }
            }).ContinueWith(t =>
            {
                t.Dispose();
            });
        }

        /// <summary>
        /// 服务运行状态
        /// </summary>
        public bool ServeIsRun { get; private set; }

        /// <summary>
        /// 事件服务模板
        /// </summary>
        private IServeTemplate serveTemplate;

        /// <summary>
        /// 点击事件
        /// </summary>
        public ICommand ClickAction
        {
            get
            {
                return new AnotherCommandImplementation(o => Click((Button)o));
            }
        }
        private void Click(object sender)
        {
            if (!ServeIsRun)
                serveTemplate.OpenClick();
            else
                serveTemplate.CloseClick();
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// 运行按钮提示
        /// </summary>
        public string PlayButtonToolTip { get; }

        /// <summary>
        /// 标题
        /// </summary>
        public string CardTitle { get; }

        /// <summary>
        /// 文本
        /// </summary>
        public string CardText { get; }

        /// <summary>
        /// 设置按钮提示
        /// </summary>
        public string SetButtonToolTip { get; }
    }
}
