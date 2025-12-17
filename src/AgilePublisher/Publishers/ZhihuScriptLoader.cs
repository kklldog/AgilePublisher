using System.Reflection;
using System.Runtime.Loader;

namespace AgilePublisher.Publishers;

public sealed class ZhihuScriptLoader
{
    private readonly string _assemblyPath;

    public ZhihuScriptLoader(string assemblyPath)
    {
        if (string.IsNullOrWhiteSpace(assemblyPath))
        {
            throw new ArgumentException("Assembly path is required", nameof(assemblyPath));
        }

        if (!File.Exists(assemblyPath))
        {
            throw new FileNotFoundException($"Zhihu script assembly not found: {assemblyPath}");
        }

        _assemblyPath = Path.GetFullPath(assemblyPath);
    }

    public IZhihuPublishScript Load()
    {
        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(_assemblyPath);
        var scriptType = assembly
            .GetTypes()
            .FirstOrDefault(t => typeof(IZhihuPublishScript).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) is not null);

        if (scriptType is null)
        {
            throw new InvalidOperationException($"No implementation of {nameof(IZhihuPublishScript)} found in {_assemblyPath}");
        }

        return (IZhihuPublishScript)Activator.CreateInstance(scriptType)!;
    }
}
