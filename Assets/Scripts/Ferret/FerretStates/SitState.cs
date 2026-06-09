using UnityEngine;

public class SitState : StateBaseClass
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        controller.AnimController.PlayAnimation(controller.AnimController.Sitting);
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {
    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
    }
}
