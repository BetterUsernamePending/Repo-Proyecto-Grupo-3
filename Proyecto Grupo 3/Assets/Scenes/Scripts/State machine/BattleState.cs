using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleState : State
{

    private List<Block> possibleTargets = new List<Block>();
    private Block targetBlock;
    
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        CharacterController current = TurnController.currentCharacter;
        possibleTargets = Pathfinding.showPossible(current.currentBlock, current.range, current.jump);
        
    }
    public override void ShowPossibleTargets(Block clicked)
    {
        if (possibleTargets.Exists(Block => Block == clicked))
        {
            CharacterController current = TurnController.currentCharacter;
            foreach (var block in possibleTargets)
            {
                block.TextureRevert();
            }

            targetBlock = clicked;
            targetBlock.TextureChange();
           
        }
    }
    public override void ExecuteAction()
    {     
        foreach (var block in possibleTargets)
        {
            block.TextureRevert();
        }

    }

}
