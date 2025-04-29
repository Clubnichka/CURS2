using KitchenSceneTao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using static KitchenSceneTao.Scene;

namespace CURS2
{
    public static class Particles
    {
        private static List<Particle> tornadoParticles = new List<Particle>();
        static float orbitAngle = 0.0f;
        static float orbitSpeed = 45.0f; // градусов в секунду
        static float orbitRadius = 1.837f;
        static float orbitCenterX = -1.5f, orbitCenterY = 0.65f, orbitCenterZ = -1.0f;
        static float tornadoCenterX = 0.0f, tornadoCenterY = 0.0f, tornadoCenterZ = 0.0f;
        static float time = 0.0f;

        public static void InitializeTornado()
        {
            Random rand = new Random();
            tornadoParticles.Clear();
            for (int i = 0; i < 200; i++) // количество частиц
            {
                Particle p = new Particle();
                p.angle = (float)(rand.NextDouble() * 360);
                p.radius = 0.2f + (float)rand.NextDouble() * 2.0f; // радиус от 0.2 до 2.2
                p.height = (float)(rand.NextDouble() * 3.0f); // высота от 0 до 3 (до потолка)
                p.speed = 50f + (float)rand.NextDouble() * 100f; // скорость вращения
                tornadoParticles.Add(p);
            }
        }
        public static void UpdateTornado(float deltaTime)
        {
            orbitAngle += orbitSpeed * deltaTime;
            if (orbitAngle > 360.0f)
                orbitAngle -= 360.0f;

            float rad = orbitAngle * (float)Math.PI / 180.0f;

            tornadoCenterX = orbitCenterX + orbitRadius * (float)Math.Cos(rad);
            tornadoCenterZ = orbitCenterZ + orbitRadius * (float)Math.Sin(rad);
            tornadoCenterY = orbitCenterY; 

            for (int i = 0; i < tornadoParticles.Count; i++)
            {
                Particle p = tornadoParticles[i];
                p.angle += p.speed * deltaTime;
                if (p.angle > 360) p.angle -= 360;

                p.height += 0.2f * deltaTime;
                if (p.height > 3.0f) p.height = 0.0f;

                tornadoParticles[i] = p;
            }
            if (!Animations.isBedJumping && Vector3.Distance(new Vector3(tornadoCenterX, tornadoCenterY, tornadoCenterZ), new Vector3(-3f, 0f, -2f)) < 1.5f)
            {
                Animations.isBedJumping = true;
                Animations.bedJumpTime = 0f; // сброс времени прыжка
            }

            if (!Animations.isTableJumping && Vector3.Distance(new Vector3(tornadoCenterX, tornadoCenterY, tornadoCenterZ), new Vector3(0f, 1.0f, 0f)) < 1.5f)
            {
                Animations.isTableJumping = true;
                Animations.tableJumpTime = 0f; // сброс времени прыжка
            }
            Animations.UpdateBedJump();
            Animations.UpdateTableJump();
        }

        public static void DrawTornado()
        {
            Gl.glPointSize(3);
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glColor3f(0f, 0f, 0f);
            foreach (var p in tornadoParticles)
            {
                float rad = p.angle * (float)Math.PI / 180.0f;
                float x = tornadoCenterX + p.radius * (float)Math.Cos(rad);
                float z = tornadoCenterZ + p.radius * (float)Math.Sin(rad);
                Gl.glVertex3f(x, p.height, z);
            }
            Gl.glEnd();
        }

        public static Vector3 GetTornadoPosition()
        {
            return new Vector3(tornadoCenterX, tornadoCenterY, tornadoCenterZ);
        }

        public struct Vector3
        {
            public float X, Y, Z;
            public Vector3(float x, float y, float z) { X = x; Y = y; Z = z; }
            public static float Distance(Vector3 v1, Vector3 v2)
            {
                return (float)Math.Sqrt(Math.Pow(v2.X - v1.X, 2) + Math.Pow(v2.Y - v1.Y, 2) + Math.Pow(v2.Z - v1.Z, 2));
            }
        }

        private struct Particle
        {
            public float angle; 
            public float radius; 
            public float height;
            public float speed; 
        }
    }
}
