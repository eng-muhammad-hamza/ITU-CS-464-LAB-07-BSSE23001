using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiSlider : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public enum Setting { Music, Sfx }

    public Setting setting;
    public RectTransform track;
    public Image fill;
    public RectTransform handle;
    public TMP_Text valueLabel;

    float value;
    Coroutine handleRoutine;

    public void Refresh()
    {
        value = setting == Setting.Music ? GameData.MusicVolume : GameData.SfxVolume;
        Apply();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Drag(eventData);
        Grow(1.25f, 0.2f, EaseType.OutBack);
    }

    public void OnDrag(PointerEventData eventData) => Drag(eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        Grow(1f, 0.2f, EaseType.OutBack);
        PlayerPrefs.Save();
        if (setting == Setting.Sfx) AudioHub.Play(SfxId.Pickup);
    }

    void Drag(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(track, eventData.position, eventData.pressEventCamera, out Vector2 local)) return;
        value = Mathf.Clamp01((local.x - track.rect.xMin) / track.rect.width);
        if (setting == Setting.Music) { GameData.MusicVolume = value; AudioHub.RefreshVolumes(); }
        else GameData.SfxVolume = value;
        Apply();
    }

    void Apply()
    {
        fill.fillAmount = value;
        handle.anchoredPosition = new Vector2(value * track.rect.width, 0f);
        if (valueLabel != null) valueLabel.text = Mathf.RoundToInt(value * 100f) + "%";
    }

    void Grow(float scale, float duration, EaseType ease)
    {
        if (handleRoutine != null) StopCoroutine(handleRoutine);
        handleRoutine = StartCoroutine(UiAnim.Scale(handle, handle.localScale, Vector3.one * scale, duration, ease));
    }
}
