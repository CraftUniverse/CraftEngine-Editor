using AvaloniaInside.MonoGame;
using Microsoft.Xna.Framework;

namespace dev.craftengine.editor.GameViewer;

public class GameViewer : Game
{
    private GraphicsDeviceManager _graphics;
    private ResolutionRenderer _res;
    private int _lastWidth, _lastHeight;

    public GameViewer()
    {
        _graphics = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        _lastWidth = GraphicsDevice.Viewport.Width;
        _lastHeight = GraphicsDevice.Viewport.Height;

        _res = new ResolutionRenderer(
            new Point(
                GraphicsDevice.Adapter.CurrentDisplayMode.Width,
                GraphicsDevice.Adapter.CurrentDisplayMode.Height
            ),
            GraphicsDevice
        )
        {
            ScreenResolution = new Point(_lastWidth, _lastHeight), Method = ResizeMethod.Fill
        };

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (_lastWidth != GraphicsDevice.Viewport.Width ||
            _lastHeight != GraphicsDevice.Viewport.Height)
        {
            _lastWidth = GraphicsDevice.Viewport.Width;
            _lastHeight = GraphicsDevice.Viewport.Height;

            _res.ScreenResolution = new Point(_lastWidth, _lastHeight);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _res.Begin();
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _res.End();

        base.Draw(gameTime);
    }
}