using System.Collections.Generic;
using SkiaSharp;

public class BezierPiece
{
    public SKPoint intersect, touch;

    public BezierPiece(SKPoint intersect, SKPoint touch)
    {
        this.intersect = intersect;
        this.touch = touch;
    }
    public BezierPiece() { }

}
public class BezierCurve : List<BezierPiece>
{

    public void Draw(ref SKCanvas canvas, SKPaint paint, SKPathFillType fillType)
    {
        for (int iter = 1; iter < base.Count; iter++)
        {
            using (SKPath path = new SKPath())
            {
                path.MoveTo(base[iter - 1].intersect);
                path.CubicTo(base[iter - 1].touch, base[iter].touch, base[iter].intersect);
                path.FillType = fillType;
                canvas.DrawPath(path, paint);
            }
            canvas.DrawCircle(base[iter].touch.X, base[iter].touch.Y, 6, paint);
            canvas.DrawCircle(base[iter].intersect.X, base[iter].intersect.Y, 6, paint);
        }
        canvas.DrawCircle(base[0].touch.X, base[0].touch.Y, 6, paint);
        canvas.DrawCircle(base[0].intersect.X, base[0].intersect.Y, 6, paint);

    }

    public BezierCurve(List<BezierPiece> biezers) : base(biezers)
    {
    }
    public BezierCurve() : base() { }
}