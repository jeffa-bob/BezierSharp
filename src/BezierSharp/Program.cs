using System;
using SkiaSharp;
using GLFW;

namespace BezierSharp2
{
  partial class Program
  {

    static BezierCurve a = new BezierCurve();
    static NativeWindow window;
    static void Main(string[] args)
    {

      a.Add(new BezierPiece(new SKPoint(80, 80), new SKPoint(-80, 150)));
      a.Add(new BezierPiece(new SKPoint(280, 80), new SKPoint(380, 180)));
      a.Add(new BezierPiece(new SKPoint(680, 80), new SKPoint(580, 10)));

      using ( window = new NativeWindow(800, 600, "BezierSharp", Monitor.None, Window.None))
      {
        Glfw.MakeContextCurrent(window);
        GRGlInterface glInterface = GRGlInterface.AssembleGlInterface(Glfw.CurrentContext, (contextHandle, name) => Glfw.GetProcAddress(name));
        GRContext context = GRContext.CreateGl(glInterface);
        GRBackendRenderTargetDesc backendRenderTargetDescription = new GRBackendRenderTargetDesc
        {
          Config = GRPixelConfig.Rgba8888,
          Height = 600,
          Width = 800,
          Origin = GRSurfaceOrigin.TopLeft,
          RenderTargetHandle = new IntPtr(0),
          SampleCount = 0,
          StencilBits = 8
        };
        

        using (var surface = SKSurface.Create(context, backendRenderTargetDescription))
        {
          var canvas = surface.Canvas;
          int height = canvas.DeviceClipBounds.Height;
          // Main application loop
          while (!window.IsClosing)
          {
            AjustCurve(ref a, height);
            canvas.Clear(SKColors.White);
            using (var paint = new SKPaint { Color = SKColors.Blue })
            {
              paint.StrokeWidth = 3;
              paint.Style = SKPaintStyle.Stroke;
              paint.IsAntialias = true;

              a.Draw(ref canvas, paint, SKPathFillType.Winding);
            }
            
            // OpenGL rendering
            // Implement any timing for flow control, etc (see Glfw.GetTime())
            canvas.Flush();
            context.Flush();
            // Swap the front/back buffers
            window.SwapBuffers();

            // Poll native operating system events (must be called or OS will think application is hanging)
            Glfw.PollEvents();
          }
        }

      }
    }
  }
}
