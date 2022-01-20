using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Graphics.GUI
{
    /// <summary>
    /// Provides the style option for MenuOption.
    /// </summary>
    public class TextMenuOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMenuOptions"/> struct.
        /// </summary>
        /// <param name="foreground">Foreground color.</param>
        /// <param name="selectedForeground">Selected option foreground color.</param>
        /// <param name="font">Font to be used.</param>
        public TextMenuOptions(Color foreground, Color selectedForeground, SpriteFont font)
        {
            this.Foreground = foreground;
            this.SelectedForeground = selectedForeground;
        }

        public TextMenuOptions()
        {

        }

        /// <summary>
        /// Primary foreground color.
        /// </summary>
        public Color Foreground { get; set; }

        /// <summary>
        /// Foreground color of selected option.
        /// </summary>
        public Color SelectedForeground { get; set; }

        /// <summary>
        /// Sound played when a new option is selected.
        /// </summary>
        public SoundEffect SelectedSoundChange { get; set; }

        /// <summary>
        /// Primary effect played on the selected item.
        /// </summary>
        public TextEffect SelectedEffect { get; set; }
    }
}
