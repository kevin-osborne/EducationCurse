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

namespace EducationCurse.Screens
{
    public class FieldScreen : Screen
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
        Rectangle _treasureRectangle1;
        Rectangle _treasureRectangle2;
        Rectangle _caveRectangle1;
        Rectangle _caveRectangle2;

        public FieldScreen(GameMain mainGame, ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
            _contentManager = contentManager;
            _graphicsDeviceManager = graphicsDeviceManager;
            _mainGame = mainGame;
        }

        public override void Initialize()
        {
            _treasureRectangle1 = new Rectangle(50, 50, 16, 16);
            _treasureRectangle2 = new Rectangle(200, 150, 16, 16);
            _caveRectangle1 = new Rectangle(150, 100, 16, 16);
            _caveRectangle2 = new Rectangle(25, 150, 16, 16);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _graphicsDevice = _contentManager.GetGraphicsDevice();
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            _bitmapFont = _contentManager.Load<BitmapFont>("montserrat-32");

            _background = _contentManager.Load<Texture2D>("Battle Field");
            var playerTexture = _contentManager.Load<Texture2D>("Teacher Mage");
            _player = new Sprite(playerTexture);
            _player.Position = new Vector2(133, 236);
            _player.Scale = new Vector2(0.09f, 0.09f);

            _textureAtlas = _contentManager.Load<Texture2D>("0x72_16x16DungeonTileset.v4");
            _caveEntrance = _contentManager.Load<Texture2D>("Cave Entrance");
        }

        public override void Update(GameTime gameTime)
        {
            if (IsVisible)
            {
                var elapsedSeconds = gameTime.GetElapsedSeconds();

                if (_mainGame.KeyboardState.IsKeyDown(Keys.Left))
                {
                    if (_player.Effect == SpriteEffects.None)
                        _player.Effect = SpriteEffects.FlipHorizontally;
                    else
                    {
                        _player.Position += new Vector2(-_playerSpeed, 0) * elapsedSeconds;
                        if (_player.Position.X < _minX)
                            _player.Position = new Vector2(_minX, _player.Position.Y);
                    }
                    CheckIntersects(_player);
                }
                else if (_mainGame.KeyboardState.IsKeyDown(Keys.Right))
                {
                    if (_player.Effect == SpriteEffects.None)
                    {
                        _player.Position += new Vector2(_playerSpeed, 0) * elapsedSeconds;
                        if (_player.Position.X > _maxX)
                            _player.Position = new Vector2(_maxX, _player.Position.Y);
                    }
                    else
                        _player.Effect = SpriteEffects.None;
                    CheckIntersects(_player);
                }
                else if (_mainGame.KeyboardState.IsKeyDown(Keys.Up))
                {
                    _player.Position += new Vector2(0, -_playerSpeed) * elapsedSeconds;
                    if (_player.Position.Y < _minY)
                        _player.Position = new Vector2(_player.Position.X, _minY);
                    CheckIntersects(_player);
                }
                else if (_mainGame.KeyboardState.IsKeyDown(Keys.Down))
                {
                    _player.Position += new Vector2(0, _playerSpeed) * elapsedSeconds;
                    if (_player.Position.Y > _maxY)
                        _player.Position = new Vector2(_player.Position.X, _maxY);
                    CheckIntersects(_player);
                }
                else if (_mainGame.KeyboardState.IsKeyDown(Keys.X))
                {
                    var creditsScreen = _mainGame.CreditsScreen;
                    creditsScreen.Show();
                }
            }
            base.Update(gameTime);
        }

        private void CheckIntersects(Transform2D player)
        {
            var rectangle = new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), Convert.ToInt32(player.Scale.X), Convert.ToInt32(player.Scale.Y));
            if (rectangle.Intersects(_caveRectangle1))
                _mainGame.CaveScreen1.Show();
            else if (rectangle.Intersects(_caveRectangle2))
                _mainGame.CaveScreen2.Show();
        }

        public override void Draw(GameTime gameTime)
        {
            if(IsVisible)
            {
                // resize to phone screen size
                _graphicsDeviceManager.PreferredBackBufferWidth = 270;
                _graphicsDeviceManager.PreferredBackBufferHeight = 540;
                _graphicsDeviceManager.ApplyChanges();

                _spriteBatch.Begin();

                // background
                _spriteBatch.Draw(_background, new Rectangle(0, 0, 270, 540), Color.White);

                // player
                _spriteBatch.Draw(_player);

                // treasure chest #1
                Rectangle sourceRectangle1 = new Rectangle(224, 208, 16, 16);
                _spriteBatch.Draw(_textureAtlas, _treasureRectangle1, sourceRectangle1, Color.White);

                // treasure chest #2
                Rectangle sourceRectangle2 = new Rectangle(240, 208, 16, 16);
                _spriteBatch.Draw(_textureAtlas, _treasureRectangle2, sourceRectangle2, Color.White);

                // cave entrance #1
                Rectangle sourceRectangle3 = new Rectangle(0, 0, 16, 16);
                _spriteBatch.Draw(_caveEntrance, _caveRectangle1, sourceRectangle3, Color.White);

                // cave entrance #2
                _spriteBatch.Draw(_caveEntrance, _caveRectangle2, sourceRectangle3, Color.White);


                // bottom text
                var textInputY = 13.75f * _bitmapFont.LineHeight - 2;
                var position = new Point2(4, textInputY);
                string typedString = "Arrow keys move";
                var stringRectangle = _bitmapFont.GetStringRectangle(typedString, position);
                _spriteBatch.DrawString(_bitmapFont, typedString, position, Color.White);
                textInputY = 14.75f * _bitmapFont.LineHeight - 2;
                position = new Point2(4, textInputY);
                typedString = "Press X for credits";
                stringRectangle = _bitmapFont.GetStringRectangle(typedString, position);
                _spriteBatch.DrawString(_bitmapFont, typedString, position, Color.White);

                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
