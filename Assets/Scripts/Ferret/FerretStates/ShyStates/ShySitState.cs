using UnityEngine;

public class ShySitState : SitState
{
    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.Stats.MouseDistance <= 3f)
        {
            controller.SwitchState(controller.Histate);
            return;
        }

        if (controller.Stats.MouseDistance <= 6f)
        {
            controller.SwitchState(controller.WanderState);
            return;
        }
    }
}
