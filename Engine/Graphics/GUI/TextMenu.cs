using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine.Common;
using XGameEngine.Logic.Input;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Represents a container with selectable textblock elements.
    /// </summary>
    public class TextMenu : Container
    {   
        /// <summary>
        /// Initializes a new instance of <see cref="TextMenu"/> class.
        /// </summary>
        /// <param name="game"></param>
        public TextMenu(XGame game) 
            : base(game)
        {
        }        

        /// <summary>           
        /// Raised when selected child has changed.        
        /// </summary>
        public event EventHandler SelectionChange;

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            private set
            {                 
                // Make sure value is not out of range.
                if (value < 0 || value > this.Items.Count())
                {               
                    throw new IndexOutOfRangeException();
                }
                selectedIndex = value; 
            }
        }
        private int selectedIndex = -1;

        /// <summary>
        /// Gets or sets a list of child menu options.
        /// </summary>
        public ListRepository<TextBlock> Items { get; set; }

        /// <summary>
        /// Gets or sets the sub menu options.
        /// </summary>
        public TextMenuOptions Options
        {
            get { return styleOptions; }
            set { styleOptions = value; }
        }
        private TextMenuOptions styleOptions;

        /// <summary>
        /// Add a new menu item to the control.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="click"></param>
        public void Add(string text, Vector2 position, EventHandler click = null)
        {
            TextBlock tb = new TextBlock(this.Game, text, position);
            tb.Parent = this;
            tb.Foreground = Options.Foreground;
            tb.Click += click;

            this.Items.Add(tb);
        }

        /// <summary>
        /// Update the menu along with child items.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (this.selectedIndex == -1)
            {
                if (this.Items.Count() > 0)
                    this.SetSelected(0);

                return;
            }

            // Check if something has changed.
            if (InputState.KeyPressed(Keys.Up, PlayerIndex.One, StateOptions.CurrentFavor)
                && this.selectedIndex != 0)
            {
                this.SetSelected(this.selectedIndex - 1);
            }
            else if (InputState.KeyPressed(Keys.Down, PlayerIndex.One, StateOptions.CurrentFavor)
                && this.selectedIndex != this.Items.Count() - 1)
            {
                this.SetSelected(this.selectedIndex + 1);
            }
            else if (InputState.KeyPressed(Keys.Enter, PlayerIndex.One, StateOptions.CurrentFavor))
            {
                // Raise element click event.
                UIObject.GetClick(this.Items[this.selectedIndex]);
            }

            for (int i = 0; i < this.Items.Count(); i++)
            {
                if (i != this.selectedIndex)
                {
                    // Make sure the child has the correct foreground.
                    if (!this.Items[i].IsActive
                        && this.Items[i].Foreground != this.styleOptions.InActiveForeground)
                    {
                        this.Items[i].Foreground = this.styleOptions.InActiveForeground;
                    }
                    else if (this.Items[i].IsActive
                        && this.Items[i].Foreground == this.styleOptions.InActiveForeground)
                    {
                        this.Items[i].Foreground = this.styleOptions.Foreground;
                    }
                    else
                    if (this.Items[i].Foreground != this.styleOptions.Foreground)
                    {
                        this.Items[i].Foreground = this.styleOptions.Foreground;
                    }
                }

                // Check if mouse is over the child.
                if (InputState.IsMouseOver(this.Items[i].Bounds, PlayerIndex.One))
                {
                    // Raise event if mouse is pressed.
                    if (InputState.MousePressed(MouseButtons.LeftButton, PlayerIndex.One))
                    {
                        UIObject.GetClick(this.Items[this.selectedIndex]);
                    }

                    // Don't reset the selected.
                    if (i != this.selectedIndex)
                    {
                        // Set this as selectedIndex.
                        this.SetSelected(i);
                    }

                    break;
                }
            }

            if (this.Options.SelectedEffect != null)
                this.Options.SelectedEffect.Update(gameTime);

            this.Items.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the title along with sub items.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            if (this.Options.SelectedEffect != null)
                this.Options.SelectedEffect.Draw(sprite, gameTime);

            this.Items.Draw(sprite, gameTime);
            base.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Raised when a child is given focus.
        /// </summary>
        /// <param name="e">Provides useful event data.</param>
        protected virtual void OnSelectionChange(EventArgs e)
        {
            if (this.SelectionChange != null)
            {
                this.SelectionChange(this, e);
            }

            // Check if we should play a sound.
            if (this.styleOptions.SelectedSoundChange != null)
            {
                this.styleOptions.SelectedSoundChange.Play();
            }
        }

        /// <summary>
        /// Set a new child to have focus.
        /// </summary>
        /// <param name="index">Index to be set.</param>
        private void SetSelected(int index)
        {
            if (index < 0 || index > this.Items.Count() - 1)
            {
                throw new IndexOutOfRangeException();
            }

            // Check if the selectedIndex has not been set. If it has been set then
            // convert selectedIndex back to original before setting new.
            if (this.selectedIndex != -1)
            {
                // Reset the previous selected element foreground.
                this.Items[this.selectedIndex].Foreground = this.styleOptions.Foreground;
            }

            // Set the new foreground.
            this.Items[index].Foreground = this.styleOptions.SelectedForeground; 

            if (this.Options.SelectedEffect != null)
                this.Options.SelectedEffect.Text = this.Items[index];

            // Set the index.
            this.selectedIndex = index;

            // Invoke.
            OnSelectionChange(EventArgs.Empty);
        }
    }
}
