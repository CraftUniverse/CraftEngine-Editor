using System;

namespace dev.craftengine.editor.Minecraft.AssetDownloader;

public class Constants
{
    public static string APPDATA => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public const string BASE_CDN_URL = "https://cdn.craftuniverse.net/CraftEngine/editor/minecraft_assets/";
    public const string INDEX_CDN_URL = $"{BASE_CDN_URL}index.json";
    public static string BASE_PATH => $"{APPDATA}/CraftEngine/minecraft-assets";
}