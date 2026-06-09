using UnityEngine;

public class Ferret :  Animal
{
    //specific states
    public FerretWanderState FerretWander { get; private set; } = new();
    public FerretChaseTreatState FerretChaseTreat { get; private set; } = new();

    //state overrides
    public override WanderState Wander { get => FerretWander; }
    public override ChaseTreatState ChaseTreat { get => FerretChaseTreat; }
}
 