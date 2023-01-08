#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common.Validators
 * 唯一标识：612c3f3d-22d2-4263-b744-880744d9ae91
 * 文件名：EnumerableValidators
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 14:56:38
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation
{
    public static class EnumerableValidators
    {
        /// <summary>
        /// 集合中没有重复元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, IEnumerable<TItem>> NotDuplicated<T, TItem>(this IRuleBuilder<T, IEnumerable<TItem>> ruleBuilder)
        {
            return ruleBuilder.Must(p => p == null || p.Distinct().Count() == p.Count());
        }

        /// <summary>
        /// 集合中不包含指定的值comparedValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="comparedValue">待匹配的值</param>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, IEnumerable<TItem>> NotContains<T, TItem>(this IRuleBuilder<T, IEnumerable<TItem>> ruleBuilder, TItem comparedValue)
        {
            return ruleBuilder.Must(p => p == null || !p.Contains(comparedValue));
        }

        /// <summary>
        /// 集合中包含指定的值comparedValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="comparedValue">待匹配的值</param>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, IEnumerable<TItem>> Contains<T, TItem>(this IRuleBuilder<T, IEnumerable<TItem>> ruleBuilder, TItem comparedValue)
        {
            return ruleBuilder.Must(p => p == null || p.Contains(comparedValue));
        }
    }
}
