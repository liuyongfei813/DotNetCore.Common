#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：c8164074-0a21-4732-986b-107f38ad5aeb
 * 文件名：EnumerableExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:01:54
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

namespace DotNetCore.Common
{
    /// <summary>
    ///  Enumerable扩展方法
    /// </summary>
    public static class EnumerableExtensions
    {
        public static bool SequenceIgnoredEqual<T>(this IEnumerable<T> items1, IEnumerable<T> items2)
        {
            if (items1 == items2)//两个相等（包括都是null）
            {
                return true;
            }
            else if (items1 == null || items2 == null)//有一个为null，就是false
            {
                return false;
            }
            else
            {
                return items1.OrderBy(e => e).SequenceEqual(items2.OrderBy(e => e));
            }
        }
    }
}
