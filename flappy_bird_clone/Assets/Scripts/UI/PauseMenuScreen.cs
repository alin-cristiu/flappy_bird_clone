using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScreen : UIScreen
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    protected override void InitUI()
    {
        _resumeButton.onClick.RemoveAllListeners();
        _resumeButton.onClick.AddListener(Resume);

        _mainMenuButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void Resume()
    {
        Events.OnResume?.Invoke();
    }

    private void ReturnToMainMenu()
    {
        Global.SaveScore();
        Events.OnGoToMainMenu?.Invoke();
    }

    public override void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public override void HideScreen()
    {
        gameObject.SetActive(false);
    }
}
