using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using dev.craftengine.editor.Firebase;
using dev.craftengine.editor.Helpers;
using SukiUI.Controls;

namespace dev.craftengine.editor.Views;

public partial class ProjectList : SukiWindow
{
    private readonly string _basePath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "CraftEngine Projects"
    );

    public ProjectList()
    {
        InitializeComponent();

        Title = "CraftEngine Editor " + VersionControl.Version;
    }

    private async void OpenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            await ProjectManagement.ProjectManagement.LoadProject(Path.Join(_basePath, "New Project"));

            new SplashScreen().Show();
            Close();
        }
        catch (Exception exp)
        {
            await Console.Error.WriteLineAsync(exp.Message);
        }
    }

    private void CreateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var newProject = new NewProject();
        newProject.ShowDialog(this);
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var obj = sender as ListBox;

        if (obj!.SelectedItems!.Count == 0)
        {
            return;
        }

        obj!.SelectedItems?.Clear();
    }

    private void TextBlock_OnLoaded(object? sender, RoutedEventArgs e)
    {
        var textBox = sender as TextBlock;

        textBox!.Foreground = ActualThemeVariant == ThemeVariant.Dark ? Brushes.White : Brushes.Black;
    }

    private void OpenProject_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        if (button.Parent!.Parent is not ListBoxItem btnParent) return;
    }

    private void Auth_OnClick(object? sender, RoutedEventArgs e)
    {
        var btn = sender as Button;

        Authentification.Authenticate(btn!);
    }

    private void Profile_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var border = sender as Border;

        if (border!.ContextMenu!.IsOpen)
        {
            border.ContextMenu.Close();
        }
        else
        {
            border.ContextMenu.Open();
        }
    }
}