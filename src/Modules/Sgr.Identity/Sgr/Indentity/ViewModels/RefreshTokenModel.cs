/**************************************************************
 * 
 * 唯一标识：ef001c0d-b1ec-4cb3-98be-3ed4473f955b
 * 命名空间：Sgr.Indentity.ViewModels
 * 创建时间：2023/8/21 19:44:29
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.Indentity.ViewModels
{
    public class RefreshTokenModel
    {
        /// <summary>
        /// 访问令牌文本内容。
        /// </summary>
        public string? AccessToken { get; set; }
        /// <summary>
        /// 刷新令牌文本内容
        /// </summary>
        public string? RefrashToken { get; set; }
    }
}
