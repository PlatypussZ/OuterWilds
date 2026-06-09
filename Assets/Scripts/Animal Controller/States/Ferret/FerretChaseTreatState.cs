using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FerretChaseTreatState : ChaseTreatState
{
    private Treat target;

    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        target = null;
    }
    public override void OnStateExit(FerretBaseBehavior controller)
    {

    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
       /* if (controller.IsEating)
        {
            return;
        }

        if ((!controller.spawner.isTreatActive && !controller.IsEating) || controller.stats.Hunger < 50)
        {
            controller.SwitchState(controller.Wander);
            return;
        }

        target = controller.spawner.GetFirstTreatInlist();

        if (controller.GoToTarget(target.transform.position, controller.stats.RunSpeed))
        {
            if (controller.transform.position.y >= target.transform.position.y - 1f && controller.transform.position.y <= target.transform.position.y + 3f)
            {
                controller.PlayAnimForSeconds(3f, controller.Eating);
                controller.spawner.ConsumeTreat(target);
                controller.decreaseHunger();
            }
        }*/
    }
}
