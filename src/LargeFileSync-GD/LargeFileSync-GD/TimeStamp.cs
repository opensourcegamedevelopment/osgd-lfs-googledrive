﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeFileSync_GD
{
    public class Data
    {
        public string fileId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
    }

    public class TimeStamp
    {
        public List<Data> data = new List<Data>();
    }
}
