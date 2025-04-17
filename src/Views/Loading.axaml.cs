using System.Threading.Tasks;
using Avalonia.Controls;
using SukiUI.Controls;

namespace dev.craftengine.editor.Views;

public partial class Loading : SukiWindow
{
    private bool _shouldClose;
    private bool _shouldTimerRun = true;
    private int _timerMinutes;
    private int _timerSeconds;
    private string _title;

    public Loading(string title)
    {
        _title = title;
        InitializeComponent();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (!_shouldClose)
        {
            e.Cancel = true;
        }
    }

    public void SetTitle(string title)
    {
        _title = title;
    }

    public new void Close()
    {
        _shouldClose = true;
        _shouldTimerRun = false;
        base.Close();
    }

    public new async Task ShowDialog(Window owner)
    {
        TitlebarTimer();
        await base.ShowDialog(owner);
    }

    private async Task TitlebarTimer()
    {
        if (!_shouldTimerRun)
        {
            return;
        }

        await Task.Delay(1000);

        _timerSeconds++;

        if (_timerSeconds >= 60)
        {
            _timerSeconds = 0;
            _timerMinutes++;
        }

        Title = $"{_title} ({_timerMinutes}m{_timerSeconds:00}s)";

        await TitlebarTimer();
    }
}