using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DotnetNoise;

using Sandbox.Input;
using Sandbox.Graphics;
using Sandbox.Fluid;

using Sandbox.Ground;
using Sandbox.Scenes;



namespace Sandbox
{
    public class Sandbox
    {
        Keyboard Keyboard;
        Mouse Mouse;
        Window Window;       

        Scene Scene;
        
        public void Run()
        {
            Window = new Window(1024, 1024, SFML.Window.Styles.Close, "Sandbox", 100);

            Scene = new Scene(1024, 1024, 512, 512);

            ParticleSystem pSys = new ParticleSystem("PSYS01", 0, 0, 1024, 1024);            
            pSys.AddForce(new GlobalForce(0.0f, 0.99f, 0.05f));
            //pSys.AddForce(new GlobalForce(0.1f, 0.0f, 0.01f));
            pSys.AddConfinement(new OuterBounds(0, 0, 1000, 1000, 0.95f));
            pSys.AddConfinement(new InnerBounds(100, 100, 450, 450, 0.95f));
            pSys.AddConfinement(new InnerBounds(550, 550, 900, 900, 0.95f));
            pSys.AddConfinement(new InnerBounds(100, 550, 450, 900, 0.95f));
            pSys.AddConfinement(new InnerBounds(550, 100, 900, 450, 0.95f));
            pSys.AddConfinement(new InnerBounds(100, 800, 900, 900, 0.95f));

            Scene.Add(pSys);

            Keyboard = new Keyboard(Window.RenderWindow);
            Mouse = new Mouse(Window.RenderWindow);

            Window.Subscribe(ref Keyboard, ref Mouse);
                       
            int counter = 0;

            Random random = new Random();
                        
            while (Window.RenderWindow.IsOpen)
            {
                Window.Events();
                
                Scene.Update(1.0f);

                
                if (Mouse.GetButton(SFML.Window.Mouse.Button.Left))
                {
                    for (int i = 0; i < 10; i++)
                        (Scene.GetEntityByName("PSYS01") as ParticleSystem).AddParticle(Mouse.Position.X, Mouse.Position.Y, 1.0f, random.Next(360), 1.0f);                    
                }
                        
                
                                

                Window.Clear();
                Window.Render(Scene.GetRenderables());
                Window.Display();

                counter++;
                if (counter > 100)
                {
                    Console.WriteLine("Particle Count : " + (Scene.GetEntityByName("PSYS01") as ParticleSystem).GetParticleCount);
                    counter = 0;
                }

            }

            Window.Unsubscribe(ref Keyboard);

        }        
    }
}
