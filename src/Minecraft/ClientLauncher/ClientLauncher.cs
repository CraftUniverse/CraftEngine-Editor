using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class ClientLauncher
{
    public static async Task Launch(string version)
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        CreateFolderStructure();

        var metadata = await VersionMetadata.VersionMetadata.Download(version);

        await VersionDownload.Download(metadata.downloads.client.url, version);
        await JavaDownload.Download(metadata.javaVersion.majorVersion);
    }

    static private void CreateFolderStructure()
    {
        var flags =
            UnixFileMode.OtherExecute
            | UnixFileMode.GroupExecute
            | UnixFileMode.UserExecute;

        List<string> folders =
        [
            Path.Combine(Constants.BASE_PATH, "assets"),
            Path.Combine(Constants.BASE_PATH, "java"),
            Path.Combine(Constants.BASE_PATH, "libraries"),
            Path.Combine(Constants.BASE_PATH, "natives"),
            Path.Combine(Constants.BASE_PATH, "versions")
        ];

        foreach (string folder in folders)
        {
            if (Directory.Exists(folder))
            {
                continue;
            }

            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(folder);
            }
            else
            {
                Directory.CreateDirectory(folder, flags);
            }
        }
    }
}