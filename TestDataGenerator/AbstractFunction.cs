using System.Collections.Generic;

namespace TestDataGenerator
{
    public abstract class AbstractFunction
    {
        public enum FitnessType
        {
            Value,
            CoverageRate,
            Distance,
            PathMatch,
            NodeMatch
        }

        public List<ParaInfo> Paras { get; set; } = new List<ParaInfo>();
        public string TargetPath { get; set; }

        public FitnessType FitnessCaculationType { private get; set; } = FitnessType.Value;

        public object GetResult(Chromosome chromosome)
        {
            return OriginalFunction(chromosome.DecodedSubValues.ToArray());
        }

        public abstract object OriginalFunction(params double[] paras);

        public abstract string StubbedFunction(params double[] paras);

        public string GetExecutionPath(Chromosome chromosome)
        {
            return StubbedFunction(chromosome.DecodedSubValues.ToArray());
        }

        protected abstract double GetFitnessByCoverageRate(Chromosome chromosome);

        protected abstract double GetFitnessByDistance(Chromosome chromosome);

        public double GetFitness(Chromosome chromosome)
        {
            if (FitnessCaculationType == FitnessType.Value)
                return (double) GetResult(chromosome);

            if (FitnessCaculationType == FitnessType.CoverageRate)
                return GetFitnessByCoverageRate(chromosome);

            if (FitnessCaculationType == FitnessType.Distance)
                return GetFitnessByDistance(chromosome);

            if (FitnessCaculationType == FitnessType.PathMatch)
                return GetFitnessByPathMatch(chromosome);

            if (FitnessCaculationType == FitnessType.NodeMatch)
                return GetFitnessByNodeMatch(chromosome);

            return double.MinValue;
        }

        //根据函数名得到函数实例
        public static AbstractFunction CreateInstance(string functionName)
        {
            const string assemblyName = "GaAutoTestSystem";
            const string namespaceString = "GaAutoTestSystem";

            return ReflectionHelper.CreateInstance<AbstractFunction>($"{namespaceString}.{functionName}", assemblyName);
        }

        private double GetFitnessByPathMatch(Chromosome chromosome)
        {
            var executionPath = GetExecutionPath(chromosome);
            return CaculationHelper.CaculatePathMatchFitness(executionPath, TargetPath);
        }

        private double GetFitnessByNodeMatch(Chromosome chromosome)
        {
            var executionPath = GetExecutionPath(chromosome);
            return CaculationHelper.CaculateNodeMatchFitness(executionPath, TargetPath);
        }
    }
}