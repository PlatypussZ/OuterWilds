using UnityEngine;

public class WanderState : StateBaseClass
{
    private Vector3 target;
    protected float waitingTime = 3;

    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        target = Vector3.zero;
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {

    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {

        if (controller.AnimController.IsWaiting) return;

        if (target == Vector3.zero)
            target = ChooseNewTarget(controller.GetScreenWidth());

        if (controller.GoToTarget(target, controller.Stats.WalkSpeed))
        {
            controller.AnimController.PlayAnimForSeconds(waitingTime, controller.AnimController.Idle);
            target = Vector3.zero;
        }
    }

    private Vector3 ChooseNewTarget(Vector2 ScreenWidth)
    {
        Vector3 target = new Vector3(Random.Range(ScreenWidth.x, ScreenWidth.y), 0);
        return target;
    }
}
