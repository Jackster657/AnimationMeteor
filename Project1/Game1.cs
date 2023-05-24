using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Texture2D shockwaveLTexture;
        Texture2D shockwaveRTexture;
        Texture2D endScreen;
        Rectangle endSizeRect;
        Rectangle shockwaveLRect;
        Rectangle shockwaveRRect;
        Rectangle meteorRect;
        Vector2 meteorSpeed;
        Vector2 shockwaveSpeedL;
        Vector2 shockwaveSpeedR;
        Vector2 meteorLocation;
        float startTime;
        float seconds;
        Song introSong;
        SoundEffect explosion;
        SpriteFont titleFont;
        MouseState mouseState;
        Screen screen;
        bool collision = false;

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
            endScreen = Content.Load<Texture2D>("terminator");
            shockwaveLTexture = Content.Load<Texture2D>("ShockwaveLeft");
            shockwaveRTexture = Content.Load<Texture2D>("ShockwaveRight");
            endSizeRect = new Rectangle(0, 0, 800, 500);
            shockwaveLRect = new Rectangle(325, 385, 50, 100);
            shockwaveRRect = new Rectangle(350,385,50,100);
            meteorRect = new Rectangle(450, -400, 600, 400);
            meteorLocation = new Vector2(meteorRect.X, meteorRect.Y);
            meteorSpeed = new Vector2(-0.5f, 0.9f);
            shockwaveSpeedL = new Vector2(-2, 0);
            shockwaveSpeedR = new Vector2(2, 0);
            impactTexture = Content.Load<Texture2D>("MeteorImpact");
            sunsetTexture = Content.Load<Texture2D>("sunset");
            explosion = Content.Load<SoundEffect>("explosion");
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
                    if (!collision)
                        explosion.Play();
                    collision = true;
                    
                }
                if (collision)
                {
                    shockwaveLRect.X += (int)shockwaveSpeedL.X;
                    shockwaveLRect.Y += (int)shockwaveSpeedL.Y;
                    shockwaveRRect.X += (int)shockwaveSpeedR.X;
                    shockwaveRRect.Y += (int)shockwaveSpeedR.Y;
                }
                if (shockwaveRRect.Right >= _graphics.PreferredBackBufferWidth)
                {
                    screen = Screen.End;
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
                _spriteBatch.DrawString(titleFont, "Collision Course", new Vector2(60, 150), Color.DarkOrchid);
            }
            else if (screen.Equals(Screen.Animation))
            {
                _spriteBatch.Draw(sunsetTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(cityTexture, new Vector2(0, 215), Color.White);
                
               
                if (collision)
                {
                    _spriteBatch.Draw(impactTexture, new Vector2(250, 420), Color.White);
                    _spriteBatch.Draw(shockwaveLTexture,shockwaveLRect, Color.White);
                    _spriteBatch.Draw(shockwaveRTexture,shockwaveRRect, Color.White);
                }
                else
                {
                    _spriteBatch.Draw(meteorTexture, meteorRect, Color.White);
                }
                
            }
            else if (screen.Equals(Screen.End))
            {
                _spriteBatch.Draw(endScreen, endSizeRect, Color.White);
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}