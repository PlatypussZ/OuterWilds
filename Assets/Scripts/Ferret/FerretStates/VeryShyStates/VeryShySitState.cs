using UnityEngine;

public class VeryShySitState : SitState
{
    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.Stats.MouseDistance <= 5f)
        {
            controller.SwitchState(controller.Histate);
            return;
        }

        if(controller.Stats.MouseDistance <= 13)
        {
            controller.SwitchState(controller.WanderState);
            return;
        }


    }
}
