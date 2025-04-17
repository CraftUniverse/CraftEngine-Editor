using System;
using System.IO;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;
using SukiUI.Controls;

namespace dev.craftengine.editor.Views;

public partial class ProjectList : SukiWindow
{
    private readonly string _basePath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "CraftEngine Projects"
    );

    public ProjectList()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version;
    }

    private async void OpenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            await ProjectManagement.ProjectManagement.LoadProject(Path.Join(_basePath, "New Project"));

            new SplashScreen().Show();
            Close();
        }
        catch (Exception exp)
        {
            await Console.Error.WriteLineAsync(exp.Message);
        }
    }

    private void CreateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var newProject = new NewProject();
        newProject.ShowDialog(this);
    }
}