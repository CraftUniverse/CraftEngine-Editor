using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;
using dev.craftengine.editor.Views;
using FluentAvalonia.Core;
using Utf8Json;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class AssetDownload
{
    public static async Task Download(Metadata metadata, Window parentWindow)
    {
        string objectsDir = Path.Combine(Constants.BASE_PATH, "assets", "objects");
        string indexDir = Path.Combine(Constants.BASE_PATH, "assets", "indexes");
        string indexPath = Path.Combine(indexDir, metadata.assetIndex.id + ".json");

        var loadingWin = new Loading("Downloading assets...");
        loadingWin.ShowDialog(parentWindow);

        if (!Directory.Exists(indexDir))
        {
            Directory.CreateDirectory(indexDir);
        }

        if (!Directory.Exists(objectsDir))
        {
            Directory.CreateDirectory(objectsDir);
        }

        var client = new HttpClient();

        Response? indexJson = null;

        if (!File.Exists(indexPath))
        {
            loadingWin.Text.IsVisible = true;
            loadingWin.Text.Text = metadata.assetIndex.url;

            try
            {
                var response = await client.GetAsync(metadata.assetIndex.url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                indexJson = JsonSerializer.Deserialize<Response>(content);

                await File.WriteAllTextAsync(indexPath, content);
            }
            catch (HttpRequestException e)
            {
                await Console.Error.WriteLineAsync(e.Message);
            }
        }
        else
        {
            string content = await File.ReadAllTextAsync(indexPath);
            indexJson = JsonSerializer.Deserialize<Response>(content);
        }

        var objectFiles = indexJson!.objects as Dictionary<string, object>;

        foreach (string key in objectFiles!.Keys)
        {
            var obj = objectFiles[key] as Dictionary<string, object>;
            var finalObject = obj!.Values.ElementAt(0) as string;
            string dir = Path.Combine(objectsDir, finalObject![..2]);
            var path = $"{finalObject![..2]}/{finalObject}";

            string filePath = Path.Combine(objectsDir, path);

            if (File.Exists(filePath))
            {
                continue;
            }

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            loadingWin.Text.IsVisible = true;
            loadingWin.Text.Text = Path.GetFileName($"{finalObject} ({Path.GetFileName(key)})");

            var response = await client.GetAsync($"{Constants.RESOURCES_URL}{path}");
            response.EnsureSuccessStatusCode();

            byte[] content = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(filePath, content);
        }

        loadingWin.Close();
    }

    // ReSharper disable once InconsistentNaming
    public class Response
    {
        public required object objects { get; init; }
    }
}