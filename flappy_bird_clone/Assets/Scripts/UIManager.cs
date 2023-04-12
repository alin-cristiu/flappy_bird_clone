using System;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class UIManager : MonoBehaviour
{
    public enum ScreenType
    {
        MainMenu,
        HUD,
        PauseMenu,
        EndScreen
    }

    [SerializeField] private List<UIScreenStructure> _uiScreens;
    private Dictionary<ScreenType, UIScreen> _uiScreensDictionary = new Dictionary<ScreenType, UIScreen>();
    private UIScreen _lastActiveScreen;

    private void Awake()
    {
        SetupScreensDictionary();
    }

    private void SetupScreensDictionary()
    {
        for (int i = 0; i < _uiScreens.Count; i++)
        {
            if (!_uiScreensDictionary.ContainsKey(_uiScreens[i].screenType))
            {
                _uiScreensDictionary.Add(_uiScreens[i].screenType, _uiScreens[i].screen);
            }

            _uiScreens[i].screen.HideScreen();
        }
    }

    public void LoadScreen(ScreenType screenType)
    {
        if (!_uiScreensDictionary.ContainsKey(screenType))
        {
            SetupScreensDictionary();
        }

        _uiScreensDictionary[screenType].ShowScreen();
        if (_lastActiveScreen != null)
        {
            _lastActiveScreen.HideScreen();
        }
        _lastActiveScreen = _uiScreensDictionary[screenType];
    }

    public void ResetUI()
    {
        for (int i = 0; i < _uiScreens.Count; i++)
        {
            _uiScreens[i].screen.ResetUI();
        }
    }

    [Serializable]
    public struct UIScreenStructure
    {
        public ScreenType screenType;
        public UIScreen screen;
    }
}
