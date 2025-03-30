using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace dev.craftengine.editor.Views;

public partial class MessageBox : Window
{
    public MessageBox(string title, string message, Button buttons, string? displayIcon = null)
    {
        InitializeComponent();

        Title = title;
        Text.Text = message;

        YesButton.IsVisible = (buttons & Button.YES) == Button.YES;
        NoButton.IsVisible = (buttons & Button.NO) == Button.NO;
        OkButton.IsVisible = (buttons & Button.OK) == Button.OK;
        CancelButton.IsVisible = (buttons & Button.CANCEL) == Button.CANCEL;

        if (displayIcon == null)
        {
            return;
        }

        Application.Current!.Resources.TryGetResource(displayIcon, ThemeVariant.Default, out object? icon);
        PathIcon.Data = (StreamGeometry)icon!;
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

    [Flags]
    public enum Button
    {
        OK = 1,
        CANCEL,
        YES,
        NO
    }

    public class Icon
    {
        public const string INFO = "info_regular";
        public const string QUESTION = "question_circle_regular";
    }
}