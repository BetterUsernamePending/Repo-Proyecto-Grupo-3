using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;

public class Pathfinding
{
    public static List<Block> showPossible(Block startingBlock, int dist, int jump)
    {
        
        List<Block> toSearch = new List<Block>() { startingBlock };
        List<Block> processed = new List<Block>();
        List<Block> possibles = new List<Block>();
        int movesleft = dist;
        while (toSearch.Count > 0 && movesleft > 0)
        {
            Block current = toSearch[0];
            possibles.Add(current);
            processed.Add(current);
            toSearch.Remove(current);
            foreach (Block block in current.Neighbors.Where(block =>
                block.isWalkable(Mathf.Abs(current.height - block.height), jump) && !processed.Contains(block)))
            {
                var inSearch = toSearch.Contains(block);
                if (!inSearch)
                {
                    toSearch.Add(block);
                }
            }
            movesleft--;
        }
        for (int i = 0; i < possibles.Count; i++)
        {
            Debug.Log(possibles[i].name);
        }

        return possibles;
    }
    public static List<Block> findPath(Block startingBlock, Block targetBlock, int jump) //targetBlock se define por el jugador (hacer sistema)
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

            foreach (Block block in current.Neighbors.Where(block => block.isWalkable(current.height,jump) == true && !processed.Contains(block)))
            {
                var inSearch = toSearch.Contains(block);
                var costToNeighbor = current.G + 1;

                if (!inSearch || costToNeighbor < block.G)
                {
                    block.SetG(costToNeighbor);
                    block.SetConnection(current);

                    if (!inSearch)
                    {
                        block.SetH(block.GetDistance(targetBlock));
                        toSearch.Add(block);
                    }

                }
            }
        }
        return null;
    }
}
