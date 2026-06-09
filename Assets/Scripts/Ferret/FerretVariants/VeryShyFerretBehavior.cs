using UnityEngine;

public class VeryShyFerretBehavior : FerretBaseBehavior
{
    public override HisState Histate { get; protected set; } = new();
    public override IdleState IdleState { get; protected set; } = new VeryShyIdleState();
    public override SleepState SleepState { get; protected set; } = new();
    public override SitState SitState { get; protected set; } = new VeryShySitState();
    public override WanderState WanderState { get; protected set; } = new VeryShyWalkAwayState();

    public override void OnBehaviorEnabled(FerretActiveStats stats, AnimationController animController, IWorldDataBaseManager worldDataManager)
    {
        base.OnBehaviorEnabled(stats, animController, worldDataManager);
        SwitchState(SitState);
    }

}
