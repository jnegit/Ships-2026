Current solution supports axial pathfinding only.
How would enabling diagonal pathfinding affect the result and what problems would that cause?

Enabling diagonal pathfinding will most likely require fewer steps on the grid to reach the target.

Enabling diagonal pathfinding will make the pathfinding algorithm more costly as the number of neighbours
doubles.

The diagonal pathfinding solution treats all neighbours as equidistant when the distance to
the diagonal neighbours is actually √2 instead of 1 like for the axial neighbours.

Two obstacles neighbouring each other diagonally do not block movement between them.
In the current project this is clearly unwanted behaviour.
