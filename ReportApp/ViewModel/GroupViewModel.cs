﻿using PropertyChanged;
using Server.DBLocal;
using System.Collections.Generic;

namespace ReportApp.ViewModel
{
    [ImplementPropertyChanged]
    public class GroupViewModel
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public List<BGroup> Groups { get; set; }
        
    }
}
