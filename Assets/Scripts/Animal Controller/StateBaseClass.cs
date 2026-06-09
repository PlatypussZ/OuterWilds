using UnityEngine;

public abstract class StateBaseClass 
{
    public abstract void OnStateEnter(Animal controller);
    public abstract void OnStateExit(Animal controller);
    public abstract void OnStateUpdate(Animal controller);
}
