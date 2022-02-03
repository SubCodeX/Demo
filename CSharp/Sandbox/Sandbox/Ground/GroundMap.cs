using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DotnetNoise;
using SFML.System;
using SFML.Graphics;

using System.Numerics;

namespace Sandbox.Ground
{
    public class GroundMap
    {
        private float[,] m_Bedrock = null;
        private Vector2[,] m_DownhillGradient = null;

        private int m_Width;
        private int m_Height;

        private bool m_Cyclic;
        private bool Initialized { get; set; } = false;

        Vertex[] vertices = null;// new Vertex[m_Width * m_Height];
        Vertex[] gradients = null; // new Vertex[((m_Width / gradientResolution) * (m_Height * gradientResolution)) * 2];

        public GroundMap(int width, int height, float seed, uint detail, float scale, bool cyclic = false)
        {
            m_Width = width;
            m_Height = height;
            m_Cyclic = cyclic;

            Initialized = GenerateBedrock(seed, detail, scale);            
        }

        public bool GenerateBedrock(float seed, uint detail, float scale)
        {
            Console.WriteLine("Generating GroundMap...");
            if (!Initialized)
            {
                try
                {
                    m_Bedrock = new float[m_Width, m_Height];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    return false;
                }
            }

            FastNoise noiseGenerator = new FastNoise();
            
            for (int yIndex = 0; yIndex < m_Height; yIndex++)
            {
                if (yIndex % (m_Height / 100) == 0)
                {
                    float percentage = (float)yIndex / (float)m_Height * 100.0f;
                    Console.WriteLine(percentage + "%..");
                }
                for (int xIndex = 0; xIndex < m_Width; xIndex++)
                {                    
                    float volume = 1.0f;
                    float octave = 0.0625f / scale;

                    m_Bedrock[xIndex, yIndex] = 0.0f;

                    for (int d = 0; d < detail; d++)
                    {
                        m_Bedrock[xIndex, yIndex] += volume * noiseGenerator.GetSimplex(xIndex * octave, yIndex * octave, seed);
                        volume /= 2.0f;
                        octave *= 2.0f;
                    }
                    
                    m_Bedrock[xIndex, yIndex] = m_Bedrock[xIndex, yIndex] > 1.0f ? 1.0f : m_Bedrock[xIndex, yIndex];
                    m_Bedrock[xIndex, yIndex] = m_Bedrock[xIndex, yIndex] < 0.0f ? 0.0f : m_Bedrock[xIndex, yIndex];

                }
            }
            Console.WriteLine("... Done!");

            GenerateDownhillGradient();

            return true;            
        }

        public void GenerateDownhillGradient()
        {
            Console.WriteLine("Generating DownHillGradientMap...");
            m_DownhillGradient = new Vector2[m_Width, m_Height];

            for (int yIndex = 0; yIndex < m_Height; yIndex++)
            {
                if (yIndex % (m_Height / 100) == 0)
                {
                    float percentage = (float)yIndex / (float)m_Height * 100.0f;
                    Console.WriteLine(percentage + "%..");
                }
                for (int xIndex = 0; xIndex < m_Width; xIndex++)
                {
                    if (xIndex == 0 || xIndex == m_Width - 1 || yIndex == 0 || yIndex == m_Height - 1)
                    {
                        m_DownhillGradient[xIndex, yIndex] = new Vector2(0.0f, 0.0f);
                        continue;
                    }

                    Vector2 newVec = Vector2.Zero;

                    newVec += new Vector2(-1.0f, +0.0f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex - 1, yIndex]);
                    newVec += new Vector2(+1.0f, +0.0f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex + 1, yIndex]);

                    newVec += new Vector2(+0.0f, -1.0f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex, yIndex - 1]);
                    newVec += new Vector2(+0.0f, +1.0f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex, yIndex + 1]);

                    //newVec += new Vector2(-1.0f / 1.4142f, -1.0f / 1.4142f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex - 1, yIndex - 1]);
                    //newVec += new Vector2(+1.0f / 1.4142f, -1.0f / 1.4142f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex + 1, yIndex - 1]);

                    //newVec += new Vector2(-1.0f / 1.4142f, +1.0f / 1.4142f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex - 1, yIndex + 1]);
                    //newVec += new Vector2(+1.0f / 1.4142f, +1.0f / 1.4142f) * (m_Bedrock[xIndex, yIndex] - m_Bedrock[xIndex + 1, yIndex + 1]);

                    m_DownhillGradient[xIndex, yIndex] = newVec;
                }
            }
            Console.WriteLine("... Done!");
        }

        public void Render(ref RenderWindow renderWindow, int gradientResolution, float scale)
        {
            if (vertices == null)
            {
                vertices = new Vertex[m_Width * m_Height];

                for (int yIndex = 0; yIndex < m_Height; yIndex++)
                {                    
                    for (int xIndex = 0; xIndex < m_Width; xIndex++)
                    {
                        Color C = new Color(0, 0, 0);

                        float height = m_Bedrock[xIndex, yIndex];

                        if (height > 0.95f) C = new Color(255, 255, 255);
                        if (height < 0.95f) C = new Color(127, 127, 127);
                        if (height < 0.50f) C = new Color(127, 255, 127);
                        if (height < 0.20f) C = new Color(0, 127, 0);
                        if (height < 0.10f) C = new Color(127, 127, 0);
                        if (height < 0.05f) C = new Color(0, 0, 127);
                        if (height == 0.0f) C = new Color(0, 0, 64);

                        Vertex v = new Vertex(new Vector2f(xIndex * scale, yIndex * scale), C);

                        vertices[xIndex + (yIndex * m_Width)] = v;
                    }
                }
            }

            if (gradients == null)
            {
                gradients = new Vertex[((m_Width / gradientResolution) * (m_Height / gradientResolution)) * 2];

                for (int yIndex = 0; yIndex < m_Height; yIndex += gradientResolution)
                {
                    for (int xIndex = 0; xIndex < m_Width; xIndex += gradientResolution)
                    {
                        int gX = xIndex / gradientResolution;
                        int gY = yIndex / gradientResolution;
                        int gIndex = (gX + (gY * (m_Width / gradientResolution))) * 2;

                        Vector2 v = Vector2.Normalize(m_DownhillGradient[xIndex, yIndex]) * gradientResolution;

                        gradients[gIndex] = new Vertex(new Vector2f(xIndex * scale, yIndex * scale), new Color(255, 255, 255, 0));
                        gradients[gIndex + 1] = new Vertex(new Vector2f(xIndex * scale + v.X * scale, yIndex * scale + v.Y * scale), new Color(255, 255, 255, 255));
                    }
                }
            }

            renderWindow.Draw(vertices, PrimitiveType.Points);
            renderWindow.Draw(gradients, PrimitiveType.Lines);
        }
    }
}
