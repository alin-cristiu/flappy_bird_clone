using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : UIScreen
{
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _highscore;
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private TMP_Text _resumeCountdown;

    protected override void InitUI()
    {
        _pauseBtn.onClick.RemoveAllListeners();
        _pauseBtn.onClick.AddListener(ShowPause);

        _highscore.text = Global.highestScore == 0 ? "" : Constants.HIGHEST_SCORE + "\n" + Global.highestScore;

        _currentScore.text = Constants.SCORE + "\n0";
        Events.OnUpdateScore += OnUpdateSCore;
        Events.OnResumeCountDown += OnResumeCountDown;
    }

    private void ShowPause()
    {
        Events.OnPause?.Invoke();
    }

    public override void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public override void HideScreen()
    {
        gameObject.SetActive(false);
    }

    private void OnUpdateSCore()
    {
        _currentScore.text = Constants.SCORE + "\n" + Global.score.ToString();
    }

    private void OnResumeCountDown(int resumeCountDown)
    {
        if(resumeCountDown > 0 && !_resumeCountdown.gameObject.activeSelf)
        {
            _resumeCountdown.gameObject.SetActive(true);
        }
        else if(resumeCountDown < 0 && _resumeCountdown.gameObject.activeSelf)
        {
            _resumeCountdown.gameObject.SetActive(false);
        }
        _resumeCountdown.text = resumeCountDown.ToString();
    }
}
