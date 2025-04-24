using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MessagePack;

namespace dev.craftengine.editor.ProjectManagement;

public class ProjectManagement
{
    public static FileObjects.ProjectConfig? CurrentProjectConfig { get; private set; }
    public static List<RecentProject> RecentProjects = [];

    static private readonly string RecentProjectsPath =
        Path.Join($"${Environment.SpecialFolder.ApplicationData}", "CraftEngine", "recent.dat");

    public static async Task CreateNewProject(string name, string gameVersion, int gameProtocol, string path)
    {
        try
        {
            var projectPath = Path.Join(path, name);

            Directory.CreateDirectory(projectPath);

            var projectConfig = new FileObjects.ProjectConfig
            {
                ProjectName = name,
                GameVersion = gameVersion,
                ProjectVersion = "1.0.0",
                GameProtocol = gameProtocol,
                ProjectAuthors = []
            };

            Console.WriteLine($"Creating new project {name}");

            var projectConfigBytes = MessagePackSerializer.Serialize(projectConfig);

            await File.WriteAllBytesAsync(Path.Join(projectPath, "config.dat"), projectConfigBytes);
            CurrentProjectConfig = projectConfig;
        }
        catch (Exception exp)
        {
            await Console.Error.WriteLineAsync(exp.Message);
        }
    }

    public static async Task LoadProject(string path)
    {
        try
        {
            byte[] projectConfigBytes = await File.ReadAllBytesAsync(Path.Join(path, "config.dat"));
            var projectConfig = MessagePackSerializer.Deserialize<FileObjects.ProjectConfig>(projectConfigBytes);

            CurrentProjectConfig = projectConfig;
        }
        catch (Exception exp)
        {
            await Console.Error.WriteLineAsync(exp.Message);
        }
    }

    public static async Task WriteRecentProjects()
    {
        List<FileObjects.RecentProject> recentProjects = [];

        foreach (var project in RecentProjects)
        {
            recentProjects.Add(new FileObjects.RecentProject()
            {
                LastAccess = project.lastAccess,
                ProjectName = project.name,
                ProjectPath = project.path,
                ProjectVersion = project.version,
            });
        }

        var file = MessagePackSerializer.Serialize(recentProjects);

        await File.WriteAllBytesAsync(RecentProjectsPath, file);
    }

    public static async Task ReaadRecentProjects()
    {
        var file = await File.ReadAllBytesAsync(RecentProjectsPath);
        var recentProjects = MessagePackSerializer.Deserialize<List<FileObjects.RecentProject>>(file);

        foreach (var project in recentProjects)
        {
            RecentProjects.Add((new RecentProject(
                project.ProjectName,
                project.ProjectPath,
                project.ProjectVersion,
                project.LastAccess)));
        }
    }

    public record RecentProject(string name, string path, string version, ulong lastAccess)
    {
    }
}