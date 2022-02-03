using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Sandbox.VectorFunctions ;

namespace Sandbox.Fluid
{
    public class OuterBounds : IConfine
    {
        private float m_Left;
        private float m_Right;
        private float m_Top;
        private float m_Bottom;
        private float m_SpeedMultiplier;
        
        public OuterBounds(float left, float top, float right, float bottom, float speedMultiplier)
        {
            m_Left = left;
            m_Top = top;
            m_Right = right;
            m_Bottom = bottom;
            m_SpeedMultiplier = speedMultiplier;
        }


        // AABB Solution - Turn into recursive func and call from Scene on all objects (or atleast constraints to begin with..)
        // The vertical bug is due to horizontal collisions and only first hit is handled, think about courners...mmmkay!?

        //public void move(int width, int height)
        //{
        //    // Add the velocity to position.
        //    x += vx;
        //    y += vy;

        //    while (0 > x || x >= width || y > 0 || y >= height)
        //    {

        //        // Same vertical bounce code as before.
        //        if (0 > x || x >= width)
        //        {
        //            vx = -vx;
        //            x = -x;
        //            if (0 > x)
        //                x += width << 1;
        //        }

        //        // Similarly for the horizontal bounce code.
        //        if (0 > y || y >= height)
        //        {
        //            vy = -vy;
        //            y = -y;
        //            if (0 > y)
        //                y += height << 1;
        //        }
        //    }
        //}

        public void ApplyConstraint(Particle p1)
        {
            float VelocityX = p1.Velocity.X;
            float VelocityY = p1.Velocity.Y;
            float newPositionX = p1.Position.X + p1.Velocity.X;
            float newPositionY = p1.Position.Y + p1.Velocity.Y;

            Vector2 Velocity = p1.Velocity;
            
            if (newPositionX < m_Left)
            {
                Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Left, m_Top), new Vector2(m_Left, m_Bottom));
                Vector2 Length = p1.Velocity;
                Vector2 Distance = Intersect - p1.Position;
                Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                newPositionX = (Intersect - LengthLeft).X;
                VelocityX *= -m_SpeedMultiplier;
            }
            if (newPositionX > m_Right)
            {
                Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Right, m_Top), new Vector2(m_Right, m_Bottom));
                Vector2 Length = p1.Velocity;
                Vector2 Distance = Intersect - p1.Position;
                Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                newPositionX = (Intersect - LengthLeft).X;
                VelocityX *= -m_SpeedMultiplier;                
            }
            if (newPositionY < m_Top)
            {
                Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Left, m_Top), new Vector2(m_Right, m_Top));
                Vector2 Length = p1.Velocity;
                Vector2 Distance = Intersect - p1.Position;
                Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                newPositionY = (Intersect - LengthLeft).Y;
                VelocityY *= -m_SpeedMultiplier;
            }
            if (newPositionY > m_Bottom)
            {
                Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Left, m_Bottom), new Vector2(m_Right, m_Bottom));
                Vector2 Length = p1.Velocity;
                Vector2 Distance = Intersect - p1.Position;
                Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                newPositionY = (Intersect - LengthLeft).Y;
                VelocityY *= -m_SpeedMultiplier;
            }

            p1.Position = new Vector2(newPositionX, newPositionY);
            p1.PositionNotSet = false;
            p1.Velocity = new Vector2(VelocityX, VelocityY);
        }
    }
}
