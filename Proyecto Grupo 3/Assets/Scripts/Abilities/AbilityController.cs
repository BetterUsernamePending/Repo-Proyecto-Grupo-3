using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AbilityController : MonoBehaviour
{
    private int currentAbilityIndex = -1;
 
    private void Start()
    {
        UIManager.abilityIndexPressed += OnStateEnter;
    }
    public void OnStateEnter(int i)
    {
        currentAbilityIndex = i;
        TurnController.currentCharacter.abilityList[i].ShowRange();
        Block.onBlockClicked += ShowClicked;
    }
    public void OnStateCancel()
    {
        TurnController.currentCharacter.abilityList[currentAbilityIndex].Cancel();
        currentAbilityIndex = -1;

        Block.onBlockClicked -= ShowClicked;
    }
    public void ExecuteAction()
    {
        if (currentAbilityIndex < 0)
        {
            return;
        }
        TurnController.currentCharacter.abilityList[currentAbilityIndex].ExecuteAbility();
        Block.onBlockClicked -= ShowClicked;
        TurnController.currentCharacter.animator.SetTrigger("Ability"+(currentAbilityIndex+1).ToString());
    }
    public void ShowClicked(Block clicked)
    {
        TurnController.currentCharacter.abilityList[currentAbilityIndex].ShowClickedTarget(clicked);
    }
    public void OnStateExit()
    {

    }
  
}
