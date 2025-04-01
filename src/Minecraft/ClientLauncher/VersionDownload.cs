using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;
using dev.craftengine.editor.Views;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class VersionDownload
{
    public static async Task Download(Metadata metadata, Window parentWindow)
    {
        string path = Path.Combine(Constants.BASE_PATH, "versions", metadata.id);
        string filePath = Path.Combine(path, metadata.id + ".jar");

        if (File.Exists(filePath))
        {
            return;
        }

        var loadingWin = new Loading(Resources.Resources.client_launcher_download_version_title);
        loadingWin.ShowDialog(parentWindow);

        var client = new HttpClient();

        try
        {
            Console.WriteLine($"Downloading {metadata.downloads.client.url}");
            var response = await client.GetAsync(metadata.downloads.client.url);
            response.EnsureSuccessStatusCode();

            byte[] content = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(filePath, content);
        }
        catch (HttpRequestException e)
        {
            await Console.Error.WriteLineAsync(e.Message);
        }

        loadingWin.Close();
    }
}