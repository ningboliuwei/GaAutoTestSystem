using System;
using System.Reflection;

namespace TestDataGenerator
{
    //from http://www.cnblogs.com/danlis/p/7723847.html
    public static class ReflectionHelper
    {
        public static T CreateInstance<T>(string fullName, string assemblyName)
        {
            var path = fullName + "," + assemblyName; //命名空间.类型名,程序集
            var o = Type.GetType(path); //加载类型
            var obj = Activator.CreateInstance(o, true); //根据类型创建实例
            return (T) obj; //类型转换并返回
        }

        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                var fullName = nameSpace + "." + className; //命名空间.类型名
                //此为第一种写法
                var ect = Assembly.Load(assemblyName).CreateInstance(fullName); //加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T) ect; //类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
    }
}