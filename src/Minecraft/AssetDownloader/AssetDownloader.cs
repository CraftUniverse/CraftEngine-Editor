﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Controls;

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
            infoText.Text = Resources.Resources.startup_minecraft_asset_index;

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
            infoText.Text = Resources.Resources.startup_minecraft_asset_index_failed;
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
            if (!asset.Versions.Contains(minecraftVersion))
            {
                continue;
            }

            var estTime = 0;
            var startTime = DateTime.Now;

            var addDelay = false;
            string filePath = Path.Combine(Constants.BASE_PATH, asset.Hash[..2], asset.Hash);

            if (!File.Exists(filePath))
            {
                var downloadUrl = new StringBuilder();

                // Creating URL for download
                downloadUrl.Append(Constants.BASE_CDN_URL);
                downloadUrl.Append(asset.Hash[..2]);
                downloadUrl.Append("/" + asset.Hash);

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
                    await Console.Error.WriteLineAsync($"Failed to download asset \"{asset.Hash}\": {e.Message}");
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

            progressBar.ProgressTextFormat = $"{{1:0}}% (ca. {estTime / 60} {Resources.Resources.startup_minutes})";

            // Calculates the Percentage based on how much is downloaded
            progressBar.Value = index / amount * 100.0;

            infoText.Text =
                $"{Resources.Resources.startup_minecraft_asset_download_asset.Replace("{asset}", asset.Hash)} ({index}/{amount})";

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
        [JsonPropertyName("hash")] public required string Hash { get; set; }
        [JsonPropertyName("path")] public required string Path { get; set; }
        [JsonPropertyName("length")] public required int Length { get; set; }
        [JsonPropertyName("versions")] public required List<string> Versions { get; set; }
    }
}