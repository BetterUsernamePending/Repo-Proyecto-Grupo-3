using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

public class Pathfinding 
{
    public static List<Block> showPossible(Block startingBlock, int dist)
    {
        List<Block> toSearch = new List<Block>() { startingBlock };
        List<Block> processed = new List<Block>();
        List<Block> possibles = new List<Block>();
        int movesleft = dist;
        while (toSearch.Any() && movesleft > 0)
        {
            Block current = toSearch[0];
            foreach (Block i in toSearch)
            possibles.Add(current);
            processed.Add(current);
            toSearch.Remove(current);


            movesleft--;
        }
        return possibles;
    }
    public static List <Block> findPath(Block startingBlock, Block targetBlock) //targetBlock se define por el jugador (hacer sistema)
    {
        List<Block> toSearch = new List <Block>() { startingBlock };
        List<Block> processed = new List <Block>();

        while (toSearch.Any())
        {
            Block current = toSearch[0];
            foreach (Block i in toSearch)
                if (i.F < current.F || i.F == current.F && i.H < current.H)
                    current = i;

            processed.Add(current);
            toSearch.Remove(current);

            foreach (Block block in current.Neighbors.Where(block => block.isWalkable(current.height) == true && !processed.Contains(block)))
            {
                var inSearch = toSearch.Contains(block);
                var costToNeighbor = current.G + 1;

                if(!inSearch || costToNeighbor < block.G)
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
