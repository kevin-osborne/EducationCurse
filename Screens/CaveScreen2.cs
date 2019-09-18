using System;
using EducationCurse.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.BitmapFonts;
using GeonBit.UI.Entities;
using GeonBit.UI;

namespace EducationCurse.Screens
{
    public class CaveScreen2 : Screen
    {
        private readonly ContentManager _contentManager;
        private readonly GameMain _mainGame;
        SpriteBatch _spriteBatch;
        private BitmapFont _bitmapFont;
        private Button _button1, _button2, _button3, _button4;
        private Texture2D _background;
        private string _answer = string.Empty;

        public CaveScreen2(GameMain mainGame, ContentManager contentManager)
        {
            _contentManager = contentManager;
            _mainGame = mainGame;
        }

        public override void Initialize()
        {
            // create and init the UI manager
            UserInterface.Initialize(_contentManager, BuiltinThemes.editor);
            UserInterface.Active.UseRenderTarget = true;

            // draw cursor outside the render target
            UserInterface.Active.IncludeCursorInRenderTarget = false;

            // create top panel
            int panelHeight = 145;
            Panel topPanel = new Panel(new Vector2(0, panelHeight + 2), PanelSkin.None, Anchor.TopLeft, new Vector2(5, 300));
            topPanel.Padding = Vector2.Zero;
            UserInterface.Active.AddEntity(topPanel);

            // add buttons
            _button1 = new Button(" ", ButtonSkin.Default, Anchor.Auto, new Vector2(85, 45));
            _button1.Opacity = (byte)125;
            _button1.OutlineOpacity = 1;
            _button1.OutlineWidth = 5;
            _button1.OnClick = (Entity btn) => { this.Button("Fog"); };
            topPanel.AddChild(_button1);

            _button2 = new Button(" ", ButtonSkin.Default, Anchor.TopRight, new Vector2(85, 45), new Vector2(10, 0));
            _button2.Opacity = (byte)125;
            _button2.OutlineOpacity = 1;
            _button2.OutlineWidth = 5;
            _button2.OnClick = (Entity btn) => { this.Button("Darkness"); };
            topPanel.AddChild(_button2);

            _button3 = new Button(" ", ButtonSkin.Default, Anchor.BottomLeft, new Vector2(85, 45));
            _button3.Opacity = (byte)125;
            _button3.OutlineOpacity = 1;
            _button3.OutlineWidth = 5;
            _button3.OnClick = (Entity btn) => { this.Button("Love"); };
            topPanel.AddChild(_button3);

            _button4 = new Button(" ", ButtonSkin.Default, Anchor.BottomRight, new Vector2(85, 45), new Vector2(10, 0));
            _button4.Opacity = (byte)125;
            _button4.OutlineOpacity = 1;
            _button4.OutlineWidth = 5;
            _button4.OnClick = (Entity btn) => { this.Button("Naivety"); };
            topPanel.AddChild(_button4);

        }

        public void Button(string answer)
        {
            Console.WriteLine($"{nameof(Button)}:{answer}");
            _answer = answer;
        }


        public override void LoadContent()
        {
            base.LoadContent();
            var graphicsDevice = _contentManager.GetGraphicsDevice();
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _background = _contentManager.Load<Texture2D>("Exit Tab");
            _bitmapFont = _contentManager.Load<BitmapFont>("montserrat-32");
        }

        public override void Update(GameTime gameTime)
        {
            // update UI
            UserInterface.Active.Update(gameTime);

            if (_mainGame.KeyboardState.IsKeyDown(Keys.R) && _answer == "Darkness")
            {
                this.Hide();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(IsVisible)
            {
                // draw ui
                UserInterface.Active.Draw(_spriteBatch);

                _spriteBatch.Begin();
                _spriteBatch.Draw(_background, new Rectangle(0, 0, 270, 540), Color.White);

                // bottom text
                var textInputY = 14f * _bitmapFont.LineHeight - 2;
                var position = new Point2(4, textInputY);
                string typedString = "Answer via button";
                if (_answer != string.Empty)
                {
                    if (_answer != "Darkness")
                        typedString = "Incorrect answer";
                    else
                        typedString = "Right! win $500";
                }
                var stringRectangle = _bitmapFont.GetStringRectangle(typedString, position);
                _spriteBatch.DrawString(_bitmapFont, typedString, position, Color.White);

                if (_answer == "Darkness")
                {
                    textInputY = 15f * _bitmapFont.LineHeight - 2;
                    position = new Point2(4, textInputY);
                    typedString = "Press R to return";
                    stringRectangle = _bitmapFont.GetStringRectangle(typedString, position);
                    _spriteBatch.DrawString(_bitmapFont, typedString, position, Color.White);
                }

                _spriteBatch.End();

                // finalize ui rendering
                UserInterface.Active.DrawMainRenderTarget(_spriteBatch);

            }
            base.Draw(gameTime);
        }
    }
}
