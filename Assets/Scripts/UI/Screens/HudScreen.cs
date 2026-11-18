using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudScreen : UIScreen
{
    public TMP_Text levelText;
    public TMP_Text noticeText;
    public TMP_Text timerText;
    public UiButton pauseButton;
    public UiButton completeButton;
    public UiButton failButton;

    protected override void OnShowStart()
    {
        int lvl = GameData.SelectedLevel + 1;
        if (levelText != null) levelText.text = "LEVEL " + lvl;
        if (noticeText != null) noticeText.text = "Game will be played here";
        if (timerText != null) timerText.text = "0:00";
    }

    void Update()
    {
        GameSession s = GameSession.Instance;
        if (!IsVisible || s == null || timerText == null) return;
        int seconds = Mathf.FloorToInt(s.Elapsed);
        timerText.text = (seconds / 60) + ":" + (seconds % 60).ToString("00");
    }
}
