using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using SFML.System;

using Sandbox.Graphics;
using Sandbox.Interfaces;


namespace Sandbox.Scenes
{
    internal class Scene
    {
        private List<IEntity> m_Entities { get; set; }

        private Vector2 m_SceneSize { get; set; }
        private Viewport m_Viewport { get; set; }

        Clock m_Timer = new SFML.System.Clock();


        public Scene(Vector2 sceneSize, Viewport viewport)
        {
            m_Entities = new List<IEntity>();

            m_SceneSize = sceneSize;
            m_Viewport = viewport;
        }

        public Scene (float sceneWidth, float sceneHeight, float viewportWidth, float viewportHeight)
        {
            m_Entities = new List<IEntity>();

            m_SceneSize = new Vector2(sceneWidth, sceneHeight);
            m_Viewport = new Viewport(new Vector2(viewportWidth, viewportHeight), 1.0f, new Vector2(viewportWidth / 2.0f, viewportHeight / 2.0f));
        }

        public Scene(Vector2 sceneSize, Vector2 viewportSize)
        {
            m_Entities = new List<IEntity>();

            m_SceneSize = sceneSize;
            m_Viewport = new Viewport(viewportSize, 1.0f, viewportSize / 2.0f);
        }

        public void Add(IEntity E)
        {
            m_Entities.Add(E);
        }

        public List<IRender> GetRenderables()
        {
            return m_Entities.Where(s => s is IRender).Select(s => s as IRender).ToList();
        }

        private float Time(float multiplier = 1.0f)
        {
            float dT = m_Timer.ElapsedTime.AsSeconds();
            m_Timer.Restart();
            return dT * multiplier;
            
        }

        public void Update(float timeMultiplier)
        {
            foreach (IEntity E in m_Entities.Where(e => e.IsActive))
            {
                E.Update(Time(timeMultiplier));
            }
        }       

        public IEntity GetEntityByName(string name)
        {
            return m_Entities.First(e => e.Name == name);
        }
        
    }
}
