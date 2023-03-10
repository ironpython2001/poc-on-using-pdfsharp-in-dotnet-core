using System.Drawing.Imaging;
using System.Drawing;

public static class MyExtensions
{
    public static Stream ToStream(this Image image, ImageFormat format)
    {
        var stream = new System.IO.MemoryStream();
        image.Save(stream, format);
        stream.Position = 0;
        return stream;
    }
}