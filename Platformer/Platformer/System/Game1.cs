using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Platformer
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteFont spriteFont;
        public KeyboardState keyState, oldkeyState;
        public Random random;
        public Viewport viewPort;
        public Rectangle worldSize = new Rectangle(0, 0, 3840, 1400);
        Manager manager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Core.Initialize(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewPort = GraphicsDevice.Viewport;
            manager = new Manager();
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            oldkeyState = keyState;
            keyState = Keyboard.GetState();
            manager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            manager.Draw();
            base.Draw(gameTime);
        }
    }
}
