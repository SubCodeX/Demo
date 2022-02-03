using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Sandbox.Interfaces
{
    internal interface IRender
    {
        int RenderType { get; }
        List<Vector2> Render { get; }
    }
}
