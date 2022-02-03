using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Sandbox.Graphics
{
    internal class Viewport
    {
        private Vector2 m_Size;
        private float m_Scale;
        private Vector2 m_Position;

        public Viewport(Vector2 size, float scale, Vector2 position)
        {
            m_Size = size;
            m_Scale = scale;
            m_Position = position;
        }
               
        public void SetPosition(float positionX, float positionY)
        {
            m_Position.X = positionX;
            m_Position.Y = positionY;
        }

        public void SetScale(float scale)
        {
            m_Scale = scale;
        }
    }
}
