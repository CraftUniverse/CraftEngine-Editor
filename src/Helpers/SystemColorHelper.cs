using System.Runtime.InteropServices;
using Avalonia.Media;
using Microsoft.Win32;

// macOS specifics
#if OSX
using AppKit;
#endif

// Linux specific
#if LINUX
using System.Diagnostics;
#endif

namespace dev.craftengine.editor.Helpers;

public static class SystemColorHelper
{
    public static Color GetSystemAccentColor()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetWindowsAccentColor();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return GetMacOSAccentColor();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return GetLinuxAccentColor();
        }

        return Colors.Transparent; // Default fallback if no platform is detected
    }

    // --- WINDOWS METHOD ---

    private static Color GetWindowsAccentColor()
    {
        try
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM");
            if (key?.GetValue("AccentColor") is int accentColor)
            {
                return Color.FromRgb(
                    (byte)(accentColor >> 16 & 0xFF), // Red
                    (byte)(accentColor >> 8 & 0xFF), // Green
                    (byte)(accentColor & 0xFF) // Blue
                );
            }
        }
        catch
        {
            return Colors.Blue; // Fallback color
        }

        return Colors.Blue; // Fallback color
    }

    // --- MACOS METHOD ---

    private static Color GetMacOSAccentColor()
    {
#if OSX
        try
        {
            var accentColor = NSColor.ControlAccentColor.UsingColorSpace(NSColorSpace.DeviceRGB);
            return Color.FromRgb(
                (byte)(accentColor.RedComponent * 255),
                (byte)(accentColor.GreenComponent * 255),
                (byte)(accentColor.BlueComponent * 255));
        }
        catch
        {
            return Colors.Blue; // Fallback color
        }
#else
        return Colors.Blue; // Fallback if macOS code is not available
#endif
    }

    // --- LINUX METHOD ---

    private static Color GetLinuxAccentColor()
    {
#if LINUX
        try
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "gsettings",
                Arguments = "get org.gnome.desktop.interface gtk-theme",
                RedirectStandardOutput = true,
                UseShellExecute = false
            });

            string themeName = process.StandardOutput.ReadToEnd().Trim().Replace("'", "");

            return themeName switch
            {
                "Adwaita-dark" => Colors.DarkGray,
                "Adwaita" => Colors.LightGray,
                _ => Colors.Blue // Fallback color
            };
        }
        catch
        {
            return Colors.Blue; // Fallback color
        }
#else
        return Colors.Blue;
#endif
    }
}