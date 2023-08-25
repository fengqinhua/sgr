/**************************************************************
 * 
 * 唯一标识：7f1534a5-3475-401c-be3c-3d9ea9d1f8a7
 * 命名空间：Sgr.Oss.Services
 * 创建时间：2023/8/25 17:31:02
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.Oss.Services
{
    public static class OssServiceExtensions
    {
        public static Task<bool> PutImageAsync(this IOssService ossService, string objectName, Stream data, CancellationToken cancellationToken = default)
        {
            return ossService.PutObjectAsync("picture", objectName, data, cancellationToken);
        }

        public static Task<bool> RemoveImageAsync(this IOssService ossService, string objectName,CancellationToken cancellationToken = default)
        {
            return ossService.RemoveObjectAsync("picture", objectName, cancellationToken);
        }


        public static async Task<string> PutImageWithFileNameAsync(this IOssService ossService, string fileName, Stream data, CancellationToken cancellationToken = default)
        {
            var suffix = Path.GetExtension(fileName).ToLower();
            string objectName = $"{Guid.NewGuid():N}.{suffix}";
            await ossService.PutObjectAsync("picture", objectName, data, cancellationToken);
            return objectName;
        }
    }
}
