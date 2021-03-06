using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace egupova.kursovic
{
    public partial class Form1 : Form
    {
        Emitter emitter = new Emitter(); // добавили эмиттер
        public Form1()
        {                     
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            
            // гравитон
            emitter.impactPoints.Add(new GravityPoint
            {
                X = (float)(picDisplay.Width * 0.25),
                Y = picDisplay.Height / 2
            });

            // в центре антигравитон
            emitter.impactPoints.Add(new AntiGravityPoint
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2
            });
            

            // снова гравитон
            emitter.impactPoints.Add(new GravityPoint
            {
                X = (float)(picDisplay.Width * 0.75),
                Y = picDisplay.Height / 2
            });
            
        }
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // а тут в эмиттер передаем положение мышьки
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // тут теперь обновляем эмиттер

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.White);
                emitter.Render(g); // а тут теперь рендерим через эмиттер
            }

            picDisplay.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = ParticlePerTrack.Value;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Red_Click(object sender, EventArgs e)
        {
            emitter.FromColor = Color.Red;
        }

        private void Blue_Click(object sender, EventArgs e)
        {
            emitter.FromColor = Color.Blue;
        }

        private void Orange_Click(object sender, EventArgs e)
        {
            emitter.FromColor = Color.Green;
        }
    }
}