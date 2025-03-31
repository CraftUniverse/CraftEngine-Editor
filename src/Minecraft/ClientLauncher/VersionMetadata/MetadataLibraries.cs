using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataLibraries
{
    [JsonPropertyName("downloads")] public required LibraryDownloads Downloads { get; set; }
    [JsonPropertyName("name")] public required string Name { get; set; }
    [JsonPropertyName("rules")] public required List<LibraryRule>? Rules { get; set; }

    public class LibraryDownloads
    {
        [JsonPropertyName("artifact")] public required LibraryArtifact Artifact { get; set; }
    }

    public class LibraryArtifact
    {
        [JsonPropertyName("path")] public required string Path { get; set; }
        [JsonPropertyName("sha1")] public required string Sha1 { get; set; }
        [JsonPropertyName("size")] public required long Size { get; set; }
        [JsonPropertyName("url")] public required string Url { get; set; }
    }

    public class LibraryRule
    {
        [JsonPropertyName("action")] public required string Action { get; set; } = "allow";
        [JsonPropertyName("os")] public required MetadataArguments.OperatingSystemRequirements Os { get; set; }
    }
}