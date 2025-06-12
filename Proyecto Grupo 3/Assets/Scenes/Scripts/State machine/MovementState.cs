using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovementState : State
{
    private List<Block> possibleBlocks = new List<Block>();
    private List<Block> pathBlocks = new List<Block>();
    private bool alreadyMoved = false;
    public override void OnStateEnter()
    {   
        base.OnStateEnter();
        CharacterController current = TurnController.currentCharacter;
        possibleBlocks = Pathfinding.showPossible(current.currentBlock, current.dist, current.jump);
        Block.onBlockClicked += ShowPossibleTargets;
        alreadyMoved = false;
    }

    public override void ShowPossibleTargets(Block clicked)
    {
        if (possibleBlocks.Exists(Block => Block == clicked))
        {
            CharacterController current = TurnController.currentCharacter;
            foreach (var block in possibleBlocks)
            {
                block.TextureRevert();
            }
            pathBlocks = Pathfinding.findPath(current.currentBlock, clicked, current.jump);

            foreach (var block in pathBlocks)
            {
                block.TextureChange();
            }
        }
    }

    public override void ExecuteAction()
    {
        if(pathBlocks.Count > 0) //Por ahora moverse se activa tocando espacio
            TurnController.currentCharacter.CharacterMove(pathBlocks);
        alreadyMoved = true;
        StateEnd();
        //Cuando termina la animacion del player, el STATE CONTROLLER (crear) debe terminar este estado (la animación del player se ejecuta desde "CharacterController")
    }
    public override void StateEnd()
    {
        base.StateEnd();
    }
    
}
