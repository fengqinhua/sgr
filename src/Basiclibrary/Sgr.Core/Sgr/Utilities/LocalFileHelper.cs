/**************************************************************
 * 
 * 唯一标识：a8b150df-50dd-4f90-b9ad-3d151aba847d
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/24 11:59:59
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Utilities
{
    /// <summary>
    /// 本地文件帮助类
    /// </summary>
    public static class LocalFileHelper
    {
        /// <summary>
        /// 获取当前应用程序所在目录
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 获取当前用户工作目录
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static string GetCurrentUserDataDirectory(string appName = "sgr")
        {
            string configDir = "";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //如果是Linux系统，首先为环境变量 XDG_DATA_HOME 对应的目录；其次为环境变量 HOME 对应的目录
                configDir = System.Environment.GetEnvironmentVariable("XDG_DATA_HOME", EnvironmentVariableTarget.Machine);
                if (!string.IsNullOrEmpty(configDir))
                    configDir = System.IO.Path.Combine(configDir, appName);
                else
                {
                    configDir = System.Environment.GetEnvironmentVariable("HOME");
                    if (!string.IsNullOrEmpty(configDir))
                        configDir = System.IO.Path.Combine(configDir, ".local", "share", appName);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ////如果是Winodws系统,为环境变量 LocalAppData AppData 对应的目录 AppData\Roaming
                //configDir = System.Environment.GetEnvironmentVariable("LocalAppData");

                configDir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!string.IsNullOrEmpty(configDir))
                    configDir = System.IO.Path.Combine(configDir, appName);
            }

            //如果实在找不到则返回当前应用程序目录
            if (string.IsNullOrEmpty(configDir))
            {
                configDir = GetApplicationDirectory();
                if (!string.IsNullOrEmpty(configDir))
                    configDir = System.IO.Path.Combine(configDir, "workdir", "current_userdata");
            }

            //检查文件夹是否存在，如果不存在则创建之
            if (!string.IsNullOrEmpty(configDir))
            {
                if (!System.IO.Directory.Exists(configDir))
                    System.IO.Directory.CreateDirectory(configDir);
            }

            return configDir;


        }

        /// <summary>
        ///  获取所有用户的共享工作目录
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static string GetAllUserDataDirectory(string appName = "sgr")
        {
            string configDir = "";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                configDir = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                if (!string.IsNullOrEmpty(configDir))
                    configDir = System.IO.Path.Combine(configDir, appName);
            }

            //如果实在找不到则返回当前应用程序目录
            if (string.IsNullOrEmpty(configDir))
            {
                configDir = GetApplicationDirectory();
                if (!string.IsNullOrEmpty(configDir))
                    configDir = System.IO.Path.Combine(configDir, "workdir", "all_userdata");
            }


            //检查文件夹是否存在，如果不存在则创建之
            if (!string.IsNullOrEmpty(configDir))
            {
                if (!System.IO.Directory.Exists(configDir))
                    System.IO.Directory.CreateDirectory(configDir);
            }

            return configDir;
        }

        /// <summary>
        /// 读取指定文件，返回文件中的文本内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string?> ReadFileContentToStringAsync(string path,  Encoding? encoding = null)
        {
            var bytes = await ReadFileContentToBytesAsync(path);

            return StringHelper.ConvertFromBytesWithoutBom(bytes, encoding);
        }

        /// <summary>
        /// 读取指定文件，返回文件中的字节数组
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadFileContentToBytesAsync(string path)
        {
            using var stream = File.Open(path, FileMode.Open);
            var result = new byte[stream.Length];
            await stream.ReadAsync(result, 0, (int)stream.Length);
            return result;
        }

        /// <summary>
        /// 字符串写入指定的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <param name="overwrite"></param>
        /// <param name="encoding"></param>
        public static void WriteStringToFileAsync(string path, string content, bool overwrite = true, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            //检查文件是否存在
            if (File.Exists(path))
            {
                if (overwrite)
                    File.WriteAllText(path, content, encoding);
                else
                    File.AppendAllText(path, content, encoding);
            }
            else
            {
                //确保文件夹存在,如果不存在则创建
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                //写入文件
                File.WriteAllText(path, content, encoding);
            }
        }

        /// <summary>
        /// 如果文件存在则删除
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool RemoveIfExists(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
            return true;
        }


        public static string CreateFileNameForOss(string fileName)
        {
            return RandomHelper.GetRandomString(4, true, true, false, false) + fileName;
        }

    }
}
