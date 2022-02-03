using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Sandbox.Scenes;
using Sandbox.Interfaces;

namespace Sandbox.Fluid
{
    public class ParticleSystem : IEntity, IRender
    {

        private List<Particle> m_particles = new List<Particle>();
        private List<IForce> m_forces = new List<IForce>();
        private List<IConfine> m_confinements = new List<IConfine>();

        private int Left { get; set; }
        private int Right { get; set; }
        private int Top { get; set; }
        private int Bottom { get; set; }

        private string m_Name { get; set; }

        private bool m_Active = true;
        public bool IsActive
        {
            get
            {
                return m_Active;
            }

            set
            {
                m_Active = value;
            }                 
        }

        public int RenderType { get { return 0; } }

        public List<Vector2> Render
        {
            get
            {
                return m_particles.Select(s => s.Position).ToList();
            }
        }

        public string Name { get { return m_Name; } }

        public ParticleSystem(string name, int left, int top, int right, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;       
            m_Name = name;
        } 

        public void AddParticle(Vector2 position, float mass, Vector2 velocity)
        {
            m_particles.Add(new Particle(position, mass, velocity));
        }
        
        public void AddParticle(float positionX, float positionY, float mass, float direction, float speed)
        {
            Vector2 dir = new Vector2((float)Math.Cos(direction / 180 * Math.PI), (float)Math.Sin(direction / 180 * Math.PI));
            dir = Vector2.Normalize(dir) * speed;
            m_particles.Add(new Particle(new Vector2(positionX, positionY), mass, dir));
        }
           

        public void Update(float deltaTime, float speedLimit)
        {
            foreach (IForceSingle force in m_forces.Where(s => s is IForceSingle).Select(s => s as IForceSingle))
            {
                //foreach(Particle particle in m_particles)
                //{
                //    force.ApplyForce(particle);
                // }

                Parallel.ForEach(m_particles, particle =>
                 {
                     force.ApplyForce(particle);
                 });
            }

            foreach (IForcePair force in m_forces.Where(s => s is IForcePair).Select(s => s as IForcePair))
            {
                for(int i = 0; i < m_particles.Count - 1;i++)
                {
                    for(int j = i + 1; j < m_particles.Count;j++)
                    {
                        force.ApplyForce(m_particles[i], m_particles[j]);
                    }
                }
            }

            Parallel.ForEach(m_particles, particle =>
            {
                particle.Calculate(speedLimit);
            });

            foreach(IConfine confinement in m_confinements)
            {
                Parallel.ForEach(m_particles, particle =>
                {
                    confinement.ApplyConstraint(particle);
                });
            }

            Parallel.ForEach(m_particles, particle =>
            {
                particle.Update(deltaTime, speedLimit);
            });
        }

        public int GetParticleCount { get { return m_particles.Count; } }

        #region Forces

        public void GlobalAttraction(float strength, float innerLimit)
        {
            for (int i = 0; i < m_particles.Count - 1; i++)
            {
                for (int j = i + 1; j < m_particles.Count; j++)
                {   
                    Vector2 i2j = m_particles[j].Position - m_particles[i].Position;                    
                    float rSqr = i2j.LengthSquared();

                    if (rSqr >= innerLimit * innerLimit)
                    {
                        float force = (m_particles[i].Mass * m_particles[j].Mass * strength) / rSqr;

                        m_particles[i].AddForce(Vector2.Normalize(i2j) * force);
                        m_particles[j].AddForce(Vector2.Normalize(i2j) * -force);
                    }
                }
            }
        }

        public void NeighbourRepulsionSlow(float strength, float outerLimit, float innerLimit)
        {

            float outerLimitSqr = outerLimit * outerLimit;
            float innerLimitSqr = innerLimit * innerLimit;

            for (int i = 0; i < m_particles.Count - 1; i++)
            {
                for (int j = i + 1; j < m_particles.Count; j++)
                {
                    Vector2 i2j = m_particles[j].Position - m_particles[i].Position;
                    float rSqr = i2j.LengthSquared();

                    if (rSqr < outerLimitSqr)
                    {
                        if (rSqr < innerLimitSqr)
                        {
                            m_particles[i].Velocity = Vector2.Reflect(m_particles[i].Velocity, Vector2.Normalize(i2j));
                            m_particles[j].Velocity = Vector2.Reflect(m_particles[j].Velocity, Vector2.Normalize(Vector2.Negate(i2j)));
                        }

                        float intrusionDepth = outerLimitSqr - rSqr;
                        float force = intrusionDepth * intrusionDepth * strength;

                        m_particles[i].AddForce(Vector2.Normalize(i2j) * -force);
                        m_particles[j].AddForce(Vector2.Normalize(i2j) * force);
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

                                        m_particles[i].AddForce(Vector2.Normalize(i2j) * -force);
                                        m_particles[j].AddForce(Vector2.Normalize(i2j) * force);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

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
            
            foreach (var particle in m_particles)
            {
                grid[(int)particle.Position.X / size][(int)particle.Position.Y / size].Add(particle);
            }

            return grid;
        }

        public void AddForce(IForce force)
        {
            m_forces.Add(force);
        }

        public void AddConfinement(IConfine confinement)
        {
            m_confinements.Add(confinement);
        }
               
        public void Update(float timeMultiplier)
        {
            this.Update(timeMultiplier, 100.0f);
        }
    }
}
