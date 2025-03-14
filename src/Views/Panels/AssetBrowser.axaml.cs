using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace dev.craftengine.editor.Views.Panels;

public partial class AssetBrowser : UserControl
{
    public AssetBrowser()
    {
        InitializeComponent();
    }

    private void Item_Click(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint((Visual?)sender).Properties.IsLeftButtonPressed) return;
    }
}