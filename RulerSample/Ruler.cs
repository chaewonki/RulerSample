using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

public class Ruler : Shape
{
    // Dependency Property 
    public static readonly DependencyProperty X1Property = DependencyProperty.Register(
        "X1", typeof(double), typeof(Ruler), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty Y1Property = DependencyProperty.Register(
        "Y1", typeof(double), typeof(Ruler), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty X2Property = DependencyProperty.Register(
        "X2", typeof(double), typeof(Ruler), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty Y2Property = DependencyProperty.Register(
        "Y2", typeof(double), typeof(Ruler), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static new readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
        "StrokeThickness", typeof(double), typeof(Ruler), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

    // Property for Dependency Property
    public double X1
    {
        get { return (double)GetValue(X1Property);}
        set { SetValue(X1Property, value);}
    }

    public double Y1
    {
        get { return (double)GetValue(Y1Property);}
        set { SetValue(Y1Property, value);}
    }

    public double X2
    {
        get { return (double)GetValue(X2Property);}
        set { SetValue(X2Property, value);}
    }

    public double Y2
    {
        get { return (double)GetValue(Y2Property);}
        set { SetValue(Y2Property, value);}
    }

    public new double StrokeThickness
    {
        get { return (double)GetValue(StrokeThicknessProperty);}
        set { SetValue(StrokeThicknessProperty, value); }
    }

    protected override Geometry DefiningGeometry
    {
        get
        {
            LineGeometry geometry = new LineGeometry(new Point(X1, Y1), new Point(X2, Y2));
            return geometry;
        }
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        Pen pen = new Pen(Stroke, StrokeThickness);
        drawingContext.DrawLine(pen, new Point(X1, Y1), new Point(X2, Y2));

        double length = Math.Sqrt(Math.Pow(X2 - X1, 2) +  Math.Pow(Y2 - Y1, 2));
        FormattedText formattedText = new FormattedText(
            length.ToString("F2") + " px",
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface("Arial"),
            12,
            Brushes.Black,
            VisualTreeHelper.GetDpi(this).PixelsPerDip);

        Point midpoint = new Point((X1 + X2) / 2, (Y1 + Y2) / 2);
        drawingContext.DrawText(formattedText, new Point(midpoint.X + 5, midpoint.Y + 5));

        DrawTicks(drawingContext, pen);
    }

    private void DrawTicks(DrawingContext drawingContext, Pen pen)
    {
        double interval = 10.0;
        double tickLength = 10.0;

        Vector lineVector = new Vector(X2 - X1, Y2 - Y1);
        double lineLength = lineVector.Length;
        lineVector.Normalize();

        // 수직벡터
        Vector perpendicularVector = new Vector(-lineVector.Y, lineVector.X);

        for (double i = 0; i < lineLength; i += interval)
        {
            Point tickPoint = new Point(X1, Y1) + lineVector * i;
            Point tickStart = tickPoint - perpendicularVector * tickLength / 2;
            Point tickEnd = tickPoint + perpendicularVector * tickLength / 2;

            drawingContext.DrawLine(pen, tickStart, tickEnd);
        }
    }
}