using System;
using SkiaSharp;
using GLFW;

namespace BezierSharp2
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var window = new NativeWindow(800, 600, "BezierSharp", Monitor.None, Window.None))
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

          // Main application loop
          while (!window.IsClosing)
          {
            canvas.Clear(SKColors.Aqua);
            using (var paint = new SKPaint { Color = SKColors.Green })
            {
              var a = new BezierCurve(new SKPoint(80,80),new SKPoint(180,150),new SKPoint(180,80),new SKPoint(80, 180));
              a.Draw(ref canvas,paint, SKPathFillType.Winding);
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
