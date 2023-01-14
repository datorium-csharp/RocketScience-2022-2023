using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketScience
{
    class Bullet : PictureBox
    {
        Timer bulletTimer = null;
        int verVelocity = -5;

        public Bullet()
        {
            this.Width = 2;
            this.Height = 10;
            this.BackColor = Color.Red;

            InitializeBullet();
        }

        private void InitializeBullet()
        {
            bulletTimer = new Timer();
            bulletTimer.Interval = 10;
            bulletTimer.Tick += BulletTimer_Tick;
            bulletTimer.Start();
        }

        private void BulletTimer_Tick(object sender, EventArgs e)
        {
            this.Top += verVelocity;
        }

    }
}
