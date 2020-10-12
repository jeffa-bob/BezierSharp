using SkiaSharp;
using System.Linq;
using GLFW;
using System;

namespace BezierSharp2
{
  partial class Program
  {
    static bool LeftMouseIsHeld = false;
    static bool holdingpiece;
    static public bool isTouch;
    static BezierPiece selected;

    public static bool IsInsidePoint(SKPoint MousePoint, SKPoint point, int radius)
    {
      var absX = (MousePoint.X - point.X);
      var absY = (MousePoint.Y - point.Y);
      //Console.WriteLine(((absX*absX)+(absY*absY)).ToString()+" "+(radius*radius).ToString());
      return (absX*absX) + (absY*absY) <= radius * radius;
    }
    public static bool findpoint(SKPoint MousePoint,ref  BezierCurve curve)
    {
      foreach (BezierPiece piece in curve)
      {
        if (IsInsidePoint(MousePoint, piece.touch, 30))
        {
          isTouch = true;
          selected = piece;
          return true;
        }
        if (IsInsidePoint(MousePoint, piece.intersect, 30))
        {
          isTouch = false;
          selected = piece;
          return true;
        }
      }
      return false;
    }

    public static void AjustCurve(ref BezierCurve curve, int surfacehieght)
    {
      InputState a = Glfw.GetMouseButton(window, MouseButton.Left);
      switch (a)
      {
        case InputState.Press:

          double x, y;
          Glfw.GetCursorPosition(window, out x, out y);
          y = -y + surfacehieght;

          if (!LeftMouseIsHeld)
            holdingpiece = findpoint(new SKPoint((float)x, (float)y),ref curve);


          if (holdingpiece)
            if (isTouch)
              selected.touch = new SKPoint((float)x, (float)y);
            else
              selected.intersect = new SKPoint((float)x, (float)y);

          //Console.WriteLine(x.ToString()+" "+y.ToString()+" "+ holdingpiece.ToString()+" "+LeftMouseIsHeld+" "+curve.Count);
          LeftMouseIsHeld = true;
          break;

        case InputState.Release:
          holdingpiece = false;
          LeftMouseIsHeld = false;
          break;
      }
    }
  }
}