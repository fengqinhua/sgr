/**************************************************************
 * 
 * 唯一标识：8dfba7aa-02a8-44df-9680-431f16c96062
 * 命名空间：Sgr.Attachments.Services
 * 创建时间：2023/8/25 9:32:53
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
    public interface IOssService
    {
        #region 存储桶

        /// <summary>
        /// 检查存储桶是否存在
        /// </summary>
        /// <param name="bucketName">存储桶名称</param>
        /// <returns></returns>
        Task<bool> BucketAnyAsync(string bucketName);
        /// <summary>
        /// 创建存储桶
        /// </summary>
        /// <param name="bucketName">存储桶名称</param>
        /// <returns></returns>
        Task<bool> CreateBucketAsync(string bucketName);
        /// <summary>
        /// 删除一个存储桶
        /// </summary>
        /// <param name="bucketName">存储桶名称</param>
        /// <returns></returns>
        Task<bool> RemoveBucketAsync(string bucketName);
        /// <summary>
        /// 获取所有存储桶名称
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetBucketNamesAsync();

        //存储桶存储权限、存储桶详细信息暂时不实现


        #endregion

        #region 存储对象

        /// <summary>
        /// 指定的存储桶内是否存在某个存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        Task<bool> ObjectsAnyAsync(string bucketName, string objectName);

        /// <summary>
        /// 获取存储桶中的存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task GetObjectAsync(string bucketName, string objectName, Func<Stream, Task> callback, CancellationToken cancellationToken = default);
        /// <summary>
        /// 获取存储桶中的存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task GetObjectAsync(string bucketName, string objectName, string filePath, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传存储对象至存储桶中
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> PutObjectAsync(string bucketName, string objectName, Stream data, CancellationToken cancellationToken = default);
        /// <summary>
        /// 上传存储对象至存储桶中
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> PutObjectAsync(string bucketName, string objectName, string filePath, CancellationToken cancellationToken = default);

        /// <summary>
        /// 复制存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destObjectName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CopyObjectAsync(string bucketName, string objectName, string destBucketName, string? destObjectName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除某个存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        Task<bool> RemoveObjectAsync(string bucketName, string objectName);


        //列出所有存储对象、获取对象的元数据

        #endregion
    }
}
