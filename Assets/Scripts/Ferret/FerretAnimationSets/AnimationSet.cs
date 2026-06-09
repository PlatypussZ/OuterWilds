using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSet", menuName = "Scriptables/AnimationSet")]
public class AnimationSet : ScriptableObject
{
    [field: SerializeField] public string Idle { get; private set; } = "Idle";
    [field: SerializeField] public string Walking { get; private set; } = "Walk";
    [field: SerializeField] public string Running { get; private set; } = "Run";
    [field: SerializeField] public string Eating { get; private set; } = "Eating";
    [field: SerializeField] public string Hissing { get; private set; } = "His";

    [field: SerializeField] public string Sitting { get; private set; }= "Sit";
}
