using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Interfaces;

namespace Sandbox.Scenes
{
    public interface IEntity
    {
        bool IsActive { get; set; }

        string Name { get; }

        void Update(float timeMultiplier);
    }
}
