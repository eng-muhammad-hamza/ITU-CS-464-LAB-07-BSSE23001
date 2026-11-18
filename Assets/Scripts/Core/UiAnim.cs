using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UiAnim
{
    static float Step(float t, float duration) => t + Time.unscaledDeltaTime / Mathf.Max(0.0001f, duration);

    public static IEnumerator Move(RectTransform rt, Vector2 from, Vector2 to, float duration, EaseType ease)
    {
        float t = 0f;
        while (t < 1f)
        {
            if (rt == null) yield break;
            t = Step(t, duration);
            rt.anchoredPosition = Vector2.LerpUnclamped(from, to, Ease.Evaluate(ease, t));
            yield return null;
        }
        if (rt != null) rt.anchoredPosition = to;
    }

    public static IEnumerator Fade(CanvasGroup group, float from, float to, float duration, EaseType ease)
    {
        float t = 0f;
        while (t < 1f)
        {
            if (group == null) yield break;
            t = Step(t, duration);
            group.alpha = Mathf.LerpUnclamped(from, to, Ease.Evaluate(ease, t));
            yield return null;
        }
        if (group != null) group.alpha = to;
    }

    public static IEnumerator FadeGraphic(Graphic graphic, float to, float duration, EaseType ease)
    {
        if (graphic == null) yield break;
        float from = graphic.color.a;
        float t = 0f;
        while (t < 1f)
        {
            if (graphic == null) yield break;
            t = Step(t, duration);
            Color c = graphic.color;
            c.a = Mathf.LerpUnclamped(from, to, Ease.Evaluate(ease, t));
            graphic.color = c;
            yield return null;
        }
        if (graphic != null) { Color c = graphic.color; c.a = to; graphic.color = c; }
    }

    public static IEnumerator ColorTo(Graphic graphic, Color to, float duration, EaseType ease)
    {
        if (graphic == null) yield break;
        Color from = graphic.color;
        float t = 0f;
        while (t < 1f)
        {
            if (graphic == null) yield break;
            t = Step(t, duration);
            graphic.color = Color.LerpUnclamped(from, to, Ease.Evaluate(ease, t));
            yield return null;
        }
        if (graphic != null) graphic.color = to;
    }

    public static IEnumerator Scale(Transform tr, Vector3 from, Vector3 to, float duration, EaseType ease)
    {
        float t = 0f;
        while (t < 1f)
        {
            if (tr == null) yield break;
            t = Step(t, duration);
            tr.localScale = Vector3.LerpUnclamped(from, to, Ease.Evaluate(ease, t));
            yield return null;
        }
        if (tr != null) tr.localScale = to;
    }

    public static IEnumerator Punch(Transform tr, float amount, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            if (tr == null) yield break;
            t = Step(t, duration);
            tr.localScale = Vector3.one * (1f + amount * (1f - Ease.Evaluate(EaseType.OutQuad, t)));
            yield return null;
        }
        if (tr != null) tr.localScale = Vector3.one;
    }

    public static IEnumerator Shake(RectTransform rt, float magnitude, float duration)
    {
        if (rt == null) yield break;
        Vector2 home = rt.anchoredPosition;
        float t = 0f;
        while (t < 1f)
        {
            if (rt == null) yield break;
            t = Step(t, duration);
            float damper = 1f - t;
            rt.anchoredPosition = home + Vector2.right * (Mathf.Sin(t * Mathf.PI * 9f) * magnitude * damper);
            yield return null;
        }
        if (rt != null) rt.anchoredPosition = home;
    }

    public static IEnumerator Count(TMP_Text text, int from, int to, float duration, EaseType ease, string prefix = "", string suffix = "")
    {
        float t = 0f;
        while (t < 1f)
        {
            if (text == null) yield break;
            t = Step(t, duration);
            int value = Mathf.RoundToInt(Mathf.LerpUnclamped(from, to, Ease.Evaluate(ease, t)));
            text.text = prefix + value + suffix;
            yield return null;
        }
        if (text != null) text.text = prefix + to + suffix;
    }

    public static IEnumerator Fill(Image image, float from, float to, float duration, EaseType ease)
    {
        float t = 0f;
        while (t < 1f)
        {
            if (image == null) yield break;
            t = Step(t, duration);
            image.fillAmount = Mathf.LerpUnclamped(from, to, Ease.Evaluate(ease, t));
            yield return null;
        }
        if (image != null) image.fillAmount = to;
    }

    public static IEnumerator Parallel(MonoBehaviour host, params IEnumerator[] routines)
    {
        Coroutine[] running = new Coroutine[routines.Length];
        for (int i = 0; i < routines.Length; i++) running[i] = host.StartCoroutine(routines[i]);
        for (int i = 0; i < running.Length; i++) yield return running[i];
    }
}
