using UnityEngine;

public class ShySleepState : SleepState
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        base.OnStateEnter(controller);

        if (controller.Stats.MouseDistance <= 3)
        {
            controller.SwitchState(controller.WanderState);
            return;
        }
    }
    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.Stats.Energy > 100)
        {
            controller.SwitchState(controller.WanderState);
        }

        if (controller.Stats.MouseDistance <= 3)
        {
            controller.SwitchState(controller.WanderState);
            return;
        }

        base.OnStateUpdate(controller);

    }
}