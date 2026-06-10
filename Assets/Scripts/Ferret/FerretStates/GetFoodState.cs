using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GetFoodState : StateBaseClass
{
    protected Treat target;
    FerretBaseBehavior currentController;
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        currentController = controller;
        currentController.AnimController.OnEatingFinished += DoneEating;
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {
        if (currentController.AnimController.IsEating)
        {
            currentController.AnimController.InteruptEating();
        }


        currentController.AnimController.OnEatingFinished -= DoneEating;
        currentController = null;
    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        if (controller.AnimController.IsEating)
        {
            return;
        }

        if (controller.Stats.HasTreat)
        {
            controller.AnimController.PlayAnimForSeconds(5f, controller.AnimController.Eating);
            return;
        }

        if (!controller.WorldData.IsTreatActiveInWorld())
        {
            controller.SwitchState(controller.WanderState);
        }

        if (controller.Stats.Hunger < 50)
        {
            controller.SwitchState(controller.WanderState);
            return;
        }

        target = controller.WorldData.GetFirstActiveTreatInWorld();


        if (target == null)
            return;

        if (controller.GoToTarget(target.transform.position, controller.Stats.RunSpeed))
        {
            if (controller.transform.position.y >= target.transform.position.y - 1f && controller.transform.position.y <= target.transform.position.y + 3f)
            {
                controller.WorldData.ConsumeTreat(target);
                controller.Stats.SetHasTreat(true);
                controller.AnimController.PlayAnimForSeconds(5f, controller.AnimController.Eating);
                target = null;
            }
        }
    }

    public void DoneEating()
    {
        currentController.Stats.ChangeHunger(-30);
        currentController.Stats.ChangeTrust(3);
        currentController.Stats.SetHasTreat(false);
    }
}
