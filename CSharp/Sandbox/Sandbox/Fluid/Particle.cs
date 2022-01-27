using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;


namespace Sandbox.Fluid
{
    internal class Particle
    {
        public Vector2 Position { get; private set; } = new Vector2(0.0f, 0.0f); 
        public Vector2 Velocity { get; set; } = new Vector2(0.0f, 0.0f);
        public Vector2 Acceleration { get; private set; } = new Vector2(0.0f, 0.0f);
        public float Mass { get; private set; } = 1.0f;

        public Particle(Vector2 position, float mass)
        {
            Position = position;
            Mass = mass;
        }

        public void AddForce(Vector2 force)
        {
            Acceleration += force / Mass;
        }

        public void ConfineTo(int left, int top, int right, int bottom)
        {
            float x = Position.X;
            float y = Position.Y;

            if (x < left)
            {
                x = left;
            }

            if (y < top)
            {
                y = top;
            }

            if (x > right)
            {
                x = right;
            }

            if (y > bottom)
            {
                y = bottom;
            }

            Position = new Vector2(x, y);
        }

        public void Update(float deltaTime, float speedLimit)
        {
            Velocity += Acceleration;

            if (Velocity.LengthSquared() > speedLimit * speedLimit)
            {
                Velocity = Vector2.Normalize(Velocity) * speedLimit;
            }

            Position += Velocity * deltaTime;

            Acceleration = Vector2.Zero;
        }                
    }
}
