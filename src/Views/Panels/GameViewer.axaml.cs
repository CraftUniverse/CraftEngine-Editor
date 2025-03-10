using Avalonia.Controls;

namespace dev.craftengine.editor.Views.Panels;

public partial class GameViewer : UserControl
{
    public GameViewer(string name)
    {
        Name = name;

        InitializeComponent();
    }
}