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

        await VersionMetadata.VersionMetadata.Download(version);
        await JavaDownload.Download(21);
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