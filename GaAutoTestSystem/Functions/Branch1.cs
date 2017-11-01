using System.Collections.Generic;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Branch1 : AbstractFunction
    {
        public override object GetResult()
        {
            var x = Paras[0].Value;
            var y = Paras[1].Value;
            var result = 0;

            if (x >= 80)
            {
                result = 1;
                if (y < 50)
                    result = 2;
            }

            return result;
        }

        protected override string GetExecutionPath()
        {
            var x = Paras[0].Value;
            var y = Paras[1].Value;
            var result = 0;
            var path = "#";

            if (x >= 80)
            {
                result = 1;
                path += "a";
                if (y < 50)
                {
                    result = 2;
                    path += "b";
                }
            }
            path += "c";

            return path;
        }

        protected override double GetFitnessByCoverageRate()
        {
            return GetExecutionPath().Length / (double) "#abc".Length;
        }

        protected override double GetFitnessByDistance()
        {
            var x = Paras[0].Value;
            var y = Paras[1].Value;

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