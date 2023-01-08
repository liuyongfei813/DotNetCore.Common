#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common.Validators
 * 唯一标识：903d82c5-3f9d-4383-b8c8-769a47e576b6
 * 文件名：RequiredGuidAttribute
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 14:58:29
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

namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// Value of Guid is not null nor Guid.Empty.
/// On asp.net core, if there is a parameter of Guid type, and there is no value for it, the value is Guid.Empty, but [Required] doesn't take Guid.Empty as invalid,
/// so please add  RequiredGuidAttribute to a parameter, property or field.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class RequiredGuidAttribute : ValidationAttribute
{
    public const string DefaultErrorMessage = "The {0} field is requird and not Guid.Empty";
    public RequiredGuidAttribute() : base(DefaultErrorMessage) { }

    /// <summary>
    /// override IsValid
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool IsValid(object value)
    {
        if (value is null)
        {
            return false;
        }
        if (value is Guid)
        {
            Guid guid = (Guid)value;
            return guid != Guid.Empty;
        }
        else
        {
            return false;
        }
    }
}

