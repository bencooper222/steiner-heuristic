using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteinerLattice
{
    class Grid
    {
        public int XLength { get; set; }    
        public int YLength { get; set; }
        public List<Point> Targets { get; }
        List<Point> Visited; // short term
        List<Point> Marked; // long term


        public Grid(int xLength, int yLength) // note this is RAW number of characters or number of lines - not number of spots in the grid
        {
            XLength = (xLength - 1) / 2;
            YLength = (yLength - 1) / 2;

            Targets = new List<Point>();
            Visited = new List<Point>();
            Marked = new List<Point>();
        }

        public Grid()
        {
            Targets = new List<Point>();
            Visited = new List<Point>();
            Marked = new List<Point>();
        }


        public void AddTarget(Point tgt) // just slightly easier than making the list Targets public
        {
            Targets.Add(tgt);
        }

        public void AddTarget(int x, int y)
        {
            Targets.Add(new Point(x, y));
        }


        public void MarkPoint(Point mark)
        {
            Marked.Add(mark);
        }


        public int CountUniqueMarked()
        {
           return (from x in Marked select x).Distinct().Count();

            // creds to: https://www.codeproject.com/questions/456824/how-to-count-distinct-in-a-list-in-csharp
            // <3 LINQ 
        }
        public void MarkPointAndParents(Point mark)
        {
            Point p = mark;
           

            while (p.Parent != null)
            {
                MarkPoint(p);
                p = p.Parent;
              
            }

            
        }
        public void AddTargets(List<Point> targets)
        {
            foreach(Point p in targets)
            {
                AddTarget(p);
            }
        }

        public void MarkVisited(int x, int y) // I'm just trusting the person using this method and not catching any exceptions

        {
            Visited.Add(new Point(x, y));
        }

        public void MarkVisited(Point p) // I'm just trusting the person using this method and not catching any exceptions

        {
            Visited.Add(p);
        }


        // this was necessary because while experimenting with the program, I actually used visited as constant. 
        // that's not true anymore but for experimental purposes, I prefer this setup - this isn't production code
        public void ResetVisited()
        {
            Visited = new List<Point>();
        }

        public bool IsVisited(int x, int y)
        {
            Point target = new Point(x, y);

            foreach(Point p in Visited)
            {
                if (p == target)
                {
                    return true;
                }
            }
            return false;
        }


        public bool IsVisited(Point target)
        {
          

            foreach (Point p in Visited)
            {
                if (p == target)
                {
                    return true;
                }
            }
            return false;
        }

        public bool InBound(int x, int y)
        {
            return (x < XLength & x >= 0) & (y < YLength & y >= 0);
        }

        

        public static Grid ParseText()
        {
            Grid grid = new Grid();
            int y = 1;
            int x;

            string line = Console.ReadLine();
            x = line.Length;
            grid.AddTargets(grid.FindTargetsInLine(line, y));
            do
            {
                y++;
                line = Console.ReadLine();
                grid.AddTargets(grid.FindTargetsInLine(line, y)); // add the targets
            } while (line[line.Length - 1] != '+');


            grid.XLength = (x - 1) / 2;
            grid.YLength = (y - 1) / 2;
            return grid;
            
        }

        // parses a line to find targets
        private List<Point> FindTargetsInLine(string line, int y)
        {
            List<Point> targets = new List<Point>();    
            for(int i=0; i<line.Length; i++)
            {
                if (line[i] == 'X')
                {
                    targets.Add(new Point((i-1)/2, (y-1)/2));
                }
            }

            return targets;
        }

     
    }
}
