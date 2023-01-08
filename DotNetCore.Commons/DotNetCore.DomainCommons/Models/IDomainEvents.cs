#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.DomainCommons.Models
 * 唯一标识：d9a12fa9-64ea-4ffa-ac76-6422906bb6c6
 * 文件名：IDomainEvents
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:35:27
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
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.DomainCommons.Models
{
    public interface IDomainEvents
    {
        IEnumerable<INotification> GetDomainEvents();
        void AddDomainEvent(INotification eventItem);
        /// <summary>
        /// 如果已经存在这个元素，则跳过，否则增加。
        /// 以避免对于同样的事件触发多次
        /// （比如在一个事务中修改领域模型的多个对象）
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEventIfAbsent(INotification eventItem);
        public void ClearDomainEvents();
    }
}
