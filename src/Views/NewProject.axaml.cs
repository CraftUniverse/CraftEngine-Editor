using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class NewProject : Window
{
    public NewProject()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version + " - New Project";

        PathInput.Text = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "CraftEngine Projects"
        );
    }

    private void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void PathButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var options = new FolderPickerOpenOptions();
        options.AllowMultiple = false;

        var dialog = StorageProvider.OpenFolderPickerAsync(options);

        if (dialog.Result.Count != 1)
        {
            return;
        }

        string path = dialog.Result[0].Path.LocalPath;

        PathInput.Text = path;
    }
}