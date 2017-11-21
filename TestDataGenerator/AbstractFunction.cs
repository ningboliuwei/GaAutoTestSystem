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

        public List<ParaInfo> Paras { get; set; } = new List<ParaInfo>();
        public string ExecutionPath => GetExecutionPath();
        public object Result => GetResult();
        public double Fitness => GetFitness();
        public string TargetPath { private get; set; }

        public FitnessType FitnessCaculationType { private get; set; } = FitnessType.Basic;

        public abstract object GetResult();

        protected abstract string GetExecutionPath();

        protected abstract double GetFitnessByCoverageRate();

        protected abstract double GetFitnessByDistance();

        protected virtual double GetFitness()
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

        //根据函数名得到函数实例
        public static AbstractFunction CreateInstance(string functionName)
        {
            const string assemblyName = "GaAutoTestSystem";
            const string namespaceString = "GaAutoTestSystem";

            return ReflectionHelper.CreateInstance<AbstractFunction>($"{namespaceString}.{functionName}", assemblyName);
        }

        private double GetFitnessByPathMatch()
        {
            return CaculationHelper.CaculatePathMatchFitness(ExecutionPath, TargetPath);
        }

        private double GetFitnessByNodeMatch()
        {
            return CaculationHelper.CaculateNodeMatchFitness(ExecutionPath, TargetPath);
        }
    }
}