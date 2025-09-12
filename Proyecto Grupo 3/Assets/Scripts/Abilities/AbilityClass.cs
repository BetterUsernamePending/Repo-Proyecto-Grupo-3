using System;
using UnityEngine;
public class  AbilityClass : MonoBehaviour
{
    
    public string _name;
    public int _cost;
    public string _description;
    public int _range;
    public Animator currentAnimator;
    public void SetAnimator()
    {
        currentAnimator = TurnController.currentCharacter.animator;
    }
    public virtual void ShowRange()
    {
        UIManager.instance.ongoingAbilityConfirmPanel.SetActive(true);
    }

    public virtual void ShowClickedTarget(Block clicked) { }
    public virtual void ExecuteAbility()
    {
        UIManager.instance.WhenExecutingAbility();
        TurnController.instance.alreadyAttacked = true;
    }
    public virtual void Cancel() {  }
}
