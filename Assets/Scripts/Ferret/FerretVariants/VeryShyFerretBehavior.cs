using UnityEngine;

public class VeryShyFerretBehavior : FerretBaseBehavior
{
    public override HisState Histate { get; protected set; } = new();
    public override IdleState IdleState { get; protected set; } = new VeryShyIdleState();
    public override SleepState SleepState { get; protected set; } = new VeryShySleepState();
    public override SitState SitState { get; protected set; } = new VeryShySitState();
    public override WanderState WanderState { get; protected set; } = new VeryShyWalkAwayState();
    public override GetFoodState GetFoodState { get; protected set; } = new VeryShyGetFoodState();

    public override void OnBehaviorCheck()
    {
        if (CurrentState == Histate)
            return;

        if (Stats.HasTreat && Stats.MouseDistance > 10)
        {
            SwitchState(GetFoodState);
            return;
        }

        if (Stats.Energy < 20 && Stats.MouseDistance > 10)
        {
            SwitchState(SleepState);
            return;
        }

        if(Stats.Hunger >= 50 && Stats.MouseDistance > 10 && WorldData.IsTreatActiveInWorld() && CurrentState != SleepState)
        {
            SwitchState(GetFoodState); 
            return;
        }

    }


}
