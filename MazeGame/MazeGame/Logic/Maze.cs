using MazeGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Logic
{
    public class Maze
    {
        private Dictionary<Vector2, Square> MazeTiles = new Dictionary<Vector2, Square>();
        private MazeExtender extender;

        public int Rows { get; set; } = 20;
        public int Columns { get; set; } = 20;
        public int ZLevels { get; set; } = 1;

        public Vector2 StartPosition { get; set; } = new Vector2(50, 50);
        public Vector2 BufferPosition { get; set; } = new Vector2(10, 10);

        public Maze() 
        {
        }

        public void GenerateMaze(GraphicsDevice gd)
        {
            Random rnd = new Random();

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Color color = Color.White;
                    Vector2 pos = GetPosition(r, c);

                    Square sq = new Square(gd);
                    sq.Color = color;
                    sq.Position = pos;
                    sq.Size = new Vector2(50, 50);

                    if (rnd.Next(0, 100) < 10)
                    {
                        sq.Disabled = true;
                        sq.Color = Color.Black;
                    }

                    MazeTiles.Add(new Vector2(r,c), sq);
                }
            }

            extender = new MazeExtender(this, 50);

            /*
            Vector2 rootTile = new Vector2(rnd.Next(0, 10), rnd.Next(0, 10));
            Vector2 lastTile = new Vector2(0, 0);
            Vector2 currentTile = rootTile;
            ChangeTileState(true, currentTile, Color.Red);

            for (int m = 0; m < 90; m++)
            {
                Move nextMove = Move.Back;
                List<Move> possibleMoves = new List<Move>()
                {
                    Move.Back,
                    Move.Forward,
                    Move.Left,
                    Move.Right,
                };

                while (true)
                {
                    if (possibleMoves.Count == 0)
                    {
                        break;
                    }

                    nextMove = possibleMoves.ElementAt(rnd.Next(0, possibleMoves.Count));

                    Vector2 temp = ModifyPosition(currentTile, nextMove);
                    if (IsValidMove(ModifyPosition(currentTile, nextMove), nextMove))
                    {
                        currentTile = temp;
                        break;
                    }
                    else
                    {
                        // Don't try that move again.
                        possibleMoves.Remove(nextMove);
                    }
                }

                ChangeTileState(true, currentTile, Color.Orange);
            }
            */
        }

        public void ChangeTileState(bool isActive, Vector2 pos)
        {
            Square sq = MazeTiles.FirstOrDefault(r => r.Key == pos).Value;
            sq.Activated = isActive;
        }

        public void ChangeTileState(bool isActive, Vector2 pos, Color newColor)
        {
            Square sq = MazeTiles.FirstOrDefault(r => r.Key == pos).Value;
            sq.Color = newColor;
            sq.Activated = isActive;
        }

        public bool IsValidMove(Vector2 pos, Move move)
        {
            Square sq = MazeTiles.FirstOrDefault(r => r.Key == pos).Value;
            if (sq != null && !sq.Activated && !sq.Disabled)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Update the tiles.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            extender.Update(gameTime);
            foreach (KeyValuePair<Vector2,Square> square in MazeTiles)
            {
                square.Value.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw the maze tiles.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<Vector2, Square> square in MazeTiles)
            {
                square.Value.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Generate position.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public Vector2 GetPosition(int r, int c)
        {
            float y = StartPosition.Y + (50 * r) + (BufferPosition.Y + r);
            float x = StartPosition.X + (50 * c) + (BufferPosition.X + c);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Modify a position by one single move.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public Vector2 ModifyPosition(Vector2 pos, Move move)
        {
            float x = pos.X;
            float y = pos.Y;

            switch (move)
            {
                case Move.Left:
                    y = pos.Y + 1;
                    break;
                case Move.Right:
                    y = pos.Y - 1;
                    break;
                case Move.Forward:
                    x = pos.X - 1;
                    break;
                case Move.Back:
                    x = pos.X + 1;
                    break;
            }

            return new Vector2(x, y);
        }
    }
}
