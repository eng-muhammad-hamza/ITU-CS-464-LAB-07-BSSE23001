using UnityEngine;

public enum EaseType
{
    Linear, InQuad, OutQuad, InOutQuad,
    InCubic, OutCubic, InOutCubic,
    InOutSine, OutExpo,
    InBack, OutBack, OutElastic, OutBounce
}

public static class Ease
{
    const float C1 = 1.70158f;
    const float C3 = C1 + 1f;

    public static float Evaluate(EaseType type, float t)
    {
        t = Mathf.Clamp01(t);
        switch (type)
        {
            case EaseType.InQuad: return t * t;
            case EaseType.OutQuad: return 1f - (1f - t) * (1f - t);
            case EaseType.InOutQuad: return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) * 0.5f;
            case EaseType.InCubic: return t * t * t;
            case EaseType.OutCubic: return 1f - Mathf.Pow(1f - t, 3f);
            case EaseType.InOutCubic: return t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) * 0.5f;
            case EaseType.InOutSine: return -(Mathf.Cos(Mathf.PI * t) - 1f) * 0.5f;
            case EaseType.OutExpo: return t >= 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
            case EaseType.InBack: return C3 * t * t * t - C1 * t * t;
            case EaseType.OutBack: return 1f + C3 * Mathf.Pow(t - 1f, 3f) + C1 * Mathf.Pow(t - 1f, 2f);
            case EaseType.OutElastic:
                if (t <= 0f) return 0f;
                if (t >= 1f) return 1f;
                return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * (2f * Mathf.PI / 3f)) + 1f;
            case EaseType.OutBounce: return Bounce(t);
            default: return t;
        }
    }

    static float Bounce(float t)
    {
        const float n1 = 7.5625f, d1 = 2.75f;
        if (t < 1f / d1) return n1 * t * t;
        if (t < 2f / d1) { t -= 1.5f / d1; return n1 * t * t + 0.75f; }
        if (t < 2.5f / d1) { t -= 2.25f / d1; return n1 * t * t + 0.9375f; }
        t -= 2.625f / d1; return n1 * t * t + 0.984375f;
    }
}
