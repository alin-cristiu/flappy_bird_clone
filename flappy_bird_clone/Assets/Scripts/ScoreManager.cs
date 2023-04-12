using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    private int _currCamPos;

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        _currCamPos = GetRoundedCamPos();
        if (Global.score < _currCamPos)
        {
            Global.score = _currCamPos;
            Events.Instance.OnUpdateScore?.Invoke();
        }
    }

    private int GetRoundedCamPos()
    {
        return Mathf.RoundToInt(_cameraTransform.position.x);
    }
}
