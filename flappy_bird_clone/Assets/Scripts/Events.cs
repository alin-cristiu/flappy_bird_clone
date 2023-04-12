using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events Instance;

    public Action OnUpdateScore = null;
    public Action<int> OnResumeCountDown = null;
    public Action<float> OnNewPipeSpawned = null;
    public Action<Pipe> OnPipeIsOutOfScreen = null;
    public Action<float> OnNewBushSpawned = null;
    public Action<Bush> OnBushIsOutOfScreen = null;
    public Action OnResetScenery = null;

    public Action OnPlay = null;
    public Action OnPause = null;
    public Action OnResume = null;
    public Action OnGoToMainMenu = null;
    public Action OnGameOver = null;

    private void Awake()
    {
        Instance = this;
    }
}
