using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Core;
using XGameEngine.Logic;
using XGameEngine.Logic.Input;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Represents a game GUI element.
    /// </summary>
    public abstract class UIObject : GameObject
    {
        /// <summary>
        /// Is the mouse currently inside the element?
        /// </summary>
        private bool mouseIsOver = false;

        /// <summary>
        /// Color of the background.
        /// </summary>
        private Color background = new Color(0, 0, 0, 0);

        /// <summary>
        /// Drawing depth of the element.
        /// </summary>
        private float depth = 0.0f;

        /// <summary>
        /// Element rotation.
        /// </summary>
        private float rotation = 0.0f;

        /// <summary>
        /// Drawing scale. (Default: 1.0f)
        /// </summary>
        private float scale = 1.0f;

        /// <summary>
        /// Height of the element.
        /// </summary>
        private int height = 1;

        /// <summary>
        /// Width of the element.
        /// </summary>
        private int width = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIObject"/> class.
        /// </summary>
        public UIObject(XGame game)
            : base(game, new Vector2(1,1))
        {
        }

        /// <summary>
        /// Raised on mouse click.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Raised when the element gains focus.
        /// </summary>
        public event EventHandler GotFocus;

        /// <summary>
        /// Raised when the element loses focus.
        /// </summary>
        public event EventHandler LostFocus;

        /// <summary>
        /// Raised when the mouse is over.
        /// </summary>
        public event EventHandler MouseOver;

        /// <summary>
        /// Raised when the mouse is no longer over.
        /// </summary>
        public event EventHandler MouseLeave;

        /// <summary>
        /// Raised when the gamepad has the elements focus.
        /// </summary>
        public event EventHandler GamePadSelected;

        /// <summary>
        /// Raised when the element is updated.
        /// </summary>
        public event EventHandler UpdateCall;

        /// <summary>
        /// Raised when the size of the element changes.
        /// </summary>
        public event EventHandler SizeChange;

        /// <summary>
        /// Gets a value indicating whether the mouse is over this element.
        /// </summary>
        public bool MouseIsOver
        {
            get { return this.mouseIsOver; }
            private set
            {
                this.mouseIsOver = value;

                if (value)
                {
                    this.OnMouseOver(EventArgs.Empty);
                }
                else
                {
                    this.OnMouseLeave(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        public Color Background
        {
            get { return this.background; }
            set { this.background = value; }
        }

        /// <summary>
        /// Gets or sets the element depth.
        /// </summary>
        public float Depth
        {
            get { return this.depth; }
            set { this.depth = value; }
        }

        /// <summary>
        /// Gets or sets the element rotation.
        /// </summary>
        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        /// <summary>
        /// Gets or sets the element scale.
        /// </summary>
        public float Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }

        /// <summary>
        /// Retrieves the actual height of the element. Modifies value depending on value type like
        /// <see cref="SpriteFont"/> size and <see cref="Scale"/>. This value is different
        /// than user set value <see cref="height"/>.
        /// </summary>
        public virtual int ActualHeight
        {
            get { return (int)(this.height * this.scale); }
        }

        /// <summary>
        /// Gets the actual width of the element.
        /// </summary>
        public virtual int ActualWidth
        {
            get { return (int)(this.width * this.scale); }
        }

        /// <summary>
        /// Gets or sets the element height.
        /// </summary>
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// Gets or sets the element width.
        /// </summary>
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        /// <summary>
        /// Gets the element bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle
                ((int)this.Position.X, (int)this.Position.Y, this.ActualWidth, this.ActualHeight); }
        }

        /// <summary>
        /// Raise element click event.
        /// </summary>
        /// <param name="element">UIElement for Click event to be raised.</param>
        public static void GetClick(UIObject element)
        {
            element.OnClick(EventArgs.Empty);
        }

        /// <summary>
        /// Draw the element.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            if (this.Visible != Visibility.Visible
                && !this.IsActive)
            {
                return;
            }

            base.Game.WorldRender.SpriteBatch.Draw
                (base.Game.EngineResource.Dummy, this.Bounds, null, this.background,
                this.rotation, new Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, this.depth);

            base.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Update the element.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Check if the mouse is over the element.
            if (InputState.IsMouseOver(this.Bounds, PlayerIndex.One)
                && !this.mouseIsOver)
            {
                this.mouseIsOver = true;
            }
            else if (this.mouseIsOver)
            {
                this.mouseIsOver = false;
            }

            // Check if the element is being clicked.
            if (this.mouseIsOver
                && InputState.MousePressed(MouseButtons.LeftButton, PlayerIndex.One, StateOptions.Current))
            {
                this.OnClick(EventArgs.Empty);
            }

            this.OnUpdateCall(EventArgs.Empty);
            base.Update(gameTime);
        }

        /// <summary>
        /// Raised when the element is clicked.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnClick(EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        /// <summary>
        /// Raised when the element gains focus.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnGotFocus(EventArgs e)
        {
            if (this.GotFocus != null)
            {
                this.GotFocus(this, e);
            }
        }

        /// <summary>
        /// Raised when the element loses focus.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnLostFocus(EventArgs e)
        {
            if (this.LostFocus != null)
            {
                this.LostFocus(this, e);
            }
        }

        /// <summary>
        /// Raised when the mouse is within the element bounds.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnMouseOver(EventArgs e)
        {
            if (this.MouseOver != null)
            {
                this.MouseOver(this, e);
            }
        }

        /// <summary>
        /// Raised when the mouse is no longer within the element bounds.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnMouseLeave(EventArgs e)
        {
            if (this.MouseLeave != null)
            {
                this.MouseLeave(this, e);
            }
        }

        /// <summary>
        /// Raised when the gamepad has selected this element.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnGamePadSelected(EventArgs e)
        {
            if (this.GamePadSelected != null)
            {
                this.GamePadSelected(this, e);
            }
        }

        /// <summary>
        /// Raised when the element is updated.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnUpdateCall(EventArgs e)
        {
            if (this.UpdateCall != null)
            {
                this.UpdateCall(this, e);
            }
        }

        /// <summary>
        /// Raised when the object size changes.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSizeChange(EventArgs e)
        {
            if (this.SizeChange != null)
                this.SizeChange(this, e);
        }
    }
}
