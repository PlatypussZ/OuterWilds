using System.Collections;
using UnityEngine;

public class HisState : StateBaseClass
{
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        controller.AnimController.PlayAnimation(controller.AnimController.Hissing);
        controller.StartCoroutine(Histimer(controller));
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {
        controller.Stats.ChangeTrust(-1);
    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
    }

    private IEnumerator Histimer(FerretBaseBehavior controller)
    {
        yield return new WaitForSeconds(2);
        controller.SwitchState(controller.LastState);
    }
}
