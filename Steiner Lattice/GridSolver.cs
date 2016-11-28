using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteinerLattice
{
    class GridSolver
    {
        Grid Grid;
        enum Directions { left, right,ignore};
     //   Directions way = Directions.left;
       
        public GridSolver(Grid grid)
        {
            Grid = grid;      
        }

        public int Solve()
        {
            List<Point> order = CreateTargetPriorityList();
            int initialQueueLength = order.Count;
            
            Console.WriteLine("Begin Actual Solving");
            for(int i=0; i< initialQueueLength - 1; i++)
            {
                Point start = order[0];
                order.RemoveAt(0);
                Point end = order[0];
                Grid.MarkPointAndParents(SolveP2P(start, end, PreferredDirection(start, end, order)));
                Grid.MarkPoint(start);
            }
            // need to adjust for left/right

            return Grid.CountUniqueMarked() - initialQueueLength - (initialQueueLength - 2);
            
        }


        private Directions PreferredDirection(Point start, Point end, List<Point> order)
        {
            Directions d = Directions.ignore;
            for(int i=1; i<order.Count; i++) // it loops so if you have multiple targets on the same line, you can get the next one or the next next etc etc
            {
                if (end.X < order[i].X) // the next thing after this solve is to the right - let's go
                {
                    return Directions.right;
                }
                if (end.X > order[i].X) // ditto but left
                {
                    return Directions.right;
                }
            }

           

            return Directions.ignore;
        }

        // returns final point with the path via the "Parent" linked list. 
        private Point SolveP2P(Point start, Point end, Directions preference) // P2P = point to point
        {
            // Breadth first search leggo
            Queue<Point> agenda = new Queue<Point>();
            List<Point> paths = new List<Point>();
         

            agenda.Enqueue(start);

            while (agenda.Count > 0)
            {
                Point current = agenda.Dequeue();
                
                Grid.MarkVisited(current); // been here done that
            
                if (current == end) // are we there yet?
                {
                    
                    paths.Add(current);
                    break;
                }

                foreach (Point p in Adjacents(current,preference)) // go through all the adjacent points - see if we need to go there
                {
                    if (!Grid.IsVisited(p))
                    {
                        p.Parent = current;
                        agenda.Enqueue(p);
                    }
                    
                }
              
                paths.Add(current);
            }

            Grid.ResetVisited();
            Console.WriteLine("End Solve of" + start + " and " + end);
            return paths.Last(); // returns the end point and the linked list via the "Parent" property
        }

        // takes all of our targets and creates the order through which we should visit them
        // note that this prioritizes by lowest to highest row index and then lowest to highest colomn index targets
        private List<Point> CreateTargetPriorityList()
        {
            List<Point> orderedTargets = new List<Point>();
            List<Point> unorderedTargets = Grid.Targets; // WATCH FOR CLONING ISSUES

            orderedTargets.Add(unorderedTargets[0]); // add an arbitrary target - we'll improve this later
            unorderedTargets.RemoveAt(0);


            int indexBestWeight = -1;
            int bestWeight = 10000;
            while (unorderedTargets.Count != 0)
            {
                if (unorderedTargets.Count == 1)
                {
                    orderedTargets.Add(unorderedTargets[0]);
                    break;
                }

                int index = 0; // I prefer foreach hence the work around
                foreach(Point p in unorderedTargets)
                {
                    int weight = SolveP2P(orderedTargets[0], p,Directions.ignore).PathLength();
                    if (weight<bestWeight)
                    {
                        indexBestWeight = index;
                        bestWeight = weight;
                    }
                    index++;
                }

                orderedTargets.Add(unorderedTargets[indexBestWeight]);
                unorderedTargets.RemoveAt(indexBestWeight);

            }

            return orderedTargets;              
        }
         
        private List<Point> Adjacents(Point start, Directions direction)
        {
            List<Point> result = new List<Point>();


            switch (direction)
            {
                case Directions.left: // go left then right
                    if (Grid.InBound(start.X - 1, start.Y)) // west or left
                    {
                        result.Add(new Point(start.X - 1, start.Y));
                    }

                    if (Grid.InBound(start.X + 1, start.Y)) // east or right
                    {
                        result.Add(new Point(start.X + 1, start.Y));
                    }

                    break;

                case Directions.right: // go right then left

                    if (Grid.InBound(start.X + 1, start.Y)) // east or right
                    {
                        result.Add(new Point(start.X + 1, start.Y));
                    }

                    if (Grid.InBound(start.X - 1, start.Y)) // west or left
                    {
                        result.Add(new Point(start.X - 1, start.Y));
                    }

                    break;

                case Directions.ignore: // idrc which way it goes but not worth introducing RNG
                    if (Grid.InBound(start.X - 1, start.Y)) // west or left
                    {
                        result.Add(new Point(start.X - 1, start.Y));
                    }

                    if (Grid.InBound(start.X + 1, start.Y)) // east or right
                    {
                        result.Add(new Point(start.X + 1, start.Y));
                    }
                    break;

            }

            

            if (Grid.InBound(start.X, start.Y-1)) //south
            {
                result.Add(new Point(start.X, start.Y-1));
            }

            if (Grid.InBound(start.X, start.Y + 1)) // north
            {
                result.Add(new Point(start.X, start.Y + 1));
            }
            return result;
        }
     
    }
}
