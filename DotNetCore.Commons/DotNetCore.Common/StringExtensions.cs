#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：84213528-3404-4929-937a-1fb44ef64cb9
 * 文件名：StringExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:18:47
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
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string s1, string s2)
        {
            return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 截取字符串s1最多前maxLen个字符
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="maxLen"></param>
        /// <returns></returns>
        public static string Cut(this string s1, int maxLen)
        {
            if (s1 == null)
            {
                return string.Empty;
            }
            int len = s1.Length <= maxLen ? s1.Length : maxLen;//不能超过字符串的最大大小
            return s1[0..len];
        }
    }
}
