﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.ResourceParameters
{
    public class CentreResourceParameters
    {
        const int maxPageSize = 100;
        public string CentreNo { get; set; }
        public string SearchQuery { get; set; }

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string OrderBy { get; set; } = "CentreNo";
        public string Fields { get; set; }
    }
}
