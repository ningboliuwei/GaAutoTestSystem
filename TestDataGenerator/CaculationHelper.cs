﻿using System;
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
        //计算条件表达式面向距离适应度
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
        //计算所有条件表达式的总适应度（每个适应度加起来）
        public static double CaculateTotalDistanceFitness(List<ConditionInfo> conditions)
        {
            return -conditions.Sum(c => CaculateDistanceFitness(new ConditionInfo {A = c.A, B = c.B, Oper = c.Oper}));
        }

        //基于路径匹配（找实际执行路径与期望路径的最长子串长度）
        public static double CaculatePathMatchFitness(string track, string target)
        {
            //最长子串长度
            return GetLongestCommonSubstringLength(track, target);
        }

        //基于节点匹配（找实际执行路径与期望路径的相同节点个数）
        public static double CaculateNodeMatchFitness(string track, string target)
        {
            //去掉路径中重复的节点
            var nodesOfTrack = track.Select(c => c).Distinct();
            var nodesOfTarget = target.Select(c => c).Distinct();
            //相同节点个数
            return nodesOfTarget.Intersect(nodesOfTrack).Count();
        }

        private static int GetLongestCommonSubstringLength(string str1, string str2)
        {
            //保持 str1 的长度大于等于 str2  
            if (str1.Length < str2.Length)
            {
                var temp = str1;
                str1 = str2;
                str2 = temp;
            }

            var allSubstrings = GetAllSubstrings(str2);

            for (var length = allSubstrings.Count; length >= 1; length--)
                foreach (var s in allSubstrings[length - 1])
                    if (str1.Contains(s))
                        return length;
            return 0;
        }
        //获得所有子串（各种长度的）
        private static List<List<string>> GetAllSubstrings(string str)
        {
            var maxLength = str.Length;
            var substrings = new List<List<string>>();

            for (var length = 1; length <= maxLength; length++)
            {
                var substringsOfCurrentLength = new List<string>();

                for (var start = 0; start <= maxLength - length; start++)
                    if (start + length <= maxLength)
                        substringsOfCurrentLength.Add(str.Substring(start, length));
                substrings.Add(substringsOfCurrentLength);
            }

            return substrings;
        }
    }
}