using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;

public class RigContoller : MonoBehaviour
{
    [SerializeField] private List<Transform> bones;
    [SerializeField] private SkinnedMeshRenderer rend;
    private List<Vector3> positions;
    private Vector3 pos;

    void Start()
    {
        int numBones = rend.bones.Length;
        for (int i = 0; i < numBones; i++)
        {
            Transform bone = rend.bones[i];


        }
    }
}
