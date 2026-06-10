using UnityEngine;

public class ShyGetFoodState : GetFoodState
{
    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.Stats.MouseDistance < 4)
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
