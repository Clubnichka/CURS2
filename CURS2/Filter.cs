using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace CURS2
{
    public static class Filter
    {
        public static int embossTextureId = -1;
        public static bool embossEnabled = false;

        public static void ToggleEmboss()
        {
            embossEnabled = !embossEnabled;
        }

        public static void ApplyEmbossFilterAsTexture(int width, int height)
        {



            Gl.glFinish();


            IntPtr ptr = Marshal.AllocHGlobal(width * height * 3);

            try
            {

                Gl.glReadPixels(0, 0, width, height, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, ptr);


                byte[] rawData = new byte[width * height * 3];
                Marshal.Copy(ptr, rawData, 0, rawData.Length);


                byte[] filtered = ApplyEmbossFilter(rawData, width, height);

                if (embossTextureId == -1)
                {
                    int[] ids = new int[1];
                    Gl.glGenTextures(1, ids);
                    embossTextureId = ids[0];
                }

                Gl.glBindTexture(Gl.GL_TEXTURE_2D, embossTextureId);
                IntPtr texturePtr = Marshal.AllocHGlobal(filtered.Length);
                try
                {
                    Marshal.Copy(filtered, 0, texturePtr, filtered.Length);
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, width, height, 0,
                                    Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, texturePtr);
                }
                finally
                {
                    Marshal.FreeHGlobal(texturePtr);
                }

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);

                Gl.glDisable(Gl.GL_DEPTH_TEST);
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glPushMatrix();
                Gl.glLoadIdentity();
                Gl.glOrtho(0, 1, 0, 1, -1, 1);
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glPushMatrix();
                Gl.glLoadIdentity();

                Gl.glEnable(Gl.GL_TEXTURE_2D);
                Gl.glBegin(Gl.GL_QUADS);
                Gl.glTexCoord2f(0, 0); Gl.glVertex2f(0, 0);
                Gl.glTexCoord2f(1, 0); Gl.glVertex2f(1, 0);
                Gl.glTexCoord2f(1, 1); Gl.glVertex2f(1, 1);
                Gl.glTexCoord2f(0, 1); Gl.glVertex2f(0, 1);
                Gl.glEnd();
                Gl.glDisable(Gl.GL_TEXTURE_2D);

                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glPopMatrix();
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glPopMatrix();
                Gl.glEnable(Gl.GL_DEPTH_TEST);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        private static byte[] ApplyEmbossFilter(byte[] data, int width, int height)
        {
            byte[] result = new byte[data.Length];
            int stride = width * 3;

            for (int y = 1; y < height; y++)
            {
                for (int x = 1; x < width; x++)
                {
                    for (int c = 0; c < 3; c++) 
                    {
                        int index = (y * width + x) * 3 + c;


                        int prevX = x - 1;
                        int prevY = y - 1;


                        if (prevX >= 0 && prevY >= 0)
                        {
                            int prev = (prevY * width + prevX) * 3 + c;
                            int diff = data[index] - data[prev] + 128;
                            result[index] = (byte)Math.Min(255, Math.Max(0, diff));
                        }
                        else
                        {

                            result[index] = data[index];
                        }
                    }
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int c = 0; c < 3; c++)
                {
                    int index = (y * width + 0) * 3 + c;
                    result[index] = result[((y * width + 1) * 3 + c)];
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int c = 0; c < 3; c++)
                {
                    int index = (0 * width + x) * 3 + c;
                    result[index] = result[((1 * width + x) * 3 + c)];
                }
            }

            return result;
        }
    }
}
