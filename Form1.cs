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

            

        }
        

        // добавил функцию обновления состояния системы
        private void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.Life -= 1; // уменьшаю здоровье
                                    // если здоровье кончилось
                if (particle.Life < 0)
                {
                    // восстанавливаю здоровье
                    particle.Life = 20 + Particle.rand.Next(100);
                    // новое начальное расположение частицы — это то, куда указывает курсор
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    /* делаю рандомное направление, скорость и размер
                    particle.Direction = Particle.rand.Next(360);
                    particle.Speed = 1 + Particle.rand.Next(10);
                    */
                    particle.Radius = 2 + Particle.rand.Next(10);
                    
                    /* ЭТО ДОБАВЛЯЮ, тут сброс состояния частицы */
                    var direction = (double)Particle.rand.Next(360);
                    var speed = 1 + Particle.rand.Next(10);
                    particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
                    particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
                }
                else
                {
                    /* а это наш старый код
                    var directionInRadians = particle.Direction / 180 * Math.PI;
                    particle.X += (float)(particle.Speed * Math.Cos(directionInRadians));
                    particle.Y -= (float)(particle.Speed * Math.Sin(directionInRadians));
                    */
                    // и добавляем новый, собственно он даже проще становится, 
                    // так как теперь мы храним вектор скорости в явном виде и его не надо пересчитывать
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                }
            }
            // добавил генерацию частиц
        // генерирую не более 10 штук за тик
        for (var i = 0; i < 10; ++i)
        {
            if (particles.Count < 500) // пока частиц меньше 500 генерируем новые
            {
                    // а у тут уже наш новый класс используем
                    var particle = new Particle.ParticleColorful();
                    // ну и цвета меняем
                    particle.FromColor = Color.Brown;
                    particle.ToColor = Color.FromArgb(0, Color.Blue);
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    particles.Add(particle);
                }
            else
            {
                break; // а если частиц уже 500 штук, то ничего не генерирую
            }
        }
        }
        // добавляем переменные для хранения положения мыши
        private int MousePositionX = 0;
        private int MousePositionY = 0;
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // в обработчике заносим положение мыши в переменные для хранения положения мыши
            MousePositionX = e.X;
            MousePositionY = e.Y;
        }
        // функция рендеринга
        private void Render(Graphics g)
        {
            // утащили сюда отрисовку частиц
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }

       
       
        private void timer1_Tick(object sender, EventArgs e)
        {

            UpdateState(); // каждый тик обновляем систему

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Beige); // добавил очистку
                Render(g); // рендерим систему
            }
            // обновить picDisplay
            picDisplay.Invalidate();
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }
    }
}