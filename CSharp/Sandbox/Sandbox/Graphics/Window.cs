using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using SFML.System;
using SFML.Window;
using SFML.Graphics;


namespace Sandbox.Graphics
{ 
    internal class Window
    {         
        public uint Width { get; private set; } = 1920;
        public uint Height { get; private set; } = 1080;
        public Styles Style { get; private set; } = Styles.Default;
        public string Title { get; private set; } = "SubCodeX Sandbox";

        private RenderWindow m_RenderWindow; 
        private Context Context { get; set; }

        SFML.System.Clock clock = new SFML.System.Clock();

        public Window()
        {
            Context = new Context();

            InitializeRenderWindow();
        }

        public Window(uint width, uint height, Styles style, string title, uint fpsLimit = 30)
        {
            Width = width;
            Height = height;
            Style = style;
            Title = title;

            Context = new Context(Width, Height, fpsLimit);

            InitializeRenderWindow();
        }

        private void InitializeRenderWindow()
        {
            m_RenderWindow = new RenderWindow(new VideoMode(Context.Width, Context.Height, Context.BitsPerPixel), Title, Style, new ContextSettings(Context.DepthBits, Context.StencilBits, Context.AntialiasingLevel, 4, 6, ContextSettings.Attribute.Default, false));
            if (Context.FPSLimit > 0) m_RenderWindow.SetFramerateLimit(Context.FPSLimit);
        }

        public void Subscribe(ref Input.Keyboard k)
        {
            if (k != null)
            {
                m_RenderWindow.Closed += new EventHandler(k.OnWindowClosed);
                m_RenderWindow.KeyPressed += new EventHandler<KeyEventArgs>(k.OnKeyPressed);
                m_RenderWindow.KeyReleased += new EventHandler<KeyEventArgs>(k.OnKeyReleased);
            }
        }

        internal void Events()
        {
            m_RenderWindow.DispatchEvents();
        }
        internal void Clear()
        {
            m_RenderWindow?.Clear(new Color(32, 32, 48));
        }

        public void Render(List<Vector2> positions)
        {            
            m_RenderWindow.Draw(positions.Select(p => new Vertex(new Vector2f(p.X, p.Y), Color.White)).ToArray(), PrimitiveType.Points);
        }

        public float Time
        {
            get
            {
                float dT = clock.ElapsedTime.AsSeconds();
                clock.Restart();
                return dT;
            }
        }

        internal void Display()
        {
            m_RenderWindow?.Display();
        }

        public void Unsubscribe(ref Input.Keyboard k)
        {
            if (k != null)
            {
                m_RenderWindow.Closed -= new EventHandler(k.OnWindowClosed);
                m_RenderWindow.KeyPressed -= new EventHandler<KeyEventArgs>(k.OnKeyPressed);
                m_RenderWindow.KeyReleased -= new EventHandler<KeyEventArgs>(k.OnKeyReleased);
            }
        }

        public ref RenderWindow RenderWindow
        {
            get
            {
                return ref m_RenderWindow;
            }
        }
    }
}
