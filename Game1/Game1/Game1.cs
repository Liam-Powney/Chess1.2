using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private MouseState oldState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D[] textureArray = new Texture2D[17];

        public Board board = new Board();
        public bool whitesTurn = true;

        public Piece selectedPiece;
        public List<BoardSquare> potentialMoves = new List<BoardSquare>();

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
            graphics.PreferredBackBufferWidth = DrawLibrary.TILE_SIZE * 8;
            graphics.PreferredBackBufferHeight = DrawLibrary.TILE_SIZE * 8;
            graphics.ApplyChanges();

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

            textureArray[0] = Content.Load<Texture2D>("square_light");
            textureArray[1] = Content.Load<Texture2D>("square_dark");
            textureArray[2] = Content.Load<Texture2D>("square_brown_light");
            textureArray[3] = Content.Load<Texture2D>("square_brown_dark");
            textureArray[4] = Content.Load<Texture2D>("w_rook");
            textureArray[5] = Content.Load<Texture2D>("b_rook");
            textureArray[6] = Content.Load<Texture2D>("w_knight");
            textureArray[7] = Content.Load<Texture2D>("b_knight");
            textureArray[8] = Content.Load<Texture2D>("w_bishop");
            textureArray[9] = Content.Load<Texture2D>("b_bishop");
            textureArray[10] = Content.Load<Texture2D>("w_queen");
            textureArray[11] = Content.Load<Texture2D>("b_queen");
            textureArray[12] = Content.Load<Texture2D>("w_king");
            textureArray[13] = Content.Load<Texture2D>("b_king");
            textureArray[14] = Content.Load<Texture2D>("w_pawn");
            textureArray[15] = Content.Load<Texture2D>("b_pawn");
            textureArray[16] = Content.Load<Texture2D>("select_circle");

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
            board.setupNewPieces();

            MouseState newState = Mouse.GetState();
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                int xBoardCoordofClick = newState.X / DrawLibrary.TILE_SIZE;
                int yBoardCoordofClick = newState.Y / DrawLibrary.TILE_SIZE;

                if (board.isPieceOnSquare(xBoardCoordofClick, yBoardCoordofClick) == true && board.pieceOnSquareIsWhite(xBoardCoordofClick, yBoardCoordofClick) == whitesTurn & selectedPiece ==  null)
                {
                    if (selectedPiece != board.pieceOnSquare(xBoardCoordofClick, yBoardCoordofClick))
                    {
                        selectedPiece = board.pieceOnSquare(xBoardCoordofClick, yBoardCoordofClick);
                        foreach(BoardSquare square in selectedPiece.validMoves(board))
                        {
                            potentialMoves.Add(square);
                        }
                        potentialMoves.Add(board.boardSquares[xBoardCoordofClick, yBoardCoordofClick]);
                    }
                    else if (selectedPiece.PieceCoord.Equals(board.pieceOnSquare(xBoardCoordofClick, yBoardCoordofClick).PieceCoord))
                    {
                        selectedPiece = null;
                        potentialMoves.Clear();
                    }
                }
                /*else if (/*click is on availble move list*//*)
                {
                    //do move with selected piece
                }*/
                else
                {
                    selectedPiece = null;
                    potentialMoves.Clear();
                }


            }

            oldState = newState; // this reassigns the old state so that it is ready for next time

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // TODO: Add your drawing code here
            DrawLibrary.DrawBoard(spriteBatch, Content, board, textureArray, true, potentialMoves);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
