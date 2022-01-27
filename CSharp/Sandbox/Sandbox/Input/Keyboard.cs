using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Sandbox.Input
{
    internal class Keyboard
    {
        Dictionary<SFML.Window.Keyboard.Key, bool> KeyState = new Dictionary<SFML.Window.Keyboard.Key, bool>();
        
        private RenderWindow m_WinRef;
        private SFML.Window.Keyboard.Key m_closeKey;

        public Keyboard(RenderWindow renderWindow, SFML.Window.Keyboard.Key closeKey = SFML.Window.Keyboard.Key.Escape)
        {
            InitiateKeyboardState();

            m_WinRef = renderWindow;
            m_closeKey = closeKey;
        }

        public bool GetKey(SFML.Window.Keyboard.Key key)
        {
            if (KeyState.ContainsKey(key))
            {
                return KeyState[key];
            }

            return false;
        }
        private void InitiateKeyboardState()
        {
            KeyState.Add(SFML.Window.Keyboard.Key.W, false);
            KeyState.Add(SFML.Window.Keyboard.Key.A, false);
            KeyState.Add(SFML.Window.Keyboard.Key.S, false);
            KeyState.Add(SFML.Window.Keyboard.Key.D, false);

            KeyState.Add(SFML.Window.Keyboard.Key.Up, false);
            KeyState.Add(SFML.Window.Keyboard.Key.Left, false);
            KeyState.Add(SFML.Window.Keyboard.Key.Down, false);
            KeyState.Add(SFML.Window.Keyboard.Key.Right, false);

            KeyState.Add(SFML.Window.Keyboard.Key.Escape, false);
        }

        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (KeyState.ContainsKey(e.Code))
            {
                KeyState[e.Code] = true;
            }

            if (e.Code == m_closeKey)
            {
                CloseWindow();
            }
        }

        public void OnKeyReleased(object sender, KeyEventArgs e)
        {
            if (KeyState.ContainsKey(e.Code))
            {
                KeyState[e.Code] = false;
            }
        }

        public void OnWindowClosed(object sender, EventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            m_WinRef?.Close();
        }
    }
}
