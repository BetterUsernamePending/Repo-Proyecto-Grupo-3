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
        _description = "Run in a straight line and damage the enemy for half their HP";
        _cost = 0;
        _range = 5;
    }
    public override void ShowClickedTarget(Block clicked)
    {
        List<Block> everyblock = Pathfinding.showPossible(clicked, 70, 70, 1, false);
        foreach (var block in everyblock)
        {
            block.TextureRevert();
        }
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
        foreach (Block currentBlock in possibleTargets)
            if (currentBlock.transform.position.x == current.transform.position.x || currentBlock.transform.position.z == current.transform.position.z)
                targetsInLine.Add(currentBlock);
        foreach (var block in targetsInLine)
        {
            block.TextureChange();
        }
    }
    public override void ExecuteAbility()
    {
        base.ExecuteAbility();
        if (targetBlock != null && targetBlock.characterOnBlock != null && possibleTargets.Exists(x => x == targetBlock))
        {
            List<Block> path = new List<Block>();
            path = Pathfinding.findPath(TurnController.currentCharacter.currentBlock, targetBlock, TurnController.currentCharacter.jump, TurnController.currentCharacter.belongsToPlayer, true,true);
            if (path.Count > 1)
            {
                path.RemoveAt(path.Count - 1);
            }
            if (path.Count != 1)
            {
                TurnController.currentCharacter.CharacterMove(path);
            }
                //cambio de dirección de mirada del personaje
            Vector3 newForward = targetBlock.coord - TurnController.currentCharacter.currentBlock.coord;
            newForward.y = 0;
            Transform lookingAt = TurnController.currentCharacter.lookingAt;
            lookingAt.rotation = Quaternion.LookRotation(newForward, lookingAt.up);
            //
            int damage = (targetBlock.characterOnBlock.currentStats["hp"] / 2);
            targetBlock.characterOnBlock.currentStats["hp"] -= (targetBlock.characterOnBlock.currentStats["hp"] / 2);
            Debug.Log("se hicieron" + damage + "puntos de daño");
            currentAnimator.SetTrigger("Ability1");
            targetBlock.TextureRevert();
        }
        else Debug.Log("Operationfailed");
        if (targetBlock.characterOnBlock = null)
        {
            Debug.Log("Requirements not met");
            targetBlock.TextureRevert();
            return;
        }
    }
}
