﻿namespace TestCodeGenerator
{
    public class FunctionLib
    {
        public static string TriangleType(params double[] paras)
        {
            string type;
            var x = paras[0];
            var y = paras[1];
            var z = paras[2];

            if (x < 1 && x > 10 && y < 1 && y > 10 && z < 1 && z > 10)
            {
                if (x + y > z && x + z > y && y + z > x)
                    if (x == y && y == z)
                    {
                        type = "equilateral triangle";
                    }
                    else
                    {
                        if (x == y || y == z || x == z)
                            type = "isosceles triangle";
                        else
                            type = "scalene triangle";
                    }
                else
                    type = "not a triangle";
            }
            else
            {
                type = "invalid value(s)";
            }

            return type;
        }
    }
}