using System.Text.Json.Serialization;

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataJavaVersion
{
    [JsonPropertyName("component")] public required string Component { get; set; }
    [JsonPropertyName("majorVersion")] public required int MajorVersion { get; set; }
}