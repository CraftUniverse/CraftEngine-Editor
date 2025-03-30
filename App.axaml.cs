using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using dev.craftengine.editor.Views;

namespace dev.craftengine.editor;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        editor.Resources.Resources.Culture = new CultureInfo("en-US");

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new ProjectList();
        }

        base.OnFrameworkInitializationCompleted();
    }
}