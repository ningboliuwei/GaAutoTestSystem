﻿using TestDataGenerator;

namespace TestDataGenerator
{
    public class GaParameterInfo
    {
        //染色体长度
        public int ChromosomeLengthForOneSubValue { get; set; } = 10;

        //染色体数量
        public int ChromosomeQuantity { get; set; } = 1000;

        //进化代数
        public int GenerationQuantity { get; set; } = 200;

        //变异率
        public double MutationRate { get; set; } = 0.3;

        //存活率
        public double RetainRate { get; set; } = 0.2;

        //随机选择率
        public double SelectionRate { get; set; } = 0.01;

        //演化策略
        public Population.SelectionType SelectionType { get; set; } = Population.SelectionType.Hybrid;

    }
}