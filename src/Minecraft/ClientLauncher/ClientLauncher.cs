using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using dev.craftengine.editor.Views;
using FluentAvalonia.Core;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace dev.craftengine.editor.Minecraft.ClientLauncher;

public class ClientLauncher
{
    public static Process? MinecraftProcess;

    public static async Task Launch(string version, Editor editorWindow)
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        MinecraftProcess?.Kill();

        var loadingWin = new Loading(
            Resources.Resources.client_launcher_launch_title
                .Replace("{version}", version)
        );

        loadingWin.ShowDialog(editorWindow);

        editorWindow.PlayOfflineButton.BorderBrush = Brushes.ForestGreen;

        CreateFolderStructure();

        var metadata = await VersionMetadata.VersionMetadata.Download(version);

        await VersionDownload.Download(metadata, loadingWin);
        await JavaDownload.Download(metadata, loadingWin);
        await LibrariesDownload.Download(metadata, loadingWin);
        await AssetDownload.Download(metadata, loadingWin);

        string jrePath = Path.Combine(
            Constants.BASE_PATH,
            "java",
            "java_" + metadata.javaVersion.majorVersion,
            "bin",
            "javaw" + (OperatingSystem.IsWindows() ? ".exe" : "")
        );

        List<string> classPath = [];
        List<string> jvmArgs = [];

        foreach (var lib in metadata.libraries)
        {
            if (lib.rules != null)
            {
                var shouldContinue = true;

                foreach (var rule in lib.rules)
                {
                    switch (rule.os.name)
                    {
                        case "linux" when OperatingSystem.IsLinux() && rule.action == "allow":
                        case "windows" when OperatingSystem.IsWindows() && rule.action == "allow":
                        case "osx" when OperatingSystem.IsMacOS() && rule.action == "allow":
                            shouldContinue = false;

                            break;
                    }
                }

                if (shouldContinue)
                {
                    continue;
                }
            }

            classPath.Add(Path.Combine(Constants.BASE_PATH, "libraries", lib.downloads.artifact.path));
        }

        classPath.Add(Path.Combine(Constants.BASE_PATH, "versions", version, version + ".jar"));

        foreach (object vmArg in metadata.arguments.jvm)
        {
            if (vmArg is string arg)
            {
                string finalArg = arg
                    .Replace("${natives_directory}", Path.Combine(Constants.BASE_PATH, "natives"))
                    .Replace("${classpath}", string.Join(OperatingSystem.IsWindows() ? ";" : ":", classPath))
                    .Replace("${launcher_name}", "CraftEngine-Editor")
                    .Replace("${launcher_version}", "6942");

                jvmArgs.Add(finalArg);
            }
            else
            {
                var ruleArg = vmArg as Dictionary<string, object>;
                var rule = (ruleArg!["rules"] as List<object>)![0] as Dictionary<string, object>;
                var os = rule!.Values.ElementAt(1) as Dictionary<string, object>;
                var osValue = os!.Values.ElementAt(0) as string;

                switch (osValue)
                {
                    case "osx" when !OperatingSystem.IsMacOS():
                    case "windows" when !OperatingSystem.IsWindows():
                    case "x86":
                        continue;
                    default:
                    {
                        string value = ruleArg["value"] switch
                        {
                            string strValue => strValue,
                            List<string> listValue => listValue[0],
                            _ => ""
                        };

                        jvmArgs.Add(value);

                        break;
                    }
                }
            }
        }

        jvmArgs.AddRange(
            [
                "-Xms6G",
                "-Xmx10G"
            ]
        );

        List<string> command =
        [
            ..jvmArgs,
            metadata.mainClass,
            "--version",
            version,
            "--assetsDir",
            Path.Combine(Constants.BASE_PATH, "assets"),
            "--assetIndex",
            metadata.assetIndex.id,
            "--username",
            "Player",
            "--uuid",
            "00000000-0000-0000-0000-000000000000",
            "--accessToken",
            "null",
            "--quickPlayMultiplayer",
            "localhost:35565",
            "--gameDir",
            Path.Combine(Constants.BASE_PATH, "profile"),
        ];

        var psi = new ProcessStartInfo
        {
            FileName = jrePath,
            Arguments = string.Join(" ", command),
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        MinecraftProcess = new Process
        {
            StartInfo = psi,
        };

        MinecraftProcess.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);

        MinecraftProcess.Start();
        MinecraftProcess.BeginOutputReadLine();

        await Task.Delay(8000);
        loadingWin.Close();

        await MinecraftProcess.WaitForExitAsync();
        MinecraftProcess = null;
        editorWindow.PlayOfflineButton.BorderBrush = Brushes.Transparent;
    }

    static private void CreateFolderStructure()
    {
        var flags =
            UnixFileMode.OtherExecute
            | UnixFileMode.GroupExecute
            | UnixFileMode.UserExecute;

        List<string> folders =
        [
            Path.Combine(Constants.BASE_PATH, "assets"),
            Path.Combine(Constants.BASE_PATH, "java"),
            Path.Combine(Constants.BASE_PATH, "libraries"),
            Path.Combine(Constants.BASE_PATH, "natives"),
            Path.Combine(Constants.BASE_PATH, "versions"),
            Path.Combine(Constants.BASE_PATH, "profile")
        ];

        foreach (string folder in folders)
        {
            if (Directory.Exists(folder))
            {
                continue;
            }

            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(folder);
            }
            else
            {
                Directory.CreateDirectory(folder, flags);
            }
        }
    }
}