#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：7887b431-dc16-42c8-a7c7-ea17b3c5662f
 * 文件名：IOHelper
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:07:36
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
    public static class IOHelper
    {
        public static async Task<byte[]> ToArrayAsync(this Stream stream)
        {
            using MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;
            return ms.ToArray();
        }

        public static byte[] ToArray(this Stream stream)
        {
            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Position = 0;
            return ms.ToArray();
        }

        public static void CreateDir(FileInfo file)
        {
            file.Directory.Create();
        }
    }
}
