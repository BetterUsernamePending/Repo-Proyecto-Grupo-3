using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class Fireball : AbilityClass
{
    private List<Block> possibleTargets = new List<Block>();
    private Block targetBlock;
    public bool alreadyAttacked = false;
    private void Start()
    {
        _name = "Fireball";
        _description = "Daña a las unidades alrededor del bloque objetivo";
        _cost = 50;
        _range = 5;
    }
    public override void ShowClickedTarget(Block clicked)
    {
        List<Block> everyblock = Pathfinding.showPossible(clicked, 70, 70, 1, false);
        foreach (var block in everyblock)
        {
            block.TextureRevert();
        }
        int targetPlayer;
        if (TurnController.currentCharacter.belongsToPlayer == 1)
            targetPlayer = 2;
        else targetPlayer = 1;
        if (possibleTargets.Exists(Block => Block == clicked))
        {
            CharacterController current = TurnController.currentCharacter;
            currentAnimator = current.animator; // animaciones
            foreach (var block in possibleTargets)
            {
                block.TextureRevert();
            }
            targetBlock = clicked;
            List<Block> surrounding = Pathfinding.showPossible(targetBlock, 1, 2, targetPlayer, false);
            foreach (Block block in surrounding)
            block.TextureChange();
        }
    }
    public override void ShowRange()
    {
        base.ShowRange();
        CharacterController current = TurnController.currentCharacter;
        possibleTargets = Pathfinding.showPossible(current.currentBlock, _range, 2, current.belongsToPlayer, false);
        foreach (var block in possibleTargets)
        {
            block.TextureChange();
        }
    }
    public override void ExecuteAbility()
    {
        base.ExecuteAbility();
        if (targetBlock != null && possibleTargets.Exists(x => x == targetBlock) && TurnController.currentCharacter.currentStats["mp"] > 49)
        {
            // Cambiar la direcci�n hacia la que mira el personaje
            Vector3 newForward = targetBlock.coord - TurnController.currentCharacter.currentBlock.coord;
            newForward.y = 0;
            Transform lookingAt = TurnController.currentCharacter.lookingAt;
            lookingAt.rotation = Quaternion.LookRotation(newForward, lookingAt.up);
            // -
            int targetPlayer;
            if (TurnController.currentCharacter.belongsToPlayer == 1)
                targetPlayer = 2;
            else targetPlayer = 1;
            List<Block> surrounding = Pathfinding.showPossible(targetBlock, 1, 1,targetPlayer, false);
            surrounding.Add(targetBlock);
            foreach(Block block in surrounding)
            {
                if(block.containsCharacter == true)
                {
                    int damage = (block.characterOnBlock.origStats["hp"] / 3);
                    block.characterOnBlock.hp -= damage;
                    Debug.Log("al personaje " + block.characterOnBlock.name + " se le hicieron " + damage + "puntos de daño");
                }
            }
            currentAnimator.SetTrigger("Ability1");
            foreach (Block block in surrounding)
                block.TextureRevert();
        }
    }
}