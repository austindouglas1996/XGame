using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;

namespace MazeGame.Logic
{
    public class MazeExtender : GameObject
    {
        private Maze _Maze;
        private int moves = 0;



        private TimeSpan _updateTime = TimeSpan.FromMilliseconds(200);
        private TimeSpan lastUpdateTime = TimeSpan.Zero;

        public MazeExtender(XGame game, Maze maze, int moves)
            : base(game, Vector2.Zero)
        {
            _Maze = maze;
            this.moves = moves;

            Random rnd = new Random();

            this.RootTile = new Vector2(rnd.Next(0, _Maze.Rows), rnd.Next(0, _Maze.Columns));
            this.CurrentTile = this.RootTile;
            _Maze.ChangeTileState(true, this.RootTile, Color.Red);
        }

        public Vector2 RootTile { get; set; }
        public Vector2 CurrentTile { get; set; }
        public List<Vector2> VisitedHistory { get; set; } = new List<Vector2>();

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - lastUpdateTime > _updateTime && moves > 0)
            {
                ExtendMaze();
                moves -= 1;
                lastUpdateTime = gameTime.TotalGameTime;
            }
        }

        public virtual void ExtendMaze()
        {
            Random rnd = new Random();

            Move nextMove = Move.Back;

            Dictionary<Move, int> possibleMoves = new Dictionary<Move, int>();
            possibleMoves.Add(Move.Nothing, 95);
            possibleMoves.Add(Move.Back, 80);
            possibleMoves.Add(Move.Forward, 60);
            possibleMoves.Add(Move.Left, 40);
            possibleMoves.Add(Move.Right, 20);

            while (possibleMoves.Count > 0)
            {
                int nextMovePercent = rnd.Next(0, 100);
                nextMove = possibleMoves.FirstOrDefault(r => r.Value < nextMovePercent).Key;

                if (nextMove == Move.Nothing)
                {
                    break;
                }

                Vector2 nextTile = _Maze.ModifyPosition(CurrentTile, nextMove);

                if (_Maze.IsValidMove(nextTile, nextMove))
                {
                    CurrentTile = nextTile;

                    // Change tile
                    _Maze.ChangeTileState(true, CurrentTile, Color.Green);

                    if (VisitedHistory.Count > 0)
                    {
                        _Maze.ChangeTileState(true, VisitedHistory.Last(), Color.Orange);
                    }

                    // Record.
                    this.VisitedHistory.Add(CurrentTile);

                    return;
                }
                else
                {
                    possibleMoves.Remove(nextMove);
                }
            }

            // No tile found, go back.
            if (this.VisitedHistory.Count > 0)
            {
                this.CurrentTile = this.VisitedHistory.ElementAt(rnd.Next(0, this.VisitedHistory.Count));
                this.VisitedHistory.Remove(this.CurrentTile);
                this.ExtendMaze();
            }
        }
    }
}
