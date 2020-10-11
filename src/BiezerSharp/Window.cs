using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using SkiaSharp;
using System;
using System.Diagnostics;

// https://gist.github.com/d-kr/eeced4157bf926accc9c6ad435d37a37

namespace BezierSharp
{
    public sealed class MainWindow : GameWindow
    {


        private GRContext context;
        private GRBackendRenderTargetDesc renderTarget;
        public int Width,Height;

        public MainWindow(int Width, int Height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
            this.Width = Width;
            this.Height = Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            var glInterface = GRGlInterface.CreateNativeGlInterface();
            Debug.Assert(glInterface.Validate());
            this.context = GRContext.Create(GRBackend.OpenGL, glInterface);
            Debug.Assert(this.context.Handle != IntPtr.Zero);
            this.renderTarget = CreateRenderTarget();
            CursorVisible = true;
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            this.context?.Dispose();
            this.context = null;
        }

        protected override void OnResize(OpenTK.Windowing.Common.ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }


        protected override void OnUpdateFrame(OpenTK.Windowing.Common.FrameEventArgs e)
        {
            HandleKeyboard();
        }
        private void HandleKeyboard()
        {
            // var keyState = OpenTK.Input.Hid.HidG;
            // if (keyState.IsKeyDown(Key.Escape))
            // {
            //     Exit();
            // }
        }

        public static GRBackendRenderTargetDesc CreateRenderTarget()
        {
            GL.GetInteger(GetPName.FramebufferBinding, out int framebuffer);
            GL.GetInteger(GetPName.StencilBits, out int stencil);
            GL.GetInteger(GetPName.Samples, out int samples);
            int bufferWidth = 0;
            int bufferHeight = 0;
            GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferWidth, out bufferWidth);
            GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferHeight, out bufferHeight);

            return new GRBackendRenderTargetDesc
            {
                Width = bufferWidth,
                Height = bufferHeight,
                Config = GRPixelConfig.Bgra8888, // Question: Is this the right format and how to do it platform independent?
                Origin = GRSurfaceOrigin.TopLeft,
                SampleCount = samples,
                StencilBits = 1,
                RenderTargetHandle = (IntPtr)framebuffer,
            };
        }


        protected override void OnRenderFrame(OpenTK.Windowing.Common.FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            Color4 backColor;
            backColor.A = 1.0f;
            backColor.R = 0.1f;
            backColor.G = 0.1f;
            backColor.B = 0.3f;
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.renderTarget.Width = this.Width;
            this.renderTarget.Height = this.Height;

            using (var surface = SKSurface.Create(this.context, this.renderTarget))
            {
                Debug.Assert(surface != null);
                Debug.Assert(surface.Handle != IntPtr.Zero);

                var canvas = surface.Canvas;

                canvas.Flush();

                var info = this.renderTarget;

                //canvas.Clear(SKColors.Beige);

                using (SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Red,
                    StrokeWidth = 25
                })
                {
                    canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
                    paint.Style = SKPaintStyle.Fill;
                    paint.Color = SKColors.Blue;
                    canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
                }

                canvas.Flush();
            }
            this.context.Flush();
            SwapBuffers();
        }


    }
}