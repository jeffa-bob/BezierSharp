using System;
using SkiaSharp;
using Glfw;

namespace BezierSharp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Glfw.Glfw3.Init();
            Glfw.Glfw3.CreateWindow(1000,800,"BezierSharp",MonitorHandle.Zero,IntPtr.Zero);
            Console.WriteLine("Hello World!");
        }
    }
}
