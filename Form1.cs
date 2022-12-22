using RocketScience.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace RocketScience
{
    public partial class Game : Form
    {
        Timer gameTimer = null;
        Timer rocketTimer = null;
        int horVelocity = 0;
        int rocketImageCounter = 0;

        public Game()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            rocketTimer = new Timer();
            rocketTimer.Interval = 50;
            rocketTimer.Tick += RocketTimer_Tick;
            rocketTimer.Start();

            this.KeyDown += Game_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            rocket.Left += horVelocity;
        }

        private void RocketTimer_Tick(object sender, EventArgs e)
        {
            string rocketImage = $"rocket_off_00{rocketImageCounter}";
            rocket.Image = (Image)Resources.ResourceManager.GetObject(rocketImage);
            rocketImageCounter += 1;
            if (rocketImageCounter > 3) rocketImageCounter = 0;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                horVelocity = -5;
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                horVelocity = 5;
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
