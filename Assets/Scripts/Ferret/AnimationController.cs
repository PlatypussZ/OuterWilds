using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AnimationController : MonoBehaviour
{
    public event Action OnEatingFinished;

    private Coroutine currentAnimCoroutine;
    private Coroutine PlayAnimCoroutine;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationSet currentAnimationSet;

    public string Idle { get => currentAnimationSet.Idle; }
    public string Walking { get => currentAnimationSet.Walking; }
    public string Running { get => currentAnimationSet.Running; }
    public string Eating { get => currentAnimationSet.Eating; }
    public string Hissing { get => currentAnimationSet.Hissing; }
    public string Sleeping { get => currentAnimationSet.Sleeping; }
    public string Sitting { get => currentAnimationSet.Sitting; }

    public bool IsEating { get; private set; }
    public bool IsWaiting { get; private set; }

    private string SwitchingAnimString;

    public void SetAnimationSet(AnimationSet set)
    {
        currentAnimationSet = set;
    }
    public void PlayAnimation(string animation)
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(animation))
        {
            return;
        }

        if (IsEating)
            return;

        if (currentAnimCoroutine != null)
        {
            if (SwitchingAnimString == animation)
                return;

            StopCoroutine(currentAnimCoroutine);
        }

        currentAnimCoroutine = StartCoroutine(PlayAnim(animation));
    }

    public void PlayAnimForSeconds(float time, string animation)
    {
        PlayAnimCoroutine = StartCoroutine(PlayAnim(time, animation));
    }

    public void InteruptEating()
    {
        StopCoroutine(PlayAnimCoroutine);
        IsEating = false;
    }

    private IEnumerator PlayAnim(string animation)
    {
        SwitchingAnimString = animation;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        animator.Play(SwitchingAnimString);
    }
    private IEnumerator PlayAnim(float time, string animation)
    {
        PlayAnimation(animation);

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
            OnEatingFinished?.Invoke();
            IsEating = false;
        }
    }



}
