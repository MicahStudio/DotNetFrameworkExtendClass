using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 基类扩展
    /// </summary>
    public static class ClassX
    {
        /// <summary>
        /// 引入M插件（如果存在的话）
        /// <para>插件方式是MEF项目的实现</para>
        /// <para>由System.ComponentModel.Composition类库实现</para>
        /// <para>如果您并未使用此功能请不要调用此方法</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="Path">插件路径</param>
        /// <returns></returns>
        public static T ComposePartsSelf<T>(this T obj, string Path = "Lib") where T : class
        {
            if (System.IO.Directory.Exists(Path))
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(Path));
                var _container = new CompositionContainer(catalog);
                _container.ComposeParts(obj);
            }
            return obj;
        }
    }
}
