using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    protected void Start()
    {
        InitUI();
    }

    public void ResetUI()
    {
        InitUI();
    }

    protected abstract void InitUI();
    public abstract void ShowScreen();
    public abstract void HideScreen();
}
