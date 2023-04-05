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

    public void LoadScreen(ScreenType screenType)
    {
        for(int i = 0; i < _uiScreens.Count; i++)
        {
            if (_uiScreens[i].screenType == screenType)
            {
                _uiScreens[i].screen.ShowScreen();
            }
            else
            {
                _uiScreens[i].screen.HideScreen();
            }
        }
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
