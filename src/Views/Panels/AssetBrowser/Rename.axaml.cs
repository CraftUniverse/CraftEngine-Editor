using System.IO;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace dev.craftengine.editor.Views.Panels.AssetBrowser;

public partial class Rename : Window
{
    private string _oldName;

    public Rename(string oldName)
    {
        _oldName = oldName;

        InitializeComponent();
        TextBox.Text = oldName;
    }

    private void TextBox_OnLoaded(object? sender, RoutedEventArgs e)
    {
        TextBox.Focus(NavigationMethod.Tab);
        TextBox.SelectionEnd = _oldName.IndexOf(Path.GetExtension(_oldName));
    }

    private void Button_Cancel(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private async void Button_Okay(object? sender, RoutedEventArgs e)
    {
        string oldExt = Path.GetExtension(_oldName);
        string ext = Path.GetExtension(TextBox.Text)!;

        if (oldExt != ext)
        {
            var msgBox = new MessageBox("Are you sure?", "Are you sure to change the File Extension?");
            await msgBox.ShowDialog(this);
        }

        Close();
    }
}