using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class NextDate : AbstractFunction
    {
        public override object OriginalFunction(params double[] paras)
        {
            var errorMessage = "Invalid date";
            var year = (int) paras[0];
            var month = (int) paras[1];
            var day = (int) paras[2];

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

        public override string StubbedFunction(params double[] paras)
        {
            var year = (int) paras[0];
            var month = (int) paras[1];
            var day = (int) paras[2];

            var errorMessage = "Invalid date";

            var path = "a";
            if (year >= 1950 && year <= 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                path += "b";
                //大月
                if (month == 12)
                {
                    path += "c";
                    if (day == 31)
                    {
                        path += "d";
                        year++;
                        // day = 1;
                        // month = 1;
                    }
                    else
                    {
                        path += "e";
                        // day++;
                    }
                }
                else
                {
                    path += "f";
                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 ||
                        month == 10)
                    {
                        path += "g";
                        if (day == 31)
                        {
                            path += "h";
                            // day = 1;
                            // month++;
                        }
                        else
                        {
                            path += "i";
                            // day++;
                        }
                    }
                    else
                    {
                        path += "j";
                        if (month == 4 || month == 6 || month == 9 || month == 11)
                        {
                            path += "k";
                            if (day == 31)
                            {
                                path += "l";
                            }

                            if (day == 30)
                            {
                                path += "m";
                                day = 1;
                                month++;
                            }
                            else
                            {
                                path += "n";
                                day++;
                            }
                        }
                        else
                        {
                            path += "o";
                            if (month == 2)
                            {
                                path += "p";
                                var endDay = 0;

                                if (year % 400 == 0)
                                {
                                    path += "q";
                                    endDay = 29;
                                }
                                else
                                {
                                    path += "r";
                                    if (year % 100 == 0)
                                    {
                                        path += "s";
                                        endDay = 28;
                                    }
                                    else
                                    {
                                        path += "t";
                                        if (year % 4 == 0)
                                        {
                                            path += "u";
                                            endDay = 29;
                                        }
                                        else
                                        {
                                            path += "v";
                                            endDay = 28;
                                        }
                                    }
                                }

                                if (day > endDay)
                                    path += "w";

                                if (day == endDay)
                                {
                                    path += "x";
                                    day = 1;
                                    month++;
                                }
                                else
                                {
                                    path += "y";
                                    day++;
                                }
                            }
                        }
                    }
                }
            }

            path += "z";
            return path;
        }

        protected override double GetFitnessByCoverageRate(Chromosome chromosome)
        {
            return GetExecutionPath(chromosome).Length / (double) "#abcde".Length;
        }

        protected override double GetFitnessByDistance(Chromosome chromosome)
        {
            var x = (int) chromosome.DecodedSubValues[0];
            var y = (int) chromosome.DecodedSubValues[1];
            var z = (int) chromosome.DecodedSubValues[2];
            var conditions = new List<ConditionInfo>
            {
                new ConditionInfo {A = x + y, B = z, Oper = ">"},
                new ConditionInfo {A = x + z, B = y, Oper = ">"},
                new ConditionInfo {A = y + z, B = x, Oper = ">"}
            };
            conditions.Add(new ConditionInfo
            {
                A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
                {
                    A = CaculationHelper.CaculateDistanceFitness(conditions[0]),
                    B = CaculationHelper.CaculateDistanceFitness(conditions[1]),
                    Oper = "&&"
                }),
                B = CaculationHelper.CaculateDistanceFitness(conditions[2]),
                Oper = "&&"
            });
            conditions.Add(new ConditionInfo {A = x, B = y, Oper = "=="});
            conditions.Add(new ConditionInfo {A = y, B = z, Oper = "=="});
            conditions.Add(new ConditionInfo
            {
                A = CaculationHelper.CaculateDistanceFitness(conditions[4]),
                B = CaculationHelper.CaculateDistanceFitness(conditions[5]),
                Oper = "&&"
            });
            conditions.Add(new ConditionInfo {A = x, B = y, Oper = "=="});
            conditions.Add(new ConditionInfo {A = y, B = z, Oper = "=="});
            conditions.Add(new ConditionInfo {A = x, B = z, Oper = "=="});
            conditions.Add(new ConditionInfo
            {
                A = CaculationHelper.CaculateDistanceFitness(new ConditionInfo
                {
                    A = CaculationHelper.CaculateDistanceFitness(conditions[7]),
                    B = CaculationHelper.CaculateDistanceFitness(conditions[8]),
                    Oper = "||"
                }),
                B = CaculationHelper.CaculateDistanceFitness(conditions[9]),
                Oper = "||"
            });
            return CaculationHelper.CaculateTotalDistanceFitness(conditions);
        }
    }
}