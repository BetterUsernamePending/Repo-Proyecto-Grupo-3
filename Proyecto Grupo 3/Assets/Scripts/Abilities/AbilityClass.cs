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
    public virtual void ShowRange(){}
    public virtual void ExecuteAbility(Block clicked){}
    public virtual void Cancel() { }
    public virtual void ShowClicked(Block clicked) { }
}
