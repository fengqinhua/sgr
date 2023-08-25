/**************************************************************
 * 
 * 唯一标识：35c87a7e-52d7-4fa8-bb39-b5b56338f92d
 * 命名空间：Sgr.Oss
 * 创建时间：2023/8/25 9:49:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.Oss
{
    public class LocalFilesystemOptions
    {
        /// <summary>
        /// 标识 WorkDir 指示的是否为相对路径
        /// </summary>
        public bool IsRelativePath { get; set; } = true;
        /// <summary>
        /// 工作目录
        /// </summary>
        public string WorkDir { get; set; } = "work_dir";


        public static LocalFilesystemOptions CreatDefault()
        {
            return new LocalFilesystemOptions()
            {
                IsRelativePath = true,
                WorkDir = "oss_work_dir"
            };
        }
    }
}
