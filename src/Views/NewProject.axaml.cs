using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using SukiUI.Controls;

namespace dev.craftengine.editor.Views;

public partial class NewProject : SukiWindow
{
    private readonly string _basePath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "CraftEngine Projects"
    );

    public NewProject()
    {
        InitializeComponent();

        PathInput.Text = _basePath;
    }

    private void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private async void PathButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var baseFolder = await StorageProvider.TryGetFolderFromPathAsync(_basePath);

            var options = new FolderPickerOpenOptions
            {
                AllowMultiple = false, SuggestedStartLocation = baseFolder, Title = "Select a folder"
            };

            var dialog = await StorageProvider.OpenFolderPickerAsync(options);

            if (dialog.Count != 1)
            {
                return;
            }

            string path = dialog[0].Path.LocalPath;

            PathInput.Text = path;
        }
        catch (Exception exp)
        {
            await Console.Error.WriteLineAsync(exp.Message);
        }
    }

    private void CreateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedVersion = ProjectVersion.SelectedItem as ComboBoxItem;
        var ownerWindow = Owner as SukiWindow;

        ProjectManagement.ProjectManagement.CreateNewProject(
            ProjectName.Text!,
            selectedVersion!.Content!.ToString()!,
            PathInput.Text!
        );

        new SplashScreen().Show();
        Close();
        ownerWindow!.Close();
    }
}