namespace Sgr.AspNetCore.Modules
{
    /// <summary>
    /// 基于json配置获取模块信息
    /// </summary>
    public class ModuleManifest
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 模块分类
        /// </summary>
        public string Category { get; set; } = "Foundation";
        /// <summary>
        /// 模块描述
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// 模块是否需要加载
        /// </summary>
        public bool IsEnable { get; set; } = true;
    }
}
