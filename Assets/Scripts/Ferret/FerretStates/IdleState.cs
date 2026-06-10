using UnityEngine;

public class IdleState : StateBaseClass
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        controller.SwitchState(controller.SitState);
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {
    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
    }
}
