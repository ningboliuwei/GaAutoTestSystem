﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TestDataGenerator;

namespace TestCodeGenerator
{
    public class UnitTestCodeGenerator
    {
        public static void GenerateNUnitCode(TestSuiteInfo testSuite, string targetFilePath)
        {
            var builder = new StringBuilder();

            builder.AppendLine("using NUnit.Framework;");
            builder.AppendLine("namespace TestCodeGenerator");
            builder.AppendLine("{");
            builder.AppendLine("\t[TestFixture]");
            var pattern = @"\.(\w+)";
            var match = Regex.Match(testSuite.Target, pattern);
            var methodName = match.Groups[1].Value;
            var className = "FunctionLib";
            builder.AppendLine($"\tpublic class {className}Tests");
            builder.AppendLine("\t{");
            builder.AppendLine("\t\t[Test]");
            builder.AppendLine($"\t\tpublic void {methodName}Test()");
            builder.AppendLine("\t\t{");
            foreach (var assertion in testSuite.TestCases.SelectMany(c => c.Assertions).ToList())
            {
                var expectedOutput = assertion.ExpectedOutput;
                var argumentList = string.Join(", ", assertion.InputValues);
                builder.AppendLine($"\t\t\tAssert.AreEqual(\"{expectedOutput}\", {className}.{methodName}({argumentList}));");
            }
            builder.AppendLine("\t\t}");
            builder.AppendLine("\t}");
            builder.AppendLine("}");
            var fileContent = builder.ToString();

            using (var writer = new StreamWriter(targetFilePath))
            {
                try
                {
                    writer.Write(fileContent);
                    writer.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}