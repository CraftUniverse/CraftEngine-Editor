using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace dev.craftengine.editor.Minecraft;

public class AssetDownloader
{
    public async static Task<List<IndexFile>> ReadIndexFile()
    {
        string indexPath = Path.Combine(Constants.BASE_PATH, "index.json");

        if (File.Exists(indexPath))
        {
            return new List<IndexFile>();
        }

        string content = await File.ReadAllTextAsync(indexPath);
        var indexJson = JsonSerializer.Deserialize<IndexFileHead>(content)!.data;

        return indexJson;
    }

    private async static Task<string> GetFileHash(string path)
    {
        var md5 = MD5.Create();
        var stream = File.OpenRead(path);
        byte[] hash = await md5.ComputeHashAsync(stream);

        return Encoding.Default.GetString(hash);
    }

    public async static Task Download(ProgressBar progressBar, TextBlock infoText, string minecraftVersion)
    {
        if (Design.IsDesignMode)
        {
            Console.WriteLine("Asset can not be downloaded in the design mode.");

            return;
        }

        infoText.Text = "Downloading index.json";

        var client = new HttpClient();
        var response = await client.GetAsync(Constants.INDEX_CDN_URL);

        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();

        var indexJsonStr = $"{{\"data\":{responseContent}}}";
        var indexJson = JsonSerializer.Deserialize<IndexFileHead>(indexJsonStr)!.data;

        Directory.CreateDirectory(Constants.BASE_PATH);
        File.WriteAllText(Path.Combine(Constants.BASE_PATH, "index.json"), indexJsonStr);

        double index = 0;

        foreach (var asset in indexJson)
        {
            if (!asset.versions.Contains(minecraftVersion))
            {
                continue;
            }

            var downloadURL = new StringBuilder();

            downloadURL.Append(Constants.BASE_CDN_URL);
            downloadURL.Append(asset.hash[..2]);
            downloadURL.Append("/" + asset.hash);

            var assetResponse = client.GetAsync(downloadURL.ToString());
            var assetResult = assetResponse.Result.EnsureSuccessStatusCode();
            byte[] byteArray = await assetResult.Content.ReadAsByteArrayAsync();

            string filePath = Path.Combine(Constants.BASE_PATH, asset.hash[..2], asset.hash);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? "");
            File.WriteAllBytes(filePath, byteArray);

            index++;

            // Calculates the Percentage based on how much is downloaded
            progressBar.Value = index / indexJson.Count * 100.0;
            infoText.Text = $"Downloading {asset.hash} ({index}/{indexJson.Count})";

            // Need to be delayed 250ms, if not await will not work.
            // and before the CDN closes the connection due to DDOS protection
            await Task.Delay(250);
        }
    }

    public class IndexFileHead
    {
        public required List<IndexFile> data { get; set; }
    }

    public class IndexFile
    {
        public required string hash { get; set; }
        public required string path { get; set; }
        public required int length { get; set; }
        public required List<string> versions { get; set; }
    }
}