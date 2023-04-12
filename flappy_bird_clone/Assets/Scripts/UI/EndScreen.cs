using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreen : UIScreen
{
    private const float TIME_TO_WAIT = 1;

    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _highestScoreText;
    [SerializeField] private Button _continueButton;
    [SerializeField] private TMP_Text _continueText;

    private float _timeOfEnable;

    protected override void InitUI()
    {
        _continueText.text = GetPlatformString() + Constants.TO_CONTINUE;

        _continueButton.onClick.RemoveAllListeners();
        _continueButton.onClick.AddListener(TapToContinue);

        _continueButton.gameObject.SetActive(false);
    }

    private string GetPlatformString()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Constants.TAP;
        }
        else
        {
            return Constants.CLICK;
        }
    }

    private void TapToContinue()
    {
        _continueButton.gameObject.SetActive(false);
        Events.Instance.OnGoToMainMenu?.Invoke();
    }

    private void OnEnable()
    {
        Global.SaveScore();
        _currentScoreText.text = Constants.SCORE + " : " + Global.score;
        _highestScoreText.text = Constants.HIGHEST_SCORE + " : " + Global.highestScore;
        InitContinue();
    }

    private void InitContinue()
    {
        _timeOfEnable = Time.time;
    }

    private void Update()
    {
        CheckTimeToShowContinue();
    }

    private void CheckTimeToShowContinue()
    {
        if(Time.time > _timeOfEnable + TIME_TO_WAIT && !_continueButton.gameObject.activeSelf)
        {
            _continueButton.gameObject.SetActive(true);
        }
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
