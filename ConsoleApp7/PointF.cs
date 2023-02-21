public struct PointF
{
    public float X { get; set; }
    public float Y { get; set; }

    public PointF(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static PointF operator +(PointF left, PointF right)
    {
        return new PointF(left.X + right.X, left.Y + right.Y);
    }

    public static PointF operator -(PointF left, PointF right)
    {
        return new PointF(left.X - right.X, left.Y - right.Y);
    }

    public static PointF operator *(float scalar, PointF point)
    {
        return new PointF(scalar * point.X, scalar * point.Y);
    }

    public static PointF operator *(PointF point, float scalar)
    {
        return scalar * point;
    }

    public static float Distance(PointF a, PointF b)
    {
        return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}