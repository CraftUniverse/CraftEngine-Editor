using System.Collections.Generic;

// ReSharper disable InconsistentNaming

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class Metadata
{
    public MetadataArguments arguments { get; init; }
    public MetadataAssetIndex assetIndex { get; init; }
    public MetadataDownloads.DownloadsHead downloads { get; init; }
    public MetadataJavaVersion javaVersion { get; init; }
    public List<MetadataLibraries> libraries { get; init; }
    public MetadataLogging logging { get; init; }
    public string mainClass { get; init; }
    public string type { get; init; }
    public string id { get; init; }
}