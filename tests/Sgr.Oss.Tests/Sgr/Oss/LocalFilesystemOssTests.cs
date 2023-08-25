/**************************************************************
 * 
 * 唯一标识：9ff35650-a3e1-47d0-b6e8-3515bfe5396a
 * 命名空间：Sgr.Oss
 * 创建时间：2023/8/25 13:46:55
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Logging;
using Moq;
using Sgr.Oss.Services;
using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Oss
{
    [TestClass]
    public class LocalFilesystemOssTests
    {
        private const string txt_content = "人之初，性本善。性相近，习相远。\r\n\r\n　　苟不教，性乃迁。教之道，贵以专。\r\n\r\n　　昔孟母，择邻处。子不学，断机杼。\r\n\r\n　　窦燕山，有义方。教五子，名俱扬。";

        private readonly string bucketName = "test1";
        private readonly string bucketName2 = "test2";

        private readonly string objectName;
        private readonly string objectFile;

        private readonly Mock<ILogger<LocalFilesystemOssService>> _logger;
        private readonly IOssService _ossService;

        public LocalFilesystemOssTests()
        {
            _logger = new Mock<ILogger<LocalFilesystemOssService>>();

            LocalFilesystemOptions localFilesystemOptions = LocalFilesystemOptions.CreatDefault();
            _ossService = new LocalFilesystemOssService(localFilesystemOptions, _logger.Object);

            objectName = Guid.NewGuid().ToString("N") + ".txt";

            objectFile = Path.Combine(LocalFileHelper.GetCurrentUserDataDirectory(), "oss_test.txt");

            LocalFileHelper.WriteStringToFileAsync(objectFile, txt_content);
        }


        [TestMethod]
        public async Task LocalFilesystem_CURD()
        {
            //检查存储桶是否存在，不存在则创建
            if (!await _ossService.BucketAnyAsync(bucketName))
                Assert.IsTrue(await _ossService.CreateBucketAsync(bucketName));

            //可获取到刚刚创建的存储桶
            Assert.IsTrue((await _ossService.GetBucketNamesAsync()).Contains(bucketName));
            //删除刚刚创建的存储桶
            Assert.IsTrue(await _ossService.RemoveBucketAsync(bucketName));
            //无法获取到刚刚删除的存储桶
            Assert.IsFalse((await _ossService.GetBucketNamesAsync()).Contains(bucketName));

            //检查文件是否存在，如果不存在则上传
            if (!await _ossService.ObjectsAnyAsync(bucketName, objectName))
                Assert.IsTrue(await _ossService.PutObjectAsync(bucketName, objectName, objectFile));

            Assert.IsTrue(await _ossService.CopyObjectAsync(bucketName, objectName, bucketName2));

            //可获取刚刚上传的文件，读取文件，确保文件与上传的内容一致
            string tmpFile = Path.Combine(LocalFileHelper.GetCurrentUserDataDirectory(), "tmp.txt");
            await _ossService.GetObjectAsync(bucketName, objectName, tmpFile);
            Assert.IsTrue((await LocalFileHelper.ReadFileContentToStringAsync(tmpFile) ?? "").Equals(txt_content));

            await _ossService.GetObjectAsync(bucketName2, objectName, tmpFile);
            Assert.IsTrue((await LocalFileHelper.ReadFileContentToStringAsync(tmpFile) ?? "").Equals(txt_content));

            //删除刚刚上传的文件
            Assert.IsTrue(await _ossService.RemoveObjectAsync(bucketName, objectName));
            Assert.IsFalse(await _ossService.ObjectsAnyAsync(bucketName, objectName));

            Assert.IsTrue(await _ossService.RemoveObjectAsync(bucketName2, objectName));
            Assert.IsFalse(await _ossService.ObjectsAnyAsync(bucketName2, objectName));

            Assert.IsTrue(await _ossService.RemoveBucketAsync(bucketName));
            Assert.IsTrue(await _ossService.RemoveBucketAsync(bucketName2));
        }
    }
}
