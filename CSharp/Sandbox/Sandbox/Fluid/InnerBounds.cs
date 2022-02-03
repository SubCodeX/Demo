using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Sandbox.VectorFunctions;

namespace Sandbox.Fluid
{
    public class InnerBounds : IConfine
    {
        private float m_Left;
        private float m_Right;
        private float m_Top;
        private float m_Bottom;
        private float m_SpeedMultiplier;
        public InnerBounds(float left, float top, float right, float bottom, float speedMultiplier)
        {
            m_Left = left;
            m_Top = top;
            m_Right = right;
            m_Bottom = bottom;
            m_SpeedMultiplier = speedMultiplier;
        }
        public void ApplyConstraint(Particle p1)
        {
            float VelocityX = p1.Velocity.X;
            float VelocityY = p1.Velocity.Y;
            
            float newPositionX = p1.Position.X + p1.Velocity.X;
            float newPositionY = p1.Position.Y + p1.Velocity.Y;
                        

            if (newPositionX > m_Left && newPositionX < m_Right && newPositionY > m_Top && newPositionY < m_Bottom)
            {
                float deltaLeft = newPositionX - m_Left;
                float deltaRight = m_Right - newPositionX;

                float deltaTop = newPositionY - m_Top;
                float deltaBottom = m_Bottom - newPositionY;

                float deltaHorizontal = Math.Min(deltaLeft, deltaRight);
                float deltaVertical = Math.Min(deltaTop, deltaBottom);

                float deltaMin = Math.Min(deltaVertical, deltaHorizontal);

                if (deltaMin == deltaLeft)
                {
                    Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Left, m_Top), new Vector2(m_Left, m_Bottom));
                    Vector2 Length = p1.Velocity;
                    Vector2 Distance = Intersect - p1.Position;
                    Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                    newPositionX = (Intersect - LengthLeft).X;
                    VelocityX *= -m_SpeedMultiplier;
                }
                if (deltaMin == deltaRight)
                {
                    Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Right, m_Top), new Vector2(m_Right, m_Bottom));
                    Vector2 Length = p1.Velocity;
                    Vector2 Distance = Intersect - p1.Position;
                    Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                    newPositionX = (Intersect - LengthLeft).X;
                    VelocityX *= -m_SpeedMultiplier;
                }
                if (deltaMin == deltaTop)
                {
                    Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Left, m_Top), new Vector2(m_Right, m_Top));
                    Vector2 Length = p1.Velocity;
                    Vector2 Distance = Intersect - p1.Position;
                    Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                    newPositionY = (Intersect - LengthLeft).Y;
                    VelocityY *= -m_SpeedMultiplier;
                }
                if (deltaMin == deltaBottom)
                {
                    Vector2 Intersect = VectorMath.FindIntersection(p1.Position, p1.Position + p1.Velocity, new Vector2(m_Left, m_Bottom), new Vector2(m_Right, m_Bottom));
                    Vector2 Length = p1.Velocity;
                    Vector2 Distance = Intersect - p1.Position;
                    Vector2 LengthLeft = (Length - Distance) * m_SpeedMultiplier;

                    newPositionY = (Intersect - LengthLeft).Y;
                    VelocityY *= -m_SpeedMultiplier;
                }
            }

            p1.Position = new Vector2(newPositionX, newPositionY);
            p1.PositionNotSet = false;
            p1.Velocity = new Vector2(VelocityX, VelocityY);
        }
    }
}
