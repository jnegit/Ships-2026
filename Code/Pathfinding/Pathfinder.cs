using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Cell = GA.Ships.Pathfinding.NavigationGrid.Cell;

namespace GA.Ships.Pathfinding
{
	public class Pathfinder
	{
		private NavigationGrid _grid = null;

		public Pathfinder(NavigationGrid grid)
		{
			_grid = grid;
		}

		/// <summary>
		/// Performs a breadth-first search to find a path from the start position to the end position.
		/// Link: https://en.wikipedia.org/wiki/Breadth-first_search
		/// </summary>
		/// <param name="startPosition">Start position</param>
		/// <param name="endPosition">End position</param>
		/// <returns>List of positions representing the path, or null if no path is found</returns>
		public IList<Vector3> BreadthFirstSearch(Vector3 startPosition, Vector3 endPosition)
		{
			Queue<Cell> frontier = new Queue<Cell>();
			Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();

			Cell startCell = _grid.GetCell(startPosition);
			Cell endCell = _grid.GetCell(endPosition);

			frontier.Enqueue(startCell);
			cameFrom[startCell] = null;

			bool isEndReached = false; // Early exit flag

			while (frontier.Count > 0)
			{
				Cell current = frontier.Dequeue();

				isEndReached = current == endCell;
				if (isEndReached)
				{
					// The end node is reached. Path is complete.
					break;
				}

				IList<Cell> neighbours = _grid.GetNeighbours(current, includeDiagonal: false);
				foreach (Cell neighbour in neighbours)
				{
					if (neighbour.IsWalkable && !cameFrom.ContainsKey(neighbour))
					{
						frontier.Enqueue(neighbour);
						cameFrom[neighbour] = current;
					}
				}
			}

			// If isEndReached is false here, there is no path to the end cell.
			if (isEndReached)
			{
				// Construct path
				return ConstructPath(startCell, endCell, cameFrom);
			}

			// There is no path between start and end positions.
			return null;
		}

		private IList<Vector3> ConstructPath(Cell startCell, Cell endCell, Dictionary<Cell, Cell> cameFrom)
		{
			IList<Vector3> path = new List<Vector3>();
			Cell current = endCell;

			while (current != startCell)
			{
				path.Add(current.WorldPosition);
				current = cameFrom[current];
			}

			path.Reverse();

			return path;
		}

		public IList<Cell> GetReachableCells(Cell start, int maxSteps)
		{
			List<Cell> reachableCells = new List<Cell>();

			if (start == null)
			{
				throw new ArgumentNullException(nameof(start), "Starting cell cannot be null.");
			}
			if (maxSteps < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(maxSteps), "Maximum steps cannot be negative.");
			}

			// Queue of cells to examine and their distance from the starting cell in grid steps.
			Queue<(Cell cell, int steps)> queue = new Queue<(Cell, int)>();

			// Examined cells are stored to avoid examining same cell multiple times.
			HashSet<Cell> examinedCells = new HashSet<Cell>();

			// Begin examining cells from the starting cell.
			queue.Enqueue((start, 0));
			examinedCells.Add(start);

			while (queue.Count > 0)
			{
				(Cell current, int currentSteps) = queue.Dequeue();
				// The maxSteps does not include the start Cell.
				if (currentSteps > 0)
				{
					reachableCells.Add(current);
				}

				// Stop examining neighbouring cells once maxSteps is reached.
				if (currentSteps >= maxSteps)
				{
					continue;
				}


				// Creates a list of the walkable cells that neighbour the current cell.
				// Diagonal neighbours are excluded.
				IList<Cell> neighbours = _grid.GetNeighbours(current, false);

				foreach (Cell cell in neighbours)
				{
					// Exclude cells that have already been examined.
					if (!examinedCells.Contains(cell))
					{
						// Add the cell for the next step to the queue.
						examinedCells.Add(cell);
						queue.Enqueue((cell, currentSteps + 1));
					}
				}
			}
			return reachableCells;
		}
	}
}