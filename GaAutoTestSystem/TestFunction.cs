using System;

namespace GaAutoTestSystem
{
    public class TestFunction
    {
        //解空间 [0,1]
        public static object Function1(params double[] paras)
        {
            var x = paras[0];
            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        //解空间 [0,9]
        public static object Function2(params double[] paras)
        {
            var x = paras[0];

            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        //简单分支函数
        public static object BranchTest1(params double[] paras)
        {
            var x = paras[0];
            var y = paras[1];
            var result = 0;

            if (x >= 80)
            {
                result = 1;
                if (y < 50)
                    result = 2;
            }

            return result;
        }

        public static object BranchTest2(params double[] paras)
        {
            double k = 0;
            double j = 0;
            var x = paras[0];
            var y = paras[1];
            var z = paras[2];

            if (x > 1 && z < 10)
            {
                k = x + z;
                j = k * k;
            }

            if (y == 4 || z > 1)
                j = z * y + 10;

            j = j % 3;

            return j;
        }

        //equilateral —— 等边， isosceles 等腰，scalene 一般
        public static object TriangleTypeTest(params double[] paras)
        {
            var type = "";
            var x = (int) paras[0];
            var y = (int) paras[0];
            var z = (int) paras[0];

            if (x + y > z && x + z > y && y + z > x)
                if (x == y && y == z)
                {
                    type = "equilateral triangle";
                }
                else
                {
                    if (x == y || y == z || x == z)
                        type = "isosceles triangle";
                    else
                        type = "scalene triangle";
                }
            else
                type = "not a triangle";

            return type;
        }

        public static object TriangleTypeTestPathCoverage(params double[] paras)
        {
            var type = "";
            var path = "";
            var x = (int) paras[0];
            var y = (int) paras[0];
            var z = (int) paras[0];

            if (x + y > z && x + z > y && y + z > x)
            {
                path += "a";
                if (x == y && y == z)
                {
                    path += "b";
                    type = "equilateral triangle";
                }
                else
                {
                    path += "c";
                    if (x == y || y == z || x == z)
                    {
                        path += "d";
                        type = "isosceles triangle";
                    }
                    else
                    {
                        path += "e";
                        type = "scalene triangle";
                    }
                }
            }
            else
            {
                path += "f";
                type = "not a triangle";
            }

            return path;
        }


        //计算下一天的日期
        public static object NextDate(int year, int month, int day)
        {
            var errorMessage = "Invalid date";

            if (year >= 1950 && year < 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
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