using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using dev.craftengine.editor.Helpers;
using SukiUI.Controls;

namespace dev.craftengine.editor.Views;

public partial class About : SukiWindow
{
    public About()
    {
        InitializeComponent();

        VersionText.Text = VersionControl.Version;
        KeyDownEvent.AddClassHandler<TopLevel>(OnKeyDown, handledEventsToo: true);
    }

    private void OnKeyDown(TopLevel topLevel, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Close();
        }
    }

    private void Github_OnClick(object? sender, RoutedEventArgs e)
    {
        Launcher.LaunchUriAsync(new Uri("https://github.com/CraftUniverse/CraftEngine-Editor"));
    }
}