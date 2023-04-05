using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Action OnUpdateScore = null;
    public static Action<int> OnResumeCountDown = null;
    public static Action<float> OnNewPipeSpawned = null;
    public static Action<Pipe> OnPipeIsOutOfScreen = null;
    public static Action<float> OnNewBushSpawned = null;
    public static Action<Bush> OnBushIsOutOfScreen = null;
    public static Action OnResetScenery = null;

    public static Action OnPlay = null;
    public static Action OnPause = null;
    public static Action OnResume = null;
    public static Action OnGoToMainMenu = null;
    public static Action OnGameOver = null;
}
