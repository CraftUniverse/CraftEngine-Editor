using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Utf8Json;

namespace dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;

public class VersionMetadata
{
    public static async Task<Metadata> Download(string version)
    {
        string url = Constants.METADATA_URL.Replace("{version}", version);
        string path = Path.Combine(Constants.BASE_PATH, "versions", version);
        string filePath = Path.Combine(path, version + ".json");
        Metadata metadata = new();

        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(path);

            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                await File.WriteAllTextAsync(filePath, content);

                metadata = JsonSerializer.Deserialize<Metadata>(content)!;
            }
            catch (HttpRequestException e)
            {
                await Console.Error.WriteLineAsync(e.Message);
            }
        }
        else
        {
            string content = await File.ReadAllTextAsync(filePath);
            metadata = JsonSerializer.Deserialize<Metadata>(content)!;
        }

        return metadata;
    }
}