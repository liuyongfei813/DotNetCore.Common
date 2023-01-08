#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.ASPNETCore
 * 唯一标识：3163a658-71aa-4c34-b83a-93ebbef20c25
 * 文件名：ControllerBaseExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 17:06:51
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
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.ASPNETCore
{
    public static class ControllerBaseExtensions
    {
        public static BadRequestObjectResult APIError(this ControllerBase controller, int code, string message)
        {
            return controller.BadRequest(new APIError(code, message));
        }
    }
}
