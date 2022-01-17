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
        // собственно список, пока пустой
        List<Particle> particles = new List<Particle>();
        public Form1()
        {
            InitializeComponent();
            // привязал изображение
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            // генерирую 500 частиц
            for (var i = 0; i < 500; ++i)
            {
                var particle = new Particle();
                // переношу частицы в центр изображения
                particle.X = picDisplay.Image.Width / 3;
                particle.Y = picDisplay.Image.Height / 2;
                // добавляю список
                particles.Add(particle);
            }

        }

        private void picDicplay_Click(object sender, EventArgs e)
        {

        }
    }
}
