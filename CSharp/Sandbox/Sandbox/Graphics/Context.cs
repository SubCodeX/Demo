using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Graphics
{
    internal class Context
    {
        public uint Width { get; private set; } = 1920;
        public uint Height { get; private set; } = 1080;
        public uint BitsPerPixel { get; private set; } = 24;
        public uint DepthBits { get; private set; } = 24;
        public uint StencilBits { get; private set; } = 8;
        public uint AntialiasingLevel { get; private set; } = 0;
        public uint FPSLimit { get; private set; } = 30;

        public Context() { }
         public Context(uint width, uint height, uint fpsLimit)
        {
            Width = width;
            Height = height;        
            FPSLimit = fpsLimit;
        }
        public Context(uint width, uint height, uint bitsPerPixel, uint depthBits, uint stencilBits, uint antialiasingLevel, uint fpsLimit)
        {
            Width = width;
            Height = height;
            BitsPerPixel = bitsPerPixel;
            DepthBits = depthBits;
            StencilBits = stencilBits;
            AntialiasingLevel = antialiasingLevel;
            FPSLimit = fpsLimit;            
        }                
    }
}
