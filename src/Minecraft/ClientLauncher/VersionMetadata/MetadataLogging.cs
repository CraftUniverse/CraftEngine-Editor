using System.Text.Json.Serialization;

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataLogging
{
    [JsonPropertyName("client")] public required LoggingClient Client { get; set; }

    public class LoggingClient
    {
        [JsonPropertyName("argument")] public required string Argument { get; set; }
        [JsonPropertyName("file")] public required LoggingFile File { get; set; }
        [JsonPropertyName("type")] public required string Type { get; set; }
    }

    public class LoggingFile
    {
        [JsonPropertyName("id")] public required string Id { get; set; }
        [JsonPropertyName("sha1")] public required string Sha1 { get; set; }
        [JsonPropertyName("size")] public required long Size { get; set; }
        [JsonPropertyName("url")] public required string Url { get; set; }
    }
}