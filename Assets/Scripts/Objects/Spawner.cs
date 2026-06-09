using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject treatPrefab;
    private List<Treat> treatList = new List<Treat>();
    public bool isTreatActive = false;
    public void SpawnTreat()
    {
        if (!isTreatActive)
        {
            isTreatActive = true;
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Transform pos = transform;
            pos.position = MousePos;
            GameObject treat = Instantiate(treatPrefab, pos);
            Treat treatScript = treat.GetComponent<Treat>();
            treatList.Add(treatScript);
        }
    }

    public void ConsumeTreat(Treat treat)
    {
        if (treatList.Count == 0)
            return;

        if (treatList.Contains(treat))
        {
            treatList.Remove(treat);
            Destroy(treat.gameObject);
            isTreatActive = false;
        }
    }

    public Treat GetFirstTreatInlist()
    {
        if (treatList.Count == 0)
            return null;

        return treatList[0];
    }
}
