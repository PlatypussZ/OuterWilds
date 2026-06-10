using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class FerretBaseBehavior : MonoBehaviour
{
    [field: SerializeField] public string CurrentStateName { get; private set; }
    public AnimationController AnimController { get; private set; }
    public FerretActiveStats Stats { get; private set; }
    public IWorldDataBaseManager WorldData { get; private set; }

    //default states
    public abstract HisState Histate { get; protected set; }
    public abstract IdleState IdleState { get; protected set; }
    public abstract SleepState SleepState { get; protected set; }
    public abstract SitState SitState { get; protected set; }
    public abstract GetFoodState GetFoodState{ get; protected set;}
    public abstract WanderState WanderState { get; protected set; }



    //Internal
    [HideInInspector] public StateBaseClass CurrentState { get; private set; }
    [HideInInspector] public StateBaseClass LastState { get; private set; }
    [HideInInspector] private Vector3 currentTarget;
    [HideInInspector] private int ticksArrived;

    private float energyTimer;
    private float hungerTimer;


    public virtual void OnBehaviorEnabled(FerretActiveStats stats, AnimationController animController, IWorldDataBaseManager worldDataManager)
    {
        Stats = stats;
        AnimController = animController;
        WorldData = worldDataManager;
        SwitchState(SitState);
    }

    public virtual void OnBehaviorDisabled()
    {

    }

    public virtual void OnBehaviorCheck()
    {
    }
    public void UpdateFerretBehavior()
    {
        OnBehaviorCheck();


        if (CurrentState != null)
            CurrentState.OnStateUpdate(this);

        if(CurrentState != SleepState)
        {
            energyTimer += Time.deltaTime;

            if (energyTimer >= 1f)
            {
                energyTimer -= 1f;
                Stats.ChangeEnenergy(-1);
            }
        }

        if (!AnimController.IsEating)
        {
            hungerTimer += Time.deltaTime;

            if (hungerTimer >= 2f)
            {
                hungerTimer -= 2f;
                Stats.ChangeHunger(1);
            }
        }
    }
    public void SwitchState(StateBaseClass newState)
    {
        if (CurrentState == newState)
            return;

        if (CurrentState != null)
            CurrentState.OnStateExit(this);

        LastState = CurrentState;

  
        CurrentState = newState;
        if (CurrentState != null)
            CurrentStateName = CurrentState.ToString();
        else
        {
            CurrentStateName = string.Empty;
        }


        if (newState != null)
            newState.OnStateEnter(this);

      
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
            AnimController.PlayAnimation(AnimController.Idle);
        }
        else
        {
            Move(target, speed);
        }

        return ticksArrived >= 3;

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

        if (!AnimController.IsEating)
        {
            if (speed >= Stats.RunSpeed)
            {
                AnimController.PlayAnimation(AnimController.Running);
            }
            else
            {
                AnimController.PlayAnimation(AnimController.Walking);
            }
        }


        transform.position = pos;
    }



}
