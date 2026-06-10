using System;
using UnityEngine;


[Serializable]
public struct StartingStats
{
    [Range(0, 100)] public int StartingTrust;
    public float StartWalkSpeed;
    public float StartRunSpeed;
    public float StartHunger;
    public float StartEnergy;
}

[Serializable]
public class FerretActiveStats
{
    public event Action OnTrustChanged;
    public void InitializeStats(StartingStats startStats)
    {
        WalkSpeed = startStats.StartWalkSpeed;
        RunSpeed = startStats.StartRunSpeed;
        Hunger = startStats.StartHunger;
        Energy = startStats.StartEnergy;
        CurrentTrust = startStats.StartingTrust;
    }

    [field: SerializeField] public int CurrentTrust { get; private set; }
    [field: SerializeField] public float WalkSpeed { get; private set; }
    [field: SerializeField] public float RunSpeed { get; private set; }
    [field: SerializeField] public float Hunger { get; private set; }
    [field: SerializeField] public float Energy { get; private set; }

    [field: SerializeField] public float MouseDistance { get; private set; }

    [field: SerializeField] public bool HasTreat { get; private set; }

    public void ChangeTrust(int amount)
    {
        CurrentTrust += amount;
        OnTrustChanged?.Invoke();
    }

    public void ChangeEnenergy(int amount)
    {
        Energy += amount;
    }

    public void ChangeHunger(int amount)
    {
        Hunger += amount;
    }

    public void SetMouseDistance (float distance)
    {
        MouseDistance = distance;
    }

    public void SetHasTreat(bool hasTreat)
    {
        HasTreat = hasTreat;
    }

 
}
