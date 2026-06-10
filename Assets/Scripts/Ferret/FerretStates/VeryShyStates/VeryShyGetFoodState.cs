using UnityEngine;

public class VeryShyGetFoodState : GetFoodState
{
    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.Stats.MouseDistance < 8)
        {
            if (controller.AnimController.IsEating)
            {
                controller.SwitchState(controller.Histate);
                return;
            }
            else
            {
                controller.SwitchState(controller.WanderState);
                return;
            }
        }

        base.OnStateUpdate(controller);
    }
}
