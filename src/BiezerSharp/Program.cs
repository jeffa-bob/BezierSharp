using System;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;

namespace BiezerSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GameWindow window = new GameWindow(GameWindowSettings.Default, NativeWindowSettings.Default);
            // NativeWindow window = new NativeWindow(NativeWindowSettings.Default);
            window.Run();
            window.MakeCurrent();

            GRContext context = GRContext.CreateGl();

            SKImageInfo imageInfo = new SKImageInfo(width: 640, height: 480, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            using (var surface = SKSurface.Create(context,false, imageInfo))
            {
                SKCanvas canvas = surface.Canvas;

                // Your drawing code goes here.
                canvas.Clear(SKColors.White);
                canvas.Flush();
            }
        }
    }
}
