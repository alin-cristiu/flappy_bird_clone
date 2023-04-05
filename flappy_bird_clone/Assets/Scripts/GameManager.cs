using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int RESUME_COUNTDOWN = 3;

    public static GameManager Instance;

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SceneryGenerator _sceneryGenerator;
    [SerializeField] private PipeManager _pipeManager;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _gameFieldOffset;
    [SerializeField] private float _maxPipeOffset;
    [SerializeField] private float _minPipeOffset;
    [SerializeField] private int _pipeOffsetReductionStep;
    [SerializeField] private float _bushOffset;

    [SerializeField] private Transform _scenery;
    [SerializeField] private BirdControl _bird;
    [SerializeField] private float _initBirdHeight;

    private int _currCamPos;
    private Vector3 _initCameraPosition;
    private float _birdCameraOffset;
    private float _currPipeOffset;

    private float _resumeTime;
    private bool _generateScenery;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _initCameraPosition = _cameraTransform.transform.position;
        _birdCameraOffset = _bird.transform.position.x - _initCameraPosition.x;
        _sceneryGenerator.InitPools();
        _currPipeOffset = _maxPipeOffset;
        _uiManager.LoadScreen(UIManager.ScreenType.MainMenu);
        HideBird();
        _bird.ResetBird(_initCameraPosition.x + _birdCameraOffset, _initBirdHeight);
        SubscribeEvents();
        _generateScenery = true;
    }

    private void SubscribeEvents()
    {
        Events.OnPlay = OnPlay;
        Events.OnPause = OnPause;
        Events.OnResume = OnResume;
        Events.OnGoToMainMenu = OnGoToMainMenu;
        Events.OnGameOver = OnGameOver;
    }

    private void OnPlay()
    {
        Global.score = 0;
        _uiManager.LoadScreen(UIManager.ScreenType.HUD);
        InitBird();
        _generateScenery = true;
    }

    private void OnPause()
    {
        _uiManager.LoadScreen(UIManager.ScreenType.PauseMenu);
        _bird.ManageBirdActive(false);
        _generateScenery = false;
    }

    private void OnResume()
    {
        _uiManager.LoadScreen(UIManager.ScreenType.HUD);
        _resumeTime = RESUME_COUNTDOWN;
        _generateScenery = true;
    }

    private void OnGoToMainMenu()
    {
        _uiManager.LoadScreen(UIManager.ScreenType.MainMenu);
        _bird.ManageBirdActive(false);
        _uiManager.ResetUI();
        HideBird();
        _bird.ResetBird(_initCameraPosition.x + _birdCameraOffset, _initBirdHeight);
        _cameraTransform.position = _initCameraPosition;
        ResetScenery();
    }

    private void OnGameOver()
    {
        _uiManager.LoadScreen(UIManager.ScreenType.EndScreen);
        _bird.ManageBirdActive(false);
        HideBird();
        _generateScenery = false;
    }

    private void InitBird()
    {
        _bird.gameObject.SetActive(true);
        _bird.InitBird();
    }

    private void HideBird()
    {
        _bird.gameObject.SetActive(false);
    }

    private void ResetScenery()
    {
        Events.OnResetScenery?.Invoke();
        _currPipeOffset = _maxPipeOffset;
        _sceneryGenerator.ResetFirstPositon();
        _pipeManager.ResetFirstPositon();
        _generateScenery = true;
    }

    private void Update()
    {
        if (_generateScenery)
        {
            GeneratePipes();
            GenerateBushes();
        }
        CameraFollowBird();
        UpdateScore();

        if(_resumeTime > 0)
        {
            ResumeCountDown();
        }
    }

    private void GeneratePipes()
    {
        if(_pipeManager.GetFirstPipePosition() < _cameraTransform.position.x + _gameFieldOffset)
        {
            _pipeManager.GeneratePipes(_currPipeOffset);
            Events.OnNewPipeSpawned?.Invoke(_cameraTransform.position.x - _gameFieldOffset);
            if (_currPipeOffset > _minPipeOffset)
            {
                ReducePipeOffset();
            }
        }
    }

    private void ReducePipeOffset()
    {
        _currPipeOffset = Mathf.Max(_minPipeOffset, _maxPipeOffset - Global.score / _pipeOffsetReductionStep);
    }

    private void GenerateBushes()
    {
        if (_sceneryGenerator.GetFirstBushPosition() < _cameraTransform.position.x + _bushOffset)
        {
            _sceneryGenerator.GenerateBush(_bushOffset);
            Events.OnNewBushSpawned?.Invoke(_cameraTransform.position.x - _bushOffset);
        }
    }

    private void CameraFollowBird()
    {
        _cameraTransform.position = new Vector3(_bird.transform.position.x + _birdCameraOffset, _cameraTransform.position.y, _cameraTransform.position.z);
        _scenery.transform.position = new Vector3(_cameraTransform.position.x, _scenery.transform.position.y, _scenery.transform.position.z);
    }

    private void UpdateScore()
    {
        _currCamPos = GetRoundedCamPos();
        if (Global.score < _currCamPos)
        {
            Global.score = _currCamPos;
            Events.OnUpdateScore?.Invoke();
        }
    }

    private int GetRoundedCamPos()
    {
        return Mathf.RoundToInt(_cameraTransform.position.x);
    }

    private void ResumeCountDown()
    {
        _resumeTime -= Time.deltaTime;

        Events.OnResumeCountDown?.Invoke(Mathf.RoundToInt(_resumeTime));

        if(_resumeTime <= 0)
        {
            _resumeTime = -1;
            Events.OnResumeCountDown?.Invoke(Mathf.RoundToInt(_resumeTime));
            Resume();
        }
    }

    private void Resume()
    {
        _bird.ManageBirdActive(true);
    }
}
