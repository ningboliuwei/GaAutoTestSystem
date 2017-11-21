using System;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Function2 : AbstractFunction
    {
        public override object OriginalFunction(params double[] paras)
        {
            var x = (int) paras[0];

            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        public override string StubbedFunction(params double[] paras)
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByCoverageRate(Chromosome chromosome)
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByDistance(Chromosome chromosome)
        {
            throw new NotImplementedException();
        }
    }
}