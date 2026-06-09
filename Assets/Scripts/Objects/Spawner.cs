using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject treatPrefab;
    [SerializeField] private IWorldDataAddmanager worldData;

    public void SpawnTreat()
    {
        if (!worldData.IsTreatActiveInWorld())
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Transform pos = transform;
            pos.position = MousePos;
            GameObject treat = Instantiate(treatPrefab, pos);
            Treat treatScript = treat.GetComponent<Treat>();
            worldData.AddTreatToList(treatScript);
        }
    }
}
