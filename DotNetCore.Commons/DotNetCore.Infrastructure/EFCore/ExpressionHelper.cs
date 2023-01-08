#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Infrastructure.EFCore
 * 唯一标识：a17b71ec-cd64-46a1-8a8c-baa4f4c23db8
 * 文件名：ExpressionHelper
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 17:35:26
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
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace DotNetCore.Infrastructure.EFCore
{
    public class ExpressionHelper
    {
        /// <summary>
        /// Users.SingleOrDefaultAsync(MakeEqual((User u) => u.PhoneNumber, phoneNumber))
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="propAccessor"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Expression<Func<TItem, bool>> MakeEqual<TItem, TProp>(Expression<Func<TItem, TProp>> propAccessor, TProp? other)
            where TItem : class
            where TProp : class
        {
            var e1 = propAccessor.Parameters.Single();//提取出来参数
            BinaryExpression? conditionalExpr = null;
            foreach (var prop in typeof(TProp).GetProperties())
            {
                BinaryExpression equalExpr;
                //other的prop属性的值
                object? otherValue = null;
                if (other != null)
                {
                    otherValue = prop.GetValue(other);
                }
                Type propType = prop.PropertyType;
                //访问待比较的属性
                var leftExpr = MakeMemberAccess(
                    propAccessor.Body,//要取出来Body部分，不能带参数
                    prop
                );
                Expression rightExpr = Convert(Constant(otherValue), propType);
                if (propType.IsPrimitive)//基本数据类型和复杂类型比较方法不一样
                {
                    equalExpr = Equal(leftExpr, rightExpr);
                }
                else
                {
                    equalExpr = MakeBinary(ExpressionType.Equal,
                        leftExpr, rightExpr, false,
                        prop.PropertyType.GetMethod("op_Equality")
                    );
                }
                if (conditionalExpr == null)
                {
                    conditionalExpr = equalExpr;
                }
                else
                {
                    conditionalExpr = AndAlso(conditionalExpr, equalExpr);
                }
            }
            if (conditionalExpr == null)
            {
                throw new ArgumentException("There should be at least one property.");
            }
            return Lambda<Func<TItem, bool>>(conditionalExpr, e1);
        }
    }
}
