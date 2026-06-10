using UnityEngine;

public class ShyIdleState : IdleState
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        controller.SwitchState(controller.SitState);
    }
}
