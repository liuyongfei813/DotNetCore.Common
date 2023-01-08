#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：afd404ad-3aed-4462-9af9-373c7c0e53c8
 * 文件名：ModuleInitializerExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:14:27
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
using DotNetCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ModuleInitializerExtensions
    {
        /// <summary>
        /// 每个项目中都可以自己写一些实现了IModuleInitializer接口的类，
        /// 在其中注册自己需要的服务，这样避免所有内容到入口项目中注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static IServiceCollection RunModuleInitializers(this IServiceCollection services,
         IEnumerable<Assembly> assemblies)
        {
            foreach (var asm in assemblies)
            {
                Type[] types = asm.GetTypes();
                var moduleInitializerTypes = types.Where(t => !t.IsAbstract && typeof(IModuleInitializer).IsAssignableFrom(t));
                foreach (var implType in moduleInitializerTypes)
                {
                    var initializer = (IModuleInitializer?)Activator.CreateInstance(implType);
                    if (initializer == null)
                    {
                        throw new ApplicationException($"Cannot create ${implType}");
                    }
                    initializer.Initialize(services);
                }
            }
            return services;
        }
    }
}
