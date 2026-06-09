using UnityEngine;

public class FerretWanderState : WanderState
{
    private Vector3 target;

    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        target = Vector3.zero;
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {

    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
      /*  if (controller.spawner.isTreatActive && controller.Hunger > 50)
        {
            controller.SwitchState(controller.ChaseTreat);
            return;
        }

        if (controller.IsWaiting) return;

        if (target == Vector3.zero)
            target = ChooseNewTarget(controller.GetScreenWidth());

        if (controller.GoToTarget(target, controller.WalkSpeed))
        {
            controller.PlayAnimForSeconds(3f, controller.Idle);
            target = Vector3.zero;
        }*/
    }

    private Vector3 ChooseNewTarget(Vector2 ScreenWidth)
    {
        Vector3 target = new Vector3( Random.Range(ScreenWidth.x, ScreenWidth.y), 0);
        return target;
    }
}
