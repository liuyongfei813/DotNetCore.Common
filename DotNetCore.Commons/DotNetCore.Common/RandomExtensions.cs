#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：afa4b38c-c5b1-4ade-b22a-8d2f2eda2d2c
 * 文件名：RandomExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:15:36
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>
namespace System
{
    public static class RandomExtensions
    {
        /// <summary>
        ///  Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns></returns>
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue cannot be bigger than maxValue");
            }
            //https://stackoverflow.com/questions/65900931/c-sharp-random-number-between-double-minvalue-and-double-maxvalue
            double x = random.NextDouble();
            return x * maxValue + (1 - x) * minValue;
        }
    }
}
