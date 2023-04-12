using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScreen : UIScreen
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _highscore;

    protected override void InitUI()
    {
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(Play);

        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            _exitButton.gameObject.SetActive(false);
        }
        else
        {
            _exitButton.gameObject.SetActive(true);
            _exitButton.onClick.RemoveAllListeners();
            _exitButton.onClick.AddListener(Exit);
        }

        Global.LoadHighscore();
        _highscore.text = Global.highestScore == 0 ? "" : Constants.HIGHEST_SCORE + "\n" + Global.highestScore;
    }

    private void Play()
    {
        Events.Instance.OnPlay?.Invoke();
    }

    private void Exit()
    {
        Application.Quit();
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
