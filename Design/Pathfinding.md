Water types:
Non-navigable - cost negative
Regular - cost 3
Shallow - cost 4
Rocky - cost 5
Current - cost 2

StaticBody3D for any terrain that is inaccessible for a ship like land and any set obstructions.
Area3D for terrain that is navigable for a ship.
PhysicsDirectSpaceState3D used to do queries against objects and areas in the navigation space.

Pseudocode:
NavigationGrid[Width, Height]
PhysicsDirectSpaceState3D
for each Y from 0 to Height:
	for each X from 0 to Width:
		Position = CalculatePosition(x,y)
		if IntersectsWithStaticBody3D(PhysicsDirectSpaceState3D, Position)
			NavigationGrid[X,Y].Cost = -1
			continue
		else 
			SWITCH(IntersectsWithArea3DType(PhysicsDirectSpaceState3D, Position)):
				CASE "Regular": NavigationGrid[X,Y].Cost = 3
				CASE "Shallow": NavigationGrid[X,Y].Cost = 4
				CASE "Rocky": NavigationGrid[X,Y].Cost = 5
				CASE "Current": NavigationGrid[X,Y].Cost = 2
				DEFAULT: NavigationGrid[X,Y].Cost = 3
