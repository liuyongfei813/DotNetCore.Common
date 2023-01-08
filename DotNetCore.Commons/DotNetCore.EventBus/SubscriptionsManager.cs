#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.EventBus
 * 唯一标识：d4cafd02-0741-4e55-ac9e-6d209731b8f2
 * 文件名：SubscriptionsManager
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 16:52:30
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

namespace DotNetCore.EventBus
{
    class SubscriptionsManager
    {
        //key是eventName，值是监听这个事件的实现了IIntegrationEventHandler接口的类型
        private readonly Dictionary<string, List<Type>> _handlers = new Dictionary<string, List<Type>>();

        public event EventHandler<string> OnEventRemoved;

        public bool IsEmpty => !_handlers.Keys.Any();
        public void Clear() => _handlers.Clear();

        /// <summary>
        /// 把eventHandlerType类型（实现了eventHandlerType接口）注册为监听了eventName事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventHandlerType"></param>
        public void AddSubscription(string eventName, Type eventHandlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }
            //如果已经注册过，则报错
            if (_handlers[eventName].Contains(eventHandlerType))
            {
                throw new ArgumentException($"Handler Type {eventHandlerType} already registered for '{eventName}'", nameof(eventHandlerType));
            }
            _handlers[eventName].Add(eventHandlerType);
        }

        public void RemoveSubscription(string eventName, Type handlerType)
        {
            _handlers[eventName].Remove(handlerType);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
                OnEventRemoved?.Invoke(this, eventName);
            }
        }

        /// <summary>
        /// 得到名字为eventName的所有监听者
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public IEnumerable<Type> GetHandlersForEvent(string eventName) => _handlers[eventName];

        /// <summary>
        /// 是否有类型监听eventName这个事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

    }
}
