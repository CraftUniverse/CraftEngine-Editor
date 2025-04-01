using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using dev.craftengine.editor.Minecraft.ClientLauncher;
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
            desktop.Exit += OnExit!;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        // Kills the Minecraft Process if Running
        if (ClientLauncher.MinecraftProcess != null)
        {
            ClientLauncher.MinecraftProcess.Kill();
        }
    }
}