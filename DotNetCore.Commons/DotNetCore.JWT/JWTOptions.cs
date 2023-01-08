#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.JWT
 * 唯一标识：01249656-0f71-40db-a32a-e68ca0417ba3
 * 文件名：JWTOptions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:25:08
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

namespace DotNetCore.JWT
{
    public class JWTOptions
    {
        /// <summary>
        /// 发行人
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Audience
        /// </summary>
        public string? Audience { get; set; }
        /// <summary>
        /// Key
        /// </summary>
        public string? Key { get; set; }
        public int ExpireSeconds { get; set; }
    }
}
