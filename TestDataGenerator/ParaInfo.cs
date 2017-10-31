﻿namespace TestDataGenerator
{
    public enum ParaDataType
    {
        Integer,
        Double
    }

    public class ParaInfo
    {
        public ParaInfo()
        {
            DataType = ParaDataType.Double;
        }

        //解空间上界
        public double UpperBound { get; set; }

        //解空间下界
        public double LowerBound { get; set; }

        //值
        public double Value { get; set; }

        //数据类型
        public ParaDataType DataType { get; set; }
    }
}