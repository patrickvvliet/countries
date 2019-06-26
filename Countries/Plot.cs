using System.Drawing;

namespace Countries
{
    public class Plot
    {
        public int Colour { get; private set; }

        private Point _point;
        public readonly Point Point;

        public int X { get { return _point.X; } }
        public int Y { get { return _point.Y; } }

        public Plot(int colour, int x, int y)
        {            
            Init(colour, new Point(x, y));
        }
        public Plot(int colour, Point point)
        {
            Init(colour, point);
        }

        private void Init(int colour, Point point) {
            this.Colour = colour;
            this._point = point;
        }
    }
}
