using UnityEngine;

public class SleepState : StateBaseClass
{
    private float TrustTimer;
    private float EnergyUpTimer;
    public override void OnStateEnter(FerretBaseBehavior controller)
    {
        controller.AnimController.PlayAnimation(controller.AnimController.Sleeping);
    }

    public override void OnStateExit(FerretBaseBehavior controller)
    {
        Debug.Log("exit sleeping state");
    }

    public override void OnStateUpdate(FerretBaseBehavior controller)
    {
        EnergyUpTimer += Time.deltaTime;
        TrustTimer += Time.deltaTime;

        if (EnergyUpTimer >= 1f)
        {
            EnergyUpTimer -= 1f;
            UpdateEnergy(controller);
        }

        if (TrustTimer >= 10f)
        {
            TrustTimer -= 10f;
            UpdateTrust(controller);
        }
    }
    private void UpdateEnergy(FerretBaseBehavior controller)
    {
        controller.Stats.ChangeEnenergy(1);
    }

    private void UpdateTrust(FerretBaseBehavior controller)
    {
        controller.Stats.ChangeTrust(1);
    }
}



