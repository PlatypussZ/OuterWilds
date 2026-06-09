using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AnimationController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationSet currentAnimationSet;

    public string Idle { get => currentAnimationSet.Idle; }
    public string Walking { get => currentAnimationSet.Walking; }
    public string Running { get => currentAnimationSet.Running; }
    public string Eating { get => currentAnimationSet.Eating; }
    public string Hissing { get => currentAnimationSet.Hissing; }

    public string Sitting { get => currentAnimationSet.Sitting; }

    public bool IsEating { get; private set; }
    public bool IsWaiting { get; private set; }

    private string SwitchingAnimString;
    private bool isSwitching;

    public void SetAnimationSet(AnimationSet set)
    {
        currentAnimationSet = set;
    }
    public void PlayAnimation(string animation)
    {
        if (isSwitching)
        {
            StopCoroutine(PlayAnim(SwitchingAnimString));
        }

        StartCoroutine(PlayAnim(animation));
    }

    public void PlayAnimForSeconds(float time, string animation)
    {
        StartCoroutine(PlayAnim(time, animation));
    }

    private IEnumerator PlayAnim (string animation)
    {
        isSwitching = true;
        SwitchingAnimString = animation;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        animator.Play(SwitchingAnimString);
        isSwitching = false;
    }
    private IEnumerator PlayAnim(float time, string animation)
    {
        animator.Play(animation);
        if (animation == currentAnimationSet.Idle)
        {
            IsWaiting = true;
            yield return new WaitForSeconds(time);
            IsWaiting = false;
        }
        else if (animation == currentAnimationSet.Eating)
        {
            IsEating = true;
            yield return new WaitForSeconds(time);
            IsEating = false;
        }
    }
}
