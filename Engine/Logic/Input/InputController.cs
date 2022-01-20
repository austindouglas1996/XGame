using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Logic.Input
{
    /// <summary>
    /// Provides the input controllers for input checking.
    /// </summary>
    public class InputController
    {
        /// <summary>
        /// Handles controller input.
        /// </summary>
        private GamePadState gamePad;

        /// <summary>
        /// Handles keyboard input.
        /// </summary>
        private KeyboardState keyboard;

        /// <summary>
        /// Handles mouse input.
        /// </summary>
        private MouseState mouse;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputController"/> class.
        /// </summary>
        /// <param name="playerIndex">The player to handle input.</param>
        public InputController(PlayerIndex playerIndex)
        {
            this.gamePad = Microsoft.Xna.Framework.Input.GamePad.GetState(playerIndex);
            this.keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            this.mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        /// <summary>
        /// Gets or sets the gamePad input state.
        /// </summary>
        public GamePadState GamePad
        {
            get { return this.gamePad; }
            set { this.gamePad = value; }
        }

        /// <summary>
        /// Gets or sets the keyboard input state.
        /// </summary>
        public KeyboardState Keyboard
        {
            get { return this.keyboard; }
            set { this.keyboard = value; }
        }

        /// <summary>
        /// Gets or sets the mouse input state.
        /// </summary>
        public MouseState Mouse
        {
            get { return this.mouse; }
            set { this.mouse = value; }
        }

        /// <summary>
        /// Gets a bool indicating whether a button is pressed or not.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <returns>bool</returns>
        public bool GetButtonPressed(Buttons button)
        {
            return this.GamePad.IsButtonDown(button);
        }

        /// <summary>
        /// Gets a bool indicating whether a button is released or not.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <returns>bool</returns>
        public bool GetButtonReleased(Buttons button)
        {
            return this.GamePad.IsButtonUp(button);
        }

        /// <summary>
        /// Gets a bool indicating whether a key is pressed or not.
        /// </summary>
        /// <param name="key">Key to be checked.</param>
        /// <returns>bool</returns>
        public bool GetKeyPressed(Keys key)
        {
            return this.keyboard.IsKeyDown(key);
        }

        /// <summary>
        /// Gets a bool indicating whether a key is released or not.
        /// </summary>
        /// <param name="key">Key to be checked.</param>
        /// <returns>bool</returns>
        public bool GetKeyReleased(Keys key)
        {
            return this.keyboard.IsKeyUp(key);
        }

        /// <summary>
        /// Gets a bool indicating whether a mouse button is pressed or not.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <returns>bool</returns>
        public bool GetMousePressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return this.mouse.LeftButton == ButtonState.Pressed;
                case MouseButtons.MiddleButton:
                    return this.mouse.MiddleButton == ButtonState.Pressed;
                case MouseButtons.RightButton:
                    return this.mouse.RightButton == ButtonState.Pressed;
                default:
                    throw new InvalidOperationException
                        ("Button not supported.");
            }
        }

        /// <summary>
        /// Gets a bool indicating whether a mouse button is released or not.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <returns>bool</returns>
        public bool GetMouseReleased(MouseButtons button)
        {
            return !GetMousePressed(button);
        }
    }
}
