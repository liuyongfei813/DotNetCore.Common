#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.ASPNETCore
 * 唯一标识：0245a5f5-0bb8-4c81-b560-371718bc48b8
 * 文件名：UnitOfWorkFilter
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 17:10:29
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
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DotNetCore.ASPNETCore
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private static UnitOfWorkAttribute? GetUoWAttr(ActionDescriptor actionDesc)
        {
            var caDesc = actionDesc as ControllerActionDescriptor;
            if (caDesc == null)
            {
                return null;
            }
            //try to get UnitOfWorkAttribute from controller,
            //if there is no UnitOfWorkAttribute on controller, 
            //try to get UnitOfWorkAttribute from action
            var uowAttr = caDesc.ControllerTypeInfo
                .GetCustomAttribute<UnitOfWorkAttribute>();
            if (uowAttr != null)
            {
                return uowAttr;
            }
            else
            {
                return caDesc.MethodInfo
                    .GetCustomAttribute<UnitOfWorkAttribute>();
            }
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var uowAttr = GetUoWAttr(context.ActionDescriptor);
            if (uowAttr == null)
            {
                await next();
                return;
            }
            using TransactionScope txScope = new(TransactionScopeAsyncFlowOption.Enabled);
            List<DbContext> dbCtxs = new List<DbContext>();
            foreach (var dbCtxType in uowAttr.DbContextTypes)
            {
                //用HttpContext的RequestServices
                //确保获取的是和请求相关的Scope实例
                var sp = context.HttpContext.RequestServices;
                DbContext dbCtx = (DbContext)sp.GetRequiredService(dbCtxType);
                dbCtxs.Add(dbCtx);
            }
            var result = await next();
            if (result.Exception == null)
            {
                foreach (var dbCtx in dbCtxs)
                {
                    await dbCtx.SaveChangesAsync();
                }
                txScope.Complete();
            }
        }
    }
}
