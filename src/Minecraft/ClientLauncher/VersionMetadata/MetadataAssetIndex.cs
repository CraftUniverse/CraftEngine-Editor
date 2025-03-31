using System.Text.Json.Serialization;

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataAssetIndex
{
    [JsonPropertyName("id")] public required string Id { get; set; }
    [JsonPropertyName("sha1")] public required string Sha1 { get; set; }
    [JsonPropertyName("size")] public required long Size { get; set; }
    [JsonPropertyName("totalSize")] public required long TotalSize { get; set; }
    [JsonPropertyName("url")] public required string Url { get; set; }
}