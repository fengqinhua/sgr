/**************************************************************
 * 
 * 唯一标识：76d3ce86-6f27-4d45-8b07-df7344dfef5a
 * 命名空间：Sgr.IntegrationEvents.Domain
 * 创建时间：2023/8/19 21:52:31
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.IntegrationEvents.Domain
{
    /// <summary>
    /// 消息状态
    /// </summary>
    public enum MessageState
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        PublishedFailed = 3
    }
}
