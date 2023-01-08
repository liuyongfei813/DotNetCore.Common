#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.CommonInitializer
 * 唯一标识：6ba5e2d4-b39a-4fb6-94f5-f2bf7f0c00bd
 * 文件名：InitializerOptions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 17:41:06
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

namespace DotNetCore.CommonInitializer
{
    public class InitializerOptions
    {
        public string LogFilePath { get; set; }

        //用于EventBus的QueueName，因此要维持“同一个项目值保持一直，不同项目不能冲突”
        public string EventBusQueueName { get; set; }
    }
}
