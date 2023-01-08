#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.EventBus
 * 唯一标识：23ef6e64-0a37-4ef4-894e-fadf8b846715
 * 文件名：DynamicIntegrationEventHandler
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 16:46:58
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
using Dynamic.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.EventBus
{
    public abstract class DynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            //https://github.com/dotnet/runtime/issues/53195
            //https://github.com/dotnet/core/issues/6444
            //.NET 6目前不支持把json反序列化为dynamic，本来preview 4支持，但是在preview 7又去掉了
            //所以暂时用Dynamic.Json来实现。
            dynamic dynamicEventData = DJson.Parse(eventData);
            return HandleDynamic(eventName, dynamicEventData);
        }

        public abstract Task HandleDynamic(string eventName, dynamic eventData);
    }
}
