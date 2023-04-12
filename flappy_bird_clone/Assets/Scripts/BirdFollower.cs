using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFollower : MonoBehaviour
{
    [SerializeField] private Transform _bird;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _scenery;

    private Vector3 _initCameraPosition;
    private float _birdCameraOffset;

    public void SetupBirdFollower()
    {
        _initCameraPosition = _cameraTransform.transform.position;
        _birdCameraOffset = _bird.transform.position.x - _initCameraPosition.x;
    }

    public void ResetCamera()
    {
        _cameraTransform.position = _initCameraPosition;
    }

    private void Update()
    {
        CameraFollowBird();
    }

    private void CameraFollowBird()
    {
        _cameraTransform.position = new Vector3(_bird.position.x + _birdCameraOffset, _cameraTransform.position.y, _cameraTransform.position.z);
        _scenery.position = new Vector3(_cameraTransform.position.x, _scenery.position.y, _scenery.position.z);
    }
}
