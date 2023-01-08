#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.EventBus
 * 唯一标识：c8d92fbd-f074-4bbc-9a00-58dc4ea833c7
 * 文件名：ApplicationBuilderExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 16:45:40
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
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.EventBus
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEventBus(this IApplicationBuilder appBuilder)
        {
            //获得IEventBus一次，就会立即加载IEventBus，这样扫描所有的EventHandler，保证消息及时消费
            object? eventBus = appBuilder.ApplicationServices.GetService(typeof(IEventBus));
            if (eventBus == null)
            {
                throw new ApplicationException("找不到IEventBus实例");
            }
            return appBuilder;
        }
    }
}
