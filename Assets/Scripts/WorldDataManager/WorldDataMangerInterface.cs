using UnityEngine;

public interface IWorldDataAddmanager : IWorldDataBaseManager
{
    public void AddTreatToList(Treat treat);
}

public interface IWorldDataBaseManager
{
    public void ConsumeTreat(Treat treat);
    public Treat GetFirstActiveTreatInWorld();
    public bool IsTreatActiveInWorld();
}
