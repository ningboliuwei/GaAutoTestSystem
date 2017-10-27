using System;
using System.Collections.Generic;

namespace TestDataGenerator
{
    public abstract class AbstractFunction
    {
        public enum FitnessType
        {
            Basic,
            Distance,
            PathMatch,
            NodeMatch
        }
        
        public List<double> Paras { get; set; } = new List<double>();
        public FitnessType FitnessCaculationType { get; set; } = FitnessType.Basic;

        public abstract object GetResult();
        
        public abstract double GetFitness();
    }
}