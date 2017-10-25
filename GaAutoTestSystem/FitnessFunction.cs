using System;
using System.Collections.Generic;
using System.Linq;

namespace GaAutoTestSystem
{
    internal class FitnessFunction
    {
        //解空间 [0,1]
        public static double Function1(params double[] paras)
        {
            var x = paras[0];
            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        //解空间 [0,9]
        public static double Function2(params double[] paras)
        {
            var x = paras[0];

            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        public static double BranchTest1_Coverage(params double[] paras)
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

            return path.Length / (double) "#ab".Length;
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

        public static double BranchTest2_Coverage(params double[] paras)
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

            return path.Length / (double) "#abc".Length;
        }

        public static double TriangleTypeTest_Distance(params double[] paras)
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
                }
                else
                {
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
            }
            return -Math.Abs(f1 + f2 + f3);
        }

        public static double TriangleTypeTest_Coverage(params double[] paras)
        {
            var path = "#";
            var x = (int) paras[0];
            var y = (int) paras[1];
            var z = (int) paras[2];

            if (x + y > z && x + z > y && y + z > x)
            {
                path += "a";
                if (x == y && y == z)
                {
                    path += "b";
                }
                else
                {
                    path += "c";

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
            return path.Length / (double) "#abcdef".Length;
        }

        public static double NextDate_CodeCoverage_Fitness(params double[] paras)
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
            var year = (int) paras[0];
            var month = (int)paras[1];
            var day = (int)paras[2];

            var errorMessage = "Invalid date";

            var path = "#";
            if (year >= 1950 && year < 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                path += "a";
                f1 = 0;
                //大月
                if (month == 12)
                {
                    path += "b";
                    if (day == 31)
                    {
                        path += "c";
                        year++;
                        day = 1;
                        month = 1;
                    }
                    else
                    {
                        path += "d";
                        day++;
                    }
                }
                else
                {
                    path += "e";
                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10)
                    {
                        path += "f";
                        if (day == 31)
                        {
                            path += "g";
                            day = 1;
                            month++;
                        }
                        else
                        {
                            path += "h";
                            day++;
                        }
                    }
                    else
                    {
                        path += "i";
                        if (month == 4 || month == 6 || month == 9 || month == 11)
                        {
                            path += "j";
                            if (day == 31)
                            {
                                path += "k";
                                //  return errorMessage;
                            }
                            
                            if (day == 30)
                            {
                                path += "l";
                                day = 1;
                                month++;
                            }
                            else
                            {
                                path += "m";
                                day++;
                            }
                        }
                        else
                        {
                            path += "n";
                            if (month == 2)
                            {
                                path += "o";
                                var endDay = 0;

                                if (year % 400 == 0)
                                {
                                    path += "p";
                                    endDay = 29;
                                }
                                else
                                {
                                    path += "q";
                                    if (year % 100 == 0)
                                    {
                                        path += "r";
                                        endDay = 28;
                                    }
                                    else
                                    {
                                        path += "s";
                                        if (year % 4 == 0)
                                        {
                                            path += "t";
                                            endDay = 29;
                                        }
                                        else
                                        {
                                            path += "u";
                                            endDay = 28;
                                        }
                                    }
                                }

                                if (day > endDay)
                                {
                                    path += "v";
                                    // return errorMessage;
                                }
                                
                                if (day == endDay)
                                {
                                    path += "w";
                                    day = 1;
                                    month++;
                                }
                                else
                                {
                                    path += "x";
                                    day++;
                                }
                            }
                        }
                    }
                }
                
            }
            return path.Length / 25.0;
        }
    }
}