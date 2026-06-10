using UnityEngine;

public class ShyWanderState : WanderState
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        waitingTime = 7;
        base.OnStateEnter(controller);
    }
    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.Stats.MouseDistance < 5)
        {
            controller.SwitchState(controller.Histate);
            return;
        }

        base.OnStateUpdate(controller);
    }
}
