using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Screens;
using EducationCurse.Screens;

namespace EducationCurse.Windows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private Texture2D _background;
        private BitmapFont _bitmapFont;
        private FieldScreen _fieldScreen;

        public KeyboardState KeyboardState { get; private set; }
        public CreditsScreen CreditsScreen { get; private set; }
        public CaveScreen1 CaveScreen1 { get; private set; }
        public CaveScreen2 CaveScreen2 { get; private set; }

        public GameMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            var screenGameComponent = new ScreenGameComponent(this);

            _fieldScreen = new FieldScreen(this, Content, _graphics);
            screenGameComponent.Register(_fieldScreen);
            _fieldScreen.Hide();

            this.CreditsScreen = new CreditsScreen(Content);
            screenGameComponent.Register(this.CreditsScreen);
            this.CreditsScreen.Hide();

            this.CaveScreen1 = new CaveScreen1(this, Content);
            screenGameComponent.Register(this.CaveScreen1);
            this.CaveScreen1.Hide();

            this.CaveScreen2 = new CaveScreen2(this, Content);
            screenGameComponent.Register(this.CaveScreen2);
            this.CaveScreen2.Hide();

            Components.Add(screenGameComponent);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Texture2D image = Content.Load<Texture2D>("gurk");
            _background = Content.Load<Texture2D>("gurk");

            _bitmapFont = Content.Load<BitmapFont>("montserrat-32");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.C))
                _fieldScreen.Show();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Rectangle(0, 0, 320, 346), Color.White);

            var textInputY = 14 * _bitmapFont.LineHeight - 2;
            var position = new Point2(4, textInputY);
            string typedString = "Press C to continue...";
            var stringRectangle = _bitmapFont.GetStringRectangle(typedString, position);

            _spriteBatch.DrawString(_bitmapFont, typedString, position, Color.White);

            //if (_isCursorVisible)
              //  _spriteBatch.DrawString(_bitmapFont, "_", new Vector2(stringRectangle.Width, textInputY), Color.White);


            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
