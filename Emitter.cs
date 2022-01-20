using System;
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
                particle.FromColor = FromColor;
                if (particle.Life < 0)
                {
                    
                   
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
                    foreach (var point in impactPoints)
                    {
                       
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
                particle.FromColor = FromColor;
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
                
                point.Render(g); // это добавили
            }
        }
        
        
        // добавил новый метод, виртуальным, чтобы переопределять можно было
        public virtual void ResetParticle(Particle particle)
        {
            particle.FromColor = FromColor;
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
