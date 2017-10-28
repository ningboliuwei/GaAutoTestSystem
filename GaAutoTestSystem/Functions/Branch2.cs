using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Branch2 : AbstractFunction
    {
        //目标执行期望路径
        public string TargetPath { get; set; }

        public override object GetResult()
        {
            double k = 0;
            double j = 0;
            var x = (int) Paras[0];
            var y = (int) Paras[1];
            var z = (int) Paras[2];

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

        protected override string GetExecutionPath()
        {
            var x = (int) Paras[0];
            var y = (int) Paras[1];
            var z = (int) Paras[2];
            var path = "#";

            if (x > 1)
            {
                path += "a";
                if (z < 10)
                    path += "b";
            }

            if (y == 4)
            {
                path += "c";
            }
            else
            {
                path += "d";
                if (z > 1)
                    path += "e";
            }

            path += "f";
            return path;
        }


        protected override double GetFitnessByCoverageRate()
        {
            return GetExecutionPath().Length / (double) "#abcde".Length;
        }

        protected override double GetFitnessByDistance()
        {
            var x = Paras[0];
            var y = Paras[1];

            var conditions = new List<ConditionInfo>
            {
                new ConditionInfo {A = x, B = 80, Oper = ">="},
                new ConditionInfo {A = y, B = 50, Oper = "<"}
            };

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