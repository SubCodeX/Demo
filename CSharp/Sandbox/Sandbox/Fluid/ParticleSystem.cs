using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Sandbox.Fluid
{
    public class ParticleSystem
    {

        List<Particle> particles = new List<Particle>();

        private int Left { get; set; }
        private int Right { get; set; }
        private int Top { get; set; }
        private int Bottom { get; set; }

        public ParticleSystem(uint numberOfParticles, int left, int top, int right, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
            Random rand = new Random();
            for(int i = 0; i < numberOfParticles; i++)
            {
                particles.Add(new Particle(new Vector2((float)rand.Next(Left, Right), (float)rand.Next(Top, Bottom)), 1.0f));
            }
        }

        public void ConfineToSystem()
        {
            ConfineTo(Left, Top, Right, Bottom);
        }

        public void ConfineTo(int left, int top, int right, int bottom)
        {
            foreach (Particle particle in particles)
            {
                particle.ConfineTo(left, top, right, bottom);
            }
        }

        public void GlobalForce(Vector2 force)
        {
            foreach (Particle particle in particles)
            {
                particle.AddForce(force);
            }
        }

        public void Update(float deltaTime, float speedLimit)
        {
            foreach(Particle particle in particles)
            {
                particle.Update(deltaTime, speedLimit);
            }
        }
        public List<Vector2> GetPositions()
        {
            return particles.Select(p => p.Position).ToList();
        }
              

        public void GlobalAttraction(float strength, float innerLimit)
        {
            for (int i = 0; i < particles.Count - 1; i++)
            {
                for (int j = i + 1; j < particles.Count; j++)
                {   
                    Vector2 i2j = particles[j].Position - particles[i].Position;                    
                    float rSqr = i2j.LengthSquared();

                    if (rSqr >= innerLimit * innerLimit)
                    {
                        float force = (particles[i].Mass * particles[j].Mass * strength) / rSqr;

                        particles[i].AddForce(Vector2.Normalize(i2j) * force);
                        particles[j].AddForce(Vector2.Normalize(i2j) * -force);
                    }
                }
            }
        }

        public void NeighbourRepulsionSlow(float strength, float outerLimit, float innerLimit)
        {

            float outerLimitSqr = outerLimit * outerLimit;
            float innerLimitSqr = innerLimit * innerLimit;

            for (int i = 0; i < particles.Count - 1; i++)
            {
                for (int j = i + 1; j < particles.Count; j++)
                {
                    Vector2 i2j = particles[j].Position - particles[i].Position;
                    float rSqr = i2j.LengthSquared();

                    if (rSqr < outerLimitSqr)
                    {
                        if (rSqr < innerLimitSqr)
                        {
                            particles[i].Velocity = Vector2.Reflect(particles[i].Velocity, Vector2.Normalize(i2j));
                            particles[j].Velocity = Vector2.Reflect(particles[j].Velocity, Vector2.Normalize(Vector2.Negate(i2j)));
                        }

                        float intrusionDepth = outerLimitSqr - rSqr;
                        float force = intrusionDepth * intrusionDepth * strength;

                        particles[i].AddForce(Vector2.Normalize(i2j) * -force);
                        particles[j].AddForce(Vector2.Normalize(i2j) * force);
                    }
                }
            }
        }
                

        public void NeighbourRepulsion(float strength, float outerLimit, float innerLimit)
        {
            var Grid = SystemGrid((int)outerLimit * 2);

            float outerLimitSqr = outerLimit * outerLimit;
            float innerLimitSqr = innerLimit * innerLimit;

            for (int i = 1; i < Grid.Count() -1; i++)
            {
                for (int j = 1; j < Grid[i].Count() - 1; j++)
                {
                    foreach (Particle p1 in Grid[i][j])
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                foreach (Particle p2 in Grid[i + k][j + l])
                                {
                                    Vector2 i2j = p2.Position - p1.Position;
                                    float rSqr = i2j.LengthSquared();

                                    if (rSqr < outerLimitSqr)
                                    {
                                        if (rSqr < innerLimitSqr)
                                        {
                                            p1.Velocity = Vector2.Reflect(p1.Velocity, Vector2.Normalize(i2j));
                                            p2.Velocity = Vector2.Reflect(p2.Velocity, Vector2.Normalize(Vector2.Negate(i2j)));
                                        }

                                        float intrusionDepth = outerLimitSqr - rSqr;
                                        float force = intrusionDepth * intrusionDepth * strength;

                                        particles[i].AddForce(Vector2.Normalize(i2j) * -force);
                                        particles[j].AddForce(Vector2.Normalize(i2j) * force);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private List<List<List<Particle>>> SystemGrid(int size)
        {
            var grid = new List<List<List<Particle>>>();

            for (int i = 0; i < ((Right-Left)/size) + 3; i++)
            {
                grid.Add(new List<List<Particle>>());

                for (int j = 0; j < ((Bottom - Top) / size) + 3; j++)
                {
                    grid[i].Add(new List<Particle>());                    
                }
            }
            
            foreach (var particle in particles)
            {
                grid[(int)particle.Position.X / size][(int)particle.Position.Y / size].Add(particle);
            }

            return grid;
        }
    }
}
