using System.Drawing.Imaging;
using System.Drawing;
using Tao.OpenGl;

public static class Textures
{
    public static uint FloorTexture;
    public static uint CeilingTexture;
    public static uint WallTexture;

    public static uint LoadTexture(string path)
    {
        Bitmap image = new Bitmap(path);
        image.RotateFlip(RotateFlipType.RotateNoneFlipY); // OpenGL читает снизу вверх

        BitmapData data = image.LockBits(
            new Rectangle(0, 0, image.Width, image.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb);

        uint tex;
        Gl.glGenTextures(1, out tex);
        Gl.glBindTexture(Gl.GL_TEXTURE_2D, tex);

        Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
        Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);

        Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB,
            data.Width, data.Height, 0,
            Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, data.Scan0);

        image.UnlockBits(data);
        image.Dispose();

        return tex;
    }

    public static void InitTextures()
    {
        FloorTexture = LoadTexture("Resources/floor.jpg");
        CeilingTexture = LoadTexture("Resources/ceiling.jpg");
        WallTexture = LoadTexture("Resources/wall.jpg");
    }
}
