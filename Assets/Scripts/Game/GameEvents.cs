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

    public event Action<int> OnBlackHolePlaced;
    public void BlackHolePlaced(int num)
    {
        OnBlackHolePlaced?.Invoke(num);
    }

    public event Action<int> OnBlackHoleRemoved;
    public void BlackHoleRemoved(int num)
    {
        OnBlackHoleRemoved?.Invoke(num);
    }

    public event Action OnReachGoal;
    public void ReachGoal()
    {
        OnReachGoal?.Invoke();
    }

}