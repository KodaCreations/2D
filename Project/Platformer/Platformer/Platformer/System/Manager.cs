using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Manager
    {
        MainMenu mainMenu;
        GameOverMenu gameOverMenu;
        Instructions instructions;
        Credits credits;
        GameManager[] gameManager = new GameManager[2];
        Song menuMusic, gameMusic;

        enum GameState { MainMenu, Instructions, Credits, LevelOne, LevelTwo, GameOver };
        GameState currentGameState = GameState.MainMenu;

        public Manager()
        {
            mainMenu = new MainMenu();
            gameOverMenu = new GameOverMenu();
            instructions = new Instructions();
            credits = new Credits();
            menuMusic = Core.Content.Load<Song>("menu");
            gameMusic = Core.Content.Load<Song>("game");
        }

        private void CreateLevelOne()
        {
            Array.Clear(gameManager, 0, 1);
            StreamReader streamReader = new StreamReader("level1.txt");
            gameManager[0] = new GameManager(streamReader);

        }

        private void CreateLevelTwo()
        {
            Array.Clear(gameManager, 1, 1);
            StreamReader streamReader = new StreamReader("level2.txt");
            gameManager[1] = new GameManager(streamReader);
        }

        public void Update(GameTime gameTime)
        {
            if (Core.KeyState.IsKeyDown(Keys.Escape))
                Core.EndGame();

            switch (currentGameState)
            {
                #region Main Menu
                case GameState.MainMenu:
                    mainMenu.Update(gameTime);
                    if (MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(menuMusic);
                    }
                    if (mainMenu.StartGame)
                    {
                        CreateLevelOne();
                        mainMenu.StartGame = false;
                        MediaPlayer.Stop();
                        currentGameState = GameState.LevelOne;
                    }
                    else if (mainMenu.ShowInstructions)
                    {
                        mainMenu.ShowInstructions = false;
                        currentGameState = GameState.Instructions;
                    }
                    else if (mainMenu.ShowCredits)
                    {
                        mainMenu.ShowCredits = false;
                        currentGameState = GameState.Credits;
                    }

                    break;
                #endregion

                #region Introductions
                case GameState.Instructions:
                    instructions.Update(gameTime);
                    if (instructions.Return)
                    {
                        instructions.Return = false;
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                #endregion

                #region Credits
                case GameState.Credits:
                    credits.Update(gameTime);
                    if (credits.Return)
                    {
                        credits.Return = false;
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                #endregion

                #region Level One
                case GameState.LevelOne:
                    gameManager[0].Update(gameTime);
                    if (MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(gameMusic);
                    }
                    if (gameManager[0].LevelWon)
                    {
                        CreateLevelTwo();
                        currentGameState = GameState.LevelTwo;
                    }
                    else if (gameManager[0].LevelLost)
                        currentGameState = GameState.GameOver;
                    break;
                #endregion

                #region Level Two
                case GameState.LevelTwo:
                    gameManager[1].Update(gameTime);
                    if (gameManager[1].LevelWon)
                    {
                        gameOverMenu.GameWon = true;
                        currentGameState = GameState.GameOver;
                    }
                    else if (gameManager[1].LevelLost)
                    {
                        gameOverMenu.GameWon = false;
                        currentGameState = GameState.GameOver;
                    }
                    break;
                #endregion

                #region Game Over
                case GameState.GameOver:
                    gameOverMenu.Update(gameTime);
                    if (gameOverMenu.MainMenu)
                    {
                        MediaPlayer.Stop();
                        gameOverMenu.MainMenu = false;
                        currentGameState = GameState.MainMenu;
                    }
                    else if (gameOverMenu.RestartGame)
                    {
                        CreateLevelOne();
                        gameOverMenu.RestartGame = false;
                        currentGameState = GameState.LevelOne;
                    }
                    break;
                #endregion
            }
        }

        public void Draw()
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw();
                    break;
                case GameState.Instructions:
                    instructions.Draw();
                    break;
                case GameState.Credits:
                    credits.Draw();
                    break;
                case GameState.LevelOne:
                    gameManager[0].Draw();
                    break;
                case GameState.LevelTwo:
                    gameManager[1].Draw();
                    break;
                case GameState.GameOver:
                    if (gameManager[0].LevelLost)
                        gameManager[0].Draw();
                    else
                        gameManager[1].Draw();
                    gameOverMenu.Draw();
                    break;
            }
        }
    }
}
