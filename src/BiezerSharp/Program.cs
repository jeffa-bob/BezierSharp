using System;
using SkiaSharp;

namespace BiezerSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SKImageInfo imageInfo = new SKImageInfo(width: 640, height: 480, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            using (var surface = SKSurface.Create(imageInfo))
            {
                SKCanvas myCanvas = surface.Canvas;

                // Your drawing code goes here.
            }
        }
    }
}
