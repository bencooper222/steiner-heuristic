using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteinerLattice
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Grid grid = new Grid(7, 7);
            grid.AddTarget(0, 0);
            //grid.AddTarget(2, 0);
            grid.AddTarget(2, 2);
           
            
            Grid grid = new Grid(7, 11);
            grid.AddTarget(0, 0);
            grid.AddTarget(2, 2);
            grid.AddTarget(0, 4);
            
            
            GridSolver solver = new GridSolver(grid);
            */

            Grid grid = Grid.ParseText();
            GridSolver solver = new GridSolver(grid);
            Console.WriteLine(grid.XLength + " " + grid.YLength);

            Console.WriteLine(solver.Solve());


           // Console.WriteLine(solver.Solve());
            Console.ReadKey();
        }
    }
}
