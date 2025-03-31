using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;
using dev.craftengine.editor.Minecraft.ClientLauncher;
using dev.craftengine.editor.Views.Panels;
using dev.craftengine.editor.Views.Panels.AssetBrowser;

namespace dev.craftengine.editor.Views;

public partial class Editor : Window
{
    public Editor()
    {
        InitializeComponent();

        Title =
            $"CraftEngine Editor {VersionControl.Version} - C:/Users/USER/Documents/CraftEngine Projects/Test Project";

        PanelLayout1.Children.Add(new Hierarchy());

        if (!Design.IsDesignMode)
        {
            PanelLayout2.Children.Clear();
            PanelLayout2.Children.Add(new Panels.GameViewer());
        }

        PanelLayout3.Children.Add(new AssetBrowser());
        PanelLayout4.Children.Add(new Properties());
    }

    private void Menu_File_Export_OnClick(object? sender, RoutedEventArgs e)
    {
        var win = new BuildProject();
        win.ShowDialog(this);
    }

    private void Menu_File_Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Menu_Edit_Preferences_OnClick(object? sender, RoutedEventArgs e)
    {
        var prefWin = new Preferences();
        prefWin.Show(this);
    }

    private void PlayOfflineButton(object? sender, RoutedEventArgs e)
    {
        ClientLauncher.Launch("1.21.4");
    }

    private void Menu_Help_About_OnClick(object? sender, RoutedEventArgs e)
    {
        var win = new About();
        win.ShowDialog(this);
    }

    private void Menu_Help_Wiki_OnClick(object? sender, RoutedEventArgs e)
    {
        Launcher.LaunchUriAsync(new Uri("https://wiki.craftengine.dev"));
    }
}