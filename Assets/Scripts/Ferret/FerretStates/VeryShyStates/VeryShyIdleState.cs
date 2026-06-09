using UnityEngine;

public class VeryShyIdleState : IdleState
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        controller.SwitchState(controller.SitState);
    }
}
