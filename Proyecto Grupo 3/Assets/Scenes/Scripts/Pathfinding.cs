using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using static Unity.Collections.AllocatorManager;

public class Pathfinding
{ 
    public static List<Block> showPossible(Block startingBlock, int dist, int jump) //crea una lista con las casillas posibles a las que se puede mover el jugador
    {
        List<Block> toSearch = new List<Block>() { startingBlock };
        List<Block> processed = new List<Block>();
        List<Block> nextToSearch = new List<Block>();
        for (int i = 0; i < dist; i++)
        {
            foreach (var current in toSearch)
            {
                current.transform.position = new Vector3(current.transform.position.x, current.transform.position.y + 1, current.transform.position.z);
                processed.Add(current);
                Debug.Log(current.name);
                foreach (Block block in current.Neighbors.Where(block => block.isWalkable(Mathf.Abs(current.height - block.height), jump)
                && !processed.Contains(block) && !block.obstacle))
                {
                    if (!nextToSearch.Contains(block))
                    {
                        nextToSearch.Add(block);
                    }

                }
            }
            toSearch = new List<Block>(nextToSearch);
            nextToSearch.Clear();
        }
        return processed;
    }
    public static List<Block> findPath(Block startingBlock, Block targetBlock, int jump) //Crea una lista con los bloques en el camino mas corto hacia el targetBlock
    {
        List<Block> toSearch = new List<Block>() { startingBlock };
        List<Block> processed = new List<Block>();

        while (toSearch.Any())
        {
            Block current = toSearch[0];
            foreach (Block i in toSearch)
                if (i.F < current.F || i.F == current.F && i.H < current.H)
                    current = i;

            processed.Add(current);
            toSearch.Remove(current);

            foreach (Block block in current.Neighbors.Where(block => block.isWalkable(Mathf.Abs(current.height - block.height), jump) == true 
                && !processed.Contains(block) && !block.obstacle))
            {
                var inSearch = toSearch.Contains(block);
                var costToNeighbor = current.G + 1;

                if (!inSearch || costToNeighbor < block.G)
                {
                    block.SetG(costToNeighbor);
                    block.SetConnection(current);

                    if (!inSearch)
                    {
                        block.SetH(block.GetDistance(startingBlock, targetBlock));
                        toSearch.Add(block);
                    }

                }
            }
        }
        return null;
    }
}

