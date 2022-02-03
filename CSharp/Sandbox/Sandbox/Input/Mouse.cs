using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using SFML.Graphics;

namespace Sandbox.Input
{
    public class Mouse
    {
        public Vector2 Position { get; set; }

        private RenderWindow m_RenderWindow;

        Dictionary<SFML.Window.Mouse.Button, bool> ButtonState = new Dictionary<SFML.Window.Mouse.Button, bool>();

        public Mouse(RenderWindow renderWindow)
        {
            InitiateMouseButtonState();
         
            m_RenderWindow = renderWindow;
            Position = new Vector2(m_RenderWindow.Size.X / 2.0f, m_RenderWindow.Size.Y / 2.0f);
        }

        public bool GetButton(SFML.Window.Mouse.Button button)
        {
            if (ButtonState.ContainsKey(button))
            {
                return ButtonState[button];
            }

            return false;
        }

        private void InitiateMouseButtonState()
        {
            ButtonState.Add(SFML.Window.Mouse.Button.Left, false);
            ButtonState.Add(SFML.Window.Mouse.Button.Middle, false);
            ButtonState.Add(SFML.Window.Mouse.Button.Right, false);
        }

        public (float, float) GetPositionXY()
        {
            return (Position.X, Position.Y);
        }
        internal void OnKeyPressed(object sender, MouseButtonEventArgs e)
        {
            if(ButtonState.ContainsKey(e.Button))
            {
                ButtonState[e.Button] = true;
            }
        }

        internal void OnKeyReleased(object sender, MouseButtonEventArgs e)
        {
            if (ButtonState.ContainsKey(e.Button))
            {
                ButtonState[e.Button] = false;
            }
        }

        internal void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            Position = new Vector2(e.X, e.Y);
        }
    }
}
