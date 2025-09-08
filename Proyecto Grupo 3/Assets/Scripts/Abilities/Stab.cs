using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class Stab : AbilityClass
{
    private List<Block> possibleTargets = new List<Block>();
    private Block targetBlock;
    public bool alreadyAttacked = false;
    private GameController gameController;
    private void Start()
    {
        _name = "Stab";
        _description = "Run in a straight line and damage the enemy for atk*2 damage";
        _cost = 0;
        _range = 5;
    }
    public override void ShowClickedTarget(Block clicked)
    {
        if (possibleTargets.Exists(Block => Block == clicked && (clicked.transform.position.x == TurnController.currentCharacter.transform.position.x)) || (clicked.transform.position.z == TurnController.currentCharacter.transform.position.z))
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
    public override void ShowRange()
    {
        base.ShowRange();
        gameController = FindAnyObjectByType<GameController>();
        CharacterController current = TurnController.currentCharacter;
        possibleTargets = Pathfinding.showPossible(current.currentBlock, _range, current.currentStats["attackHeight"], current.belongsToPlayer, false);
        List<Block> targetsInLine = new List<Block> { };
        foreach(Block currentBlock in  possibleTargets)
            if(currentBlock.transform.position.x == current.transform.position.x || currentBlock.transform.position.z == current.transform.position.z)
                targetsInLine.Add(currentBlock);
        foreach (var block in targetsInLine)
        {
                block.TextureChange();
        }
    }
}
