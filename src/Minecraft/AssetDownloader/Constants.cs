using System;

namespace dev.craftengine.editor.Minecraft.AssetDownloader;

public class Constants
{
    public static string APPDATA => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string BASE_CDN_URL => "https://cdn.craftuniverse.net/CraftEngine/editor/minecraft_assets/";
    public static string INDEX_CDN_URL => $"{BASE_CDN_URL}index.json";
    public static string BASE_PATH => $"{APPDATA}/CraftEngine/minecraft-assets";
}