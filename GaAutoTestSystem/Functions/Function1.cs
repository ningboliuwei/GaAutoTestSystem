using System;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Function1 : AbstractFunction
    {
        public override object GetResult()
        {
            var x = Paras[0].Value;

            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        protected override string GetExecutionPath()
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByCoverageRate()
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByDistance()
        {
            throw new NotImplementedException();
        }

        protected override double GetFitness()
        {
            return (double) GetResult();
        }
    }
}