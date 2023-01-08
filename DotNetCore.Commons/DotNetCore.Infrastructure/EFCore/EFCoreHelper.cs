#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Infrastructure.EFCore
 * 唯一标识：4c650bfb-ca7b-470b-ae13-7ae282d6e9b9
 * 文件名：EFCoreHelper
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 17:33:52
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
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.Infrastructure.EFCore
{
    public static class EFCoreHelper
    {
        /// <summary>
        /// 得到表名
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbCtx"></param>
        /// <returns></returns>
        public static string GetTableName<TEntity>(this DbContext dbCtx)
        {
            var entityType = dbCtx.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
            {
                throw new ArgumentOutOfRangeException("TEntity is nof found in DbContext");
            }
            return entityType.GetTableName();
        }

        /// <summary>
        /// 得到实体中属性对应的列名
        /// 用法：string fName = dbCtx.GetColumnName<Person>(p=>p.Name);
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbCtx"></param>
        /// <param name="propertyLambda"></param>
        /// <returns></returns>

        public static string GetColumnName<TEntity>(this DbContext dbCtx, Expression<Func<TEntity, object>> propertyLambda)
        {
            var entityType = dbCtx.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
            {
                throw new ArgumentOutOfRangeException("TEntity is nof found in DbContext");
            }

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                var unary = propertyLambda.Body as UnaryExpression;
                if (unary != null)
                {
                    member = unary.Operand as MemberExpression;
                }
            }
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));
            }

            Type type = typeof(TEntity);
            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(), type));
            }

            string propertyName = propInfo.Name;
            var objId = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
            return entityType.FindProperty(propertyName).GetColumnName(objId.Value);
        }
    }
}
