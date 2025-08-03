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
        Block.onBlockClicked += ExecuteAction;
    }
    public void OnStateCancel()
    {
        TurnController.currentCharacter.abilityList[currentAbilityIndex].Cancel();
        currentAbilityIndex = -1;
    }
    public void ExecuteAction(Block clicked)
    {
        if (currentAbilityIndex < 0) 
        {
            return;
        }
        if (clicked != null)
        {
            // rotacion para orientar el sprite del personaje
            Vector3 newForward = clicked.transform.position - TurnController.currentCharacter.transform.position;
            newForward.y = 0;
            Transform lookingAt = TurnController.currentCharacter.lookingAt; // transform de objeto LookingAt
            lookingAt.rotation = Quaternion.LookRotation(newForward, lookingAt.up);
            // -
        }
        TurnController.currentCharacter.abilityList[currentAbilityIndex].ExecuteAbility(clicked);
    }
    public void ShowClicked(Block clicked)
    {
        TurnController.currentCharacter.abilityList[currentAbilityIndex].ShowClicked(clicked);
    }
    public void OnStateExit()
    {

    }
  
}
