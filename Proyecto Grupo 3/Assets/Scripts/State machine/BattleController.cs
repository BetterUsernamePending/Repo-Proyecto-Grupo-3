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
    private GameController gameController;
    private Animator currentAnimator; // animaciones

    public void OnStateEnter()
    {
        gameController = FindAnyObjectByType<GameController>();
        alreadyAttacked = false;
        CharacterController current = TurnController.currentCharacter;
        possibleTargets = Pathfinding.showPossible(current.currentBlock, current.currentStats["range"], current.currentStats["attackHeight"]);
        Block.onBlockClicked += ShowClickedTarget;
        foreach (var block in possibleTargets)
        {
            block.TextureChange();
        }
    }
    public void ShowClickedTarget(Block clicked)
    {
        if (possibleTargets.Exists(Block => Block == clicked))
        {
            CharacterController current = TurnController.currentCharacter;
            currentAnimator = current.animator; // animaciones
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
        //llamar animaci�n de ataque ac�
        if (targetBlock == null)
            return;
        if (targetBlock.characterOnBlock != null)
        {
            currentAnimator.SetTrigger("Attack"); // animaciones
            targetBlock.TextureRevert();
            int damage = TurnController.currentCharacter.currentStats["atk"] - targetBlock.characterOnBlock.currentStats["def"] / 2;
            targetBlock.characterOnBlock.currentStats["hp"] = targetBlock.characterOnBlock.currentStats["hp"] - damage;
            Debug.Log("se hizo" + damage + "de daño");
            alreadyAttacked = true;
            if (targetBlock.characterOnBlock.currentStats["hp"] <= 0)
            {   
                //ejecutar ac� la animaci�n de muerte
                targetBlock.characterOnBlock.IsDead();
                Debug.Log("La unidad enemiga " + targetBlock.characterOnBlock.name + " fue eliminada");
                gameController.CheckIfGameOver();
            }
        }
    }
    public void OnStateCancel()
    {
        Block.onBlockClicked -= ShowClickedTarget;
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
