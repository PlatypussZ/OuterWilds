using UnityEngine;

public class VeryShySleepState : SleepState
{

    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        base.OnStateEnter(controller);

        if (controller.Stats.MouseDistance <= 8)
        {
            controller.SwitchState(controller.WanderState);
            return;
        }
    }
    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.Stats.MouseDistance < 3)
        {
            controller.SwitchState(controller.Histate);
        }

        if(controller.Stats.Energy > 50)
        {
            controller.SwitchState(controller.WanderState);
        }

        base.OnStateUpdate(controller);

    }
}
