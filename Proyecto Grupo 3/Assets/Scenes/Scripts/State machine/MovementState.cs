using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovementState
{
    private List<Block> possibleBlocks = new List<Block>();
    public void OnStateEnter() //Volver "OnStateEnter", placeholder.
    {   
        CharacterController current = TurnController.currentCharacter;
        possibleBlocks = Pathfinding.showPossible(current.currentBlock, current.dist, current.jump);
        Block.onBlockClicked += MoveToClicked;
    }

    private void MoveToClicked(Block clicked)
    {
        if(possibleBlocks.Exists(Block => Block == clicked))
        {
            CharacterController current = TurnController.currentCharacter;
            List<Block> path = Pathfinding.findPath(current.currentBlock, clicked, current.jump);
            TurnController.currentCharacter.CharacterMove(path);
        }
    }
    public void OnStateEnd()
    {
        Block.onBlockClicked -= MoveToClicked;
    }
}
