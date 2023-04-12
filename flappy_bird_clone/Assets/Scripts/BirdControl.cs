using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _flapForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Animator _birdAnimator;

    private float _currentSpeed;
    private bool _fly = false;
    private Rigidbody _rigidBody;
    private Vector3 _savedVelocity;

    private Vector3 _birdRotation;
    private float _minRotationZ = -90;
    private float _maxRotationZ = 60;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void InitBird()
    {
        _fly = true;
        _rigidBody.useGravity = true;
        _rigidBody.isKinematic = false;
    }

    public void ResetBird(float x, float y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
        _birdRotation = Vector3.zero;
        transform.rotation = Quaternion.Euler(_birdRotation);
    }

    public void ManageBirdActive(bool fly)
    {
        _fly = fly;
        if (!_fly)
        {
            _savedVelocity = _rigidBody.velocity;
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.useGravity = false;
            _rigidBody.isKinematic = true;
        }
        else
        {
            _rigidBody.velocity = _savedVelocity;
            _rigidBody.useGravity = true;
            _rigidBody.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        if (_fly)
        {
            Fly();
        }
    }

    private void Update()
    {
        if (_fly)
        {
            UserControl();
            AnimateBird();
        }
    }

    private void Fly()
    {
        if(_currentSpeed < _maxSpeed)
        {
            Accelerate();
        }
        
        _rigidBody.velocity = new Vector3(_currentSpeed, _rigidBody.velocity.y, _rigidBody.velocity.z);
    }

    private void Accelerate()
    {
        _currentSpeed += _acceleration + Time.deltaTime;
    }

    private void UserControl()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            MobileControl();
        }
        else
        {
            PcControl();
        }
    }

    private void MobileControl()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Flap();
            }
        }
    }

    private void PcControl()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }
    }

    private void Flap()
    {
        _birdAnimator.SetTrigger("Flapped");

        _rigidBody.AddForce(new Vector3(0, _flapForce, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Events.Instance.OnGameOver?.Invoke();
    }

    private void AnimateBird()
    {
        _birdRotation.x = 0;
        _birdRotation.y = 0;
        _birdRotation.z += _rigidBody.velocity.y * _rotationSpeed * Time.deltaTime;
        _birdRotation.z = Mathf.Clamp(_birdRotation.z, _minRotationZ, _maxRotationZ);

        transform.rotation = Quaternion.Euler(_birdRotation);
    }
}
