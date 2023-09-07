/**************************************************************
 * 
 * 唯一标识：ab3fc3be-7b53-44f3-af8d-4415de624c80
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/7 15:19:01
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    public class HttpRequestBackGroundTask : IBackGroundTask<HttpRequestArgs>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpRequestBackGroundTask(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task ExecuteAsync(HttpRequestArgs data)
        {
            using var httpClient = _httpClientFactory.CreateClient(data.ClientName);

            // 添加请求报文头 User-Agent
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36 Edg/104.0.1293.47");

            // 创建请求对象
            var httpRequestMessage = new HttpRequestMessage(data.HttpMethod, data.RequestUrl);

            // 添加请求报文体，默认只支持发送 application/json 类型
            if (data.HttpMethod != HttpMethod.Get
                && data.HttpMethod != HttpMethod.Head
                && !string.IsNullOrWhiteSpace(data.Body))
            {
                var stringContent = new StringContent(data.Body, Encoding.UTF8);
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpRequestMessage.Content = stringContent;
            }

            // 发送请求
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            // 确保请求成功
            if (data.EnsureSuccessStatusCode)
            {
                httpResponseMessage = httpResponseMessage.EnsureSuccessStatusCode();
            }
        }
    }
}
