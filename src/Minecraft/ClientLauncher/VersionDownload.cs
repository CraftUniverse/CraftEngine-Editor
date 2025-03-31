using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class VersionDownload
{
    public static async Task Download(Metadata metadata)
    {
        string path = Path.Combine(Constants.BASE_PATH, "versions", metadata.id);
        string filePath = Path.Combine(path, metadata.id + ".jar");

        if (File.Exists(filePath))
        {
            return;
        }

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
    }
}