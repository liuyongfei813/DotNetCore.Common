#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.DomainCommons.Models
 * 唯一标识：e5491203-63c1-4d37-aefd-904e780026c8
 * 文件名：IEntity
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:35:50
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
    public interface IEntity
    {
        public Guid Id { get; }
        //在MySQL中，用Guid做物理主键性能非常低。因此，物理上用自增的列做主键。但是逻辑上、包括两表之间的关联都用Guid。
        //public long AutoIncId { get; }
    }
}
