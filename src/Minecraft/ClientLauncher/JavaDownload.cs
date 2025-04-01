using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Controls;
using dev.craftengine.editor.Minecraft.ClientLauncher.VersionMetadata;
using dev.craftengine.editor.Views;

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class JavaDownload
{
    public static async Task Download(Metadata metadata, Window parentWindow)
    {
        string finalPath = Path.Combine(Constants.BASE_PATH, "java", $"java_{metadata.javaVersion.majorVersion}");

        if (Directory.Exists(finalPath))
        {
            return;
        }

        var loadingWin = new Loading(
            Resources.Resources.client_launcher_download_java_title
                .Replace("{version}", metadata.javaVersion.majorVersion.ToString())
        );

        loadingWin.ShowDialog(parentWindow);

        string os;

        if (OperatingSystem.IsWindows())
        {
            os = "windows";
        }
        else if (OperatingSystem.IsLinux())
        {
            os = "linux";
        }
        else if (OperatingSystem.IsMacOS())
        {
            os = "macos";
        }
        else
        {
            throw new NotSupportedException("Unsupported Operating System");
        }

        string arch = RuntimeInformation.OSArchitecture switch
        {
            Architecture.Arm64 => "arm64",
            Architecture.X64 => "x64",
            _ => throw new NotSupportedException("Unsupported architecture")
        };

        string url = Constants.JAVA_URL
            .Replace("{version}", metadata.javaVersion.majorVersion.ToString())
            .Replace("{os}", os)
            .Replace("{arch}", arch);

        var client = new HttpClient();

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<List<Response>>(content)!;

            Console.WriteLine($"Downloading {json[0].DownloadUrl}");
            var executableResponse = await client.GetAsync(json[0].DownloadUrl);
            executableResponse.EnsureSuccessStatusCode();

            string downloadName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()) + ".zip";

            // TODO: Change to stream
            byte[] executableContent = await executableResponse.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(downloadName, executableContent);

            ZipFile.ExtractToDirectory(downloadName, Path.Combine(Constants.BASE_PATH, "java"));
            File.Delete(downloadName);

            Directory.Move(
                Path.Combine(Constants.BASE_PATH, "java", json[0].Name.Replace(".zip", "")),
                finalPath
            );
        }
        catch (HttpRequestException e)
        {
            await Console.Error.WriteLineAsync(e.Message);
        }

        loadingWin.Close();
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private class Response
    {
        [JsonPropertyName("availability_type")]
        public required string AvailabilityType { get; set; }

        [JsonPropertyName("distro_version")] public required List<int> DistroVersion { get; set; }
        [JsonPropertyName("download_url")] public required string DownloadUrl { get; set; }
        [JsonPropertyName("java_version")] public required List<int> JavaVersion { get; set; }
        [JsonPropertyName("latest")] public required bool Latest { get; set; }
        [JsonPropertyName("name")] public required string Name { get; set; }

        [JsonPropertyName("openjdk_build_number")]
        public required int OpenjdkBuildNumber { get; set; }

        [JsonPropertyName("package_uuid")] public required string PackageUuid { get; set; }
        [JsonPropertyName("product")] public required string Product { get; set; }
    }
}