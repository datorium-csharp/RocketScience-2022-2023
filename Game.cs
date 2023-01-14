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
using System.Net.Sockets;

namespace RocketScience
{
    public partial class Game : Form
    {
        Timer gameTimer = null;
        Timer rocketTimer = null;
        int horVelocity = 0;
        int rocketImageCounter = 0;

        List<Asteroid> asteroids = new List<Asteroid>();
        List<Bullet> bullets = new List<Bullet>();

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

            this.BackColor = Color.Black;

            InitializeAsteroids();
        }

        private void InitializeAsteroids()
        {
            var asteroid = new Asteroid(this);
            asteroid.Top = 100;
            asteroid.Left = 300;
            this.Controls.Add(asteroid);
            asteroids.Add(asteroid);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            rocket.Left += horVelocity;
            CollisionCheck();
            BulletTopCollision();
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
            else if(e.KeyCode == Keys.Space)
            {
                FireBullet();
            }
        }

        private void FireBullet()
        {
            Bullet bullet = new Bullet();
            bullet.Left = rocket.Left + rocket.Width / 2;
            bullet.Top = rocket.Top;
            this.Controls.Add(bullet);
            bullets.Add(bullet);
        }

        private void CollisionCheck()
        {
            if (asteroids.Count == 0 || bullets.Count == 0) return;
            try
            {   
                foreach (var asteroid in asteroids)
                {
                    foreach (var bullet in bullets)
                    {
                        if (asteroid.Bounds.IntersectsWith(bullet.Bounds))
                        {
                            //collision detected
                            //we should remove both
                            //asteroid
                            //and the bullet

                            asteroid.Explode();
                            asteroids.Remove(asteroid);
                            this.Controls.Remove(bullet);
                            bullets.Remove(bullet);
                            if (asteroids.Count == 0 || bullets.Count == 0) return;
                        }
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }            
        }

        private void BulletTopCollision()
        {
            if (bullets.Count == 0) return;
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (bullets[i].Top <= 0)
                {
                    var bullet = bullets[i];
                    this.Controls.Remove(bullet);
                    bullets.RemoveAt(i);
                    bullet.Dispose();
                }
            }
        }

        

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
