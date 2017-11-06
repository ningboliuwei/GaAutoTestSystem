﻿using System.Collections.Generic;

namespace TestDataGenerator
{
    public class AssertionInfo
    {
        public string Id { get; set; }
        public List<double> InputValues { get; set; } = new List<double>();
        public object ExpectedOutput { get; set; } = "";
        public object ActualOutput { get; set; } = "";
        public string Result { get; set; } = "";
    }
}