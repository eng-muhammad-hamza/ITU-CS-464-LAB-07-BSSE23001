using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiToggle : MonoBehaviour, IPointerClickHandler
{
    public enum Setting { Haptics, ScreenShake }

    public Setting setting;
    public RectTransform knob;
    public Image track;
    public TMP_Text stateLabel;
    public float offX = -42f;
    public float onX = 42f;

    bool value;

    public void Refresh()
    {
        value = setting == Setting.Haptics ? GameData.Haptics : GameData.ScreenShake;
        Apply(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        value = !value;
        if (setting == Setting.Haptics) GameData.Haptics = value; else GameData.ScreenShake = value;
        PlayerPrefs.Save();
        AudioHub.Play(SfxId.Click, value ? 1.15f : 0.85f);
        if (value) GameData.Vibrate();
        Apply(true);
    }

    void Apply(bool animate)
    {
        Vector2 to = new Vector2(value ? onX : offX, knob.anchoredPosition.y);
        Color trackColor = value ? UiTheme.Action : UiTheme.Locked;
        if (stateLabel != null) stateLabel.text = value ? "ON" : "OFF";

        if (!animate || !gameObject.activeInHierarchy)
        {
            knob.anchoredPosition = to;
            track.color = trackColor;
            return;
        }
        StopAllCoroutines();
        StartCoroutine(UiAnim.Move(knob, knob.anchoredPosition, to, 0.22f, EaseType.InOutCubic));
        StartCoroutine(UiAnim.ColorTo(track, trackColor, 0.22f, EaseType.InOutSine));
        StartCoroutine(UiAnim.Punch(knob, 0.18f, 0.4f));
    }
}
