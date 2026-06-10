using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldDataManager : MonoBehaviour, IWorldDataAddmanager
{
    private WorldData worldData = new WorldData();
    public void AddTreatToList(Treat treat)
    {
        if (worldData.treatList.Contains(treat))
            return;

        worldData.treatList.Add(treat);
    }
    public void ConsumeTreat(Treat treat)
    {
        if (worldData.treatList.Contains(treat))
        {
            worldData.treatList.Remove(treat);
            Destroy(treat.gameObject);
        }
    }
    public Treat GetFirstActiveTreatInWorld()
    {
        if(worldData.treatList.Count == 0)
            return null;

        return worldData.treatList[0];
    }

    public bool IsTreatActiveInWorld()
    {
        return worldData.treatList.Count > 0;
    }
}
