using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CautiousFerretBehavior : FerretBaseBehavior
{
    public override HisState Histate { get; protected set; } = new();
    public override IdleState IdleState { get; protected set; } = new();
    public override SleepState SleepState { get; protected set; } = new();
    public override SitState SitState { get; protected set; } = new();
    public override GetFoodState GetFoodState { get; protected set; } = new();
    public override WanderState WanderState { get; protected set; } = new();


    public override void OnBehaviorEnabled(FerretActiveStats stats, AnimationController animController, IWorldDataBaseManager worldDataManager)
    {
        base.OnBehaviorEnabled(stats, animController, worldDataManager);
        SwitchState(SitState);
    }

    public override void OnBehaviorCheck()
    {
        if (Stats.MouseDistance > 1)
        {
            SwitchState(Histate);
            return;
        }

        if (CurrentState == Histate)
            return;

        if (Stats.HasTreat && Stats.MouseDistance > 2)
        {
            SwitchState(GetFoodState);
            return;
        }

        if (Stats.Energy < 20 && Stats.MouseDistance > 2)
        {
            SwitchState(SleepState);
            return;
        }

        if (Stats.Hunger >= 30 && Stats.MouseDistance > 2 && WorldData.IsTreatActiveInWorld() && CurrentState != SleepState)
        {
            SwitchState(GetFoodState);
            return;
        }

    }
}
