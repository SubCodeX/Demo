using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;


namespace Sandbox.Fluid
{
    public class Particle
    {
        public Vector2 Position { get; set; } = new Vector2(0.0f, 0.0f);
        public bool PositionNotSet = true;
        public Vector2 Velocity { get; set; } = new Vector2(0.0f, 0.0f);
        public Vector2 Acceleration { get; set; } = new Vector2(0.0f, 0.0f);
        public float Mass { get; private set; } = 1.0f;

        public Particle(Vector2 position, float mass, Vector2 velocity)
        {
            Position = position;
            Mass = mass;
            Velocity = velocity;
        }

        public void AddForce(Vector2 force)
        {
            Acceleration += force / Mass;
        }
        
        public void Calculate(float speedLimit)
        {
            Velocity += Acceleration;
            Acceleration = Vector2.Zero;

            if (Velocity.LengthSquared() > speedLimit * speedLimit)
            {
                Velocity = Vector2.Normalize(Velocity) * speedLimit;
            }
        }

        public void Update(float deltaTime, float speedLimit)
        {
            if (PositionNotSet)
            {
                Position += Velocity * deltaTime;
            }
            PositionNotSet = false;
        }                
    }
}
