using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class VersionDownload
{
    public static async Task Download(string url, string version)
    {
        string path = Path.Combine(Constants.BASE_PATH, "versions", version);
        string filePath = Path.Combine(path, version + ".jar");

        if (File.Exists(filePath))
        {
            Console.WriteLine($"Minecraft {version} is already downloaded, skipping download.");

            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            byte[] executableContent = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(filePath, executableContent);
        }
        catch (HttpRequestException e)
        {
            await Console.Error.WriteLineAsync(e.Message);
        }
    }
}