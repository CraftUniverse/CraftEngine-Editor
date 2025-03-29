using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace dev.craftengine.editor.Views;

public partial class MessageBox : Window
{
    public MessageBox(string title, string message, List<Button> buttons)
    {
        InitializeComponent();

        Title = title;
        Text.Text = message;

        foreach (var button in buttons)
        {
            switch (button)
            {
                case Button.YES: YesButton.IsVisible = true; break;
                case Button.NO: NoButton.IsVisible = true; break;
                case Button.OK: OkButton.IsVisible = true; break;
                case Button.CANCEL: CancelButton.IsVisible = true; break;
            }
        }
    }

    private Button _returnButton;

    public new async Task<Button> ShowDialog(Window owner)
    {
        YesButton.Click += (sender, args) =>
        {
            _returnButton = Button.YES;
            Close();
        };

        NoButton.Click += (sender, args) =>
        {
            _returnButton = Button.NO;
            Close();
        };

        OkButton.Click += (sender, args) =>
        {
            _returnButton = Button.OK;
            Close();
        };

        CancelButton.Click += (sender, args) =>
        {
            _returnButton = Button.CANCEL;
            Close();
        };

        await base.ShowDialog(owner);

        return _returnButton;
    }

    public enum Button
    {
        OK,
        CANCEL,
        YES,
        NO
    }
}