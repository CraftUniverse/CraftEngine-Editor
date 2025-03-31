using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataArguments
{
    public required List<object> game { get; set; }
    public required List<object> jvm { get; set; }

    public class GameRule
    {
        public required string action { get; set; } = "allow";
        public required GameFeatures features { get; set; }
    }

    public class GameFeatures
    {
        public required bool? has_custom_resolution { get; set; }
        public required bool? has_quick_plays_support { get; set; }
    }

    public class JvmRule
    {
        public required string action { get; set; } = "allow";
        public required OperatingSystemRequirements os { get; set; }
    }

    public class OperatingSystemRequirements
    {
        public required string? name { get; set; }
        public required string? arch { get; set; }
    }
}