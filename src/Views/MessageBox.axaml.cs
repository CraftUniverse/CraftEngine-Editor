using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace dev.craftengine.editor.Views;

public partial class MessageBox : Window
{
    public MessageBox(string title, string message, List<Button> buttons, string? displayIcon = null)
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

    public enum Button
    {
        OK,
        CANCEL,
        YES,
        NO
    }

    public class Icon
    {
        public static string INFO => "info_regular";
        public static string QUESTION => "question_circle_regular";
    }
}