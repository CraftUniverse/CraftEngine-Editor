using System;
using AvaloniaInside.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dev.craftengine.editor.GameViewer;

public class GameViewer : Game
{
    private GraphicsDeviceManager _graphics;
    private ResolutionRenderer _res;

    private int _lastWidth, _lastHeight;

    private BasicEffect _effect;
    private VertexBuffer _vertexBuffer;

    private Matrix _world;
    private Matrix _view;
    private Matrix _projection;

    public GameViewer()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
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

        _world = Matrix.Identity;
        _view = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);

        _projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4,
            _graphics.GraphicsDevice.Viewport.AspectRatio,
            0.1f,
            100f
        );

        base.Initialize();

        Console.WriteLine("Game Viewer Initialized");
    }

    protected override void Update(GameTime gameTime)
    {
        _world = Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds);

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

        var rotation = (float)gameTime.TotalGameTime.TotalSeconds;
        _effect.World = Matrix.CreateRotationY(rotation);

        GraphicsDevice.SetVertexBuffer(_vertexBuffer);

        foreach (var pass in _effect.CurrentTechnique.Passes)
        {
            pass.Apply();
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
        }

        _res.End();
        base.Draw(gameTime);
    }

    protected override void LoadContent()
    {
        _effect = new BasicEffect(GraphicsDevice)
        {
            VertexColorEnabled = true,
            LightingEnabled = false,
            World = _world,
            View = _view,
            Projection = _projection
        };

        var color = Color.Purple;

        var cubeVertices = new[]
        {
            // Front
            new VertexPositionColor(new Vector3(-1, -1, -1), color), //
            new VertexPositionColor(new Vector3(-1, 1, -1), color), //
            new VertexPositionColor(new Vector3(1, 1, -1), color), //
            new VertexPositionColor(new Vector3(-1, -1, -1), color), //
            new VertexPositionColor(new Vector3(1, 1, -1), color), //
            new VertexPositionColor(new Vector3(1, -1, -1), color), //

            // Back
            new VertexPositionColor(new Vector3(-1, -1, 1), color), //
            new VertexPositionColor(new Vector3(1, 1, 1), color), //
            new VertexPositionColor(new Vector3(-1, 1, 1), color), //
            new VertexPositionColor(new Vector3(-1, -1, 1), color), //
            new VertexPositionColor(new Vector3(1, -1, 1), color), //
            new VertexPositionColor(new Vector3(1, 1, 1), color), //

            // Left
            new VertexPositionColor(new Vector3(-1, -1, 1), color), //
            new VertexPositionColor(new Vector3(-1, 1, 1), color), //
            new VertexPositionColor(new Vector3(-1, 1, -1), color), //
            new VertexPositionColor(new Vector3(-1, -1, 1), color), //
            new VertexPositionColor(new Vector3(-1, 1, -1), color), //
            new VertexPositionColor(new Vector3(-1, -1, -1), color), //

            // Right
            new VertexPositionColor(new Vector3(1, -1, -1), color), //
            new VertexPositionColor(new Vector3(1, 1, -1), color), //
            new VertexPositionColor(new Vector3(1, 1, 1), color), //
            new VertexPositionColor(new Vector3(1, -1, -1), color), //
            new VertexPositionColor(new Vector3(1, 1, 1), color), //
            new VertexPositionColor(new Vector3(1, -1, 1), color), //

            // Top
            new VertexPositionColor(new Vector3(-1, 1, -1), color), //
            new VertexPositionColor(new Vector3(-1, 1, 1), color), //
            new VertexPositionColor(new Vector3(1, 1, 1), color), //
            new VertexPositionColor(new Vector3(-1, 1, -1), color), //
            new VertexPositionColor(new Vector3(1, 1, 1), color), //
            new VertexPositionColor(new Vector3(1, 1, -1), color), //

            // Bottom
            new VertexPositionColor(new Vector3(-1, -1, -1), color), //
            new VertexPositionColor(new Vector3(1, -1, 1), color), //
            new VertexPositionColor(new Vector3(-1, -1, 1), color), //
            new VertexPositionColor(new Vector3(-1, -1, -1), color), //
            new VertexPositionColor(new Vector3(1, -1, -1), color), //
            new VertexPositionColor(new Vector3(1, -1, 1), color), //
        };

        _vertexBuffer = new VertexBuffer(
            GraphicsDevice,
            typeof(VertexPositionColor),
            cubeVertices.Length,
            BufferUsage.WriteOnly
        );

        _vertexBuffer.SetData(cubeVertices);

        base.LoadContent();
    }
}