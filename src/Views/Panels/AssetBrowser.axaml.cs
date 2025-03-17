using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using dev.craftengine.editor.ViewModels;

namespace dev.craftengine.editor.Views.Panels;

public partial class AssetBrowser : UserControl
{
    private readonly List<Border> _borders;

    public AssetBrowser()
    {
        InitializeComponent();

        _borders = new List<Border>();
        DataContext = new AssetBrowserModel();
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as AssetBrowserModel;
        var children = new ObservableCollection<AssetBrowserModel.Entry>();

        for (var x = 1; x <= 5; x++)
        {
            children.Add(new AssetBrowserModel.Entry($"test_{x}", $"@/test/test_{x}"));
        }

        vm!.Directories.Add(new AssetBrowserModel.Entry("test", "@/test/test", children));
        vm!.Directories.Add(new AssetBrowserModel.Entry("Test_22", "@/test/Test_22"));

        var rand = new Random();

        for (var x = 0; x < 5; x++)
        {
            int num = rand.Next(1000000, 9999999);
            vm!.Files.Add(new AssetBrowserModel.Entry($"test{num}.txt", $"@/test/test{num}.txt"));
        }
    }

    private void ItemClick(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint((Visual?)sender).Properties.IsRightButtonPressed)
        {
            return;
        }

        bool shiftDown = e.KeyModifiers == KeyModifiers.Shift;

        if (!shiftDown)
        {
            ClearBorderList();
        }

        Application.Current!.Styles
            .TryGetResource("SystemAccentColor", null, out var accentColor);

        var border = (Border)sender!;
        _borders.Add(border);

        border.Background = new SolidColorBrush((Color)accentColor!);

        if (e.ClickCount >= 2)
        {
            Console.WriteLine($"OPEN FILE, {border.Name!.Replace("border_", "")}");
        }
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var presenters = FileContentHolder.ItemsPanelRoot!.Children;

        foreach (ContentPresenter child in presenters)
        {
            if (child.Child!.IsPointerOver)
            {
                return;
            }
        }

        ClearBorderList();
    }

    private void ClearBorderList()
    {
        foreach (var board in _borders)
        {
            board.Background = new SolidColorBrush(Colors.Gray);
        }

        _borders.Clear();
    }
}