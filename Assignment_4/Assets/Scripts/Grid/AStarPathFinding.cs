using System.Collections.Generic;
using UnityEngine;

public static class AStarPathFinding
{
    //finds shortest path from start to end parameters
    public static List<TileInfo> FindPath(TileInfo start, TileInfo end)
    {
        List<TileInfo> startSet = new List<TileInfo>();
        startSet.Add(start);

        Dictionary<TileInfo, TileInfo> cameFrom = new Dictionary<TileInfo, TileInfo>();
        Dictionary<TileInfo, float> fScore = new Dictionary<TileInfo, float>();//path cost to end 
        Dictionary<TileInfo, float> gScore = new Dictionary<TileInfo, float>();//path cost from start

        // initialize g and h scores to infinity as they are not explored yet 
        for (int x = 0; x < GridGenerator.Instance.gridSizeX; x++)
        {
            for (int z = 0; z < GridGenerator.Instance.gridSizeZ; z++)
            {
                TileInfo tile = GridGenerator.Instance.grid[x, z];
                gScore[tile] = Mathf.Infinity;
                fScore[tile] = Mathf.Infinity;
            }
        }
        //setting start tile gscore to 0 
        gScore[start] = 0;
        fScore[start] = GetHeuristic(start, end);

        while (startSet.Count > 0)
        {
            TileInfo currentTile = null;
            float minF = Mathf.Infinity;

            foreach (TileInfo tile in startSet)
            {
                if (fScore[tile] < minF)
                {
                    minF = fScore[tile];
                    currentTile = tile;
                }
            }


            if (currentTile == end)
                return ReconstructPath(cameFrom, currentTile);

            startSet.Remove(currentTile);

            foreach (TileInfo neighbor in GetNeighbors(currentTile))
            {
                float tentativeGScore = gScore[currentTile] + 1;

                if (tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = currentTile;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + GetHeuristic(neighbor, end);

                    if (!startSet.Contains(neighbor))
                        startSet.Add(neighbor);
                }
            }
        }

        return null;
    }
    //gets the h score i.e. distance between a and b tiles 
    private static float GetHeuristic(TileInfo a, TileInfo b)
    {
        return Mathf.Abs(a.tileX - b.tileX) + Mathf.Abs(a.tileZ - b.tileZ);
    }

    //gets valid neighbors of the node parameter
    private static List<TileInfo> GetNeighbors(TileInfo node)
    {
        List<TileInfo> totalNeighbors = new List<TileInfo>();
        int x = node.tileX;
        int z = node.tileZ;

        //checks all 4 directions of current tile
        CheckAndAddNeighbor(x + 1, z, totalNeighbors);
        CheckAndAddNeighbor(x - 1, z, totalNeighbors);
        CheckAndAddNeighbor(x, z + 1, totalNeighbors);
        CheckAndAddNeighbor(x, z - 1, totalNeighbors);

        return totalNeighbors;
    }
    //checks and adds neighbours which are eligible
    private static void CheckAndAddNeighbor(int x, int z, List<TileInfo> neighbors)
    {
        if (x >= 0 && x < GridGenerator.Instance.gridSizeX &&
            z >= 0 && z < GridGenerator.Instance.gridSizeZ)
        {
            neighbors.Add(GridGenerator.Instance.grid[x, z]);
        }
    }
    //after reaching the final destination it goes through the came from list to get the shortest path list
    private static List<TileInfo> ReconstructPath(Dictionary<TileInfo, TileInfo> cameFrom, TileInfo current)
    {
        List<TileInfo> path = new List<TileInfo> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}