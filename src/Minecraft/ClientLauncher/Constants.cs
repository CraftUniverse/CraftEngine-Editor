using System;
using System.IO;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class Constants
{
    public static string APPDATA => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string BASE_PATH => Path.Combine(APPDATA, "CraftEngine", "minecraft");

    public const string METADATA_URL =
        "https://raw.githubusercontent.com/InventivetalentDev/minecraft-assets/refs/heads/{version}/{version}.json";

    public const string JAVA_URL =
        "https://api.azul.com/metadata/v1/zulu/packages/?java_version={version}&os={os}&arch={arch}&archive_type=zip&java_package_type=jre&distro_version={version}&release_status=ga&availability_types=CA&certifications=tck&page=1&page_size=1";
}