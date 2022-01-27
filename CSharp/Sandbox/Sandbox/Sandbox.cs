using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DotnetNoise;

using Sandbox.Input;
using Sandbox.Graphics;
using Sandbox.Fluid;



namespace Sandbox
{
    public class Sandbox
    {
        Keyboard m_Keyboard;
        Window m_Window;
        
        public void Run()
        {
            // Prepare Window and Render Context
            m_Window = new Window(800,600, SFML.Window.Styles.Close, "Sandbox", 120);

            // Prepare Input Handlers
            m_Keyboard = new Keyboard(m_Window.RenderWindow);

            //Subscribe to Events
            m_Window.Subscribe(ref m_Keyboard);

            ParticleSystem particleSystem = new ParticleSystem(1000, 0, 0, 800, 600);

            while (m_Window.RenderWindow.IsOpen)
            {
                m_Window.Events();
                //particleSystem.GlobalForce(new System.Numerics.Vector2(0.0f, 0.1f));
                particleSystem.GlobalAttraction(100.0f, 10.0f);
                particleSystem.NeighbourRepulsionSlow(0.001f, 15.0f, 5.0f);

                particleSystem.Update(m_Window.Time, 10);
                

                particleSystem.ConfineTo(0, 0, 800, 600);

                m_Window.Clear();

                m_Window.Render(particleSystem.GetPositions());
            
                m_Window.Display();
            }

            m_Window.Unsubscribe(ref m_Keyboard);

        }        
    }
}
