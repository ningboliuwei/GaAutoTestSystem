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
            var x = Paras[0];

            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        public override double GetFitness()
        {
            return (double) GetResult();
        }
    }
}
