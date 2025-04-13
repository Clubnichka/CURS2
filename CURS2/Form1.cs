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
        // Положение камеры
        float camX = 0, camY = 1.5f, camZ = 5;
        // Направление взгляда
        float camYaw = 0.0f, camPitch = 0.0f;
        // Скорость
        float moveSpeed = 0.1f;
        float mouseSensitivity = 0.2f;
        Point lastMousePos;
        bool isRotating = false;

        public Form1()
        {
            InitializeComponent();
            InitializeOpenGLControl();
            this.KeyPreview = true; // Обязательно! Чтобы форма обрабатывала нажатия клавиш

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

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
            Gl.glClearColor(0.5f, 0.8f, 0.92f, 1); // небесный фон
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

            // Перевод углов в направление
            float lx = (float)(Math.Cos(camPitch) * Math.Sin(camYaw));
            float ly = (float)(Math.Sin(camPitch));
            float lz = (float)(-Math.Cos(camPitch) * Math.Cos(camYaw));

            Glu.gluLookAt(camX, camY, camZ, camX + lx, camY + ly, camZ + lz, 0.0f, 1.0f, 0.0f);

            DrawRoom();
            DrawTable();
            DrawPlates();
            DrawBed();
            DrawChandelier();

            glControl.Invalidate();
        }

        private void DrawCube(float x, float y, float z, float sx, float sy, float sz, Color color)
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(x, y, z);
            Gl.glScalef(sx, sy, sz);
            Gl.glColor3f(color.R / 255f, color.G / 255f, color.B / 255f);

            Glut.glutSolidCube(1);
            Gl.glPopMatrix();
        }

        private void DrawRoom()
        {
            // Пол
            DrawCube(0, -1.6f, 0, 6, 0.1f, 6, Color.LightGray);
            // Стены
            DrawCube(0, 1f, -3, 6, 3f, 0.1f, Color.Beige);
            DrawCube(-3, 1f, 0, 0.1f, 3f, 6, Color.Beige);
            DrawCube(3, 1f, 0, 0.1f, 3f, 6, Color.Beige);
            // Потолок
            DrawCube(0, 3f, 0, 6, 0.1f, 6, Color.WhiteSmoke);
        }

        private void DrawTable()
        {
            // Стол
            DrawCube(0, -0.5f, 0, 2.0f, 1f, 1f, Color.SaddleBrown);
        }

        private void DrawPlates()
        {
            // Тарелки на столе
            DrawCube(-0.6f, 0.1f, 0.3f, 0.3f, 0.05f, 0.3f, Color.White);
            DrawCube(0.6f, 0.1f, -0.3f, 0.3f, 0.05f, 0.3f, Color.White);
        }

        private void DrawBed()
        {
            DrawCube(-2.3f, -1.1f, -2f, 1.5f, 0.5f, 2.5f, Color.DarkSlateBlue);
        }

        private void DrawChandelier()
        {
            DrawCube(0, 2.8f, 0, 0.4f, 0.1f, 0.4f, Color.Gold);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            float dx = (float)Math.Sin(camYaw) * moveSpeed;
            float dz = (float)-Math.Cos(camYaw) * moveSpeed;

            switch (e.KeyCode)
            {
                case Keys.W:
                    camX += dx;
                    camZ += dz;
                    break;
                case Keys.S:
                    camX -= dx;
                    camZ -= dz;
                    break;
                case Keys.A:
                    camX -= dz;
                    camZ += dx;
                    break;
                case Keys.D:
                    camX += dz;
                    camZ -= dx;
                    break;
                case Keys.Q:
                    camY -= moveSpeed;
                    break;
                case Keys.E:
                    camY += moveSpeed;
                    break;
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
            {
                isRotating = false;
            }
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRotating)
            {
                int dx = e.X - lastMousePos.X;
                int dy = e.Y - lastMousePos.Y;
                camYaw += dx * mouseSensitivity * 0.01f;
                camPitch -= dy * mouseSensitivity * 0.01f;

                // Ограничение на угол обзора вверх/вниз
                camPitch = Clamp(camPitch, -1.5f, 1.5f);

                lastMousePos = e.Location;
                glControl.Invalidate();
            }
        }
        private float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}