using System.Collections.Generic;
using UnityEngine;

public class Heal : AbilityClass
{
    private List<Block> possibleTargets = new List<Block>();
    private Block targetBlock;
    public bool alreadyAttacked = false;
    private GameController gameController;

    private void Start()
    {
        _name = "Heal";
        _description = "Heal character for a third of their HP";
        _cost = 50;
        _range = 3;
    }

    public override void ShowClickedTarget(Block clicked)
    {
        List<Block> everyblock = Pathfinding.showPossible(clicked, 70, 70, 1, false);
        foreach (var block in everyblock)
        {
            block.TextureRevert();
        }
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

    public override void ExecuteAbility()
    {
        base.ExecuteAbility();
        SetAnimator();
        if (targetBlock != null && targetBlock.characterOnBlock != null && possibleTargets.Exists(x => x == targetBlock) && TurnController.currentCharacter.currentStats["mp"] > TurnController.currentCharacter.origStats["mp"] / 4)
        {
            // Cambiar la direcci�n hacia la que mira el personaje
            Vector3 newForward = targetBlock.coord - TurnController.currentCharacter.currentBlock.coord;
            newForward.y = 0;
            Transform lookingAt = TurnController.currentCharacter.lookingAt;
            lookingAt.rotation = Quaternion.LookRotation(newForward, lookingAt.up);
            // -

            // Curar
            int healing = (targetBlock.characterOnBlock.currentStats["hp"] / 3);
            targetBlock.characterOnBlock.currentStats["hp"] += (targetBlock.characterOnBlock.currentStats["hp"] / 3);
            Debug.Log("se curaron" + healing + "de hp");
            currentAnimator.SetTrigger("Ability1");
            targetBlock.TextureRevert();
            // -
        }
        if (targetBlock.characterOnBlock = null)
        {
            Debug.Log("Requirements not met");
            targetBlock.TextureRevert();
            return;
        }
    }
    public override void ShowRange()
    {
        base.ShowRange();
        gameController = FindAnyObjectByType<GameController>();
        CharacterController current = TurnController.currentCharacter;
        possibleTargets = Pathfinding.showPossible(current.currentBlock,_range, current.currentStats["attackHeight"],current.belongsToPlayer,false);
        foreach (var block in possibleTargets)
        {
            block.TextureChange();
        }
    }
    public override void Cancel() 
    {
        foreach (var block in possibleTargets)
        {
            block.TextureRevert();
            UIManager.instance.ongoingAbilityConfirmPanel.SetActive(false);
        }
    }
}
