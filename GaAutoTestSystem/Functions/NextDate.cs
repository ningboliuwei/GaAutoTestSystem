using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class NextDate : AbstractFunction
    {
        public override object GetResult()
        {
            var errorMessage = "Invalid date";
            var year = (int) Paras[0].Value;
            var month = (int) Paras[1].Value;
            var day = (int) Paras[2].Value;

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

        protected override string GetExecutionPath()
        {
            var year = (int) Paras[0].Value;
            var month = (int) Paras[1].Value;
            var day = (int) Paras[2].Value;

            var errorMessage = "Invalid date";

            var path = "#";
            if (year >= 1950 && year <= 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                path += "a";
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
                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 ||
                        month == 10)
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
                                path += "k";

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
                                    path += "v";

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
            return path;
        }

        protected override double GetFitnessByCoverageRate()
        {
            return GetExecutionPath().Length / (double) "#abcde".Length;
        }

        protected override double GetFitnessByDistance()
        {
            var x = (int) Paras[0].Value;
            var y = (int) Paras[1].Value;
            var z = (int) Paras[2].Value;
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

        protected override double GetFitnessByPathMatch()
        {
            return CaculationHelper.CaculatePathMatchFitness(GetExecutionPath(), TargetPath);
        }

        protected override double GetFitnessByNodeMatch()
        {
            return CaculationHelper.CaculateNodeMatchFitness(GetExecutionPath(), TargetPath);
        }
    }
}