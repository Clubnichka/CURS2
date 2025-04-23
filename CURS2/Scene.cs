using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace KitchenSceneTao
{
    public static class Scene
    {
        public static void DrawRoom()
        {
            Gl.glBegin(Gl.GL_QUADS);

            // Floor (brown)
            Gl.glColor3f(0.6f, 0.4f, 0.2f);
            Gl.glVertex3f(-5, 0, -5);
            Gl.glVertex3f(5, 0, -5);
            Gl.glVertex3f(5, 0, 5);
            Gl.glVertex3f(-5, 0, 5);

            // Ceiling (light gray)
            Gl.glColor3f(0.9f, 0.9f, 0.95f);
            Gl.glVertex3f(-5, 3, -5);
            Gl.glVertex3f(5, 3, -5);
            Gl.glVertex3f(5, 3, 5);
            Gl.glVertex3f(-5, 3, 5);

            // Left wall (blueish)
            Gl.glColor3f(0.7f, 0.8f, 0.9f);
            DrawWall(-5, 0, -5, -5, 3, 5);

            // Right wall (blueish)
            Gl.glColor3f(0.7f, 0.8f, 0.9f);
            DrawWall(5, 0, -5, 5, 3, 5);

            // Back wall (blueish)
            Gl.glColor3f(0.7f, 0.8f, 0.9f);
            DrawWall(-5, 0, -5, 5, 3, -5);

            Gl.glEnd();
        }

        private static void DrawWall(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            Gl.glVertex3f(x1, y1, z1);
            Gl.glVertex3f(x2, y1, z2);
            Gl.glVertex3f(x2, y2, z2);
            Gl.glVertex3f(x1, y2, z1);
        }

        public static void DrawTable()
        {
            Gl.glColor3f(0.6f, 0.3f, 0.1f);
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 1.0f, 0);
            Gl.glScalef(2, 0.1f, 1);
            Glut.glutSolidCube(1);
            Gl.glPopMatrix();

            DrawLeg(-0.9f, 0.5f, -0.4f);
            DrawLeg(0.9f, 0.5f, -0.4f);
            DrawLeg(-0.9f, 0.5f, 0.4f);
            DrawLeg(0.9f, 0.5f, 0.4f);
        }

        private static void DrawLeg(float x, float y, float z)
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(x, y, z);
            Gl.glScalef(0.1f, 1.0f, 0.1f);
            Glut.glutSolidCube(1);
            Gl.glPopMatrix();
        }

        public static void DrawPlates()
        {
            Gl.glColor3f(1f, 1f, 1f);
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 1.05f, 0);
            Gl.glRotatef(-90, 1, 0, 0);
            Glut.glutSolidTorus(0.05f, 0.2f, 16, 32);
            Gl.glPopMatrix();
        }

        //public static void DrawGlass()
        //{
        //    Gl.glColor3f(0.6f, 0.8f, 1.0f);
        //    Gl.glPushMatrix();
        //    Gl.glTranslatef(0.5f, 1.05f, 0);
        //    DrawHermiteSurface();
        //    Gl.glPopMatrix();
        //}

        //private static void DrawHermiteSurface()
        //{
        //    int slices = 32;
        //    int stacks = 16;

        //    Gl.glBegin(Gl.GL_QUAD_STRIP);
        //    for (int i = 0; i <= stacks; i++)
        //    {
        //        float t = (float)i / stacks;
        //        float y = t * 0.6f;
        //        float r = HermiteRadius(t);

        //        for (int j = 0; j <= slices; j++)
        //        {
        //            float angle = (float)(2 * Math.PI * j / slices);
        //            float x = (float)(r * Math.Cos(angle));
        //            float z = (float)(r * Math.Sin(angle));
        //            Gl.glVertex3f(x, y, z);
        //        }
        //    }
        //    Gl.glEnd();
        //}

        //private static float HermiteRadius(float t)
        //{
        //    float h00 = 2 * t * t * t - 3 * t * t + 1;
        //    float h10 = t * t * t - 2 * t * t + t;
        //    float h01 = -2 * t * t * t + 3 * t * t;
        //    float h11 = t * t * t - t * t;

        //    float p0 = 0.05f;
        //    float p1 = 0.15f;
        //    float m0 = 0.2f;
        //    float m1 = 0.0f;

        //    return h00 * p0 + h10 * m0 + h01 * p1 + h11 * m1;
        //}

        //public static void DrawRugWithTree()
        //{
        //    Gl.glPushMatrix();
        //    Gl.glTranslatef(0, 0.01f, 0);
        //    Gl.glColor3f(0.9f, 0.8f, 0.7f);
        //    Gl.glBegin(Gl.GL_QUADS);
        //    Gl.glVertex3f(-1.5f, 0, -0.75f);
        //    Gl.glVertex3f(1.5f, 0, -0.75f);
        //    Gl.glVertex3f(1.5f, 0, 0.75f);
        //    Gl.glVertex3f(-1.5f, 0, 0.75f);
        //    Gl.glEnd();

        //    Gl.glTranslatef(0, 0.001f, 0);
        //    Gl.glColor3f(0.3f, 0.2f, 0.1f);
        //    DrawFractalTree(0, 0, 0.5f, -90, 5);

        //    Gl.glPopMatrix();
        //}

        //private static void DrawFractalTree(float x, float y, float length, float angle, int depth)
        //{
        //    if (depth == 0) return;

        //    float rad = (float)(angle * Math.PI / 180);
        //    float x2 = x + (float)(Math.Cos(rad) * length);
        //    float y2 = y + (float)(Math.Sin(rad) * length);

        //    Gl.glBegin(Gl.GL_LINES);
        //    Gl.glVertex3f(x, y, 0);
        //    Gl.glVertex3f(x2, y2, 0);
        //    Gl.glEnd();

        //    DrawFractalTree(x2, y2, length * 0.7f, angle - 30, depth - 1);
        //    DrawFractalTree(x2, y2, length * 0.7f, angle + 30, depth - 1);
        //}
        private static void DrawFractalTree(float x, float y, float length, float angle, int depth)
        {
            if (depth == 0) return;

            float rad = angle * (float)Math.PI / 180;
            float x2 = x + (float)Math.Cos(rad) * length;
            float y2 = y + (float)Math.Sin(rad) * length;

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(x, y, 0);
            Gl.glVertex3f(x2, y2, 0);
            Gl.glEnd();

            DrawFractalTree(x2, y2, length * 0.7f, angle - 30, depth - 1);
            DrawFractalTree(x2, y2, length * 0.7f, angle + 30, depth - 1);
        }
        public static void DrawGlass()
        {
            Gl.glColor3f(0.6f, 0.8f, 1.0f); // светло-голубой
            Gl.glPushMatrix();
            Gl.glTranslatef(0.5f, 1.05f, 0); // на столе
            DrawHermiteSurface();
            Gl.glPopMatrix();
        }

        private static void DrawHermiteSurface()
        {
            int slices = 32, stacks = 16;

            for (int i = 0; i < stacks; i++)
            {
                float t1 = (float)i / stacks;
                float t2 = (float)(i + 1) / stacks;
                float y1 = t1 * 0.6f, y2 = t2 * 0.6f;
                float r1 = HermiteRadius(t1), r2 = HermiteRadius(t2);

                Gl.glBegin(Gl.GL_QUAD_STRIP);
                for (int j = 0; j <= slices; j++)
                {
                    float angle = (float)(2 * Math.PI * j / slices);
                    float cos = (float)Math.Cos(angle), sin = (float)Math.Sin(angle);

                    Gl.glVertex3f(r1 * cos, y1, r1 * sin);
                    Gl.glVertex3f(r2 * cos, y2, r2 * sin);
                }
                Gl.glEnd();
            }
        }

        private static void DrawQuad(float r, float g, float b,
                             float x1, float y1, float z1,
                             float x2, float y2, float z2,
                             float x3, float y3, float z3,
                             float x4, float y4, float z4)
        {
            Gl.glColor3f(r, g, b); // установка цвета заливки
            Gl.glVertex3f(x1, y1, z1);
            Gl.glVertex3f(x2, y2, z2);
            Gl.glVertex3f(x3, y3, z3);
            Gl.glVertex3f(x4, y4, z4);
        }

        private static float HermiteRadius(float t)
        {
            float h00 = 2 * t * t * t - 3 * t * t + 1;
            float h10 = t * t * t - 2 * t * t + t;
            float h01 = -2 * t * t * t + 3 * t * t;
            float h11 = t * t * t - t * t;

            float p0 = 0.05f;   // радиус у основания
            float p1 = 0.15f;   // радиус вверху
            float m0 = 0.2f;    // касательная внизу
            float m1 = 0.0f;    // касательная вверху

            return h00 * p0 + h10 * m0 + h01 * p1 + h11 * m1;
        }

        public static void DrawRugWithTree()
        {
            Gl.glPushMatrix();

            // Рисуем ковёр (в плоскости XZ)
            Gl.glTranslatef(0, 0.02f, 0); // немного приподнят над полом
            Gl.glColor3f(0.9f, 0.8f, 0.7f);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3f(-1.5f, 0, -0.75f);
            Gl.glVertex3f(1.5f, 0, -0.75f);
            Gl.glVertex3f(1.5f, 0, 0.75f);
            Gl.glVertex3f(-1.5f, 0, 0.75f);
            Gl.glEnd();

            // Рисуем дерево на поверхности ковра
            Gl.glTranslatef(0, 0.001f, 0); // чуть выше ковра
            Gl.glColor3f(0.3f, 0.2f, 0.1f);

            // Поворачиваем дерево, чтобы оно "росло" по оси X, вдоль ковра
            DrawFractalTreeX(0, 0, 0.5f, 0, 5);

            Gl.glPopMatrix();
        }

        // Фрактальное дерево в плоскости XZ, "растущее" по оси X
        private static void DrawFractalTreeX(float x, float z, float length, float angle, int depth)
        {
            if (depth == 0) return;
            float rad = angle * (float)Math.PI / 180;
            float x2 = x + (float)Math.Cos(rad) * length;
            float z2 = z + (float)Math.Sin(rad) * length;

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(x, 0, z);
            Gl.glVertex3f(x2, 0, z2);
            Gl.glEnd();

            DrawFractalTreeX(x2, z2, length * 0.7f, angle - 30, depth - 1);
            DrawFractalTreeX(x2, z2, length * 0.7f, angle + 30, depth - 1);
        }
        public static void DrawBed()
        {
            Gl.glColor3f(0.5f, 0.2f, 0.2f);
            Gl.glPushMatrix();
            Gl.glTranslatef(-3, 0.0f, -2);
            Gl.glScalef(2, 0.3f, 1);
            Glut.glutSolidCube(1);
            Gl.glPopMatrix();

            Gl.glColor3f(0.9f, 0.8f, 0.9f);
            Gl.glPushMatrix();
            Gl.glTranslatef(-3, 0.3f, -2);
            Gl.glScalef(1.9f, 0.3f, 0.9f);
            Glut.glutSolidCube(1);
            Gl.glPopMatrix();

            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glPushMatrix();
            Gl.glTranslatef(-3.5f, 0.5f, -2);
            Gl.glScalef(0.4f, 0.2f, 0.6f);
            Glut.glutSolidSphere(1, 16, 16);
            Gl.glPopMatrix();
        }

        public static void DrawChandelier()
        {
            Gl.glColor3f(1.0f, 1.0f, 0.8f);
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 2.8f, 0);

            DrawLamp(-0.4f, -0.3f);
            DrawLamp(0.4f, -0.3f);
            DrawLamp(0.0f, 0.4f);

            Gl.glPopMatrix();
        }

        private static void DrawLamp(float xOffset, float zOffset)
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(xOffset, 0, zOffset);
            Gl.glScalef(0.2f, 0.2f, 0.2f);
            Glut.glutSolidSphere(1, 16, 16);
            Gl.glPopMatrix();
        }
    }
}