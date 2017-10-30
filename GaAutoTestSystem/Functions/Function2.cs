using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    class Function2: AbstractFunction
    {
        public override object GetResult()
        {
            var x = Paras[0].Value;

            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        protected override string GetExecutionPath()
        {
            return "not available";
        }

        protected override double GetFitnessByCoverageRate()
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByDistance()
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByPathMatch()
        {
            throw new NotImplementedException();
        }

        protected override double GetFitnessByNodeMatch()
        {
            throw new NotImplementedException();
        }

        public override double GetFitness()
        {
            return (double) GetResult();
        }
    }
}
