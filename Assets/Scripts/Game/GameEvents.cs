using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    private static GameEvents _instance;
    public static GameEvents instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEvents();
            }
            return _instance;
        }
    }

    private GameEvents()
    {

    }

    public event Action OnBlackHolePlaced;
    public void BlackHolePlaced()
    {
        OnBlackHolePlaced?.Invoke();
    }

    public event Action OnReachGoal;
    public void ReachGoal()
    {
        OnReachGoal?.Invoke();
    }

}