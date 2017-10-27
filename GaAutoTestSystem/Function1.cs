using System;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    internal class Function1 : AbstractFunction
    {
        public override object GetResult()
        {
            var x = Paras[0];

            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        public override double GetFitness()
        {
            return (double) GetResult();
        }
    }
}