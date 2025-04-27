using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace dev.craftengine.editor.Views.Modules;

public partial class AccountIcon : UserControl
{
    public AccountIcon()
    {
        InitializeComponent();
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var border = sender as Border;

        if (border!.ContextMenu!.IsOpen)
        {
            border.ContextMenu.Close();
        }
        else
        {
            border.ContextMenu.Open();
        }
    }

    private void Context_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (sender is not ContextMenu context) return;

        foreach (Control? child in context.Items)
        {
            if (child!.GetType() != typeof(MenuItem)) continue;

            if (child is not MenuItem item) return;

            if (item.GroupName == "LOGGED_IN")
            {
                item.IsVisible = false;
            }
        }
    }
}