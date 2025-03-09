using Avalonia.Controls;
using Avalonia.Interactivity;

namespace dev.craftengine.editor.Views;

public partial class ProjectList : Window
{
    public ProjectList()
    {
        InitializeComponent();
    }

    private void OpenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var splashscreen = new SplashScreen();

        splashscreen.Show();
        Close();
    }
}