using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class TriangleType : AbstractFunction
    {
        public override object OriginalFunction(params double[] paras)
        {
            var x = (int) paras[0];
            var y = (int) paras[1];
            var z = (int) paras[2];

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

        public override string StubbedFunction(params double[] paras)
        {
            var x = (int) paras[0];
            var y = (int) paras[1];
            var z = (int) paras[2];

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

        protected override double GetFitnessByCoverageRate(Chromosome chromosome)
        {
            return GetExecutionPath(chromosome).Length / (double) "abcdefghij".Length;
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