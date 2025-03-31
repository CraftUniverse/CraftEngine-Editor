// ReSharper disable InconsistentNaming

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataDownloads
{
    public required string sha1 { get; set; }
    public required long size { get; set; }
    public required string url { get; set; }

    public class DownloadsHead
    {
        public required MetadataDownloads client { get; set; }
    }
}