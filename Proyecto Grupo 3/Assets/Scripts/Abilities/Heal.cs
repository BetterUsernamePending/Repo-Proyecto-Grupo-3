using System.Collections.Generic;
using UnityEngine;

public class Heal : AbilityClass
{
    private List<Block> possibleTargets = new List<Block>();
    private Block targetBlock;
    public bool alreadyAttacked = false;
    private GameController gameController;

    private void Start ()
    {
        _name = "Heal";
        _description = "Heal character for a third of their HP";
        _cost = TurnController.currentCharacter.origStats["mp"]/4;
        _range = 3;
    }
    public override void ExecuteAbility(Block clicked)
    {
        SetAnimator();
        if (clicked != null && clicked.characterOnBlock != null && possibleTargets.Exists(x=> x == clicked) && TurnController.currentCharacter.currentStats["mp"] > TurnController.currentCharacter.origStats["mp"]/4)
        {
            int healing = (clicked.characterOnBlock.currentStats["hp"] / 3);
            clicked.characterOnBlock.currentStats["hp"] += (clicked.characterOnBlock.currentStats["hp"] / 3);
            Debug.Log("se curaron" + healing + "de hp");
            currentAnimator.SetTrigger("Attack");
        }
    }
    public override void ShowRange()
    {
        gameController = FindAnyObjectByType<GameController>();
        CharacterController current = TurnController.currentCharacter;
        possibleTargets = Pathfinding.showPossible(current.currentBlock,_range, current.currentStats["attackHeight"]);
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
        }
    }
}
