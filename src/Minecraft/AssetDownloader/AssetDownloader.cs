using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;

// ReSharper disable InconsistentNaming

namespace dev.craftengine.editor.Minecraft.AssetDownloader;

public abstract class AssetDownloader
{
    public static async Task Download(ProgressBar progressBar, TextBlock infoText, string minecraftVersion)
    {
        if (Design.IsDesignMode)
        {
            // ReSharper disable once LocalizableElement
            Console.WriteLine("Asset can not be downloaded in the design mode.");

            return;
        }

        progressBar.ProgressTextFormat = "{1:0}%";

        var client = new HttpClient();
        string indexPath = Path.Combine(Constants.BASE_PATH, "index.json");
        var indexJson = new List<IndexFile>();

        try
        {
            infoText.Text = "Downloading index.json";

            var response = await client.GetAsync(Constants.INDEX_CDN_URL);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            var indexJsonStr = $"{{\"data\":{responseContent}}}";

            indexJson = JsonSerializer.Deserialize<IndexFileHead>(indexJsonStr)!.data;

            Directory.CreateDirectory(Constants.BASE_PATH);
            await File.WriteAllTextAsync(indexPath, indexJsonStr);
        }
        catch (HttpRequestException e)
        {
            infoText.Text = "Failed to download index.json";
            await Console.Error.WriteLineAsync($"Failed to download index.json: {e.Message}");

            if (!File.Exists(indexPath))
            {
                return;
            }
        }

        double index = 0;
        double amount = indexJson.Count;
        var delayCount = 0;

        foreach (var asset in indexJson)
        {
            if (!asset.versions.Contains(minecraftVersion))
            {
                continue;
            }

            var estTime = 0;
            var startTime = DateTime.Now;

            var addDelay = false;
            string filePath = Path.Combine(Constants.BASE_PATH, asset.hash[..2], asset.hash);

            if (!File.Exists(filePath))
            {
                var downloadUrl = new StringBuilder();

                // Creating URL for download
                downloadUrl.Append(Constants.BASE_CDN_URL);
                downloadUrl.Append(asset.hash[..2]);
                downloadUrl.Append("/" + asset.hash);

                try
                {
                    var assetResponse = client.GetAsync(downloadUrl.ToString());
                    var assetResult = assetResponse.Result.EnsureSuccessStatusCode();
                    byte[] byteArray = await assetResult.Content.ReadAsByteArrayAsync();

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? "");
                    File.WriteAllBytesAsync(filePath, byteArray);
                }
                catch (HttpRequestException e)
                {
                    await Console.Error.WriteLineAsync($"Failed to download asset \"{asset.hash}\": {e.Message}");
                }

                delayCount++;
                addDelay = true;
                index++;

                var endTime = DateTime.Now;
                estTime = (int)((endTime - startTime).Milliseconds * (amount - index) / 1000);
            }
            else
            {
                amount--;
            }

            progressBar.ProgressTextFormat = "{1:0}% (ca. " + (estTime / 60) + " minutes)";

            // Calculates the Percentage based on how much is downloaded
            progressBar.Value = index / amount * 100.0;
            infoText.Text = $"Downloading {asset.hash} ({index}/{amount})";

            // Needs to be delayed 350ms, if not await will not work.
            // and before the CDN closes the connection due to DDOS protection
            if (addDelay && delayCount == 30)
            {
                await Task.Delay(1000);
                delayCount = 0;
            }
            else if (addDelay)
            {
                await Task.Delay(1);
            }
        }
    }

    public class IndexFileHead
    {
        public required List<IndexFile> data { get; init; }
    }

    public class IndexFile
    {
        public required string hash { get; set; }
        public required string path { get; set; }
        public required int length { get; set; }
        public required List<string> versions { get; set; }
    }
}