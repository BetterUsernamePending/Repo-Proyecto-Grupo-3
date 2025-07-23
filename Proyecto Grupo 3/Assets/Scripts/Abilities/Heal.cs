using Unity.VisualScripting;
using UnityEngine;

public class Heal : AbilityClass
{
    private void Start ()
    {
        _name = "Heal";
        _description = "Heal character for " + GetComponent<CharacterController>().currentStats["atk"] / 2 + " HP";
        _cost = TurnController.currentCharacter.origStats["mp"]/4;
    }

    public override void ExecuteAbility()
    {
        
    }

    public override void ShowRange()
    {
        
    }
}
