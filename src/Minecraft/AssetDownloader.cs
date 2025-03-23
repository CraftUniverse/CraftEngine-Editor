using System;
using System.Net.Http;

namespace dev.craftengine.editor.Minecraft;

public class AssetDownloader
{
    public static string INDEX_CDN_URL
        => "https://cdn.craftuniverse.net/CraftEngine/editor/minecraft_assets/index.json";

    public static void DownloadIndex()
    {
        var client = new HttpClient();
        var response = client.GetAsync(INDEX_CDN_URL);

        response.Result.EnsureSuccessStatusCode();

        var responseContent = response.Result.Content.ReadAsStringAsync();
        responseContent.Wait();

        Console.WriteLine(responseContent.Result);
    }
}