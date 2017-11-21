using System;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Function1 : AbstractFunction
    {
        public override object OriginalFunction(params double[] paras)
        {
            var x = (int) paras[0];

            return x * Math.Sin(10 * Math.PI * x) + 2.0;
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