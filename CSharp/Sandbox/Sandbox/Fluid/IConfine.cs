using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Fluid
{
    public interface IConfine
    {
        void ApplyConstraint(Particle p1);
    }
}
