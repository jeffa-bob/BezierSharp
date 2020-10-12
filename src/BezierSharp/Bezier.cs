using SkiaSharp;

public class BezierCurve {
  SKPoint start, end, touch1,touch2;

  public void Draw(ref SKCanvas canvas, SKPaint paint, SKPathFillType fillType){
    using (SKPath path = new SKPath()){
      path.MoveTo(start);
      path.CubicTo(touch1,touch2,end);
      path.FillType = fillType;
      canvas.DrawPath(path,paint);
    }
  }
  public BezierCurve(SKPoint start, SKPoint end, SKPoint touch1, SKPoint touch2){
    this.start = start;
    this.end = end;
    this.touch1 = touch1;
    this.touch2 = touch2;
  }  
  public BezierCurve(){}
}