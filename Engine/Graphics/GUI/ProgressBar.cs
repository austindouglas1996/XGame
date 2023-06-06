using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Represents a displayable control that can show the user how much of a percent an unknown variable has gone up.
    /// </summary>
    public class ProgressBar : UIObject
    {
        /// <summary>
        /// Represents the bar.
        /// </summary>
        private Rectangle _Bar;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name="game"></param>
        public ProgressBar(XGame game) 
            : base(game)
        {
        }

        /// <summary>
        /// Invoked when <see cref="CurrentProgress"/> has changed.
        /// </summary>
        public event EventHandler ProgressChanged;

        /// <summary>
        /// Invoked when <see cref="CurrentProgress"/> equals <see cref="MaxProgress"/>.
        /// </summary>
        public event EventHandler ProgressCompleted;

        /// <summary>
        /// Gets or sets the current progress of the bar.
        /// </summary>
        public double CurrentProgress
        {
            get => _CurrentProgress;
            set
            {
                _CurrentProgress = value;
                if (CurrentProgress >= MaxProgress)
                {
                    CurrentProgress = MaxProgress;
                    this.OnProgressCompleted(EventArgs.Empty);
                }

                this.OnProgressChanged(EventArgs.Empty);
            }
        }
        private double _CurrentProgress = 0;

        /// <summary>
        /// Gets or sets the maximum amount of progress this bar is allowed to contain.
        /// </summary>
        public double MaxProgress = 100;

        /// <summary>
        /// Gets or sets the <see cref="Border"/> object that controls the outline of the bar.
        /// </summary>
        public Border Outline
        {
            get => _Outline;
            set => _Outline = value;
        }
        private Border _Outline;

        /// <summary>
        /// Gets or sets the color of the progress bar.
        /// </summary>
        public Color Progress
        {
            get => _Progress;
            set => _Progress = value;
        }
        private Color _Progress = new Color(10, 194, 86);

        /// <summary>
        /// Update the current length of the progress bar.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the progress bar.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Game.WorldRender.SpriteBatch[Layer].Draw(base.Game.EngineResource.Dummy, _Bar, _Progress);
        }

        /// <summary>
        /// Invokes the <see cref="ProgressChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProgressChanged(EventArgs e)
        {
            double percent = (CurrentProgress / MaxProgress) * 100;
            double length = (percent * this.Width) / 100;

            _Bar = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)length, this.Height);

            if (this.ProgressChanged != null)
                this.ProgressChanged(this, e);
        }

        /// <summary>
        /// Invokes the <see cref="ProgressCompleted"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProgressCompleted(EventArgs e)
        {
            if (this.ProgressCompleted != null)
                this.ProgressCompleted(this, e);
        }
    }
}
