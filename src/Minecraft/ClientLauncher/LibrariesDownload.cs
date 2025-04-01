using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;
using dev.craftengine.editor.Views;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class LibrariesDownload
{
    public static async Task Download(Metadata metadata, Window parentWindow)
    {
        var loadingWin = new Loading(Resources.Resources.common_loading_title);
        loadingWin.ShowDialog(parentWindow);

        var client = new HttpClient();

        foreach (var lib in metadata.libraries)
        {
            if (lib.rules != null)
            {
                var shouldContinue = true;

                foreach (var rule in lib.rules)
                {
                    switch (rule.os.name)
                    {
                        case "linux" when OperatingSystem.IsLinux() && rule.action == "allow":
                        case "windows" when OperatingSystem.IsWindows() && rule.action == "allow":
                        case "osx" when OperatingSystem.IsMacOS() && rule.action == "allow":
                            shouldContinue = false;

                            break;
                    }
                }

                if (shouldContinue)
                {
                    continue;
                }
            }

            string filepath = Path.Combine(Constants.BASE_PATH, "libraries", lib.downloads.artifact.path);

            if (File.Exists(filepath))
            {
                continue;
            }

            loadingWin.SetTitle(Resources.Resources.client_launcher_download_libraries_title);
            loadingWin.Text.IsVisible = true;
            loadingWin.Text.Text = Path.GetFileName(filepath);

            string dir = Path.GetDirectoryName(filepath)!;
            Directory.CreateDirectory(dir);

            Console.WriteLine($"Downloading {lib.downloads.artifact.url}");
            var response = await client.GetAsync(lib.downloads.artifact.url);
            response.EnsureSuccessStatusCode();

            byte[] content = await response.Content.ReadAsByteArrayAsync();

            await File.WriteAllBytesAsync(filepath, content);
        }

        loadingWin.Close();
    }
}