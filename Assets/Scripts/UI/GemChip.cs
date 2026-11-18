using TMPro;
using UnityEngine;

public class GemChip : MonoBehaviour
{
    public TMP_Text amountText;
    int shown = -1;
    Coroutine countRoutine;

    void OnEnable()
    {
        shown = GameData.Gems;
        amountText.text = shown.ToString();
        amountText.color = UiTheme.Text;
    }

    void Update()
    {
        int now = GameData.Gems;
        if (now == shown) return;
        if (countRoutine != null) StopCoroutine(countRoutine);
        countRoutine = StartCoroutine(UiAnim.Count(amountText, shown, now, 0.5f, EaseType.OutQuad));
        StartCoroutine(UiAnim.Punch(transform, 0.2f, 0.45f));
        shown = now;
    }

    public void FlashDenied()
    {
        StartCoroutine(UiAnim.Shake((RectTransform)transform, 14f, 0.35f));
        StartCoroutine(Flash());
    }

    System.Collections.IEnumerator Flash()
    {
        amountText.color = UiTheme.Danger;
        yield return new WaitForSecondsRealtime(0.15f);
        yield return UiAnim.ColorTo(amountText, UiTheme.Text, 0.5f, EaseType.OutQuad);
    }
}
