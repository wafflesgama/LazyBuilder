using Sceelix.Mathematics.Spatial;

namespace Sceelix.Mathematics.Data
{
    public struct Rectangle
    {
        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }



        public Rectangle(UnityEngine.Vector2 min, UnityEngine.Vector2 max)
        {
            X = min.x;
            Y = min.y;
            Width = max.x - min.x;
            Height = max.y - min.y;
        }



        public Rectangle(BoundingBox boundingBox)
        {
            X = boundingBox.Min.x;
            Y = boundingBox.Min.y;
            Width = boundingBox.Width;
            Height = boundingBox.Length;
        }



        public float X
        {
            get;
        }


        public float Y
        {
            get;
        }


        public float Width
        {
            get;
        }


        public float Height
        {
            get;
        }


        public UnityEngine.Vector2 Min => new UnityEngine.Vector2(X, Y);


        public UnityEngine.Vector2 Max => new UnityEngine.Vector2(X + Width, Y + Height);



        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}, Width: {2}, Height: {3}", X, Y, Width, Height);
        }



        public Rectangle Merge(Rectangle rectangle)
        {
            return new Rectangle(UnityEngine.Vector2.Minimize(rectangle.Min, Min), UnityEngine.Vector2.Maximize(rectangle.Max, Max));
        }



        public static implicit operator System.Drawing.Rectangle(Rectangle rectangle)
        {
            return new System.Drawing.Rectangle((int) rectangle.x, (int) rectangle.y, (int) rectangle.Width, (int) rectangle.Height);
        }



        public static implicit operator Rectangle(System.Drawing.Rectangle rectangle)
        {
            return new Rectangle(rectangle.x, rectangle.y, rectangle.Width, rectangle.Height);
        }
    }
}