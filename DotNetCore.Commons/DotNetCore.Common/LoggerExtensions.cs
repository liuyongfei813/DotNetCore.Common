#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：598d6ada-dc37-4910-8373-518ca3af95c5
 * 文件名：LoggerExtensions
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:09:07
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
namespace Microsoft.Extensions.Logging
{
    public static class LoggerExtensions
    {
        public static void LogInterpolatedCritical(this ILogger logger, FormattableString formattableString,
            Exception? exception = default, EventId eventId = default)
        {
            //todo: 插值字符串支持$"a={a,3:C}"这样的写法，目前这样不支持，需要解析，参考https://gist.github.com/artemious7/c7d9856e128a8b2e9e92d096ca0e69ee#file-serilog-loggerstringinterpolationextensions-cs
            logger.LogCritical(eventId, exception, formattableString.Format, formattableString.GetArguments());
        }

        public static void LogInterpolatedDebug(this ILogger logger, FormattableString formattableString,
            Exception? exception = default, EventId eventId = default)
        {
            logger.LogDebug(eventId, exception, formattableString.Format, formattableString.GetArguments());
        }

        public static void LogInterpolatedError(this ILogger logger, FormattableString formattableString,
            Exception? exception = default, EventId eventId = default)
        {
            logger.LogError(eventId, exception, formattableString.Format, formattableString.GetArguments());
        }

        public static void LogInterpolatedInformation(this ILogger logger, FormattableString formattableString,
            Exception? exception = default, EventId eventId = default)
        {
            logger.LogInformation(eventId, exception, formattableString.Format, formattableString.GetArguments());
        }

        public static void LogInterpolatedTrace(this ILogger logger, FormattableString formattableString,
            Exception? exception = default, EventId eventId = default)
        {
            logger.LogTrace(eventId, exception, formattableString.Format, formattableString.GetArguments());
        }

        public static void LogInterpolatedWarning(this ILogger logger, FormattableString formattableString,
            Exception? exception = default, EventId eventId = default)
        {
            logger.LogWarning(eventId, exception, formattableString.Format, formattableString.GetArguments());
        }
    }
}
