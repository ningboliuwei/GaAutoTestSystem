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
        public string ExecutionPath => GetExecutionPath();
        public object Result => GetResult();
        public double Fitness => GetFitness();

        public FitnessType FitnessCaculationType { get; set; } = FitnessType.Basic;

        public abstract object GetResult();

        protected abstract string GetExecutionPath();

        protected abstract double GetFitnessByCoverageRate();

        protected abstract double GetFitnessByDistance();

        protected abstract double GetFitnessByPathMatch();

        protected abstract double GetFitnessByNodeMatch();

        public virtual double GetFitness()
        {
            if (FitnessCaculationType == FitnessType.Basic)
                return GetFitnessByCoverageRate();

            if (FitnessCaculationType == FitnessType.Distance)
                return GetFitnessByDistance();

            if (FitnessCaculationType == FitnessType.PathMatch)
                return GetFitnessByPathMatch();

            if (FitnessCaculationType == FitnessType.NodeMatch)
                return GetFitnessByNodeMatch();

            return double.MinValue;
        }
    }
}