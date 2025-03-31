
// ReSharper disable InconsistentNaming

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataAssetIndex
{
    public required string id { get; set; }
    public required string sha1 { get; set; }
    public required long size { get; set; }
    public required long totalSize { get; set; }
    public required string url { get; set; }
}