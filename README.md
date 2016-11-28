# steiner-heuristic
A heuristic to solve the Steiner Tree Problem for Lattice Graphs - proudly written in C#


## Getting started
This should work instantly with Visual Studio assuming you have the latest .NET framework. If not, open an issue.

## Problem Domain
Given a graph **G** that is a [grid graph](http://mathworld.wolfram.com/GridGraph.html) of dimension m x n (m≥ n≥ 2) with equally weighted edges (weight>0) and a set of vertices **V** that are all members of **G**, find a route them that minimizes this: (total number of vertices visited - |**V**|). 

Quick example: If you had a 3 x 3 graph with zero based indexing (i.e. the graph goes from 0,0 to 2,2), the best route would be (0,0) -> (1,0) -> (2,0) -> (2,1) -> (2,2) which would return a value of 3. Remember that |**V**| can be >2 so this is about as trivial of an example as you can get. 

It's easy to see that the subgraph induced by the vertex set that represents the route must be a tree. This makes this problem the [Steiner Tree Problem](http://www.geeksforgeeks.org/steiner-tree/) applied to grid graphs. 

## Solution
Note: it's possible that there's a perfect algorithim for this. Finding it would undoubtedly require more mathematical analysis & research than I did.  

### Algorithim Explanation
1. Create an order of visiting for the targets
    1. Of the vertices with the lowest vertical index, pick the one with the lowest horizontal index (i.e. the one closes to the "northwest".
    2. If there's only one other target, that's next. If not, pick the target closest to the current target for the next.
2. Select the optimal direction  for the first two points from step 1. This direction (left/right) will represent where the program tends to go. For example, in the example above, were the preferred direction left, the solution would have gone through 0,2 and not 2,0. 
    1. Go in the direction (relative to the destination of this step) of the next target x value that is not the same as the current destinations's x value. If there is none that meets that condition, set the program to go left (note: this is flawed - easy improvement here)
3. Using [Breadth First Search](https://en.wikipedia.org/wiki/Breadth-first_search), find the optimal route between the two points. Use the direction from step 2 to determine which neighbors get added to the Queue ADT first.
4. Repeat step 2 and 3 |**V**|-1 times.

## License
Licensed under the MIT License - do what you will with this code, within reason.
