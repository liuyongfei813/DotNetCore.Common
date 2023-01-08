#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.ASPNETCore
 * 唯一标识：d97bc818-d64b-4be9-a106-e8e06ac0d2b0
 * 文件名：CacheKeyHelper
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 17:06:15
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
    public static class CacheKeyHelper
    {
        /// <summary>
        /// 获取和这个Action请求相关的CacheKey，主要考虑Controller名字、Action名字、参数等
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string CalcCacheKeyFromAction(this ControllerBase controller)
        {
            return GetCacheKey(controller.ControllerContext);
        }

        public static string GetCacheKey(this ControllerContext controllerContext)
        {
            var routeValues = controllerContext.RouteData.Values.Values;
            string cacheKey = string.Join(".", routeValues);
            return cacheKey;
        }
    }
}
