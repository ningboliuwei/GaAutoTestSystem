﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestDataGenerator
{
    public class TestSuiteInfo
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public List<TestCaseInfo> TestCases { get; set; } = new List<TestCaseInfo>();

        //得到被测函数名（如：TriangleType）
        public string FunctionName
        {
            get
            {
                var pattern = @"\.(\w+)";
                var match = Regex.Match(Target, pattern);
                var methodName = match.Groups[1].Value;

                return methodName;
            }
        }
    }
}