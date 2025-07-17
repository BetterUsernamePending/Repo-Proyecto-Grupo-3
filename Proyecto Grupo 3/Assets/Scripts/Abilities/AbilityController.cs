using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private int currentAbilityIndex = -1;
    private void Start()
    {
        UIManager.abilityIndexPressed += OnStateEnter;
    }
    public void OnStateEnter(int i)
    {
        TurnController.currentCharacter.abilityList[i].ShowRange();
        currentAbilityIndex = i;
        Block.onBlockClicked += ShowClicked;
    }
    public void OnStateCancel()
    {
        currentAbilityIndex = -1;
    }
    public void ExecuteAction()
    {
        if (currentAbilityIndex < 0) 
        {
            return;
        }
        TurnController.currentCharacter.abilityList[currentAbilityIndex].ExecuteAbility();
    }
    public void ShowClicked(Block block)
    {

    }
    public void OnStateExit()
    {

    }
  
}
