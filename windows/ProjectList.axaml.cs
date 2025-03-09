using Avalonia.Interactivity;
using Eremex.AvaloniaUI.Controls.Common;

namespace dev.craftengine.editor.windows;

public partial class ProjectList : MxWindow
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