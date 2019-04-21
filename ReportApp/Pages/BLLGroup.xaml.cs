using ServerApp.INotify;
using ServerApp.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using Data;
using Data.Local;

namespace ServerApp.Pages
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class BLLGroup : UserControl
    {
        BLLAddNotify addNotify;

        public BLLGroup()
        {
            InitializeComponent();

            if (addNotify == null)
            {
                addNotify = new BLLAddNotify();
            }

            DataContext = addNotify;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //addNotify.BllAdd = new MBLLAdd(DBKeeper.Instance.DBObject<B_Group>().SelectAll());
        }

        private void SQL_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {

            }
        }
    }
}
