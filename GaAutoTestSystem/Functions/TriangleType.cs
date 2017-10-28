using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class TriangleType : AbstractFunction
    {
        //目标执行期望路径
        public string TargetPath { get; set; }

        public override object GetResult()
        {
            string type;
            var x = (int) Paras[0];
            var y = (int) Paras[1];
            var z = (int) Paras[2];

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

        protected override string GetExecutionPath()
        {
            var type = "";
            var path = "#";
            var x = (int) Paras[0];
            var y = (int) Paras[1];
            var z = (int) Paras[2];

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


        protected override double GetFitnessByCoverageRate()
        {
            return GetExecutionPath().Length / (double) "#abcde".Length;
        }

        protected override double GetFitnessByDistance()
        {
            var x = (int) Paras[0];
            var y = (int) Paras[1];
            var z = (int) Paras[2];

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