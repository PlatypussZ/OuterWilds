using UnityEngine;

public abstract class StateBaseClass 
{
    public abstract void OnStateEnter(FerretBaseBehavior controller);
    public abstract void OnStateExit(FerretBaseBehavior controller);
    public abstract void OnStateUpdate(FerretBaseBehavior controller);
}
