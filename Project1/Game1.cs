using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Project1
{
    public class Game1 : Game
    {
        enum Screen
        {
            Intro,
            Animation,
            End
        }
       
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D introTexture;
        Texture2D cityTexture;
        Texture2D meteorTexture;
        Texture2D impactTexture;
        Texture2D sunsetTexture;
        Rectangle meteorRect;
        Vector2 meteorSpeed;
        Vector2 meteorLocation;

        float startTime;
        float seconds;
        Song introSong;
        SpriteFont titleFont;
        MouseState mouseState;
        Screen screen;
        bool colision = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Animation";
            screen = Screen.Intro;
            introTexture = Content.Load<Texture2D>("DarkSky2");
            cityTexture = Content.Load<Texture2D>("CityScape2");
            introSong = Content.Load<Song>("Nino Nardini - Morning Dew");
            titleFont = Content.Load<SpriteFont>("titleFont");
            meteorTexture = Content.Load<Texture2D>("Meteor");
            meteorRect = new Rectangle(400, -400, 600, 400);
            meteorLocation = new Vector2(meteorRect.X, meteorRect.Y);

            meteorSpeed = new Vector2(-0.5f, 0.9f);
            impactTexture = Content.Load<Texture2D>("MeteorImpact");
            sunsetTexture = Content.Load<Texture2D>("sunset");
            MediaPlayer.Play(introSong);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Animation;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;

                }
            }
            else if (screen == Screen.Animation)
            {
                //this.Window.Title = meteorLocation.X.ToString() + "," + meteorLocation.Y.ToString();

                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                if (seconds >= 2)
                {
                    meteorLocation.X += meteorSpeed.X;
                    meteorLocation.Y += meteorSpeed.Y;
                    meteorRect.X = (int)Math.Round(meteorLocation.X);
                    meteorRect.Y = (int)Math.Round(meteorLocation.Y);   


                }
                if (meteorRect.Bottom >= _graphics.PreferredBackBufferHeight)
                {
                    colision = true;
                }
            }

            

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen.Equals(Screen.Intro))
            {
                _spriteBatch.Draw(introTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(titleFont, "Time", new Vector2(275, 150), Color.DarkOrchid);
            }
            else if (screen.Equals(Screen.Animation))
            {
                _spriteBatch.Draw(sunsetTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(cityTexture, new Vector2(0, 215), Color.White);
                
                _spriteBatch.Draw(meteorTexture, meteorRect, Color.White);
                if (colision)
                {
                    _spriteBatch.Draw(impactTexture, new Vector2(200, 420), Color.White);
                    
                }
                
            }
            else if (screen.Equals(Screen.End))
            {

            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}