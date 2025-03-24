using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace dev.craftengine.editor.Minecraft;

public class AssetDownloader
{
    static string APPDATA => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    static string BASE_CDN_URL => "https://cdn.craftuniverse.net/CraftEngine/editor/minecraft_assets/";
    static string INDEX_CDN_URL => $"{BASE_CDN_URL}index.json";
    static private string BASE_PATH => $"{APPDATA}/CraftEngine/minecraft-assets";

    public async static Task DownloadIndex(ProgressBar progressBar)
    {
        if (Design.IsDesignMode)
        {
            Console.WriteLine("Asset can not be downloaded in the design mode.");

            return;
        }

        var client = new HttpClient();
        var response = client.GetAsync(INDEX_CDN_URL);

        response.Result.EnsureSuccessStatusCode();

        string responseContent = await response.Result.Content.ReadAsStringAsync();

        var indexJsonStr = $"{{\"data\":{responseContent}}}";
        var indexJson = JsonSerializer.Deserialize<IndexFileHead>(indexJsonStr)!.data;

        Directory.CreateDirectory(BASE_PATH);
        File.WriteAllText(Path.Combine(BASE_PATH, "index.json"), indexJsonStr);

        double index = 0;

        foreach (var asset in indexJson)
        {
            var downloadURL = new StringBuilder();

            downloadURL.Append(BASE_CDN_URL);
            downloadURL.Append(asset.hash[..2]);
            downloadURL.Append("/" + asset.hash);

            var assetResponse = client.GetAsync(downloadURL.ToString());
            var assetResult = assetResponse.Result.EnsureSuccessStatusCode();
            var byteArray = assetResult.Content.ReadAsByteArrayAsync();

            string filePath = Path.Combine(BASE_PATH, asset.hash[..2], asset.hash);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? "");
            File.WriteAllBytes(filePath, byteArray.Result);

            index++;

            // Calculates the Percentage based on how much is downloaded
            progressBar.Value = index / indexJson.Count * 100.0;
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