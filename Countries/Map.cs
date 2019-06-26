using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Countries
{
    public class Map : IEnumerable
    {
        private readonly List<Plot> _plots;

        public Map(int[][] matrix)
        {
            _plots = ConvertMatrix(matrix);
        }

        public Map(int[,] matrix)
        {
            _plots = ConvertMatrix(matrix);
        }

        private List<Plot> ConvertMatrix(int[][] matrix)
        {
            List<Plot> result = new List<Plot>();

            int rows = matrix.Length;
            int columns = matrix[0].Length;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    result.Add(new Plot(matrix[row][column], new Point(row, column)));
                }
            }

            return result;
        }

        private List<Plot> ConvertMatrix(int[,] matrix)
        {
            List<Plot> result = new List<Plot>();

            int rows = matrix.GetUpperBound(0);
            int columns = matrix.GetUpperBound(1);

            for (int row = 0; row <= rows; row++)
            {
                for (int column = 0; column <= columns; column++)
                {
                    result.Add(new Plot(matrix[row,column], new Point(row, column)));
                }
            }

            return result;
        }

        public Plot Get(int x, int y)
        {
            Plot result = _plots.Find(p => p.X == x && p.Y == y);
            return result;
        }

        public Plot Get(Point point)
        {
            Plot result = _plots.Find(p => p.X == point.X && p.Y == point.Y);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public MapEnumerator GetEnumerator()
        {
            return new MapEnumerator(_plots);
        }

        override
        public string ToString() {
            StringBuilder result = new StringBuilder();

            int row = 0;

            foreach (Plot plot in _plots) {
                if (plot.X != row) {
                    result.Append(System.Environment.NewLine);
                    row++;
                }
                result.Append($"[ {plot.Colour} ] ");
            }

            return result.ToString();
        }
    }

    public class MapEnumerator : IEnumerator
    {
        private int _position;

        public List<Plot> Plots;

        public MapEnumerator(List<Plot> plots)
        {
            this._position = -1;
            this.Plots = plots;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < Plots.Count;
        }

        public void Reset()
        {
            _position = -1;
        }

        object IEnumerator.Current => Plots[_position];

        public Plot Current
        {
            get
            {
                try
                {
                    return Plots[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }

            }
        }
    }
}