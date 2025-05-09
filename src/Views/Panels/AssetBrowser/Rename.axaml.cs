﻿using System;
using System.IO;
using Avalonia.Input;
using Avalonia.Interactivity;
using SukiUI.Controls;

namespace dev.craftengine.editor.Views.Panels.AssetBrowser;

public partial class Rename : SukiWindow
{
    private readonly string _oldName;

    public Rename(string oldName)
    {
        _oldName = oldName;

        InitializeComponent();
        TextBox.Text = oldName;
    }

    private void TextBox_OnLoaded(object? sender, RoutedEventArgs e)
    {
        TextBox.Focus(NavigationMethod.Tab);

        TextBox.SelectionEnd = _oldName.IndexOf(
            Path.GetExtension(_oldName),
            StringComparison.Ordinal
        );
    }

    private void Button_Cancel(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    // ReSharper disable once AsyncVoidMethod
    private async void Button_Okay(object? sender, RoutedEventArgs e)
    {
        string oldExt = Path.GetExtension(_oldName);
        string ext = Path.GetExtension(TextBox.Text)!;

        if (oldExt != ext)
        {
            var msgBox = new MessageBox(
                editor.Resources.Resources.rename_quest_file_ext_title,
                editor.Resources.Resources.rename_quest_file_ext_message,
                MessageBox.Button.YES | MessageBox.Button.NO,
                MessageBox.Icon.QUESTION
            );

            if (await msgBox.ShowDialog(this) == MessageBox.Button.NO)
            {
                return;
            }
        }

        Close();
    }
}