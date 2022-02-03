using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Fluid
{
    internal interface IForcePair : IForce
    {
        void ApplyForce(Particle p1, Particle p2);
    }
}
