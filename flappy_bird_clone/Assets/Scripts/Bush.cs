using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private void Start()
    {
        Events.OnNewBushSpawned += OnNewBushSpawned;
        Events.OnResetScenery += OnResetScenery;
    }

    private void OnNewBushSpawned(float cameraPos)
    {
        if (transform.position.x < cameraPos)
        {
            Events.OnBushIsOutOfScreen?.Invoke(this);
        }
    }

    private void OnResetScenery()
    {
        Events.OnBushIsOutOfScreen?.Invoke(this);
    }
}
