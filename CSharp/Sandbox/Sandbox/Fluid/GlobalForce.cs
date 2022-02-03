using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Sandbox.Fluid
{
    public class GlobalForce : IForceSingle
    {
        private Vector2 m_Direction;
        
        public GlobalForce(Vector2 direction)
        {
            m_Direction = direction;            
        }

        public GlobalForce(float x, float y, float length)
        {
            m_Direction = new Vector2(x, y);
            m_Direction = Vector2.Normalize(m_Direction) * length;
        }
        public void ApplyForce(Particle p1)
        {
               p1.AddForce(m_Direction);
        }        
    }
}
