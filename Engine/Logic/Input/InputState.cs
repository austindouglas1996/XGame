using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGameEngine.Logic.Input
{
    /// <summary>
    /// Helps with handling game input.
    /// </summary>
    public class InputState
    {
        /// <summary>
        /// Holds the current state controllers.
        /// </summary>
        private static InputController[] currentState = new InputController[4]
        {
            new InputController(PlayerIndex.One),
            new InputController(PlayerIndex.Two),
            new InputController(PlayerIndex.Three),
            new InputController(PlayerIndex.Four)
        };

        /// <summary>
        /// Holds the previous state controllers.
        /// </summary>
        private static InputController[] previousState = new InputController[4]
        {
            new InputController(PlayerIndex.One),
            new InputController(PlayerIndex.Two),
            new InputController(PlayerIndex.Three),
            new InputController(PlayerIndex.Four)
        };

        /// <summary>
        /// Gets the current gamepad state.
        /// </summary>
        /// <param name="player">The player to retrieve the data from.</param>
        public static GamePadState CurrentGamePad(PlayerIndex player = PlayerIndex.One)
        {
            return currentState[(int)player].GamePad;
        }

        /// <summary>
        /// Gets the previous gamepad state.
        /// </summary>
        /// <param name="player">The player to retrieve the data from.</param>
        public static GamePadState PreviousGamePad(PlayerIndex player = PlayerIndex.One)
        {
            return previousState[(int)player].GamePad;
        }

        /// <summary>
        /// Gets the current keyboard state.
        /// </summary>
        /// <param name="player">The player to retrieve the data from.</param>
        public static KeyboardState CurrentKeyBoard(PlayerIndex player = PlayerIndex.One)
        {
            return currentState[(int)player].Keyboard;
        }

        /// <summary>
        /// Gets the previous keyboard state.
        /// </summary>
        /// <param name="player">The player to retrieve the data from.</param>
        public static KeyboardState PreviousKeyBoard(PlayerIndex player = PlayerIndex.One)
        {
            return previousState[(int)player].Keyboard;
        }

        /// <summary>
        /// Gets the current mouse state.
        /// </summary>
        public static MouseState Mouse
        {
            get { return currentState[0].Mouse; }
        }

        /// <summary>
        /// Gets the mouse position as a <see cref="Point"/>.
        /// </summary>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <returns>Point</returns>
        public static Point MousePosition(PlayerIndex playerIndex = PlayerIndex.One)
        {
            return new Point
                (currentState[(int)playerIndex].Mouse.X, currentState[(int)playerIndex].Mouse.Y);
        }

        /// <summary>
        /// Gets the distance the mouse has travelled since the last update.
        /// </summary>
        public static Point MouseDistance
        {
            get { return mouseDistance; }
        }
        private static Point mouseDistance = new Point(0, 0);

        /// <summary>
        /// Gets a bool indicicating whether the mouse is within a rectangle.
        /// </summary>
        /// <param name="bounds">Bounds for the mouse to be over.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <returns>bool</returns>
        public static bool IsMouseOver(Rectangle bounds, PlayerIndex playerIndex)
        {
            return bounds.Contains(MousePosition(playerIndex));
        }

        /// <summary>
        /// Gets a bool indicating whether a button is pressed.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool ButtonPressed(Buttons button, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            return GetButtonBool(option, playerIndex, button, ButtonState.Pressed);
        }

        /// <summary>
        /// Gets a bool indicating whether a button array is pressed. If any one button is pressed
        /// true is returned.
        /// </summary>
        /// <param name="button">Array of buttons to check.</param>
        /// <param name="playerIndex">Index of the player</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns></returns>
        public static bool ButtonPressed(Buttons[] button, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            foreach (Buttons b in button)
                if (GetButtonBool(option, playerIndex, b, ButtonState.Pressed) == true)
                    return true;

            return false;
        }

        /// <summary>
        /// Gets a bool indicating whether a button is released.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool ButtonReleased(Buttons button, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            return GetButtonBool(option, playerIndex, button, ButtonState.Released);
        }

        /// <summary>
        /// Gets a bool indicating whether a button array is released.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool ButtonReleased(Buttons[] button, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            foreach (Buttons b in button)
                if (GetButtonBool(option, playerIndex, b, ButtonState.Released) == false)
                    return true;

            return false;
        }

        /// <summary>
        /// Gets a bool indicating whether a mouse button is currently over and pressed over a boundry.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="button"></param>
        /// <param name="playerIndex"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static bool IsMouseClicked(Rectangle bounds, MouseButtons button, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            // Check is mouse is over and pressed.
            if (IsMouseOver(bounds, playerIndex)
                && MousePressed(button, playerIndex, option))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a bool indicating whether a key is pressed.
        /// </summary>
        /// <param name="key">Key to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool KeyPressed(Keys key, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            return GetKeyBool(option, playerIndex, key, KeyState.Down);
        }

        /// <summary>
        /// Gets a bool indicating whether a key array is pressed.
        /// </summary>
        /// <param name="key">Key to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool KeyPressed(Keys[] keys, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            foreach (var k in keys)
            {
                if (GetKeyBool(option, playerIndex, k, KeyState.Down))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a bool indicating whether a key is released.
        /// </summary>
        /// <param name="key">Key to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool KeyReleased(Keys key, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            return GetKeyBool(option, playerIndex, key, KeyState.Up);
        }

        /// <summary>
        /// Gets a bool indicating whether a key array is released.
        /// </summary>
        /// <param name="key">Key to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool KeyReleased(Keys[] keys, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            foreach (var k in keys)
            {
                if (GetKeyBool(option, playerIndex, k, KeyState.Up))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a bool indicating whether a mouse button is pressed.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool MousePressed(MouseButtons button, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Current)
        {
            switch (option)
            {
                case StateOptions.Current:
                    return currentState[(int)playerIndex].GetMousePressed(button);
                case StateOptions.Previous:
                    return previousState[(int)playerIndex].GetMousePressed(button);
                default:
                    throw new InvalidOperationException
                        ("CurrentOnly and PreviousOnly supported.");
            }
        }

        /// <summary>
        /// Gets a bool indicating whether a mouse button is released.
        /// </summary>
        /// <param name="button">Button to be checked.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="option">Preffered checking type.</param>
        /// <returns>bool</returns>
        public static bool MouseReleased(MouseButtons button, PlayerIndex playerIndex,
            StateOptions option = StateOptions.Previous)
        {
            return !MousePressed(button, playerIndex, option);
        }

        /// <summary>
        /// Converts a key into a string.
        /// </summary>
        /// <param name="key">Key to be converted.</param>
        /// <returns>string</returns>
        public static string GetKey(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    return "a";
                case Keys.B:
                    return "b";
                case Keys.C:
                    return "c";
                case Keys.D:
                    return "d";
                case Keys.E:
                    return "e";
                case Keys.F:
                    return "f";
                case Keys.G:
                    return "g";
                case Keys.H:
                    return "h";
                case Keys.I:
                    return "i";
                case Keys.J:
                    return "j";
                case Keys.K:
                    return "k";
                case Keys.L:
                    return "l";
                case Keys.M:
                    return "m";
                case Keys.N:
                    return "n";
                case Keys.O:
                    return "o";
                case Keys.P:
                    return "p";
                case Keys.Q:
                    return "q";
                case Keys.R:
                    return "r";
                case Keys.S:
                    return "s";
                case Keys.T:
                    return "t";
                case Keys.U:
                    return "u";
                case Keys.V:
                    return "v";
                case Keys.W:
                    return "w";
                case Keys.X:
                    return "x";
                case Keys.Z:
                    return "z";
                case Keys.NumPad0:
                    return "0";
                case Keys.NumPad1:
                    return "1";
                case Keys.NumPad2:
                    return "2";
                case Keys.NumPad3:
                    return "3";
                case Keys.NumPad4:
                    return "4";
                case Keys.NumPad5:
                    return "5";
                case Keys.NumPad6:
                    return "6";
                case Keys.NumPad7:
                    return "7";
                case Keys.NumPad8:
                    return "8";
                case Keys.NumPad9:
                    return "9";
                case Keys.Space:
                    return " ";
            }

            return string.Empty;
        }

        /// <summary>
        /// Update the game input.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public static void Update(GameTime gameTime)
        {
            // Set previous state.
            previousState = currentState;

            // Initialize new state.
            currentState = new InputController[4]
            {
                new InputController(PlayerIndex.One),
                new InputController(PlayerIndex.Two),
                new InputController(PlayerIndex.Three),
                new InputController(PlayerIndex.Four)
            };
        }

        /// <summary>
        /// Gets a bool depending on check type.
        /// </summary>
        /// <param name="option">Option to be checked on the button.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="button">Button to be checked.</param>
        /// <param name="prefferedState">Preffered button state.</param>
        /// <returns>bool</returns>
        private static bool GetButtonBool(StateOptions option, PlayerIndex playerIndex,
            Buttons button, ButtonState prefferedState)
        {
            switch (option)
            {
                // Check both states but in the current favor.
                case StateOptions.CurrentFavor:
                    if (currentState[(int)playerIndex].GetButtonPressed(button)
                        && previousState[(int)playerIndex].GetButtonReleased(button)
                        && prefferedState == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else if (currentState[(int)playerIndex].GetButtonPressed(button)
                        && previousState[(int)playerIndex].GetButtonReleased(button)
                        && prefferedState == ButtonState.Released)
                    {
                        return true;
                    }

                    return false;

                // Check both states but in the previous favor.
                case StateOptions.PreviousFavor:
                    if (currentState[(int)playerIndex].GetButtonReleased(button)
                        && previousState[(int)playerIndex].GetButtonReleased(button)
                        && prefferedState == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else if (currentState[(int)playerIndex].GetButtonPressed(button)
                        && previousState[(int)playerIndex].GetButtonReleased(button)
                        && prefferedState == ButtonState.Released)
                    {
                        return true;
                    }

                    return false;

                // Check the current state only.
                case StateOptions.Current:
                    if (currentState[(int)playerIndex].GetButtonPressed(button)
                        && prefferedState == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else if (currentState[(int)playerIndex].GetButtonReleased(button)
                        && prefferedState == ButtonState.Released)
                    {
                        return true;
                    }

                    return false;

                // Check the previous state only.
                case StateOptions.Previous:
                    if (previousState[(int)playerIndex].GetButtonPressed(button)
                        && prefferedState == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else if (previousState[(int)playerIndex].GetButtonReleased(button)
                        && prefferedState == ButtonState.Released)
                    {
                        return true;
                    }

                    return false;
                default:
                    throw new InvalidOperationException
                        ("Option not supported.");
            }
        }

        /// <summary>
        /// Gets a bool depending on check type.
        /// </summary>
        /// <param name="option">Option to be checked on the button.</param>
        /// <param name="playerIndex">The player for the button to correspond to.</param>
        /// <param name="key">Key to be checked.</param>
        /// <param name="prefferedState">Preffered key state.</param>
        /// <returns>bool</returns>
        private static bool GetKeyBool(StateOptions option, PlayerIndex playerIndex,
            Keys key, KeyState prefferedState)
        {
            switch (option)
            {
                case StateOptions.CurrentFavor:
                    if (currentState[(int)playerIndex].GetKeyPressed(key)
                        && previousState[(int)playerIndex].GetKeyReleased(key)
                        && prefferedState == KeyState.Down)
                    {
                        return true;
                    }
                    else if (currentState[(int)playerIndex].GetKeyReleased(key)
                        && previousState[(int)playerIndex].GetKeyPressed(key)
                        && prefferedState == KeyState.Up)
                    {
                        return true;
                    }

                    return false;
                case StateOptions.PreviousFavor:
                    if (!currentState[(int)playerIndex].GetKeyReleased(key)
                        && previousState[(int)playerIndex].GetKeyPressed(key)
                        && prefferedState == KeyState.Down)
                    {
                        return true;
                    }
                    else if (!currentState[(int)playerIndex].GetKeyReleased(key)
                        && previousState[(int)playerIndex].GetKeyReleased(key)
                        && prefferedState == KeyState.Up)
                    {
                        return true;
                    }

                    return false;
                case StateOptions.Current:
                    if (currentState[(int)playerIndex].GetKeyPressed(key)
                        && prefferedState == KeyState.Down)
                    {
                        return true;
                    }
                    else if (currentState[(int)playerIndex].GetKeyReleased(key)
                        && prefferedState == KeyState.Up)
                    {
                        return true;
                    }

                    return false;
                case StateOptions.Previous:
                    if (previousState[(int)playerIndex].GetKeyPressed(key)
                        && prefferedState == KeyState.Down)
                    {
                        return true;
                    }
                    else if (previousState[(int)playerIndex].GetKeyReleased(key)
                        && prefferedState == KeyState.Up)
                    {
                        return true;
                    }

                    return false;
                default:
                    throw new InvalidOperationException
                        ("Option not supported");
            }
        }  
    }
}
