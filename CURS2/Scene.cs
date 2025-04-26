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
            Gl.glColor3f(0.8f, 0.8f, 0.8f);
            DrawWall(-5, 0, -5, -5, 3, 5);

            // Back wall (blueish)
            Gl.glColor3f(0.8f, 0.8f, 0.8f);
            DrawWall(-5, 0, -5, 5, 3, -5);

            Gl.glEnd();
            DrawRightWallWithDoor();

            
        }

        public static void DrawWoman(float offset)
        {
            Gl.glPushMatrix();
            float baseX = offset+1f;
            float baseZ = -3f;
            Gl.glTranslatef(baseX - 0.5f, 0, baseZ);
            Gl.glRotatef(90, 0, 1, 0); // Повернуть к двери
            Gl.glColor3f(1.0f, 0.8f, 0.6f);
            // Голова
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 1.7f, 0);
            Glut.glutSolidSphere(0.15f, 12, 12);
            Gl.glPopMatrix();

            // Руки
            for (float dx = -0.2f; dx <= 0.2f; dx += 0.4f)
            {
                Gl.glPushMatrix();
                Gl.glTranslatef(dx, 1.1f, 0); // ниже, чтобы быть вдоль платья
                Gl.glRotatef(10 * Math.Sign(dx), 0, 0, 1); // слегка наклонены к телу
                Gl.glScalef(0.1f, 0.5f, 0.1f);
                Glut.glutSolidCube(1);
                Gl.glPopMatrix();
            }

            // Ноги
            for (float dx = -0.15f; dx <= 0.15f; dx += 0.3f)
            {
                Gl.glPushMatrix();
                Gl.glTranslatef(dx, 0.3f, 0);
                Gl.glScalef(0.1f, 0.6f, 0.1f);
                Glut.glutSolidCube(1);
                Gl.glPopMatrix();
            }

            // Платье (розовый конус)
            Gl.glPushMatrix();
            Gl.glColor3f(1.0f, 0.4f, 0.7f); // Розовый цвет
            Gl.glTranslatef(0, 0.5f, 0);
            Gl.glRotatef(-90, 1, 0, 0); // Конус вертикально
            Glut.glutSolidCone(0.35f, 1.2f, 20, 20);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
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

        public static void DrawPlate()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 1.1f, 0);
            Gl.glRotatef(-90, 1, 0, 0);

            // Цвет фарфора
            Gl.glColor3f(1f, 1f, 1f);

            // Наружный обод (тор)
            Glut.glutSolidTorus(0.02f, 0.2f, 16, 32);

            // Дно (тонкий диск)
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3f(0, 0, 0); // центр
            for (int i = 0; i <= 32; i++)
            {
                float angle = (float)(2 * Math.PI * i / 32);
                float x = 0.18f * (float)Math.Cos(angle);
                float y = 0.18f * (float)Math.Sin(angle);
                Gl.glVertex3f(x, y, 0);
            }
            Gl.glEnd();

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
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glEnable(Gl.GL_CULL_FACE);               // <--- вот это
            Gl.glCullFace(Gl.GL_BACK);
            Gl.glColor4f(0.6f, 0.8f, 1.0f, 0.4f); // полупрозрачный цвет

            Gl.glPushMatrix();
            Gl.glTranslatef(0.5f, 1.05f, 0);
            DrawHermiteSurface();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_CULL_FACE);
            Gl.glDisable(Gl.GL_BLEND);
        }

        private static void DrawHermiteSurface()
        {
            int slices = 32, stacks = 16;
            float height = 0.6f;

            for (int i = 0; i < stacks; i++)
            {
                float t1 = (float)i / stacks;
                float t2 = (float)(i + 1) / stacks;

                float y1 = t1 * height;
                float y2 = t2 * height;

                float r1 = HermiteRadius(t1);
                float r2 = HermiteRadius(t2);

                Gl.glBegin(Gl.GL_QUAD_STRIP);
                for (int j = 0; j <= slices; j++)
                {
                    float angle = (float)(2 * Math.PI * j / slices);
                    float x1 = r1 * (float)Math.Cos(angle);
                    float z1 = r1 * (float)Math.Sin(angle);

                    float x2 = r2 * (float)Math.Cos(angle);
                    float z2 = r2 * (float)Math.Sin(angle);

                    Gl.glVertex3f(x1, y1, z1);
                    Gl.glVertex3f(x2, y2, z2);
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

        public static void DrawRealisticChandelier()
        {
            Gl.glPushMatrix();

            float centerX = 0, centerY = 3.0f, centerZ = 0;
            float verticalRodLength = 0.5f;
            float ringRadius = 0.5f;

            // Цвет стержней
            Gl.glColor3f(0.2f, 0.2f, 0.2f);

            // Центральный вертикальный стержень
            DrawCylinder(centerX, centerY, centerZ, centerX, centerY - verticalRodLength, centerZ, 0.03f);

            // Позиция кольца (горизонтального каркаса)
            float ringY = centerY - verticalRodLength;

            // Горизонтальный "каркас" - тор
            Gl.glColor3f(0.3f, 0.3f, 0.3f);
            Gl.glPushMatrix();
            Gl.glTranslatef(centerX, ringY, centerZ);
            Gl.glRotatef(90, 1, 0, 0); // положим тор в горизонтальную плоскость
            Glut.glutSolidTorus(0.02f, ringRadius, 12, 24);
            Gl.glPopMatrix();

            // Расположение 4 плафонов по кругу
            Gl.glColor4f(1.0f, 1.0f, 0.8f, 0.7f); // полупрозрачные
            EnableTransparency();

            for (int i = 0; i < 4; i++)
            {
                double angle = i * Math.PI / 2;
                float lampX = centerX + ringRadius * (float)Math.Cos(angle);
                float lampZ = centerZ + ringRadius * (float)Math.Sin(angle);
                float lampY = ringY - 0.25f;

                // Стрежень от кольца к плафону
                Gl.glColor3f(0.2f, 0.2f, 0.2f);
                DrawCylinder(lampX, ringY, lampZ, lampX, lampY, lampZ, 0.015f);

                // Плафон
                Gl.glColor4f(1.0f, 1.0f, 0.8f, 0.7f);
                DrawLamp(lampX, lampY, lampZ);
            }

            DisableTransparency();
            Gl.glPopMatrix();
        }

        private static void DrawCylinder(float x1, float y1, float z1, float x2, float y2, float z2, float radius)
        {
            Gl.glPushMatrix();

            // Вектор направления
            float dx = x2 - x1;
            float dy = y2 - y1;
            float dz = z2 - z1;
            float length = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);

            // Переместиться в начальную точку
            Gl.glTranslatef(x1, y1, z1);

            // Если длина нулевая, рисовать не нужно
            if (length < 0.0001f)
            {
                Gl.glPopMatrix();
                return;
            }

            // Нормализованный вектор направления
            float ux = dx / length;
            float uy = dy / length;
            float uz = dz / length;

            // Вектор (0,1,0)
            float vx = 0;
            float vy = 1;
            float vz = 0;

            // Вектор оси вращения — векторное произведение v × u
            float rx = vy * uz - vz * uy;
            float ry = vz * ux - vx * uz;
            float rz = vx * uy - vy * ux;

            float sinA = (float)Math.Sqrt(rx * rx + ry * ry + rz * rz);
            float cosA = vy * uy + vz * uz + vx * ux; // скалярное произведение
            float angle = (float)(Math.Atan2(sinA, cosA) * 180.0 / Math.PI);

            if (sinA > 0.0001f)
                Gl.glRotatef(angle, rx, ry, rz);

            // Масштаб по высоте
            Gl.glScalef(radius, length, radius);

            // Рисуем единичный цилиндр вдоль Y
            Glut.glutSolidCylinder(1, 1, 16, 4);

            Gl.glPopMatrix();
        }

        private static void DrawRod(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            Gl.glPushMatrix();

            // Центр между точками
            float cx = (x1 + x2) / 2;
            float cy = (y1 + y2) / 2;
            float cz = (z1 + z2) / 2;
            Gl.glTranslatef(cx, cy, cz);

            // Вектор направления
            float dx = x2 - x1;
            float dy = y2 - y1;
            float dz = z2 - z1;
            float length = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);

            // Вычисляем угол поворота и ось (с помощью кросс-продукта)
            float[] dir = { dx, dy, dz };
            Normalize(ref dir);
            float[] defaultDir = { 0, -1, 0 }; // по умолчанию "вниз"
            float[] axis = Cross(defaultDir, dir);
            float angle = (float)(Math.Acos(Dot(defaultDir, dir)) * 180.0 / Math.PI);

            if (axis[0] != 0 || axis[1] != 0 || axis[2] != 0)
                Gl.glRotatef(angle, axis[0], axis[1], axis[2]);

            // Рисуем стержень — удлинённый цилиндр
            Gl.glScalef(0.03f, length / 2, 0.03f);
            Glut.glutSolidCube(1);

            Gl.glPopMatrix();
        }

        private static void DrawRightWallWithDoor()
        {
            float wallX = 5f;
            float wallHeight = 3f;

            float doorHeight = 2f;
            float doorWidth = 1f;
            float doorBottom = 0f;
            float doorZ = -3.5f; // Смещение по Z

            Gl.glColor3f(0.8f, 0.8f, 0.8f); // Цвет стены

            // Левая часть стены
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3f(wallX, 0, -5);
            Gl.glVertex3f(wallX, wallHeight, -5);
            Gl.glVertex3f(wallX, wallHeight, doorZ);
            Gl.glVertex3f(wallX, 0, doorZ);
            Gl.glEnd();

            // Правая часть стены
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3f(wallX, 0, doorZ + doorWidth);
            Gl.glVertex3f(wallX, wallHeight, doorZ + doorWidth);
            Gl.glVertex3f(wallX, wallHeight, 5);
            Gl.glVertex3f(wallX, 0, 5);
            Gl.glEnd();

            // Верхняя часть над дверью (полная перемычка)
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3f(wallX, doorHeight, doorZ);
            Gl.glVertex3f(wallX, wallHeight, doorZ);
            Gl.glVertex3f(wallX, wallHeight, doorZ + doorWidth);
            Gl.glVertex3f(wallX, doorHeight, doorZ + doorWidth);
            Gl.glEnd();
        }

        // Дверь
        public static void DrawDoor(float angle)
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(5f, 0f, -3.5f); // точка крепления к правой стене
            Gl.glRotatef(angle, 0, 1, 0); // поворот вокруг вертикальной оси

            Gl.glColor3f(0.4f, 0.2f, 0.1f); // коричневая дверь
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3f(0, 0, 0);
            Gl.glVertex3f(0, 2, 0);
            Gl.glVertex3f(0, 2, 1);
            Gl.glVertex3f(0, 0, 1);
            Gl.glEnd();

            Gl.glPopMatrix();
        }
        private static void Normalize(ref float[] v)
        {
            float len = (float)Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
            if (len == 0) return;
            v[0] /= len; v[1] /= len; v[2] /= len;
        }

        private static float Dot(float[] a, float[] b)
        {
            return a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
        }

        private static float[] Cross(float[] a, float[] b)
        {
            return new float[]
            {
        a[1] * b[2] - a[2] * b[1],
        a[2] * b[0] - a[0] * b[2],
        a[0] * b[1] - a[1] * b[0]
            };
        }

        private static void DrawLamp(float x, float y, float z)
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(x, y, z);
            Gl.glScalef(0.2f, 0.2f, 0.2f);
            Glut.glutSolidSphere(1, 16, 16);
            Gl.glPopMatrix();
        }

        private static void EnableTransparency()
        {
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glDisable(Gl.GL_DEPTH_TEST);
        }

        private static void DisableTransparency()
        {
            Gl.glDisable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
        }
        public struct Vector3
        {
            public float X, Y, Z;
            public Vector3(float x, float y, float z) { X = x; Y = y; Z = z; }
        }

    }
}