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
         IEnumerable<Assembly> assemblies,Action<IocOption> option)
        {
            //根据实现的IModuleInitializer接口的实体类自动注入
            //通过反射创建实体类进行方法的执行，进行手动的注入
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

            //liuyongfei 添加根据传入的option参数接口类型
            //实现实现相关接口实体类的自动注入
            if (option != null)
            {
                var iocOption = new IocOption();
                option(iocOption);//客户端类型注入 
                var definedTypes = assemblies.SelectMany(y => y.DefinedTypes).ToList();
                foreach (var type in iocOption.Types)
                {
                    var allTypes = definedTypes.Where(t => type.GetTypeInfo().IsAssignableFrom(t.AsType()));

                    var abstractTypes = allTypes.Where(t => t.IsInterface || t.IsAbstract);
                    var implTypes = allTypes.Where(t => t.IsInterface == false && t.IsAbstract == false).ToList();


                    foreach (var abstractType in abstractTypes)
                    {
                        if (abstractTypes.Count() > 1
                            && abstractType.AssemblyQualifiedName == type.AssemblyQualifiedName)
                        {
                            continue;
                        }
                        var _implTypes = implTypes.Where(t => t.ImplementedInterfaces.Any(impl => impl.AssemblyQualifiedName == abstractType.AssemblyQualifiedName)
                                    || t.BaseType?.AssemblyQualifiedName == abstractType.AssemblyQualifiedName);
                        foreach (var implClass in _implTypes)
                        {
                            if (iocOption.ExceptTypes.Count > 0)
                            {
                                if (iocOption.ExceptTypes.Where(
                                    t => t.AssemblyQualifiedName == abstractType.AssemblyQualifiedName
                                    || t.AssemblyQualifiedName == implClass.AssemblyQualifiedName).Any())
                                {
                                    continue;
                                }
                            }
                            //配置ScopedDI
                            services.AddScoped(abstractType, implClass);
                        }
                    }
                }
            }

            return services;
        }
    }
}
