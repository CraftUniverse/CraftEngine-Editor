
// ReSharper disable InconsistentNaming

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataLogging
{
    public required LoggingClient client { get; set; }

    public class LoggingClient
    {
        public required string argument { get; set; }
        public required LoggingFile file { get; set; }
        public required string type { get; set; }
    }

    public class LoggingFile
    {
        public required string id { get; set; }
        public required string sha1 { get; set; }
        public required long size { get; set; }
        public required string url { get; set; }
    }
}