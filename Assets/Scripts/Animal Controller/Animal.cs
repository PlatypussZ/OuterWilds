using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class Animal : MonoBehaviour
{

    [field: Header("stats")]
    [field: SerializeField] public float WalkSpeed { get; private set; }
    [field: SerializeField] public float RunSpeed { get; private set; }
    [field: SerializeField] public float Trust { get; private set; }
    [field: SerializeField] public float Hunger { get; private set; }
    [field: SerializeField] public float Energy { get; private set; }
    [field: SerializeField] public float Attention { get; private set; }


    [field: Header("Components")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Spawner spawner { get; private set; }

    public abstract WanderState Wander { get; }
    public abstract ChaseTreatState ChaseTreat { get; }

    [field: Header("Animations")]
    public string Idle;
    public string Walking;
    public string Running;
    public string Eating;


    //Internal
    [HideInInspector] public StateBaseClass CurrentState { get; private set; }
    [HideInInspector] public bool IsEating;
    [HideInInspector] public bool IsWaiting;
    [HideInInspector] private Vector3 currentTarget;
    [HideInInspector] private int ticksArrived;


    private void Start()
    {
        SwitchState(Wander);
    }

    private void Update()
    {
     /*   if (CurrentState != null)
            CurrentState.OnStateUpdate(this);*/

        //if (spawner.isTreatActive)
        //    SwitchState(ChaseTreat);
    }

    public void SwitchState(StateBaseClass newState)
    {
    /*    if (CurrentState != null)
            CurrentState.OnStateExit(this);

        newState.OnStateEnter(this);
*/
        CurrentState = newState;
    }

    public bool GoToTarget(Vector3 target, float speed)
    {

        if (target != currentTarget)
        {
            currentTarget = target;
        }

        //distance check
        bool isArrived = transform.position.x >= currentTarget.x - 1f && transform.position.x <= currentTarget.x + 0.5f;
        if (isArrived)
        {
            ticksArrived++;
        }
        else
        {
            ticksArrived = 0;
        }

        if (ticksArrived >= 3)
        {
            PlayAnimation(Idle);
        }
        else
        {
            Move(target, speed);
        }

        return isArrived;

    }
    public Vector2 GetScreenWidth()
    {
        Camera cam = Camera.main;

        Vector3 leftEdge = cam.ScreenToWorldPoint(
            new Vector3(0, Screen.height / 2f, cam.nearClipPlane));

        Vector3 rightEdge = cam.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height / 2f, cam.nearClipPlane));

        float leftX = leftEdge.x;
        float rightX = rightEdge.x;

        return new Vector2(leftX, rightX);
    }
    public void Move(Vector3 target, float speed)
    {
        Vector3 pos = transform.position;

        if (target.x > pos.x)
        {
            pos.x += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }
        else if (target.x < pos.x)
        {
            pos.x -= speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (speed >= RunSpeed)
        {
            PlayAnimation(Running);
        }
        else
        {
            PlayAnimation(Walking);
        }

        transform.position = pos;
    }

    public void PlayAnimation(string animation)
    {
        Animator.Play(animation);
    }

    public void PlayAnimForSeconds(float time, string animation)
    {
        StartCoroutine(PlayAnim(time, animation));
    }
    
    private IEnumerator PlayAnim(float time, string animation)
    {
        Animator.Play(animation);
        if (animation == Idle)
        {
            IsWaiting = true;
            yield return new WaitForSeconds(time);
            IsWaiting = false;
        }
        else if (animation == Eating)
        {
            IsEating = true;
            yield return new WaitForSeconds(time);
            IsEating = false;
        }
    }
    public void decreaseHunger()
    {
        Hunger--;
    }
}