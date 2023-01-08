#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：d52a1cb2-b3c0-4ee2-a974-b470f128d867
 * 文件名：FormattableStringHelper
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:03:04
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
    /// 构建tableString帮助类
    /// </summary>
    public static class FormattableStringHelper
    {
        public static string BuildUrl(FormattableString urlFormat)
        {
            var invariantParameters = urlFormat.GetArguments()
                .Select(a => FormattableString.Invariant($"{a}"));
            object[] escapedParameters = invariantParameters
              .Select(s => (object)Uri.EscapeDataString(s)).ToArray();
            return string.Format(urlFormat.Format, escapedParameters);
        }
    }
}
