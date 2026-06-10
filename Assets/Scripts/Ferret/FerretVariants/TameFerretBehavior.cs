using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TameFerretBehavior : FerretBaseBehavior
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
        if (CurrentState == Histate)
            return;

        if (Stats.HasTreat)
        {
            SwitchState(GetFoodState);
            return;
        }

        if (Stats.Energy < 20)
        {
            SwitchState(SleepState);
            return;
        }

        if (Stats.Hunger >= 10 && WorldData.IsTreatActiveInWorld() && CurrentState != SleepState)
        {
            SwitchState(GetFoodState);
            return;
        }

    }
}
