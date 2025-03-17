using Avalonia;
using Avalonia.Media;
using AvaloniaColorPicker;

namespace dev.craftengine.editor.Views;

public class ColorPicker : ColorPickerWindow
{
    private void SetProperties()
    {
        IsColourBlindnessSelectorVisible = false;
        IsHSBVisible = false;
        IsPaletteVisible = false;
        IsColourSpacePreviewVisible = false;
        IsAlphaVisible = false;
        IsCIELABVisible = false;
        IsColourSpaceSelectorVisible = false;

#if DEBUG
        this.AttachDevTools();
#endif
    }

    public ColorPicker()
    {
        SetProperties();
    }

    public ColorPicker(Color? previousColour) : base(previousColour)
    {
        SetProperties();
    }
}