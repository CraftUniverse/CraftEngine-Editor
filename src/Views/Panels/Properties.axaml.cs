﻿using Avalonia.Controls;

namespace dev.craftengine.editor.Views.Panels;

public partial class Properties : UserControl
{
    public Properties(string name)
    {
        Name = name;

        InitializeComponent();
    }
}