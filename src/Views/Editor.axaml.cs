using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;
using dev.craftengine.editor.Views.Panels;

namespace dev.craftengine.editor.Views;

public partial class Editor : Window
{
    public Editor()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version;

        var assetBrowser = new AssetBrowser("AssetBrowser");

        MainPanel.Children.Add(assetBrowser);
    }

    private void Menu_File_Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}