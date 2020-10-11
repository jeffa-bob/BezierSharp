using System;
using SkiaSharp;
using SkiaSharp.Views;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;

namespace BezierSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new NativeWindowSettings();
            
            NativeWindow window = new NativeWindow(NativeWindowSettings.Default);
            window.Context.MakeCurrent();
            // window.MakeCurrent();

            var glInterface = GRGlInterface.Create();
            var context = GRContext.CreateGl(glInterface);
            
            System.Diagnostics.Debug.Assert(context != null);

            SKImageInfo imageInfo = new SKImageInfo(width: 640, height: 480, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            var surface = SKSurface.Create(context, false, imageInfo);

            //System.Diagnostics.Debug.Assert(surface != null);
            SKCanvas canvas = surface.Canvas;

            // Your drawing code goes here.
            canvas.Clear(SKColors.White);
            using (SKPaint paint = new SKPaint { Color = SKColors.AliceBlue })
            {
                canvas.DrawCircle(60, 90, 60, paint);
            }

            canvas.Flush();
            context.Flush();

        }
    }
}
