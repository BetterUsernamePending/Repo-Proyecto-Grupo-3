using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

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
        int damage;
 
        //llamar animación de ataque acá

        if (targetBlock.characterOnBlock != null)
        {
            targetBlock.TextureRevert();
            damage = TurnController.currentCharacter.atk - targetBlock.characterOnBlock.def / 2;
            targetBlock.characterOnBlock.hp = targetBlock.characterOnBlock.hp - damage;
            Debug.Log("se hizo" + damage + "de daño");
        }
    }
}
