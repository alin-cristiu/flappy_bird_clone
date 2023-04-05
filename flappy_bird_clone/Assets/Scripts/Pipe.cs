using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private int _pipeTypeIndex;
    public int PipeTypeIndex => _pipeTypeIndex;

    private void Start()
    {
        Events.OnNewPipeSpawned += OnNewPipeSpawned;
        Events.OnResetScenery += OnResetScenery;
    }

    private void OnNewPipeSpawned(float cameraPos)
    {
        if (transform.position.x < cameraPos)
        {
            Events.OnPipeIsOutOfScreen?.Invoke(this);
        }
    }

    private void OnResetScenery()
    {
        Events.OnPipeIsOutOfScreen?.Invoke(this);
    }
}
