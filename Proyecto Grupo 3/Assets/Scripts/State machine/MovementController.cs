using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovementController : MonoBehaviour
{
    private List<Block> possibleBlocks = new List<Block>();
    private List<Block> pathBlocks = new List<Block>();
    [SerializeField] private GameObject canvas;
    public void OnStateEnter() //Volver "OnStateEnter", placeholder.
    {
        CharacterController current = TurnController.currentCharacter;
        possibleBlocks = Pathfinding.showPossible(current.currentBlock, current.currentStats["dist"], current.currentStats["jump"], current.belongsToPlayer,true);
        Block.onBlockClicked += ShowPathFound;
        Block.onBlockClicked += ActivateMovementPan;
        foreach (var block in possibleBlocks)
            block.TextureChange();
    }
    public void OnStateCancel()
    {
        Block.onBlockClicked -= ShowPathFound;
        foreach (var block in pathBlocks)
        {
            block.TextureRevert();
        }
        foreach (var block in possibleBlocks)
        {
            block.TextureRevert();
        }
    }
    public void ActivateMovementPan(Block clicked)
    {
        if(possibleBlocks.Contains(clicked))
        UIManager.instance.ActivateMovementPanel();
    }
    public void ShowPathFound(Block clicked)
    {

        if (possibleBlocks.Exists(Block => Block == clicked) && !clicked.containsCharacter)
        {
            CharacterController current = TurnController.currentCharacter;
            foreach (var block in possibleBlocks)
            {
                block.TextureRevert();
            }
            pathBlocks = Pathfinding.findPath(current.currentBlock, clicked, current.currentStats["jump"],current.belongsToPlayer,true);

            foreach (var block in pathBlocks)
            {
                block.TextureChange();
            };
        }
        UIManager.instance.ongoingMovementPanel.SetActive(true);
    }
    
    public void MoveToClicked()
    {
        if (TurnController.instance.alreadyMoved == false)
        {
            if (pathBlocks.Count > 0) //Por ahora moverse se activa tocando espacio
                TurnController.currentCharacter.CharacterMove(pathBlocks);
                UIManager.instance.Move.interactable = false;
                UIManager.instance.ongoingMovementPanel.SetActive(false);
 
            foreach (var block in pathBlocks)
                {
                    block.TextureRevert();
                }
            TurnController.instance.alreadyMoved = true;
            UIManager.instance.ReactivateCertainButtons();
            StateEnd();
        }
    }
    public void StateEnd()
    {
        Block.onBlockClicked -= ShowPathFound;
    }
}
