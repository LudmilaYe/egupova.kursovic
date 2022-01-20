﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace egupova.kursovic
{
    class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();
        //public List<Point> gravityPoints = new List<Point>(); // тут буду хранится точки притяжения
        public int MousePositionX;
        public int MousePositionY;
        public float GravitationX = 0;
        public float GravitationY = 0; // отключила // пусть гравитация будет силой один пиксель за такт, нам хватит
        public Color FromColor =Color.Blue;

        public int ParticlesPerTick = 1; // добавил новое поле

        public void UpdateState()
        {
            int particlesToCreate = ParticlesPerTick; // фиксируем счетчик сколько частиц нам создавать за тик
            foreach (var particle in particles)
            {

                particle.Life -= 1; // уменьшаю здоровье
                                    // если здоровье кончилось
                if (particle.Life < 0)
                {
                    particle.FromColor = FromColor;
                    /*
                    // восстанавливаю здоровье
                    particle.Life = 20 + Particle.rand.Next(100);
                    // новое начальное расположение частицы — это то, куда указывает курсор
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    // делаю рандомное направление, скорость и размер
                    particle.Direction = Particle.rand.Next(360);
                    particle.Speed = 1 + Particle.rand.Next(10);
                    
                    particle.Radius = 2 + Particle.rand.Next(10);

                    // ЭТО ДОБАВЛЯЮ, тут сброс состояния частицы //
                    var direction = (double)Particle.rand.Next(360);
                    var speed = 1 + Particle.rand.Next(10);
                    particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
                    particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
                    */
                    //ResetParticle(particle); // заменили этот блок на вызов сброса частицы 
                    if (particlesToCreate > 0)
                    {
                        /* у нас как сброс частицы равносилен созданию частицы */
                        particlesToCreate -= 1; // поэтому уменьшаем счётчик созданных частиц на 1
                        ResetParticle(particle);
                    }
                    else
                    { particle.Life = 0; } 
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

                    // сделаем сначала для одной точки
                    // и так считаем вектор притяжения к точке

                    // каждая точка по-своему воздействует на вектор скорости

                    foreach (var point in impactPoints)
                    {
                        /*
                        float gX = point.X - particle.X;
                        float gY = point.Y - particle.Y;

                        // считаем квадрат расстояния между частицей и точкой r^2
                        //float r2 = gX * gX + gY * gY;
                        float r2 = (float)Math.Max(100, gX * gX + gY * gY); // ограничил
                        float M = 100; // сила притяжения к точке, пусть 100 будет
                        
                        // пересчитываем вектор скорости с учетом притяжения к точке
                        particle.SpeedX += (gX) * M / r2;
                        particle.SpeedY += (gY) * M / r2;
                        */
                        point.ImpactParticle(particle);
                            
                        particle.X += particle.SpeedX;
                        particle.Y += particle.SpeedY;
                        // гравитация воздействует на вектор скорости, поэтому пересчитываем его
                        particle.SpeedX += GravitationX;
                        particle.SpeedY += GravitationY;
                       

                    }
                    
                }
                
            }
            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }

           
        }
        public void Render(Graphics g)
        {

            // это не трогаем
            foreach (var particle in particles)
            {

                particle.FromColor = FromColor;
                particle.Draw(g);
            }

            // рисую точки притяжения красными кружочками
            foreach (var point in impactPoints)
            {
                /*
                g.FillEllipse(
                    new SolidBrush(Color.Red),
                    point.X - 5,
                    point.Y - 5,
                    10,
                    10
                );
                */
                point.Render(g); // это добавили
            }
        }
        
        
        // добавил новый метод, виртуальным, чтобы переопределять можно было
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 20 + Particle.rand.Next(100);
            particle.X = MousePositionX;
            particle.Y = MousePositionY;

            var direction = (double)Particle.rand.Next(360);
            var speed = 1 + Particle.rand.Next(10);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = 2 + Particle.rand.Next(10);
        }
        public virtual Particle CreateParticle()
        {
            var particle = new Particle.ParticleColorful();
            particle.FromColor = FromColor;
            return particle;
        }
       
    }  

    
}
