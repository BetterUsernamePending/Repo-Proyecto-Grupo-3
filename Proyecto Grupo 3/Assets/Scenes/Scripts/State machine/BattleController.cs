using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

public class BattleController : MonoBehaviour
{

    private List<Block> possibleTargets = new List<Block>();
    private Block targetBlock;
    public bool alreadyAttacked = false;

    public void OnStateEnter()
    {
        alreadyAttacked = false;
        CharacterController current = TurnController.currentCharacter;
        possibleTargets = Pathfinding.showPossible(current.currentBlock, current.range, current.jump);
        Block.onBlockClicked += ShowClickedTarget;

    }
    public void ShowClickedTarget(Block clicked)
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
    public void ExecuteAttack()
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
        alreadyAttacked = true;
        if (targetBlock.characterOnBlock.hp <= 0)
        {
            //ejecutar acá la animación de muerte
            targetBlock.characterOnBlock.IsDead();
            Debug.Log ("La unidad enemiga " +  targetBlock.characterOnBlock.name + " fue eliminada");
        }
    }
    public void OnStateCancel()
    {
        foreach (var block in possibleTargets)
        {
            block.TextureRevert();
        }
    }
    public void OnTurnFinished()
    {
        alreadyAttacked = false;
    }
}
