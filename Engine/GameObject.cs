using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGameEngine.Common;
using XGameEngine.Core;

namespace XGameEngine
{    
    /// <summary>
    /// Represents a game object with basic rendering and update capabilities.
    /// </summary>
    public abstract class GameObject : IGameObject
    {
        /// <summary>
        /// Has Draw() been called yet?
        /// </summary>
        private bool firstDraw = false;

        /// <summary>
        /// Has Update() been called yet?
        /// </summary>
        private bool firstUpdate = false;

        /// <summary>
        /// Is the object currently active?
        /// </summary>
        private bool isActive = true;

        /// <summary>
        /// Has the object been disposed of?
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// Has initiailize been called?
        /// </summary>
        private bool isInitialized = false;

        /// <summary>
        /// Holds the display name of the object.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// Specifies the current display state.
        /// </summary>
        private Visibility visible = Visibility.Visible;

        /// <summary>
        /// Holds the owner of the current game.
        /// </summary>
        private XGame game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="game">The current game.</param>
        /// <param name="position">The position of the object.</param>
        public GameObject(XGame game, Vector2 position)
        {
            this.game = game;

            this.Children = new ListRepository<GameObject>();
            this.Children.Owner = this;

            this.Position = position;
        }

        /// <summary>
        /// Raised when IsActive changes.
        /// </summary>
        public event EventHandler ActiveChange;

        /// <summary>
        /// Raised when object visibility changes.
        /// </summary>
        public event EventHandler VisibilityChange;

        /// <summary>
        /// Raised when Draw() is first called.
        /// </summary>
        protected event EventHandler FirstDraw;

        /// <summary>
        /// Raised when Update() is first called.
        /// </summary>
        protected event EventHandler FirstUpdate;

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets a value indicating whether the object is active.
        /// </summary>
        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; this.OnActiveChange(EventArgs.Empty); }
        }

        /// <summary>
        /// Gets a value indicating whether the object has been disposed of.
        /// </summary>
        public bool IsDisposed
        {
            get { return this.isDisposed; }
        }

        /// <summary>
        /// Gets a value indicating whether the object has been initialized.
        /// </summary>
        public bool IsInitialized
        {
            get { return this.isInitialized; }
        }

        /// <summary>
        /// Gets or sets the parent of this object.
        /// </summary>
        public GameObject Parent { get; set; }

        /// <summary>
        /// Gets or sets the name of this element.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the list of children this object contains.
        /// </summary>
        public ListRepository<GameObject> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }
        private ListRepository<GameObject> _Children;

        /// <summary>
        /// Gets or sets the object display state.
        /// </summary>
        public Visibility Visible
        {
            get { return this.visible; }
            set { this.visible = value; this.OnVisibilityChange(EventArgs.Empty); }
        }

        /// <summary>
        /// Gets the current game.
        /// </summary>
        public XGame Game
        {
            get { return this.game; }
        }

        /// <summary>
        /// Gets or sets the object position.
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        private Vector2 position;

        /// <summary>
        /// Gets the position of the element based on the screen.
        /// </summary>
        public Vector2 ScreenPosition
        {
            get
            {
                Vector2 sp = new Vector2(this.position.X, this.position.Y);

                // Only add the parent position if this is a child element.
                if (this.PositionType == Common.PositionType.Relative && this.Parent != null)
                    sp = sp + this.Parent.position;

                return sp;
            }
        }

        /// <summary>
        /// Gets or sets the value detailing how to handle with rendering with a parent.
        /// </summary>
        public Common.PositionType PositionType
        {
            get { return _PositionType; }
            set { this._PositionType = value; }
        }
        private Common.PositionType _PositionType = Common.PositionType.Relative;

        /// <summary>
        /// Dispose of game object contents.
        /// </summary>
        public virtual void Dispose()
        {
            if (!this.isDisposed)
            {
                this.isDisposed = true;
                this.isActive = false;
            }
        }

        /// <summary>
        /// Draw the game object.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            // Make sure we have been initialized.
            if (!this.IsInitialized)
            {
                //throw new InvalidOperationException
                //("Initialize() must be called before Draw()");
                return;
            }

            // Has Draw() been called yet?
            if (!this.firstDraw)
            {
                this.OnFirstDraw(EventArgs.Empty);
                this.firstDraw = true;
            }

            this.Children.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Initialize the game object.
        /// </summary>
        public virtual void Initialize()
        {
            this.isInitialized = true;
        }

        /// <summary>
        /// Update the game object.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            // Make sure we have been initialized.
            if (!this.IsInitialized)
            {
                //throw new InvalidOperationException
                //("Initialize() must be called before Update()");

                this.Initialize();
            }

            // Has Update() been called yet?
            if (!this.firstUpdate)
            {
                this.OnFirstUpdate(EventArgs.Empty);
                this.firstUpdate = true;
            }

            this.Children.Update(gameTime);
        }

        /// <summary>
        /// Raised when IsActive value changes.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnActiveChange(EventArgs e)
        {
            if (this.ActiveChange != null)
            {
                this.ActiveChange(this, e);
            }
        }

        /// <summary>
        /// Raised when Draw() is first called.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnFirstDraw(EventArgs e)
        {
            if (this.FirstDraw != null)
            {
                this.FirstDraw(this, e);
            }
        }

        /// <summary>
        /// Raised when Update() is first called.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnFirstUpdate(EventArgs e)
        {
            if (this.FirstUpdate != null)
            {
                this.FirstUpdate(this, e);
            }
        }

        /// <summary>
        /// Raised when object visibility changes.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnVisibilityChange(EventArgs e)
        {
            if (this.VisibilityChange != null)
            {
                this.VisibilityChange(this, e);
            }
        }
    }
}
