using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class MovementController : MonoBehaviour
{
    private List<Block> possibleBlocks = new List<Block>();
    private List<Block> pathBlocks = new List<Block>();
    private bool alreadyMoved = false;

    [SerializeField] private GameObject canvas;
    public void OnStateEnter() //Volver "OnStateEnter", placeholder.
    {
        alreadyMoved = false;
        CharacterController current = TurnController.currentCharacter;
        possibleBlocks = Pathfinding.showPossible(current.currentBlock, current.dist, current.jump);
        Block.onBlockClicked += ShowPathFound;
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

    public void ShowPathFound(Block clicked)
    {

        if (possibleBlocks.Exists(Block => Block == clicked) && !clicked.containsCharacter)
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
            };
        }
    }

    public void MoveToClicked()
    {
        if (alreadyMoved == false)
        {
            if (pathBlocks.Count > 0) //Por ahora moverse se activa tocando espacio
                TurnController.currentCharacter.CharacterMove(pathBlocks);
            foreach (var block in pathBlocks)
            {
                block.TextureRevert();
            }
            alreadyMoved = true;
            StateEnd();
        }
        //Cuando termina la animacion del player, el STATE CONTROLLER (crear) debe terminar este estado (la animación del player se ejecuta desde "CharacterController")
    }
    public void StateEnd()
    {
        Block.onBlockClicked -= ShowPathFound;
    }
}
