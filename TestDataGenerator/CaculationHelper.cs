using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDataGenerator
{
    public class ConditionInfo
    {
        public double A { get; set; }
        public double B { get; set; }
        public string Oper { get; set; }
    }

    public class CaculationHelper
    {
        public static double CaculateDistanceFitness(ConditionInfo condition)
        {
            const double k = 0.01;
            const double precision = 0.000001;
            var a = condition.A;
            var b = condition.B;
            var oper = condition.Oper;

            switch (oper)
            {
                case "==":
                    return Math.Abs(a - b) < precision ? 0 : Math.Abs(a - b);
                case "!=":
                    return Math.Abs(a - b) > precision ? 0 : k;
                case "<":
                    return a < b ? 0 : a - b + k;
                case "<=":
                    return a <= b ? 0 : a - b;
                case ">":
                    return a > b ? 0 : b - a + k;
                case ">=":
                    return a >= b ? 0 : b - a;
                case "&&":
                    return Math.Abs(a) < precision && Math.Abs(b) < precision ? 0 : Math.Min(a, b);
                case "||":
                    return Math.Abs(a) < precision || Math.Abs(b) < precision ? 0 : a + b;
                default:
                    return double.MaxValue;
            }
        }

        public static double CaculateTotalDistanceFitness(List<ConditionInfo> conditions)
        {
            return -conditions.Sum(c => CaculateDistanceFitness(new ConditionInfo {A = c.A, B = c.B, Oper = c.Oper}));
        }
    }
}