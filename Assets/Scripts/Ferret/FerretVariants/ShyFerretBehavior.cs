using UnityEngine;

public class ShyFerretBehavior : FerretBaseBehavior
{
    public override HisState Histate { get; protected set; } = new();
    public override IdleState IdleState { get; protected set; } = new ShyIdleState();
    public override SleepState SleepState { get; protected set; } = new ShySleepState();
    public override SitState SitState { get; protected set; } = new ShySitState();
    public override GetFoodState GetFoodState { get; protected set; } = new ShyGetFoodState();
    public override WanderState WanderState { get; protected set; } = new ShyWanderState();


    public override void OnBehaviorEnabled(FerretActiveStats stats, AnimationController animController, IWorldDataBaseManager worldDataManager)
    {
        base.OnBehaviorEnabled(stats, animController, worldDataManager);
        SwitchState(SitState);
    }

    public override void OnBehaviorCheck()
    {
        if (CurrentState == Histate)
            return;

        if (Stats.HasTreat && Stats.MouseDistance > 5)
        {
            SwitchState(GetFoodState);
            return;
        }

        if (Stats.Energy < 20 && Stats.MouseDistance > 5)
        {
            SwitchState(SleepState);
            return;
        }

        if (Stats.Hunger >= 40 && Stats.MouseDistance > 5 && WorldData.IsTreatActiveInWorld() && CurrentState != SleepState)
        {
            SwitchState(GetFoodState);
            return;
        }

    }
}
