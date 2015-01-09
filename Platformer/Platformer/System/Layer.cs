using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Layer
    {
        private readonly Camera camera;

        public Layer(Camera camera)
        {
            this.camera = camera;
            Parallax = Vector2.One;
            Objects = new List<Object>();
            Platforms = new List<Platform>();
            Enemies = new List<Enemy>();
            Weapons = new List<Weapon>();
            Doors = new List<Door>();
            Keys = new List<Key>();
            Players = new List<Player>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Object obj in Objects)
                obj.Update(gameTime);

            foreach (Platform platform in Platforms)
                platform.Update(gameTime);

            foreach (Enemy enemy in Enemies)
                enemy.Update(gameTime);

            foreach (Weapon weapon in Weapons)
                weapon.Update(gameTime);

            foreach (Door door in Doors)
                door.Update(gameTime);

            foreach (Key key in Keys)
                key.Update(gameTime);

            foreach (Player player in Players)
                player.Update(gameTime);
        }

        public void Draw()
        {
            Core.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransformation(Parallax));

            foreach (Object obj in Objects)
                obj.Draw();

            foreach (Platform platform in Platforms)
                platform.Draw();

            foreach (Door door in Doors)
                door.Draw();

            foreach (Key key in Keys)
                key.Draw();

            foreach (Enemy enemy in Enemies)
                enemy.Draw();

            foreach (Weapon weapon in Weapons)
                weapon.Draw();

            foreach (Player player in Players)
                player.Draw();

            Core.spriteBatch.End();
        }

        public Vector2 Parallax { get; set; }

        public List<Object> Objects { get; private set; }
        public List<Platform> Platforms { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<Weapon> Weapons { get; private set; }
        public List<Door> Doors { get; private set; }
        public List<Key> Keys { get; private set; }
        public List<Player> Players { get; private set; }
    }
}
