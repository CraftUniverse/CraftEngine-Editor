using Avalonia.Controls;
using Avalonia.Input;
using dev.craftengine.editor.Helpers;

namespace dev.craftengine.editor.Views;

public partial class About : Window
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
}