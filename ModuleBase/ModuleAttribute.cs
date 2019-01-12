using System;

namespace Module
{
    /// <summary>
    /// 视觉模块自定义特性
    /// </summary>
    public class ModuleAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">模块名称</param>
        /// <param name="category">模块类别</param>
        public ModuleAttribute(string name, string category)
        {
            Name = name;
            Category = category;
        }
    }
}
