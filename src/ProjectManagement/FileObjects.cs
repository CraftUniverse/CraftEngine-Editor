using System.Collections.Generic;
using MessagePack;

namespace dev.craftengine.editor.ProjectManagement;

public class FileObjects
{
    [MessagePackObject]
    public class RecentProject
    {
        [Key(0)] public required string ProjectName { get; init; }
        [Key(1)] public required string ProjectVersion { get; init; }
        [Key(2)] public required string ProjectPath { get; init; }
        [Key(3)] public required ulong LastAccess { get; init; }
    }
    
    [MessagePackObject]
    public class ProjectConfig
    {
        [Key(0)] public int MagicNumber { get; set; } = 0xCE9C1;
        [Key(1)] public required string ProjectName { get; init; }
        [Key(2)] public required string ProjectVersion { get; init; }
        [Key(3)] public required List<ProjectAuthor> ProjectAuthors { get; init; }
        [Key(4)] public required string GameVersion { get; init; }
        [Key(5)] public required int GameProtocol { get; init; }

        [MessagePackObject]
        public class ProjectAuthor
        {
            [Key(0)] public required string Name { get; init; }
            [Key(1)] public required string Website { get; init; }
            [Key(2)] public required string EMail { get; init; }
        }
    }
}