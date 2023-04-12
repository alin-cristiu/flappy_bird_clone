using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private void Start()
    {
        Events.Instance.OnNewBushSpawned = OnNewBushSpawned;
        Events.Instance.OnResetScenery = OnResetScenery;
    }

    private void OnNewBushSpawned(float cameraPos)
    {
        if (transform.position.x < cameraPos)
        {
            Events.Instance.OnBushIsOutOfScreen?.Invoke(this);
        }
    }

    private void OnResetScenery()
    {
        Events.Instance.OnBushIsOutOfScreen?.Invoke(this);
    }
}
