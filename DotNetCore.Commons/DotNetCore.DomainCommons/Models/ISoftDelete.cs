#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.DomainCommons.Models
 * 唯一标识：f4b15bdc-11cd-4359-953e-4a5743d6b8df
 * 文件名：ISoftDelete
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:37:33
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

namespace DotNetCore.DomainCommons.Models
{
    public interface ISoftDelete
    {
        /// <summary>
        /// 不能写成get;protected set;否则在实现类中，这个属性不能是public http://www.itpow.com/c/2019/05/11443.asp
        /// </summary>
        bool IsDeleted { get; }
        void SoftDelete();
    }
}
