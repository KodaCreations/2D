using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    abstract class Core
    {
        static Game1 sGame;

        public static void Initialize(Game1 game)
        { sGame = game; }

        public static ContentManager Content
        { get { return sGame.Content; } }

        public static KeyboardState KeyState
        { get { return sGame.keyState; } }

        public static KeyboardState OldKeyState
        { get { return sGame.oldkeyState; } }

        public static Rectangle Window
        { get { return sGame.Window.ClientBounds; } }

        public static Viewport Viewport
        { get { return sGame.viewPort; } }

        public static SpriteBatch spriteBatch
        { get { return sGame.spriteBatch; } }

        public static void EndGame()
        { sGame.Exit(); }

        public static Rectangle WorldSize
        { get { return sGame.worldSize; } }

        public static bool IsMouseVisible
        {
            get { return sGame.IsMouseVisible; }
            set { sGame.IsMouseVisible = value; }
        }
    }
}
