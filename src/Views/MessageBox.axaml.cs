using Avalonia.Controls;

namespace dev.craftengine.editor.Views;

public partial class MessageBox : Window
{
    public MessageBox(string title, string message)
    {
        InitializeComponent();

        Title = title;
        Text.Text = message;
    }
}