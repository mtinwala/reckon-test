using System;
using System.Collections.Generic;
using System.Text;

namespace DivisorConsoleApp
{
    public class RangeInfo
    {
        public int lower { get; set; }
        public int upper { get; set; }
    }

    public class OutputDetail
    {
        public int divisor { get; set; }
        public string output { get; set; }
    }

    public class DivisorInfo
    {
        public OutputDetail[] outputDetails { get; set; }
    }
}
