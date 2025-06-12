using UnityEngine;

public abstract class State

{
    public virtual void OnStateEnter() { Block.onBlockClicked += ShowPossibleTargets; }
    public virtual void StateEnd() { Block.onBlockClicked -= ShowPossibleTargets; }
    public virtual void ExecuteAction() { }
    public virtual void ShowPossibleTargets(Block clicked) { }


}
