using System;
using SkiaSharp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;

namespace BezierSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainWindow window = new MainWindow(1080, 1920);
            window.Run();
        }
    }
}
