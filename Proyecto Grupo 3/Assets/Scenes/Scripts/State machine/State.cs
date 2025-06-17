using UnityEngine;

public abstract class State : MonoBehaviour

{
    public virtual void OnStateEnter() { Block.onBlockClicked += ShowPossibleTargets; }
    public virtual void StateEnd() { Block.onBlockClicked -= ShowPossibleTargets; }
    public virtual void ExecuteAttack() { }
    public virtual void ShowPossibleTargets(Block clicked) { }


}
