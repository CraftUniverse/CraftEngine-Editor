using System;
using System.IO;
using System.Threading.Tasks;
using MessagePack;

namespace dev.craftengine.editor.ProjectManagement;

public class ProjectManagement
{
    public static FileObjects.ProjectConfig? CurrentProjectConfig { get; set; }

    public static async Task CreateNewProject(string name, string version, string path)
    {
        try
        {
            string projectPath = Path.Join(path, name);

            Directory.CreateDirectory(projectPath);

            var projectConfig = new FileObjects.ProjectConfig
            {
                ProjectName = name,
                GameVersion = version,
                ProjectVersion = "1.0.0",
                GameProtocol = 769,
                ProjectAuthors = []
            };

            byte[] projectConfigBytes = MessagePackSerializer.Serialize(projectConfig);

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
}