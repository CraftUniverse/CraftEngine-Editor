using System.Collections.Generic;
using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class Metadata
{
    [JsonPropertyName("arguments")] public MetadataArguments Arguments { get; init; }
    [JsonPropertyName("assetIndex")] public MetadataAssetIndex AssetIndex { get; init; }
    [JsonPropertyName("assets")] public string Assets { get; init; }
    [JsonPropertyName("complianceLevel")] public int ComplianceLevel { get; init; }
    [JsonPropertyName("downloads")] public MetadataDownloads.DownloadsHead Downloads { get; init; }
    [JsonPropertyName("id")] public string Id { get; init; }
    [JsonPropertyName("javaVersion")] public MetadataJavaVersion JavaVersion { get; init; }
    [JsonPropertyName("libraries")] public List<MetadataLibraries> Libraries { get; init; }
    [JsonPropertyName("logging")] public MetadataLogging Logging { get; init; }
    [JsonPropertyName("mainClass")] public string MainClass { get; init; }
    [JsonPropertyName("type")] public string Type { get; init; }
}