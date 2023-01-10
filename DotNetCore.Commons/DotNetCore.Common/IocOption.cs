#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：8dc5fa5e-cf5f-4228-89b3-c6242ef937bb
 * 文件名：IocOption
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/10 15:52:12
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

namespace DotNetCore.Common
{
    /// <summary>
    ///  Ioc选择项
    /// </summary>
    public class IocOption
    {
        /// <summary>
        /// Ioc类型
        /// </summary>
        public List<Type> Types { get; private set; } = new List<Type>();

        /// <summary>
        /// 依赖注入需要排除的类型
        /// </summary>
        public List<Type> ExceptTypes { get; private set; } = new List<Type>();

        /// <summary>
        /// 添加目标接口
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IocOption Add(params Type[] types)
        {
            foreach (var type in types)
            {
                if (Types.Find(t => t.Name == type.Name
                && t.Namespace == type.Namespace) == null)
                {
                    Types.Add(type);
                }
            }
            return this;
        }

        /// <summary>
        /// 排序不需要的接口类型注入
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IocOption Excepts(params Type[] types)
        {
            foreach (var type in types)
            {
                if (ExceptTypes.Find(t => t.Name == type.Name
                && t.Namespace == type.Namespace) == null)
                {
                    ExceptTypes.Add(type);
                }
            }
            return this;
        }

    }
}
