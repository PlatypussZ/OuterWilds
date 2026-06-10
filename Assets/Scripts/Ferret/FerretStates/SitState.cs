using UnityEngine;

public class SitState : StateBaseClass
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        controller.SwitchState(controller.WanderState);
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {
    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
    }
}
