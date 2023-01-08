#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：cf1169ad-f92b-40b3-91e7-921990e5dc9d
 * 文件名：IModuleInitializer
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:06:58
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
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.Common
{
    /// <summary>
    /// 所有项目中的实现了IModuleInitializer接口都会被调用，请在Initialize中编写注册本模块需要的服务。
    /// 一个项目中可以放多个实现了IModuleInitializer的类。不过为了集中管理，还是建议一个项目中只放一个实现了IModuleInitializer的类
    /// </summary>
    public interface IModuleInitializer
    {
        public void Initialize(IServiceCollection services);
    }
}
