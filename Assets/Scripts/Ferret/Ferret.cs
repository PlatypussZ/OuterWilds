using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TrustBehaviors
{
    public int StartValue;
    public FerretBaseBehavior Behavior;
    public AnimationSet animationSet;
}

public class Ferret : MonoBehaviour
{
    [field: Header("Ferret Settings")]
    [field: SerializeField] private AnimationController ferretAnimationController;
    [field: SerializeField] private MouseDistanceCalculator mouseDistanceCalculator;

    [SerializeField] private StartingStats startStats;

    [field: SerializeField] private List<TrustBehaviors> behaviors = new List<TrustBehaviors>();

    [field: SerializeField] private WorldDataManager worldDataManager;


    [field: Space]

    [field: Header("Debug stats")]

    [field: SerializeField] private FerretActiveStats stats;
    public FerretBaseBehavior CurrentFerretBehaviorSet { get; private set; } = null;

    private void Start()
    {
        InitializeFerret();
        EvaluateTrust();
    }

    private void InitializeFerret()
    {
        stats.InitializeStats(startStats);
        stats.OnTrustChanged += OnTrustChanged;
    }

    private void OnDisable()
    {
        stats.OnTrustChanged -= OnTrustChanged;
    }
    private void Update()
    {
        stats.SetMouseDistance(mouseDistanceCalculator.GetMouseDistance(gameObject));
        UpdateFeretBehavior();
    }
    public void UpdateFeretBehavior()
    {
        if (CurrentFerretBehaviorSet != null)
            CurrentFerretBehaviorSet.UpdateFerretBehavior();
    }

    public void OnTrustChanged()
    {
        EvaluateTrust();
    }
    public void ChangeTrust(int amount)
    {
        stats.ChangeTrust(amount);
        EvaluateTrust();
    }

    public void EvaluateTrust()
    {
        FerretBaseBehavior Ferret = null;
        AnimationSet animationSet = null;

        for (int i = 0; i < behaviors.Count; i++)
        {
            if (behaviors[i].StartValue <= stats.CurrentTrust)
            {
                Ferret = behaviors[i].Behavior;
            }
        }

        if (Ferret != null && Ferret != CurrentFerretBehaviorSet)
        {
            ChangeFerretBehaviorSet(Ferret, animationSet);
        }
    }

    private void ChangeFerretBehaviorSet(FerretBaseBehavior newBehaviorSet, AnimationSet animSet)
    {
        if (CurrentFerretBehaviorSet != null)
            CurrentFerretBehaviorSet.OnBehaviorDisabled();

        CurrentFerretBehaviorSet = newBehaviorSet;

        if (animSet != null)
            ferretAnimationController.SetAnimationSet(animSet);

        if (CurrentFerretBehaviorSet != null)
            CurrentFerretBehaviorSet.OnBehaviorEnabled(stats, ferretAnimationController, worldDataManager);
    }

}
