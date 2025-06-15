using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

public class BattleController : State
{

    private List<Block> possibleTargets = new List<Block>();
    private Block targetBlock;
    public bool alreadyAttacked = false;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        alreadyAttacked = false;
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
 
        //llamar animaci�n de ataque ac�

        if (targetBlock.characterOnBlock != null)
        {
            targetBlock.TextureRevert();
            damage = TurnController.currentCharacter.atk - targetBlock.characterOnBlock.def / 2;
            targetBlock.characterOnBlock.hp = targetBlock.characterOnBlock.hp - damage;
            Debug.Log("se hizo" + damage + "de da�o");
        }
        alreadyAttacked = true;
    }
    public void OnStateCancel()
    {
        foreach (var block in possibleTargets)
        {
            block.TextureRevert();
        }
    }
}
