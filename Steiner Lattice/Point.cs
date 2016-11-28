using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteinerLattice
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point Parent { get; set; } // linked lists op
      


        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
            
        }


     

        public Point(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
            
        }

        public static bool operator ==(Point one, Point two)
        {
            if (object.ReferenceEquals(one, null) || (object.ReferenceEquals(two, null)))
            {
                return object.ReferenceEquals(one, two);
            }


            if (one.X != two.X) return false;
            if (one.Y != two.Y) return false;
           
            return true;

        }

        public int PathLength()
        {
            Point p = this;
            int count = 1;

            while (p.Parent != null)
            {
                Console.WriteLine(p);
                p = p.Parent;
                count++;
            }

            return count;
        }

        public static bool operator !=(Point one, Point two)
        {
            return !(one == two); // short and sweet
        }

        public static Point operator +(Point one , Point two) // note this does NOT transfer IsTarget
        {
            return new Point(one.X + two.X, one.Y + two.Y);
        }


        public static Point operator -(Point one, Point two) // note this does NOT transfer IsTarget
        {
            return new Point(one.X - two.X, one.Y - two.Y);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
