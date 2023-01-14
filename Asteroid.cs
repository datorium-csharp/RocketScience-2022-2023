using RocketScience.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketScience
{
    class Asteroid : PictureBox
    {
        int expImageCounter = 0;
        Timer explosionTimer = null;
        Game mainGame = null;

        public Asteroid(Game game)
        {
            this.BackColor = Color.Orange;
            this.Width = 120;
            this.Height = 120;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            mainGame = game;
        }

        public void Explode()
        {
            explosionTimer = new Timer();
            explosionTimer.Interval = 50;
            explosionTimer.Tick += ExplosionTimer_Tick;
            explosionTimer.Start();
        }

        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
            string explosionImage = $"exp0{expImageCounter.ToString("00")}";
            this.Image = (Image)Resources.ResourceManager.GetObject(explosionImage);
            expImageCounter += 1;
            if (expImageCounter > 22)
            {
                explosionTimer.Stop();
                mainGame.Controls.Remove(this);
                this.Dispose();
            }
        }

    }
}
