﻿using System;
using Avalonia.Controls;
using Avalonia.Platform;

namespace dev.craftengine.editor.GameViewer;

public class ControlHost : NativeControlHost
{
    public IntPtr Handle { get; private set; }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        var handle = base.CreateNativeControlCore(parent);
        Handle = handle.Handle;

        return handle;
    }
}