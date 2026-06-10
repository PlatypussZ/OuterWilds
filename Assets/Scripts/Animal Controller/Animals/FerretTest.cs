 using UnityEngine;

public class FerretTest :  FerretBaseBehavior
{
    //specific states
    public FerretWanderState FerretWander { get; private set; } = new();
    public FerretChaseTreatState FerretChaseTreat { get; private set; } = new();
    public override HisState Histate { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    public override IdleState IdleState { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    public override SleepState SleepState { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    public override SitState SitState { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    public override WanderState WanderState { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    public override GetFoodState GetFoodState { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
}
 