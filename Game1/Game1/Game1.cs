using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        const int TILE_SIZE = 128;
        private MouseState oldState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Board board = new Board();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
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
            graphics.PreferredBackBufferWidth = TILE_SIZE * 8;
            graphics.PreferredBackBufferHeight = TILE_SIZE * 8;
            graphics.ApplyChanges();

            board.boardSetup();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Piece.LoadPieceTextures(Content);
            board.LoadBoardTextures(Content);


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState newState = Mouse.GetState();

            // on mouse click:
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                int xClickCoord = newState.X / TILE_SIZE;
                int yClickCoord = (-newState.Y+8*TILE_SIZE) / TILE_SIZE;

                // if there is a piece selected and user clicks on selected piece
                if (board.selectedPiece() != null && board.pieceOnCoord(xClickCoord, yClickCoord) == board.selectedPiece())
                {
                    board.selectedPiece().isSelected = false;
                    Console.WriteLine("piece was deselected 1");
                }
                // if there is no piece selected, and there is a piece on the square the user clicked on that is the correct colour for the turn
                else if (board.selectedPiece() == null && board.pieceOnCoord(xClickCoord, yClickCoord) != null && board.pieceOnCoord(xClickCoord, yClickCoord).isWhite == board.whitesTurn)
                {
                    board.pieceOnCoord(xClickCoord, yClickCoord).isSelected = true;
                    Console.WriteLine("a piece was selected");
                }

                //if there is a piece selected and user clicks on another piece of the correct colour
                else if (board.selectedPiece() != null && board.pieceOnCoord(xClickCoord, yClickCoord) != null && board.pieceOnCoord(xClickCoord, yClickCoord).isWhite == board.whitesTurn && board.pieceOnCoord(xClickCoord, yClickCoord) != board.selectedPiece())
                {
                    board.selectedPiece().isSelected = false;
                    board.pieceOnCoord(xClickCoord, yClickCoord).isSelected = true;
                    Console.WriteLine("another piece was selected");
                }

                // if there is a piece selected and player clicks on a square that doesn't have a white piece on it or isn't in the list of available moves
                else if (board.selectedPiece() != null && board.isMoveValid(xClickCoord, yClickCoord) == false)
                {
                    board.selectedPiece().isSelected = false;
                    Console.WriteLine("piece was deselected 2");
                }

                // if there is a piece selected and player clicks on an available move square
                else if (board.selectedPiece() != null && board.isMoveValid(xClickCoord, yClickCoord) == true)
                {
                    board.Move(board.selectedPiece(), xClickCoord, yClickCoord);
                    board.selectedPiece().isSelected = false;
                }
            }

            oldState = newState; 

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

            spriteBatch.Begin();

            board.DrawBoard(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
