using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGameEngine;
using XGameEngine.Graphics;
using XGameEngine.Graphics.GUI;
using XGameEngine.Logic;
using XGameEngine.Logic.Input;

namespace Example01.Screens
{
    /// <summary>
    /// A screen for allowing the user to select a ship. 
    /// </summary>
    public class ShipSelectionScreen : MenuGameScreen
    {
        /// <summary>
        /// Contains information on a ship texture.
        /// </summary>
        private class ShipInfo
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ShipInfo"/> class.
            /// </summary>
            /// <param name="index"></param>
            /// <param name="sprite"></param>
            public ShipInfo(int index, Sprite sprite)
            {
                this.TextureIndex = index;
                this.Sprite = sprite;
            }

            /// <summary>
            /// Index of the texture in <see cref="Sprites"/>. This value is mostly used
            /// for when pushing for the next or the previous texture.
            /// </summary>
            public int TextureIndex { get; set; }

            /// <summary>
            /// Holds the current texture.
            /// </summary>
            public Sprite Sprite { get; set; } 
        }

        /// <summary>
        /// Holds a list of ship textures.
        /// </summary>
        private List<ShipResource> Sprites = new List<ShipResource>();

        /// <summary>
        /// Holds information on the current placeholders.
        /// </summary>
        private List<ShipInfo> ShipInfos = new List<ShipInfo>();

        /// <summary>
        /// The index of the current player selected texture.
        /// </summary>
        private int SelectedIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipSelectionScreen"/> class.
        /// </summary>
        /// <param name="game"></param>
        public ShipSelectionScreen(XGame game) : base(game)
        {
        }

        /// <summary>
        /// Get the user selected texture for the player.
        /// </summary>
        public ShipResource GetSelectedShip
        {
            get
            {
                ShipResource r = Sprites[SelectedIndex];

                if (r == null)
                    throw new ArgumentException("Unable to find ship.");

                return r;
            }
        }

        /// <summary>
        /// Initialize the game content.
        /// </summary>
        public override void Initialize()
        {            
            // Create the title block. Middle of the screen.
            TextBlock title = new TextBlock(base.Game, "SELECT YOUR SHIP!", Vector2.Zero);
            title.Scale = Resources.TitleScale;
            title.Foreground = Color.White;
            title.Position = new Vector2((Game.GraphicsDevice.Viewport.Width - title.ActualWidth) / 2, 250);
            title.Background = new Color(0, 0, 0, 100);
            this.Children.Add(title);

            Border b = new Border(title);
            b.Thickness = new Thickness(20);
            b.Background = new Color(0, 0, 0, 100);
            this.Children.Add(b);

            // Create the options for the sub menu items.
            TextMenuOptions options = new TextMenuOptions();
            options.Foreground = Color.White;
            options.SelectedForeground = Resources.MenuSelectedForecolor;

            TextMenu menu = new TextMenu(this.Game);
            menu.Options = options;
            menu.Add("PLAY!", 
                new Vector2(Game.GraphicsDevice.Viewport.Width - menu.ActualWidth - 200, Game.GraphicsDevice.Viewport.Height - menu.ActualHeight - 150),
                Resources.MenuItemScale,
                this.PlayClick);

            menu.Add("BACK",
                new Vector2(Game.GraphicsDevice.Viewport.Width - menu.ActualWidth - 400, Game.GraphicsDevice.Viewport.Height - menu.ActualHeight - 150),
                Resources.MenuItemScale,
                this.BackClick);


            options.SelectedEffect = new TextEffectPulse(menu.Items[0], 0.01f, 0.2f, 0.0002f, TimeSpan.FromSeconds(0.4));

            this.Children.Add(menu);

            PrepareShipSprites();

            base.Initialize();
        }

        /// <summary>
        /// Update the rotation of the selected ship.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Go Right?
            if (InputState.KeyPressed(GlobalKeys.RightSideKey, PlayerIndex.One, StateOptions.CurrentFavor))
                this.ChangePlaceHolderPosition(1);

            // Go left?
            if (InputState.KeyPressed(GlobalKeys.LeftSideKey, PlayerIndex.One, StateOptions.CurrentFavor))
                this.ChangePlaceHolderPosition(-1);

            // Update our center ship.
            this.ShipInfos[2].Sprite.Rotation += 0.02f;

            base.Update(gameTime);
        }

        /// <summary>
        /// Go to the play game screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayClick(object sender, EventArgs e)
        {
            // We don't want the user going back here.
            base.Game.Screens.Pop();
            base.Game.Screens.Push(new PlayScreen(this.Game, this.GetSelectedShip));
        }

        /// <summary>
        /// Go back to the previous screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackClick(object sender, EventArgs e)
        {
            base.Game.Screens.Pop();
        }

        /// <summary>
        /// Add the logic for the placeholders. Set our default entries.
        /// </summary>
        private void PrepareShipSprites()
        {
            // Load initial textures.
            foreach (var shipInfo in Resources.Ships)
                Sprites.Add(shipInfo);

            // Create placeholder positions.
            List<Vector2> shipsPos = new List<Vector2>();
            shipsPos.Add(new Vector2(200, 500));
            shipsPos.Add(new Vector2(400, 500));
            shipsPos.Add(new Vector2(600, 500));
            shipsPos.Add(new Vector2(800, 500));
            shipsPos.Add(new Vector2(1000, 500));

            for (int i = 0; i < shipsPos.Count; i++)
            {
                Sprite s = new Sprite(this.Game, base.Game.EngineResource.Dummy, shipsPos[i], Color.White);
                ShipInfo ship = new ShipInfo(i, s);

                this.Children.Add(s);
                this.ShipInfos.Add(ship);
            }

            FirstPopulation();
        }

        /// <summary>
        /// Place our first textures for the placeholders.
        /// </summary>
        private void FirstPopulation()
        {
            // We wanna find a middle ground.
            double half = Math.Round((double)(Sprites.Count / 2));

            ShipInfos[0].TextureIndex = (int)(half - 2);
            ShipInfos[1].TextureIndex = (int)(half - 1);
            ShipInfos[2].TextureIndex = (int)(half);
            ShipInfos[3].TextureIndex = (int)(half + 1);
            ShipInfos[4].TextureIndex = (int)(half + 2);

            this.SelectedIndex = (int)half;

            // Force an update.
            this.ChangePlaceHolderPosition(0);
        }

        /// <summary>
        /// Change the holder position and display the new textures.
        /// </summary>
        /// <param name="amount"></param>
        private void ChangePlaceHolderPosition(int amount)
        {
            // Do not let the user go out of bounds.
            if (SelectedIndex > Sprites.Count - 2 && amount > 0 ||
                SelectedIndex <= 0 && amount < 0)
                return;

            // Update the texture indexes.
            ShipInfos[0].TextureIndex += amount;
            ShipInfos[1].TextureIndex += amount;
            ShipInfos[2].TextureIndex += amount;
            ShipInfos[3].TextureIndex += amount;
            ShipInfos[4].TextureIndex += amount;

            // Update the placeholder textures.
            foreach (var ship in ShipInfos)
                ship.Sprite.Texture = GetResourceOrDefault(ship.TextureIndex);

            // Update the selected index.
            this.SelectedIndex = ShipInfos[2].TextureIndex;
        }

        /// <summary>
        /// Grab the required texture by index or return the dummy texture.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Texture2D GetResourceOrDefault(int index)
        {
            if (index > Sprites.Count - 1 || index < 0)
            {
                return base.Game.EngineResource.Dummy;
            }
            else
            {
                return Sprites[index].Texture;
            }
        }

        protected override void UpdateInput(GameTime gameTime)
        {
        }
    }
}
