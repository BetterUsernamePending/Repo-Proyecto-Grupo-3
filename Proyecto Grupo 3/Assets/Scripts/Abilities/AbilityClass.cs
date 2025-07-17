using System;
using UnityEngine;
public class  AbilityClass : MonoBehaviour
{
    
    public string _name;
    public int _cost;
    public string _description;
    public virtual void ShowRange(){}
    public virtual void ExecuteAbility(){}
}
