using System.Reflection;

namespace dev.craftengine.editor.Helpers;

public class VersionControl
{
    public static readonly string Version = Assembly
        .GetExecutingAssembly()?
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        .InformationalVersion.Split("+")[0] ?? "MISSING_VER";
}