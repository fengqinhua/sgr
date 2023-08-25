/**************************************************************
 * 
 * 唯一标识：f803d157-aa5b-42ba-9c63-0207374f925d
 * 命名空间：Sgr.Attachments.Services
 * 创建时间：2023/8/25 9:49:15
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
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;
using System.Linq;

namespace Sgr.Attachments.Services
{
    public class LocalFilesystemOssService : IOssService
    {
        private readonly LocalFilesystemOptions _localFilesystemOptions;

        public LocalFilesystemOssService(LocalFilesystemOptions localFilesystemOptions)
        {
            _localFilesystemOptions = localFilesystemOptions;
        }

        #region

        /// <summary>
        /// 判断名称是否合规
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual bool IsCompliant(string name)
        {
            if (name.Length == 0)
                return false;

            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                return false;
            return true;
        }
        /// <summary>
        /// 获得工作目录
        /// </summary>
        /// <returns></returns>
        protected virtual string GetWorkDirPath()
        {
            string workDirPath;
            if(_localFilesystemOptions.IsRelativePath)
                workDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _localFilesystemOptions.WorkDir);
            else
                workDirPath = _localFilesystemOptions.WorkDir;
            
            if(!Directory.Exists(workDirPath))
                Directory.CreateDirectory(workDirPath);

            return workDirPath;
        }


        #region 存储桶

        /// <summary>
        /// 检查存储桶是否存在
        /// </summary>
        /// <param name="bucketName">存储桶名称</param>
        /// <returns></returns>
        public virtual Task<bool> BucketAnyAsync(string bucketName)
        {
            string dir = Path.Combine(GetWorkDirPath(), bucketName); 
            return Task.FromResult(Directory.Exists(dir));
        }

        /// <summary>
        /// 创建存储桶
        /// </summary>
        /// <param name="bucketName">存储桶名称</param>
        /// <returns></returns>
        public virtual Task<bool> CreateBucketAsync(string bucketName)
        {
            if (!IsCompliant(bucketName))
                return Task.FromResult(false);

            string dir = Path.Combine(GetWorkDirPath(), bucketName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return Task.FromResult(true);
        }

        /// <summary>
        /// 删除一个存储桶
        /// </summary>
        /// <param name="bucketName">存储桶名称</param>
        /// <returns></returns>
        public virtual Task<bool> RemoveBucketAsync(string bucketName)
        {
            string dir = Path.Combine(GetWorkDirPath(), bucketName);
            if (Directory.Exists(dir))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                directoryInfo.Delete(true);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// 获取所有存储桶名称
        /// </summary>
        /// <returns></returns>
        public virtual Task<IEnumerable<string>> GetBucketNamesAsync()
        {
            string dir = GetWorkDirPath();

            List<string> names = new List<string>();
            if (Directory.Exists(dir))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                foreach (var item in directoryInfo.GetDirectories())
                {
                    names.Add(item.Name);
                }
            }

            return Task.FromResult(names.AsEnumerable());
        }


        //存储桶存储权限、存储桶详细信息暂时不实现


        #endregion

        #region 存储对象

        /// <summary>
        /// 指定的存储桶内是否存在某个存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public virtual async Task<bool> ObjectsAnyAsync(string bucketName, string objectName)
        {
            //检查对象名称是否合规
            if (!IsCompliant(objectName))
                return false;

            //存储桶不存在则创建
            if (!await BucketAnyAsync(bucketName))
            {
                await CreateBucketAsync(objectName);
            }
               



        }


        /// <summary>
        /// 获取存储桶中的存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task GetObjectAsync(string bucketName, string objectName, Action<Stream> callback, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }

        /// <summary>
        /// 获取存储桶中的存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task GetObjectAsync(string bucketName, string objectName, string filePath, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }


        /// <summary>
        /// 上传存储对象至存储桶中
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> PostObjectAsync(string bucketName, string objectName, Stream data, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }

        /// <summary>
        /// 上传存储对象至存储桶中
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> PostObjectAsync(string bucketName, string objectName, string filePath, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }


        /// <summary>
        /// 复制存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destObjectName"></param>
        /// <returns></returns>
        public virtual Task<bool> CopyObjectAsync(string bucketName, string objectName, string destBucketName, string? destObjectName = null)
        {
            throw new Exception();
        }


        /// <summary>
        /// 删除某个存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public virtual Task<bool> RemoveObjectAsync(string bucketName, string objectName)
        {
            throw new Exception();
        }


        /// <summary>
        /// 删除多个存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectNames"></param>
        /// <returns></returns>
        public virtual Task<bool> RemoveObjectAsync(string bucketName, List<string> objectNames)
        {
            throw new Exception();
        }


        //列出所有存储对象、获取对象的元数据

        #endregion

        #endregion


    }
}
