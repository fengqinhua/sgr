/**************************************************************
 * 
 * 唯一标识：f803d157-aa5b-42ba-9c63-0207374f925d
 * 命名空间：Sgr.Oss.Services
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
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics.SymbolStore;

namespace Sgr.Oss.Services
{
    public class LocalFilesystemOssService : IOssService
    {
        private const int bufferSize = 10240;

        private readonly LocalFilesystemOptions _localFilesystemOptions;
        private readonly ILogger<LocalFilesystemOssService> _logger;

        public LocalFilesystemOssService(LocalFilesystemOptions localFilesystemOptions,
            ILogger<LocalFilesystemOssService> logger)
        {
            _localFilesystemOptions = localFilesystemOptions;
            _logger = logger;
        }


        /// <summary>
        /// 判断名称是否合规
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual bool IsCompliant(string name)
        {
            if (name.Length == 0)
                return false;

            if(name.StartsWith("."))
            {
                _logger.LogError($"LocalFilesystemOss : Name : {name} 不合规");
                return false;
            }

            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                _logger.LogError($"LocalFilesystemOss : Name : {name} 不合规");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得工作目录
        /// </summary>
        /// <returns></returns>
        protected virtual string GetWorkDirPath()
        {
            string workDirPath;
            if (_localFilesystemOptions.IsRelativePath)
                workDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _localFilesystemOptions.WorkDir);
            else
                workDirPath = _localFilesystemOptions.WorkDir;

            if (!Directory.Exists(workDirPath))
                Directory.CreateDirectory(workDirPath);

            return workDirPath;
        }

        /// <summary>
        /// 获取文件存放部分路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual string GetFilePartPathByName(string name)
        {
            int index = name.IndexOf('.');
            string front_part = name;
            if(index > 0)
                front_part = name.Substring(0, index);

            if (front_part.Length <= 2)
                return name;

            if (front_part.Length > 4)
                return Path.Combine(front_part.Substring(0, 2), front_part.Substring(2, 2), name);

            return Path.Combine(front_part.Substring(0, 2), name);
        }

        /// <summary>
        /// 获取文件存放路径
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        protected virtual string GetObjectFilePathFromBucket(string bucketName, string objectName)
        {
            string dir = Path.Combine(GetWorkDirPath(), bucketName);
            string filePartPath = GetFilePartPathByName(objectName);
            return Path.Combine(dir, filePartPath);
        }

        protected virtual void EnsureParentDirectoryExists(string filePath)
        {
            string parentPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(parentPath) && !Directory.Exists(parentPath))
                Directory.CreateDirectory(parentPath);
        }

        #region IOssService

        #region 存储桶

        /// <summary>
        /// 检查存储桶是否存在
        /// </summary>
        /// <param name="bucketName">存储桶名称</param>
        /// <returns></returns>
        public virtual Task<bool> BucketAnyAsync(string bucketName)
        {
            if (string.IsNullOrWhiteSpace(bucketName))
                throw new ArgumentNullException(nameof(bucketName));

            string dir = Path.Combine(GetWorkDirPath(), bucketName);

            bool result = Directory.Exists(dir);
            if (!result)
                _logger.LogWarning($"LocalFilesystemOss : BucketName : {bucketName} Not Exists!");

            return Task.FromResult(result);
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
            //存储桶不存在，返回假
            if (!await BucketAnyAsync(bucketName))
                return false;

            //存储对象名称不合规，返回假
            if (!IsCompliant(objectName))
                return false;

            return File.Exists(GetObjectFilePathFromBucket(bucketName, objectName));
        }



        /// <summary>
        /// 获取存储桶中的存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task GetObjectAsync(string bucketName, string objectName, Func<Stream, Task> callback, CancellationToken cancellationToken = default)
        {
            //存储桶不存在，返回假
            if (!await BucketAnyAsync(bucketName))
                return;

            //存储对象名称不合规，返回
            if (!IsCompliant(objectName))
                return;

            string filePath = GetObjectFilePathFromBucket(bucketName, objectName);

            //文件不存在，返回
            if (!File.Exists(filePath))
            {
                _logger.LogError($"LocalFilesystemOss.GetObjectAsync : ObjectName : {objectName} Is Not Exist!");
                return;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await callback(fs);
                fs.Close();
            }
        }

        /// <summary>
        /// 获取存储桶中的存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task GetObjectAsync(string bucketName, string objectName, string filePath, CancellationToken cancellationToken = default)
        {
            EnsureParentDirectoryExists(filePath);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await GetObjectAsync(bucketName, objectName, async (stream) =>
                {
                    await stream.CopyToAsync(fs);
                }, cancellationToken);
                
                fs.Close();
            }
        }


        /// <summary>
        /// 上传存储对象至存储桶中
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> PutObjectAsync(string bucketName, string objectName, Stream data, CancellationToken cancellationToken = default)
        {
            //存储对象名称不合规，返回
            if (!IsCompliant(objectName))
                return false;

            //检查存储桶是否存在，不存在则创建之，创建失败则返回假
            if (!await BucketAnyAsync(bucketName))
            {
                if (!await CreateBucketAsync(bucketName))
                {
                    _logger.LogError($"LocalFilesystemOss.PutObjectAsync : BucketName : {bucketName} Not Exists , Attempt To Create Failed!");
                    return false;
                }
            }

            string filePath = GetObjectFilePathFromBucket(bucketName, objectName);
            //创建父文件夹
            EnsureParentDirectoryExists(filePath);

            //写入文件
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await data.CopyToAsync(fs, bufferSize, cancellationToken);
                fs.Close();
            }

            return true;
        }

        /// <summary>
        /// 上传存储对象至存储桶中
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> PutObjectAsync(string bucketName, string objectName, string filePath, CancellationToken cancellationToken = default)
        {
            if (!File.Exists(filePath))
                return false;

            //存储对象名称不合规，返回
            if (!IsCompliant(objectName))
                return false;

            //检查存储桶是否存在，不存在则创建之，创建失败则返回假
            if (!await BucketAnyAsync(bucketName))
            {
                if (!await CreateBucketAsync(bucketName))
                {
                    _logger.LogError($"LocalFilesystemOss.PutObjectAsync : BucketName : {bucketName} Not Exists , Attempt To Create Failed!");
                    return false;
                }
            }

            string target_filePath = GetObjectFilePathFromBucket(bucketName, objectName);
            //创建父文件夹
            EnsureParentDirectoryExists(target_filePath);

            //写入文件
            using (FileStream data = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fs = new FileStream(target_filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    await data.CopyToAsync(fs, bufferSize, cancellationToken);
                    fs.Close();
                }
                data.Close();
            }

            return true;    
        }


        /// <summary>
        /// 复制存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destObjectName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> CopyObjectAsync(string bucketName, string objectName, string destBucketName, string? destObjectName = null, CancellationToken cancellationToken = default)
        { 

            //当前->存储对象名称不合规，返回
            if (!IsCompliant(objectName))
                return false;

            //当前->存储桶是否存在，不存在则返回
            if (!await BucketAnyAsync(bucketName))
                return false;


            //当前->文件不存在，返回
            string filePath = GetObjectFilePathFromBucket(bucketName, objectName);
            if (!File.Exists(filePath))
            {
                _logger.LogError($"LocalFilesystemOss.CopyObjectAsync : ObjectName : {objectName} Is Not Exist!");
                return false;
            }

            destObjectName ??= objectName;

            //目标->存储对象名称不合规，返回
            if (!IsCompliant(destObjectName))
                return false;

            //目标->存储桶是否存在，不存在则创建之，创建失败则返回假
            if (!await BucketAnyAsync(destBucketName))
            {
                if (!await CreateBucketAsync(destBucketName))
                {
                    _logger.LogError($"LocalFilesystemOss.CopyObjectAsync : DestBucketName : {destBucketName} Not Exists , Attempt To Create Failed!");
                    return false;
                }
            }
            //目标->确保文件夹存在

            string target_filePath = GetObjectFilePathFromBucket(destBucketName, destObjectName);
            EnsureParentDirectoryExists(target_filePath);

            //写入文件
            using (FileStream data = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fs = new FileStream(target_filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    await data.CopyToAsync(fs, bufferSize, cancellationToken);
                    fs.Close();
                }
                data.Close();
            }

            return true;

        }


        /// <summary>
        /// 删除某个存储对象
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public virtual async Task<bool> RemoveObjectAsync(string bucketName, string objectName)
        {
            //存储对象名称不合规，返回
            if (!IsCompliant(objectName))
                return false;

            //检查存储桶是否存在，不存在则返回
            if (!await BucketAnyAsync(bucketName))
                return false;

            string filePath = GetObjectFilePathFromBucket(bucketName, objectName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);

                string parentPath = Path.GetDirectoryName(filePath);
                if (Directory.GetFiles(parentPath).Length == 0 && !parentPath.EndsWith(bucketName))
                    Directory.Delete(parentPath);
            }

            return true;
        }


        //列出所有存储对象、获取对象的元数据

        #endregion

        #endregion


    }
}
