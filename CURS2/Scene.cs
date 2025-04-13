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
            Glut.glutSolidTorus(0.05f, 0.2f, 16, 32);
            Gl.glPopMatrix();
        }

        public static void DrawBed()
        {
            Gl.glColor3f(0.5f, 0.2f, 0.2f);
            Gl.glPushMatrix();
            Gl.glTranslatef(-3, 0.5f, -2);
            Gl.glScalef(2, 0.3f, 1);
            Glut.glutSolidCube(1);
            Gl.glPopMatrix();

            Gl.glColor3f(0.9f, 0.8f, 0.9f);
            Gl.glPushMatrix();
            Gl.glTranslatef(-3, 0.8f, -2);
            Gl.glScalef(1.9f, 0.3f, 0.9f);
            Glut.glutSolidCube(1);
            Gl.glPopMatrix();

            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glPushMatrix();
            Gl.glTranslatef(-3.5f, 1.0f, -2);
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
