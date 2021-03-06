﻿using ReportApp.ViewModel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ReportApp.Panel
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class PanelHome : UserControl
    {
        public PanelHome()
        {
            InitializeComponent();

            DataContext = new HomeViewModel(new List<ViewModel.Home>
            {
                new ViewModel.Home( "版本 1.0.0.0",
                "1.底层架构全面升级\n" +
                "2.SQL可以动态维护\n" +
                "3.本地保存使用SQLite\n" +
                "4.作业调度使用Quartz\n" +
                "5.以WebApi方式外放接口" +
                "6.隔离接口数据与数据库数据，即接口不直接请求数据库",
                "最新版本 1.0.0.0")
            });
        }
    }
}
