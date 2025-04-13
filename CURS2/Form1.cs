// MainForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace KitchenSceneTao
{
    public partial class Form1 : Form
    {
        float camX = 0, camY = 1.5f, camZ = 5;
        float camYaw = 0.0f, camPitch = 0.0f;
        float moveSpeed = 0.1f;
        float mouseSensitivity = 0.2f;
        Point lastMousePos;
        bool isRotating = false;

        public Form1()
        {
            InitializeComponent();
            InitializeOpenGLControl();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseUp += glControl_MouseUp;
            glControl.MouseMove += glControl_MouseMove;
        }

        private void InitializeOpenGLControl()
        {
            this.Controls.Add(glControl);
            glControl.InitializeContexts();
            Application.Idle += (s, e) => RenderScene();
            Glut.glutInit();
            InitScene();
        }

        private void InitScene()
        {
            Gl.glClearColor(0.5f, 0.8f, 0.92f, 1);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)glControl.Width / glControl.Height, 0.1, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }

        private void RenderScene()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            float lx = (float)(Math.Cos(camPitch) * Math.Sin(camYaw));
            float ly = (float)(Math.Sin(camPitch));
            float lz = (float)(-Math.Cos(camPitch) * Math.Cos(camYaw));

            Glu.gluLookAt(camX, camY, camZ, camX + lx, camY + ly, camZ + lz, 0.0f, 1.0f, 0.0f);

            Scene.DrawRoom();
            Scene.DrawTable();
            Scene.DrawPlates();
            Scene.DrawBed();
            Scene.DrawChandelier();

            glControl.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            float dx = (float)Math.Sin(camYaw) * moveSpeed;
            float dz = (float)-Math.Cos(camYaw) * moveSpeed;

            switch (e.KeyCode)
            {
                case Keys.W: camX += dx; camZ += dz; break;
                case Keys.S: camX -= dx; camZ -= dz; break;
                case Keys.A: camX -= dz; camZ += dx; break;
                case Keys.D: camX += dz; camZ -= dx; break;
                case Keys.Q: camY -= moveSpeed; break;
                case Keys.E: camY += moveSpeed; break;
            }

            glControl.Invalidate();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isRotating = true;
                lastMousePos = e.Location;
            }
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                isRotating = false;
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRotating)
            {
                int dx = e.X - lastMousePos.X;
                int dy = e.Y - lastMousePos.Y;
                camYaw += dx * mouseSensitivity * 0.01f;
                camPitch -= dy * mouseSensitivity * 0.01f;
                camPitch = Clamp(camPitch, -1.5f, 1.5f);
                lastMousePos = e.Location;
                glControl.Invalidate();
            }
        }

        private float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}
