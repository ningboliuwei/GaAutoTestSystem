﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GaAutoTestSystem
{
    internal class StubbedFunction
    {
        //解空间 [0,1]
        public static double FitnessFunction1(params double[] paras)
        {
            var x = paras[0];
            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        //解空间 [0,9]
        public static double FitnessFunction2(params double[] paras)
        {
            var x = paras[0];

            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        public static double StubbedBranchTest1_CodeCoverage(params double[] paras)
        {
            var x = paras[0];
            var y = paras[1];
            var path = "#";

            if (x >= 80)
            {
                path += "a";
                if (y < 50)
                    path += "b";
            }

            return path.Length / 3.0;
        }


        //简单分支函数（插桩，用于计算适应度——面向距离方法的）
        public static double StubbedBranchTest1_B(params double[] paras)
        {
            var f1 = 0.0;
            var f2 = 0.0;
            var x = paras[0];
            var y = paras[1];
            const int k = 1;
            //
            if (80 - x <= 0)
                f1 = 0;
            else
                f1 = Math.Abs(80 - x) + k;
            //

            if (x > 80)
                if (y - 50 < 0)
                    f2 = 0;
                else
                    f2 = Math.Abs(50 - y) + k;

            return -(f1 + f2);
        }

        public static double StubbedBranchTest2_CodeCoverage(params double[] paras)
        {
            double k = 0;
            double j = 0;
            var x = paras[0];
            var y = paras[1];
            var z = paras[2];
            var path = "#";

            if (x > 1 && z < 10)
            {
                k = x + z;
                j = k * k;
                path += "a";
            }

            if (y == 4 || z > 1)
            {
                j = z * y + 10;
                path += "b";
            }

            j = j % 3;
            path += "c";

            return path.Length / 4.0;
        }

        public static double StubbedTriangleTypeTest_B(params double[] paras)
        {
            var f1 = 0;
            var f2 = 0;
            var f3 = 0;
            var f4 = 0;
            var f5 = 0;
            var k = 1;
            var x = (int) paras[0];
            var y = (int) paras[0];
            var z = (int) paras[0];

            if (x + y > z && x + z > y && y + z > x)
            {
                f1 = 0;

                if (x == y && y == z)
                {
                    f2 = 0;
                    //                    type = "equilateral triangle";
                }
                else
                {
                    //这里真的是 MAX 吗？
                    f2 = new List<int> {Math.Abs(x - y), Math.Abs(y - z)}.Sum();

                    if (x == y || y == z || x == z)
                        f3 = 0;
                    else
                        f3 = new List<int> {Math.Abs(x - y), Math.Abs(y - z), Math.Abs(x - z)}.Min();
                }
            }
            else
            {
                f1 = new List<int> {z - (x + y) + k, y - (x + z) + k, x - (y + z) + k}.Min();
                //                type = "not a triangle";
            }
            return -Math.Abs(f1 + f2 + f3);
        }

        public static double StubbedTriangleTypeTestPathCoverage(int x, int y, int z)
        {
            var path = "";
            if (x + y > z && x + z > y && y + z > x)
            {
                path += "a";
                if (x == y && y == z)
                {
                    path += "b";
                    //                    type = "equilateral triangle";
                }
                else
                {
                    path += "c";
                    //这里真的是 MAX 吗？

                    if (x == y || y == z || x == z)
                        path += "d";
                    else
                        path += "e";
                }
            }
            else
            {
                path += "f";
            }
            //                type = "not a triangle";
            return path.Length / 6.0;
        }

        public static string StubbedNextDate(int year, int month, int day)
        {
            var f1 = 0;
            var f2 = 0;
            var f3 = 0;
            var f4 = 0;
            var f5 = 0;
            var f6 = 0;
            var f7 = 0;
            var f8 = 0;
            var f9 = 0;
            var f10 = 0;
            var f11 = 0;
            var f12 = 0;
            var f13 = 0;
            var f14 = 0;
            var f15 = 0;
            var f16 = 0;

            var errorMessage = "Invalid date";

            if (year >= 1950 && year < 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                f1 = 0;
                //大月
                if (month == 12)
                {
                    if (day == 31)
                    {
                        year++;
                        day = 1;
                        month = 1;
                    }
                    else
                    {
                        day++;
                    }
                }
                else
                {
                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10)
                    {
                        if (day == 31)
                        {
                            day = 1;
                            month++;
                        }
                        else
                        {
                            day++;
                        }
                    }
                    else
                    {
                        if (month == 4 || month == 6 || month == 9 || month == 11)
                        {
                            if (day == 31)
                                return errorMessage;
                            if (day == 30)
                            {
                                day = 1;
                                month++;
                            }
                            else
                            {
                                day++;
                            }
                        }
                        else
                        {
                            if (month == 2)
                            {
                                var endDay = 0;

                                if (year % 400 == 0)
                                {
                                    endDay = 29;
                                }
                                else
                                {
                                    if (year % 100 == 0)
                                    {
                                        endDay = 28;
                                    }
                                    else
                                    {
                                        if (year % 4 == 0)
                                            endDay = 29;
                                        else
                                            endDay = 28;
                                    }
                                }

                                if (day > endDay)
                                    return errorMessage;
                                if (day == endDay)
                                {
                                    day = 1;
                                    month++;
                                }
                                else
                                {
                                    day++;
                                }
                            }
                        }
                    }
                }
                return $"{year}-{month}-{day}";
            }
            return errorMessage;
        }
    }
}