using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class TriangleType : AbstractFunction
    {
        public override object GetResult()
        {
            var x = (int) Paras[0].Value;
            var y = (int) Paras[1].Value;
            var z = (int) Paras[2].Value;

            string type;

            if (x >= 1 && x <= 10 && y >= 1 && y <= 10 && z >= 1 && z <= 10)
            {
                if (x + y > z && x + z > y && y + z > x)
                {
                    if (x == y && y == z)
                    {
                        type = "equilateral triangle";
                    }
                    else
                    {
                        if (x == y || y == z || x == z)
                        {
                            type = "isosceles triangle";
                        }
                        else
                        {
                            type = "scalene triangle";
                        }
                    }
                }
                else
                {
                    type = "not a triangle";
                }
            }
            else
            {
                type = "invalid value(s)";
            }

            return type;
        }

        protected override string GetExecutionPath()
        {
            var x = (int) Paras[0].Value;
            var y = (int) Paras[1].Value;
            var z = (int) Paras[2].Value;

            // string type;
            var path = "a";

            if (x >= 1 && x <= 10 && y >= 1 && y <= 10 && z >= 1 && z <= 10)
            {
                path += "b";
                if (x + y > z && x + z > y && y + z > x)
                {
                    path += "c";
                    if (x == y && y == z)
                    {
                        path += "d";
                        // type = "equilateral triangle";
                    }
                    else
                    {
                        path += "e";
                        if (x == y || y == z || x == z)
                        {
                            path += "f";
                            // type = "isosceles triangle";
                        }
                        else
                        {
                            path += "g";
                            // type = "scalene triangle";
                        }
                    }
                }
                else
                {
                    path += "h";
                    // type = "not a triangle";
                }
            }
            else
            {
                path += "i";
                // type = "invalid value(s)";
            }
            path += "j";
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