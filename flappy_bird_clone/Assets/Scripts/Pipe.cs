using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private int _pipeTypeIndex;
    public int PipeTypeIndex => _pipeTypeIndex;

    private void Start()
    {
        Events.Instance.OnNewPipeSpawned = OnNewPipeSpawned;
        Events.Instance.OnResetScenery = OnResetScenery;
    }

    private void OnNewPipeSpawned(float cameraPos)
    {
        if (transform.position.x < cameraPos)
        {
            Events.Instance.OnPipeIsOutOfScreen?.Invoke(this);
        }
    }

    private void OnResetScenery()
    {
        Events.Instance.OnPipeIsOutOfScreen?.Invoke(this);
    }
}
