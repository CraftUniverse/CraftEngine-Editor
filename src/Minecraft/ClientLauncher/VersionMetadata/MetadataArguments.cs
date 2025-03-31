using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataArguments
{
    [JsonPropertyName("game")] public required List<GameArgument> Game { get; set; }
    [JsonPropertyName("jvm")] public required List<JvmArgument> Jvm { get; set; }

    public class GameArgument
    {
        [JsonPropertyName("value")] public required string[]? ValueArray { get; set; }
        [JsonPropertyName("value")] public required string? ValueString { get; set; }
        [JsonPropertyName("rules")] public required List<GameRule>? Rules { get; set; }
    }

    public class GameRule
    {
        [JsonPropertyName("action")] public required string Action { get; set; } = "allow";
        [JsonPropertyName("features")] public required GameFeatures Features { get; set; }
    }

    public class GameFeatures
    {
        [JsonPropertyName("has_custom_resolution")]
        public required bool? HasCustomResolution { get; set; }

        [JsonPropertyName("has_quick_plays_support")]
        public required bool? HasQuickPlaysSupport { get; set; }
    }

    public class JvmArgument
    {
        [JsonPropertyName("value")] public required string[]? ValueArray { get; set; }
        [JsonPropertyName("value")] public required string? ValueString { get; set; }
        [JsonPropertyName("rules")] public required List<JvmRule>? Rules { get; set; }
    }

    public class JvmRule
    {
        [JsonPropertyName("action")] public required string Action { get; set; } = "allow";
        [JsonPropertyName("os")] public required OperatingSystemRequirements Os { get; set; }
    }

    public class OperatingSystemRequirements
    {
        [JsonPropertyName("name")] public required string? Name { get; set; }
        [JsonPropertyName("arch")] public required string? Arch { get; set; }
    }
}