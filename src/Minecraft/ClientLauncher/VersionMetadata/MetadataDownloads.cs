using System.Text.Json.Serialization;

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataDownloads
{
    [JsonPropertyName("sha1")] public required string Sha1 { get; set; }
    [JsonPropertyName("size")] public required long Size { get; set; }
    [JsonPropertyName("url")] public required string Url { get; set; }

    public class DownloadsHead
    {
        [JsonPropertyName("client")] public required MetadataDownloads Client { get; set; }
        [JsonPropertyName("client_mappings")] public required MetadataDownloads ClientMappings { get; set; }
        [JsonPropertyName("server")] public required MetadataDownloads Server { get; set; }
        [JsonPropertyName("server_mappings")] public required MetadataDownloads ServerMappings { get; set; }
    }
}