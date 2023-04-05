using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    private const float INIT_FIRST_PIPE_POSITION = 0;

    [SerializeField] private SceneryGenerator _sceneryGenerator;

    private float _firstPipePosition = INIT_FIRST_PIPE_POSITION;

    public void ResetFirstPositon()
    {
        _firstPipePosition = INIT_FIRST_PIPE_POSITION;
    }

    public void GeneratePipes(float pipeOffset)
    {
        GameObject currentPipe = _sceneryGenerator.GetPipeOfType(GetRandomTypeOfPipe());
        currentPipe.transform.position = new Vector3(_firstPipePosition, currentPipe.transform.position.y, currentPipe.transform.position.z);

        _firstPipePosition += pipeOffset;
    }

    public float GetFirstPipePosition()
    {
        return _firstPipePosition;
    }

    private int GetRandomTypeOfPipe()
    {
        return Random.Range(0, _sceneryGenerator.GetMaximumNumberOfPipeTypes());
    }
}
