using Avalonia.Controls;
using Avalonia.Input;

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
}