/**************************************************************
 * 
 * 唯一标识：ea7a23ab-e2ee-4c37-ba19-b555e9d6918b
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 15:41:08
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    public interface IBackGroundTaskExecutor
    {
        Task ExecuteAsync(BackGroundTaskContext context);
    }

}
