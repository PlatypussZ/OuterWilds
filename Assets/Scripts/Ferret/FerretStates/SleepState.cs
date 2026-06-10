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

        if (TrustTimer >= 3f)
        {
            TrustTimer -= 3f;
            UpdateTrust(controller);
        }

        if (controller.Stats.Energy > 100)
        {
            controller.SwitchState(controller.WanderState);
        }
    }
    private void UpdateEnergy(FerretBaseBehavior controller)
    {
        controller.Stats.ChangeEnenergy(3);
    }

    private void UpdateTrust(FerretBaseBehavior controller)
    {
        controller.Stats.ChangeTrust(1);
    }
}



