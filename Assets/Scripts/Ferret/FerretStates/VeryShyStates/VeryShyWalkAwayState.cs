using UnityEngine;
using UnityEngine.InputSystem;

public class VeryShyWalkAwayState : WanderState
{
    private Vector3 target;

    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        target = Vector3.zero;
        Debug.Log("Walk away state entered");
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {

    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {

        if (controller.Stats.MouseDistance <= 5f)
        {
            controller.SwitchState(controller.Histate);
            return;
        }

        if (controller.Stats.MouseDistance > 13)
        {
            controller.SwitchState(controller.SitState);
            return;
        }

        if (target == Vector3.zero)
            target = ChooseNewTarget(controller.GetScreenWidth(), controller);

        if (controller.Stats.MouseDistance <= 8)
        {
            if (controller.GoToTarget(target, controller.Stats.RunSpeed))
            {
                target = Vector3.zero;
            }
        }
        else
        {
            if (controller.GoToTarget(target, controller.Stats.WalkSpeed))
            {
                target = Vector3.zero;
            }
        }
    }

    private Vector3 ChooseNewTarget(Vector2 screenEdges, FerretBaseBehavior controller)
    {
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        float targetX;

        if (mouseWorld.x < (screenEdges.x + screenEdges.y) * 0.5f)
        {
            targetX = Random.Range(
                (screenEdges.x + screenEdges.y) * 0.5f,
                screenEdges.y);
        }
        else
        {
            targetX = Random.Range(
                screenEdges.x,
                (screenEdges.x + screenEdges.y) * 0.5f);
        }

        return new Vector3(
            targetX,
            controller.transform.position.y,
            controller.transform.position.z);
    }
}
