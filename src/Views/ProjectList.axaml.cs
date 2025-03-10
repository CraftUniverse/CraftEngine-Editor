using Avalonia.Controls;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class ProjectList : Window
{
    public ProjectList()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version;
    }

    private void OpenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        new SplashScreen().Show();
        Close();
    }

    private void CreateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var newProject = new NewProject();

        newProject.ShowDialog(this);
    }
}