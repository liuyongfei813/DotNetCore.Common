#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common.JsonConverters
 * 唯一标识：510b23b6-7758-4184-a629-e8c1323531b5
 * 文件名：DateTimeJsonConverter
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 14:54:34
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DotNetCore.Common.JsonConverters
{
    /// <summary>
    ///日期时间格式化
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormatString;
        public DateTimeJsonConverter()
        {
            _dateFormatString = "yyyy-MM-dd HH:mm:ss";
        }

        public DateTimeJsonConverter(string dateFormatString)
        {
            _dateFormatString = dateFormatString;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? str = reader.GetString();
            if (str == null)
            {
                return default(DateTime);
            }
            else
            {
                return DateTime.Parse(str);
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            //固定用服务器所在的时区，前端如果想适应用户的时区，请自己调整
            writer.WriteStringValue(value.ToString(_dateFormatString));
        }
    }
}
