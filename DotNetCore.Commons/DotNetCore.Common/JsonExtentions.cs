#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：fff606f8-e569-48c4-9b81-c7e7619ca69c
 * 文件名：JsonExtentions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:05:37
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
using DotNetCore.Common.JsonConverters;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace System
{
    public static class JsonExtentions
    {
        public readonly static JavaScriptEncoder Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

        public static JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = false)
        {
            JsonSerializerOptions opt = new() { Encoder = Encoder };
            if (camelCase)
            {
                opt.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                opt.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }
            opt.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            return opt;
        }

        public static string ToJsonString(this object value, bool camelCase = false)
        {
            var opt = CreateJsonSerializerOptions(camelCase);
            return JsonSerializer.Serialize(value, value.GetType(), opt);
        }

        public static T? ParseJson<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }
            var opt = CreateJsonSerializerOptions();
            return JsonSerializer.Deserialize<T>(value, opt);
        }
    }
}
