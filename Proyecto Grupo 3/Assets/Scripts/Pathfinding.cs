using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;


public class Pathfinding
{
    public static List<Block> showPossible(Block startingBlock, int dist, int jump, int belongsToPlayer,bool moving) //crea una lista con las casillas posibles a las que se puede mover el jugador
    {
        dist ++;
        List<Block> toSearch = new List<Block>() { startingBlock };
        List<Block> processed = new List<Block>();
        List<Block> nextToSearch = new List<Block>();
        for (int i = 0; i < dist; i++)
        {
            foreach (var current in toSearch)
            {
                processed.Add(current);
                if (!moving)
                {
                    foreach (Block block in current.Neighbors.Where(block => block.isWalkable(Mathf.Abs(current.height - block.height), jump)
                    && !processed.Contains(block) && !block.obstacle))
                    {
                        if (!nextToSearch.Contains(block))
                        {
                            nextToSearch.Add(block);
                        }
                    }
                }
                else
                {
                    foreach (Block block in current.Neighbors.Where(block => block.isWalkable(Mathf.Abs(current.height - block.height), jump)
                   && !processed.Contains(block) && !block.obstacle && (belongsToPlayer == block.ReturnCharacterPlayerNumber() || !block.containsCharacter)))
                    {
                        if (!nextToSearch.Contains(block))
                        {
                            nextToSearch.Add(block);
                        }
                    }
                }
            }
            toSearch = new List<Block>(nextToSearch);
            nextToSearch.Clear();
        }
        return processed;
    }


    public static List<Block> findPath(Block startingBlock, Block targetBlock, int jump, int belongsToPlayer,bool moving) //Crea una lista con los bloques en el camino mas corto hacia el targetBlock
    {
        bool isMoving = moving;
        List<Block> toSearch = new List<Block>() { startingBlock };
        List<Block> processed = new List<Block>();
        if (targetBlock.containsCharacter != true)
        {
            while (toSearch.Any())
            {
                Block current = toSearch[0];
                foreach (Block i in toSearch)
                    if (i.F < current.F || i.F == current.F && i.H < current.H)
                        current = i;
                processed.Add(current);
                toSearch.Remove(current);

                if (current == targetBlock)
                {
                    var currentPathTile = targetBlock;
                    var path = new List<Block>();
                    var count = 100;
                    while (currentPathTile != startingBlock)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0) throw new Exception();
                    }
                    path.Reverse();
                    return path;
                }
                if (!moving)
                {
                    foreach (Block block in current.Neighbors.Where(block => block.isWalkable(Mathf.Abs(current.height - block.height), jump) == true
                    && !processed.Contains(block) && !block.obstacle))
                    {
                        var inSearch = toSearch.Contains(block);
                        var costToNeighbor = current.G + current.GetDistance(current, block);

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
                else
                {
                    foreach (Block block in current.Neighbors.Where(block => block.isWalkable(Mathf.Abs(current.height - block.height), jump) == true
                                && !processed.Contains(block) && !block.obstacle && (belongsToPlayer == block.ReturnCharacterPlayerNumber() || !block.containsCharacter)))
                    {
                        var inSearch = toSearch.Contains(block);
                        var costToNeighbor = current.G + current.GetDistance(current, block);

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
            }
        }
        return null;
    }
}

