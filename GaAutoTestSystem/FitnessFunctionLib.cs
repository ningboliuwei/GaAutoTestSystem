using System;
using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class FitnessFunctionLib
    {
//        //解空间 [0,1]
//        public static double Function1(ParaPackage paraPackage)
//        {
//            var x = paraPackage.Paras[0];
//            
//            return x * Math.Sin(10 * Math.PI * x) + 2.0;
//        }
//
//        //解空间 [0,9]
//        public static double Function2(ParaPackage paraPackage)
//        {
//            var x = paraPackage.Paras[0];
//
//            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
//        }
//
//        public static double Branch1_CoverageRate(ParaPackage paraPackage)
//        {
//            var x = paraPackage.Paras[0];
//            var y = paraPackage.Paras[1];
//            var result = 0;
//            var path = "#";
//
//            if (x >= 80)
//            {
//                result = 1;
//                path += "a";
//                if (y < 50)
//                {
//                    result = 2;
//                    path += "b";
//                }
//            }
//
//            return path.Length / (double) "#ab".Length;
//        }
//
//        public static double Branch1_Distance(ParaPackage paraPackage)
//        {
//            var x = paraPackage.Paras[0];
//            var y = paraPackage.Paras[1];
//            var result = 0;
//
//            if (x >= 80)
//            {
//                result = 1;
//                if (y < 50)
//                    result = 2;
//            }
//
//            var conditions = new List<ConditionInfo>
//            {
//                new ConditionInfo {A = x, B = 80, Oper = ">="},
//                new ConditionInfo {A = y, B = 50, Oper = "<"}
//            };
//
//            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
//        }
//
//        public static double Branch2_CoverageRate(ParaPackage paraPackage)
//        {
//            double k = 0;
//            double j = 0;
//            var x = paraPackage.Paras[0];
//            var y = paraPackage.Paras[1];
//            var z = paraPackage.Paras[2];
//            var path = "#";
//
//            if (x > 1 && z < 10)
//            {
//                k = x + z;
//                j = k * k;
//                path += "a";
//            }
//
//            if (y == 4 || z > 1)
//            {
//                j = z * y + 10;
//                path += "b";
//            }
//
//            j = j % 3;
//            path += "c";
//
//            return path.Length / (double) "#abc".Length;
//        }
//
//        public static double Branch2_BasicPath_CoverageRate(ParaPackage paraPackage)
//        {
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//            var path = "#";
//
//            if (x > 1)
//            {
//                path += "a";
//                if (z < 10)
//                    path += "b";
//            }
//
//            if (y == 4)
//            {
//                path += "c";
//            }
//            else
//            {
//                if (z > 1)
//                    path += "d";
//            }
//
//            path += "e";
//
//            return path.Length / (double) "#abcde".Length;
//        }
//
//        public static double Branch2_Distance(ParaPackage paraPackage)
//        {
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//
//            var conditions = new List<ConditionInfo>
//            {
//                new ConditionInfo {A = x, B = 1, Oper = ">"},
//                new ConditionInfo {A = z, B = 10, Oper = "<"}
//            };
//
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(conditions[0]),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[1]),
//                Oper = "||"
//            });
//            conditions.Add(new ConditionInfo {A = y, B = 4, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = z, B = 1, Oper = ">"});
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(conditions[3]),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[4]),
//                Oper = "||"
//            });
//
//            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
//        }
//
//        public static double Branch2_PathMatch(ParaPackage paraPackage)
//        {
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//            var path = "#";
//
//            if (x > 1 && z < 10)
//                path += "a";
//
//            if (y == 4 || z > 1)
//                path += "b";
//
//            path += "c";
//
//            return CaculationHelper.CaculatePathMatchFitness(path, "#abc");
//        }
//
//        public static double Branch2_NodeMatch(ParaPackage paraPackage)
//        {
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//            var path = "#";
//
//            if (x > 1 && z < 10)
//                path += "a";
//
//            if (y == 4 || z > 1)
//                path += "b";
//
//            path += "c";
//
//            return CaculationHelper.CaculateNodeMatchFitness(path, "#abc");
//        }
//
//
//        public static double TriangleTypeTest_Distance(ParaPackage paraPackage)
//        {
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//
//            var conditions = new List<ConditionInfo>
//            {
//                new ConditionInfo {A = x + y, B = z, Oper = ">"},
//                new ConditionInfo {A = x + z, B = y, Oper = ">"},
//                new ConditionInfo {A = y + z, B = x, Oper = ">"}
//            };
//
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                {
//                    A = CaculationHelper.CaculateDistanceFitness(conditions[0]),
//                    B = CaculationHelper.CaculateDistanceFitness(conditions[1]),
//                    Oper = "&&"
//                }),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[2]),
//                Oper = "&&"
//            });
//            conditions.Add(new ConditionInfo {A = x, B = y, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = y, B = z, Oper = "=="});
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(conditions[4]),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[5]),
//                Oper = "&&"
//            });
//            conditions.Add(new ConditionInfo {A = x, B = y, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = y, B = z, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = x, B = z, Oper = "=="});
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                {
//                    A = CaculationHelper.CaculateDistanceFitness(conditions[7]),
//                    B = CaculationHelper.CaculateDistanceFitness(conditions[8]),
//                    Oper = "||"
//                }),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[9]),
//                Oper = "||"
//            });
//
//            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
//        }
//
//        public static double TriangleTypeTest_Coverage(ParaPackage paraPackage)
//        {
//            var path = "#";
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//
//            if (x + y > z && x + z > y && y + z > x)
//            {
//                path += "a";
//                if (x == y && y == z)
//                {
//                    path += "b";
//                }
//                else
//                {
//                    path += "c";
//
//                    if (x == y || y == z || x == z)
//                        path += "d";
//                    else
//                        path += "e";
//                }
//            }
//            else
//            {
//                path += "f";
//            }
//            return path.Length / (double) "#abcdef".Length;
//        }
//
//        //三角形分类——执行路径
//        public static string TriangleTypeTest_ExecutionPath(ParaPackage paraPackage)
//        {
//            var path = "#";
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//
//            if (x + y > z && x + z > y && y + z > x)
//            {
//                path += "a";
//                if (x == y && y == z)
//                {
//                    path += "b";
//                }
//                else
//                {
//                    path += "c";
//
//                    if (x == y || y == z || x == z)
//                        path += "d";
//                    else
//                        path += "e";
//                }
//            }
//            else
//            {
//                path += "f";
//            }
//            return path;
//        }
//
//
//        //三角形分类——路径匹配
////        public static double TriangleTypeTest_PathMatch(ParaPackage paraPackage)
////        {
////        }
//
//        //三角形分类——节点匹配
//        public static double TriangleTypeTest_NodeMatch(ParaPackage paraPackage)
//        {
//            var path = "#";
//            var x = (int) paraPackage.Paras[0];
//            var y = (int) paraPackage.Paras[1];
//            var z = (int) paraPackage.Paras[2];
//
//            if (x + y > z && x + z > y && y + z > x)
//            {
//                path += "a";
//                if (x == y && y == z)
//                {
//                    path += "b";
//                }
//                else
//                {
//                    path += "c";
//
//                    if (x == y || y == z || x == z)
//                        path += "d";
//                    else
//                        path += "e";
//                }
//            }
//            else
//            {
//                path += "f";
//            }
//            return CaculationHelper.CaculateNodeMatchFitness(path, "#ace");
//        }
//
//        public static double NextDate_Coverage(ParaPackage paraPackage)
//        {
//            var year = (int) paraPackage.Paras[0];
//            var month = (int) paraPackage.Paras[1];
//            var day = (int) paraPackage.Paras[2];
//
//            var errorMessage = "Invalid date";
//
//            var path = "#";
//            if (year >= 1950 && year < 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
//            {
//                path += "a";
//                //大月
//                if (month == 12)
//                {
//                    path += "b";
//                    if (day == 31)
//                    {
//                        path += "c";
//                        year++;
//                        day = 1;
//                        month = 1;
//                    }
//                    else
//                    {
//                        path += "d";
//                        day++;
//                    }
//                }
//                else
//                {
//                    path += "e";
//                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10)
//                    {
//                        path += "f";
//                        if (day == 31)
//                        {
//                            path += "g";
//                            day = 1;
//                            month++;
//                        }
//                        else
//                        {
//                            path += "h";
//                            day++;
//                        }
//                    }
//                    else
//                    {
//                        path += "i";
//                        if (month == 4 || month == 6 || month == 9 || month == 11)
//                        {
//                            path += "j";
//                            if (day == 31)
//                                path += "k";
//
//                            if (day == 30)
//                            {
//                                path += "l";
//                                day = 1;
//                                month++;
//                            }
//                            else
//                            {
//                                path += "m";
//                                day++;
//                            }
//                        }
//                        else
//                        {
//                            path += "n";
//                            if (month == 2)
//                            {
//                                path += "o";
//                                var endDay = 0;
//
//                                if (year % 400 == 0)
//                                {
//                                    path += "p";
//                                    endDay = 29;
//                                }
//                                else
//                                {
//                                    path += "q";
//                                    if (year % 100 == 0)
//                                    {
//                                        path += "r";
//                                        endDay = 28;
//                                    }
//                                    else
//                                    {
//                                        path += "s";
//                                        if (year % 4 == 0)
//                                        {
//                                            path += "t";
//                                            endDay = 29;
//                                        }
//                                        else
//                                        {
//                                            path += "u";
//                                            endDay = 28;
//                                        }
//                                    }
//                                }
//
////                                if (day > endDay)
////                                    path += "v";
//
//                                if (day == endDay)
//                                {
//                                    path += "w";
//                                    day = 1;
//                                    month++;
//                                }
//                                else
//                                {
//                                    path += "x";
//                                    day++;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return path.Length / 25.0;
//        }
//
//        public static double NextDate_Distance(ParaPackage paraPackage)
//        {
//            var year = (int) paraPackage.Paras[0];
//            var month = (int) paraPackage.Paras[1];
//            var day = (int) paraPackage.Paras[2];
//            var endDay = 0;
//
//            if (year >= 1950 && year < 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
//                if (month == 12)
//                {
//                    if (day == 31)
//                    {
//                        year++;
//                        day = 1;
//                        month = 1;
//                    }
//                    else
//                    {
//                        day++;
//                    }
//                }
//                else
//                {
//                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10)
//                    {
//                        if (day == 31)
//                        {
//                            day = 1;
//                            month++;
//                        }
//                        else
//                        {
//                            day++;
//                        }
//                    }
//                    else
//                    {
//                        if (month == 4 || month == 6 || month == 9 || month == 11)
//                        {
//                            if (day == 31)
////                                return errorMessage;
//                                if (day == 30)
//                                {
//                                    day = 1;
//                                    month++;
//                                }
//                                else
//                                {
//                                    day++;
//                                }
//                        }
//                        else
//                        {
//                            if (month == 2)
//                            {
//                                if (year % 400 == 0)
//                                {
//                                    endDay = 29;
//                                }
//                                else
//                                {
//                                    if (year % 100 == 0)
//                                    {
//                                        endDay = 28;
//                                    }
//                                    else
//                                    {
//                                        if (year % 4 == 0)
//                                            endDay = 29;
//                                        else
//                                            endDay = 28;
//                                    }
//                                }
//
//                                if (day > endDay)
////                                    return errorMessage;
//                                    if (day == endDay)
//                                    {
//                                        day = 1;
//                                        month++;
//                                    }
//                                    else
//                                    {
//                                        day++;
//                                    }
//                            }
//                        }
//                    }
//                }
////            return errorMessage;
//
//            var conditions = new List<ConditionInfo>
//            {
//                //[0]
//                new ConditionInfo {A = year, B = 1950, Oper = ">="},
//                new ConditionInfo {A = year, B = 2050, Oper = "<"},
//                new ConditionInfo {A = month, B = 1, Oper = ">="},
//                new ConditionInfo {A = month, B = 12, Oper = "<="},
//                new ConditionInfo {A = day, B = 1, Oper = ">="},
//                new ConditionInfo {A = day, B = 31, Oper = "<="}
//            };
//            //[6]
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(
//                    new ConditionInfo
//                    {
//                        A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                        {
//                            A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                            {
//                                A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                                {
//                                    A = CaculationHelper.CaculateDistanceFitness(conditions[0]),
//                                    B = CaculationHelper.CaculateDistanceFitness(conditions[1]),
//                                    Oper = "&&"
//                                }),
//                                B = CaculationHelper.CaculateDistanceFitness(conditions[2]),
//                                Oper = "&&"
//                            }),
//                            B = CaculationHelper.CaculateDistanceFitness(conditions[3]),
//                            Oper = "&&"
//                        }),
//                        B = CaculationHelper.CaculateDistanceFitness(conditions[4]),
//                        Oper = "&&"
//                    }),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[5]),
//                Oper = "&&"
//            });
//            //[7]
//            conditions.Add(new ConditionInfo {A = month, B = 12, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = day, B = 31, Oper = "=="});
//            //[9]
//            conditions.Add(new ConditionInfo {A = month, B = 1, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 3, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 5, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 7, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 8, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 10, Oper = "=="});
//            //[15]
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(
//                    new ConditionInfo
//                    {
//                        A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                        {
//                            A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                            {
//                                A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                                {
//                                    A = CaculationHelper.CaculateDistanceFitness(conditions[9]),
//                                    B = CaculationHelper.CaculateDistanceFitness(conditions[10]),
//                                    Oper = "||"
//                                }),
//                                B = CaculationHelper.CaculateDistanceFitness(conditions[11]),
//                                Oper = "||"
//                            }),
//                            B = CaculationHelper.CaculateDistanceFitness(conditions[12]),
//                            Oper = "||"
//                        }),
//                        B = CaculationHelper.CaculateDistanceFitness(conditions[13]),
//                        Oper = "||"
//                    }),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[14]),
//                Oper = "||"
//            });
//            //[16]
//            conditions.Add(new ConditionInfo {A = day, B = 31, Oper = "=="});
//            //[17]
//            conditions.Add(new ConditionInfo {A = month, B = 4, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 6, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 9, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 11, Oper = "=="});
//            //[21]
//            conditions.Add(new ConditionInfo
//            {
//                A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                {
//                    A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
//                    {
//                        A = CaculationHelper.CaculateDistanceFitness(conditions[17]),
//                        B = CaculationHelper.CaculateDistanceFitness(conditions[18]),
//                        Oper = "||"
//                    }),
//                    B = CaculationHelper.CaculateDistanceFitness(conditions[19]),
//                    Oper = "||"
//                }),
//                B = CaculationHelper.CaculateDistanceFitness(conditions[20]),
//                Oper = "||"
//            });
//            conditions.Add(new ConditionInfo {A = day, B = 31, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = day, B = 30, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = month, B = 2, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = year % 400, B = 0, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = year % 100, B = 0, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = year % 4, B = 0, Oper = "=="});
//            conditions.Add(new ConditionInfo {A = day, B = endDay, Oper = ">"});
//            conditions.Add(new ConditionInfo {A = day, B = endDay, Oper = "=="});
//
//            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
//        }
    }
}