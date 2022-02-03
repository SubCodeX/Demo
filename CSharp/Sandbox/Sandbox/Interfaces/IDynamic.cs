using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Interfaces
{
    internal interface IDynamic
    {
        bool IsCollidable { get; }

        void Update(float deltaTime);
    }
}
