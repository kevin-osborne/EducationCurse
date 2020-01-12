using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using EducationCurse.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Tiled.Graphics;

namespace EducationCurse.Screens
{
    public class IsometricScreen : Screen
    {
        private readonly ContentManager _contentManager;
        private readonly GameMain _mainGame;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureAtlas;
        private Texture2D _background;
        private Texture2D _caveEntrance;
        private BitmapFont _bitmapFont;
        private Sprite _player;
        private const int _playerSpeed = 64;
        private const int _minX = 25;
        private const int _maxX = 245;
        private const int _minY = 45;
        private const int _maxY = 240;
        private Camera2D _camera;
        // The tile map
        private TiledMap map;
        // The renderer for the map
        private TiledMapRenderer mapRenderer;
        public IsometricScreen(GameMain mainGame, ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
            _contentManager = contentManager;
            _graphicsDeviceManager = graphicsDeviceManager;
            _mainGame = mainGame;
        }

        public override void Initialize()
        {
            // Load the compiled map
            map = _contentManager.Load<TiledMap>("isometric");
            // Create the map renderer
            mapRenderer = new TiledMapRenderer(_contentManager.GetGraphicsDevice());

            var viewportAdapter = new BoxingViewportAdapter(_mainGame.Window, _contentManager.GetGraphicsDevice(), 1200, 800);
            _camera = new Camera2D(viewportAdapter);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _graphicsDevice = _contentManager.GetGraphicsDevice();
            _spriteBatch = new SpriteBatch(_graphicsDevice);

        }

        public override void Update(GameTime gameTime)
        {
            if (IsVisible)
            {
                // Update the map
                // map Should be the `TiledMap`
                mapRenderer.Update(map, gameTime);

                base.Update(gameTime);
            }
            base.Update(gameTime);
        }



        public override void Draw(GameTime gameTime)
        {
            if(IsVisible)
            {
                // resize to phone screen size
                //_graphicsDeviceManager.PreferredBackBufferWidth = 270;
                //_graphicsDeviceManager.PreferredBackBufferHeight = 540;
                _graphicsDeviceManager.PreferredBackBufferWidth = 1200;
                _graphicsDeviceManager.PreferredBackBufferHeight = 800;
                _graphicsDeviceManager.ApplyChanges();


                // Clear the screen
                _contentManager.GetGraphicsDevice().Clear(Color.Pink);

                // Transform matrix is only needed if you have a Camera2D
                // Setting the sampler state to `SamplerState.PointClamp` is reccomended to remove gaps between the tiles when rendering
                _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

                // map Should be the `TiledMap`
                // Once again, the transform matrix is only needed if you have a Camera2D
                mapRenderer.Draw(map, _camera.GetViewMatrix());

                // End the sprite batch
                _spriteBatch.End();

                base.Draw(gameTime);

            }

            base.Draw(gameTime);
        }
    }
}
