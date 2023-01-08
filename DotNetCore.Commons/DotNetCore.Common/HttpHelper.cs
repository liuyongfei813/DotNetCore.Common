#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-3QCB1KU
 * 公司名称：
 * 命名空间：DotNetCore.Common
 * 唯一标识：924c6aa9-8a47-4f41-b3c6-959572c43d16
 * 文件名：HttpHelper
 * 当前用户域：DESKTOP-3QCB1KU
 * 
 * 创建者：liuyongfei
 * 电子邮箱：953917939@qq.com
 * 创建时间：2023/1/8 15:04:43
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.Common
{
    /// <summary>
    /// Http帮助类
    /// </summary>
    public static class HttpHelper
    {
        public static async Task SaveToFileAsync(this HttpResponseMessage respMsg, string file,
            CancellationToken cancellationToken = default)
        {
            if (respMsg.IsSuccessStatusCode == false)
            {
                throw new ArgumentException($"StatusCode of HttpResponseMessage is {respMsg.StatusCode}", nameof(respMsg));
            }
            using FileStream fs = new FileStream(file, FileMode.Create);
            await respMsg.Content.CopyToAsync(fs, cancellationToken);
        }

        public static async Task<HttpStatusCode> DownloadFileAsync(this HttpClient httpClient, Uri url, string localFile,
            CancellationToken cancellationToken = default)
        {
            var resp = await httpClient.GetAsync(url, cancellationToken);
            if (resp.IsSuccessStatusCode)
            {
                await SaveToFileAsync(resp, localFile, cancellationToken);
                return resp.StatusCode;
            }
            else
            {
                return HttpStatusCode.OK;
            }
        }

        public static async Task<T?> GetJsonAsync<T>(this HttpClient httpClient, Uri url, CancellationToken cancellationToken = default)
        {
            string json = await httpClient.GetStringAsync(url, cancellationToken);
            return json.ParseJson<T>();
        }
    }
}
