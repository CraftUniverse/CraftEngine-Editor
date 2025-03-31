using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class MetadataLibraries
{
    public required LibraryDownloads downloads { get; set; }
    public required string name { get; set; }
    public required List<LibraryRule>? rules { get; set; }

    public class LibraryDownloads
    {
        public required LibraryArtifact artifact { get; set; }
    }

    public class LibraryArtifact
    {
        public required string path { get; set; }
        public required string sha1 { get; set; }
        public required long size { get; set; }
        public required string url { get; set; }
    }

    public class LibraryRule
    {
        public required string action { get; set; } = "allow";
        public required MetadataArguments.OperatingSystemRequirements os { get; set; }
    }
}