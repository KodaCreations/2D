using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platformer
{
    class GameManager
    {
        HeadsUpDisplay headsUpDisplay;
        Camera camera = new Camera() { Limits = Core.WorldSize };
        Texture2D worldTexture;
        StreamReader streamReader;
        List<string> levelData = new List<string>();
        List<Layer> layers;
        Song bgMusic;

        public bool LevelWon { get; set; }
        public bool LevelLost { get; set; }

        enum EntityType
        { ThickLargePlatform, ThickMediumPlatform, ThickSmallPlatform, ThinLargePlatform, ThinMediumPlatform, ThinSmallPlatform, GoalPlatform, Goal, Key, Player, Enemy, None }
        EntityType entityType = EntityType.None;

        public GameManager(StreamReader streamReader)
        {
            worldTexture = Core.Content.Load<Texture2D>("Objects/WorldTiles");
            this.streamReader = streamReader;
            CreateLayers();
            CreateMap();
            CreateBackgrounds();
            headsUpDisplay = new HeadsUpDisplay(Vector2.Zero, layers[8].Players[0].Health);
            layers[9].Objects.Add(headsUpDisplay);
            
        }

        private void CreateLayers()
        {
            layers = new List<Layer>
            {
                new Layer(camera) { Parallax = new Vector2(0.0f, 0.0f) },      // 0 - Sky
                new Layer(camera) { Parallax = new Vector2(0.2f, 0.1f) },      // 1 - Cloud Rear
                new Layer(camera) { Parallax = new Vector2(0.3f, 0.3f) },      // 2 - Cloud Front
                new Layer(camera) { Parallax = new Vector2(0.4f, 0.6f) },      // 3 - Mount Rear
                new Layer(camera) { Parallax = new Vector2(0.5f, 0.7f) },      // 4 - Mount Front
                new Layer(camera) { Parallax = new Vector2(0.7f, 0.8f) },      // 5 - Hills
                new Layer(camera) { Parallax = new Vector2(0.8f, 0.9f) },      // 6 - Bush Rear
                new Layer(camera) { Parallax = new Vector2(0.9f, 0.95f) },     // 7 - Bush Front
                new Layer(camera) { Parallax = new Vector2(1.0f, 1.0f) },      // 8 - Level
                new Layer(camera) { Parallax = new Vector2(0.0f, 0.0f) }       // 9 - HUD
            };
        }

        private void CreateMap()
        {
            while (!streamReader.EndOfStream)
                levelData.Add(streamReader.ReadLine());

            for (int i = 0; i < levelData.Count; i++)
            {
                if (ChangeType(levelData[i]))
                    continue;
                else
                    AddEntity(levelData[i]);
            }

            for (int i = 0; i < Core.WorldSize.Width / 64; i++)
            {
                Platform ground = new Platform(worldTexture, new Vector2(i * 70, Core.WorldSize.Height - 70), new Rectangle(0, 0, 70, 110));
                layers[8].Platforms.Add(ground);
            }
        }

        public bool ChangeType(String level_data)
        {
            if (level_data == "[ThinSmallPlatform]")
            {
                entityType = EntityType.ThinSmallPlatform;
                return true;
            }
            else if (level_data == "[ThinMediumPlatform]")
            {
                entityType = EntityType.ThinMediumPlatform;
                return true;
            }
            else if (level_data == "[ThinLargePlatform]")
            {
                entityType = EntityType.ThinLargePlatform;
                return true;
            }
            else if (level_data == "[ThickSmallPlatform]")
            {
                entityType = EntityType.ThickSmallPlatform;
                return true;
            }
            else if (level_data == "[ThickMediumPlatform]")
            {
                entityType = EntityType.ThickMediumPlatform;
                return true;
            }
            else if (level_data == "[ThickLargePlatform]")
            {
                entityType = EntityType.ThickLargePlatform;
                return true;
            }
            else if (level_data == "[GoalPlatform]")
            {
                entityType = EntityType.GoalPlatform;
                return true;
            }
            else if (level_data == "[Goal]")
            {
                entityType = EntityType.Goal;
                return true;
            }
            else if (level_data == "[Key]")
            {
                entityType = EntityType.Key;
                return true;
            }
            else if (level_data == "[Enemies]")
            {
                entityType = EntityType.Enemy;
                return true;
            }
            else if (level_data == "[Player]")
            {
                entityType = EntityType.Player;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddEntity(String level_data)
        {
            int x = int.Parse(level_data.Split(';')[0]);
            int y = int.Parse(level_data.Split(';')[1]);

            switch (entityType)
            {
                #region Platforms

                case EntityType.ThinSmallPlatform:
                    Platform p = new Platform(worldTexture, new Vector2(x, y), new Rectangle(70, 70, 70, 40));
                    layers[8].Platforms.Add(p);
                    break;

                case EntityType.ThinMediumPlatform:
                    p = new Platform(worldTexture, new Vector2(x, y), new Rectangle(140, 70, 140, 40));
                    layers[8].Platforms.Add(p);
                    break;

                case EntityType.ThinLargePlatform:
                    p = new Platform(worldTexture, new Vector2(x, y), new Rectangle(0, 180, 280, 40));
                    layers[8].Platforms.Add(p);
                    break;

                case EntityType.ThickSmallPlatform:
                    p = new Platform(worldTexture, new Vector2(x, y), new Rectangle(70, 0, 70, 70));
                    layers[8].Platforms.Add(p);
                    break;

                case EntityType.ThickMediumPlatform:
                    p = new Platform(worldTexture, new Vector2(x, y), new Rectangle(140, 0, 140, 70));
                    layers[8].Platforms.Add(p);
                    break;

                case EntityType.ThickLargePlatform:
                    p = new Platform(worldTexture, new Vector2(x, y), new Rectangle(0, 110, 280, 70));
                    layers[8].Platforms.Add(p);
                    break;

                case EntityType.GoalPlatform:
                    p = new Platform(worldTexture, new Vector2(x, y), new Rectangle(0, 220, 280, 70));
                    layers[8].Platforms.Add(p);
                    break;

                #endregion

                case EntityType.Goal:
                    Door door = new Door(worldTexture, new Vector2(x, y));
                    layers[8].Doors.Add(door);
                    break;

                case EntityType.Key:
                    Key key = new Key(worldTexture, new Vector2(x, y));
                    layers[8].Keys.Add(key);
                    break;

                case EntityType.Enemy:
                    Enemy e = new Enemy(new Vector2(x, y));
                    layers[8].Enemies.Add(e);
                    break;

                case EntityType.Player:
                    Player player = new Player(new Vector2(x, y));
                    player.RespawnPosition = new Vector2(x, y);
                    layers[8].Players.Add(player);
                    break;

                default:
                    break;
            }
        }

        private void CreateBackgrounds()
        {
            #region Textures
            Texture2D layer0 = Core.Content.Load<Texture2D>("Backgrounds/Skybox");
            Texture2D layer1 = Core.Content.Load<Texture2D>("Backgrounds/CloudRear");
            Texture2D layer2 = Core.Content.Load<Texture2D>("Backgrounds/CloudFront");
            Texture2D layer3 = Core.Content.Load<Texture2D>("Backgrounds/MountRear");
            Texture2D layer4 = Core.Content.Load<Texture2D>("Backgrounds/MountFront");
            Texture2D layer5 = Core.Content.Load<Texture2D>("Backgrounds/Hill");
            Texture2D layer6 = Core.Content.Load<Texture2D>("Backgrounds/BushRear");
            Texture2D layer7 = Core.Content.Load<Texture2D>("Backgrounds/BushFront");
            #endregion

            Background background = new Background(Vector2.Zero, layer0);
            layers[0].Objects.Add(background);

            for (int i = 0; i < Core.WorldSize.Width / Core.Window.Width; i++)
            {
                background = new Background(new Vector2(i * layer1.Width, 0), layer1);
                layers[1].Objects.Add(background);
                background = new Background(new Vector2(i * layer2.Width, 100), layer2);
                layers[2].Objects.Add(background);
                background = new Background(new Vector2(i * (layer3.Width + 500) + 300, Core.WorldSize.Height - layer3.Height - 250), layer3);
                layers[3].Objects.Add(background);
                background = new Background(new Vector2(i * (layer4.Width + 500) + 100, Core.WorldSize.Height - layer4.Height - 200), layer4);
                layers[4].Objects.Add(background);
                background = new Background(new Vector2(i * (layer5.Width + 500) + 1000, Core.WorldSize.Height - layer5.Height - 150), layer5);
                layers[5].Objects.Add(background);
                background = new Background(new Vector2(0, Core.WorldSize.Height - layer6.Height - 100), layer6);
                layers[6].Objects.Add(background);
                background = new Background(new Vector2(0, Core.WorldSize.Height - layer7.Height), layer7);
                layers[7].Objects.Add(background);
            }
        }

        private void CameraControl()
        {
            camera.LookAt(layers[8].Players[0].Position);

            //Zoom
            if (Core.KeyState.IsKeyDown(Keys.Q))
                camera.Zoom += 0.01f;
            else if (Core.KeyState.IsKeyDown(Keys.W))
                camera.Zoom -= 0.01f;
            else if (Core.KeyState.IsKeyDown(Keys.E))
                camera.Zoom = 1.0f;

            //Rotate
            if (Core.KeyState.IsKeyDown(Keys.A))
                camera.Rotation += 0.01f;
            else if (Core.KeyState.IsKeyDown(Keys.S))
                camera.Rotation -= 0.01f;
            else if (Core.KeyState.IsKeyDown(Keys.D))
                camera.Rotation = 0.0f;
        }

        private void CheckCollisions()
        {
            #region Player on World collision
            for (int j = 0; j < layers[8].Platforms.Count; j++)
                if (layers[8].Players[0].PlatformCollision(layers[8].Platforms[j].HitBox))
                    break;
            #endregion

            #region Player on Item collision
            if (layers[8].Players[0].SimpleCollision(layers[8].Keys[0].HitBox))
            {
                layers[8].Doors[0].Open = true;
                layers[8].Keys[0].Hit = true;
            }
            if (layers[8].Doors[0].SimpleContains(layers[8].Players[0].HitBox))
                if (layers[8].Doors[0].Open == true)
                    LevelWon = true;
            #endregion

            #region Player on Enemy collision
            foreach (Enemy enemy in layers[8].Enemies)
                if (layers[8].Players[0].SimpleCollision(enemy.HitBox))
                    if (layers[8].Players[0].PixelCollision(enemy.Texture, enemy.HitBox, enemy.SourceRectangle))
                        break;
            #endregion

            #region Enemy on World collision
            foreach (Enemy enemy in layers[8].Enemies)
                for (int j = 0; j < layers[8].Platforms.Count; j++)
                    if (enemy.PlatformPatrol(layers[8].Platforms[j].HitBox))
                        break;
            #endregion

            #region Enemy on Weapon collision
            for (int i = 0; i < layers[8].Enemies.Count; i++)
            {
                for (int j = 0; j < layers[8].Weapons.Count; j++)
                {
                    if (layers[8].Enemies[i].SimpleCollision(layers[8].Weapons[j].HitBox))
                    {
                        layers[8].Enemies.RemoveAt(i);
                        layers[8].Weapons.RemoveAt(j);
                    }
                }
            }
            #endregion

            #region Weapon on Platform collision
            foreach (Weapon weapon in layers[8].Weapons)
                if (!weapon.Flying)
                    for (int j = 0; j < layers[8].Platforms.Count; j++)
                        if (weapon.PlatformCollision(layers[8].Platforms[j].HitBox))
                            break;
            #endregion

        }

        private void HUDControl()
        {
            headsUpDisplay.Health = layers[8].Players[0].Health;
            if (layers[8].Keys[0].PickedUp)
                headsUpDisplay.GotKey = true;
            if (layers[8].Players[0].Health == 0)
                LevelLost = true;
        }

        public void Update(GameTime gameTime)
        {     
            Core.IsMouseVisible = false;
            CameraControl();
            layers[8].Update(gameTime);

            #region Use Weapon
            if (layers[8].Players[0].ShootGun == true)
            {
                Projectile projectile = new Projectile(new Vector2(layers[8].Players[0].Position.X + (layers[8].Players[0].HitBox.Width / 2) + (layers[8].Players[0].GetDirection * 15), layers[8].Players[0].Position.Y + layers[8].Players[0].HitBox.Height / 2), layers[8].Players[0].GetDirection);
                layers[8].Weapons.Add(projectile);
                layers[8].Players[0].ShootGun = false;
            }
            if (layers[8].Players[0].PlaceTrap == true)
            {
                Trap trap = new Trap(new Vector2(layers[8].Players[0].Position.X + (layers[8].Players[0].HitBox.Width / 2) - 7.5f + (layers[8].Players[0].GetDirection * 30), layers[8].Players[0].Position.Y + layers[8].Players[0].HitBox.Height - 16), layers[8].Players[0].GetDirection);
                trap.IsOnGround = true;
                layers[8].Weapons.Add(trap);
                layers[8].Players[0].PlaceTrap = false;
            }
            #endregion

            CheckCollisions();
            HUDControl();
        }

        public void Draw()
        {
            foreach (Layer layer in layers)
                layer.Draw();
        }
    }
}