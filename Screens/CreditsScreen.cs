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
    public class CreditsScreen : Screen
    {
        private readonly ContentManager _contentManager;
        SpriteBatch _spriteBatch;
        private Texture2D _background;

        public CreditsScreen(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            var graphicsDevice = _contentManager.GetGraphicsDevice();
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _background = _contentManager.Load<Texture2D>("End Screen");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(IsVisible)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_background, new Rectangle(0, 0, 270, 540), Color.White);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
