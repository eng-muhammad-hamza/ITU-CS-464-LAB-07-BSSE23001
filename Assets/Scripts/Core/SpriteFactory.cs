using System.Collections.Generic;
using UnityEngine;

public static class SpriteFactory
{
    static readonly Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => cache.Clear();

    public static readonly string[] IconShapes =
    {
        "circle", "ring", "square", "glow", "heart", "star", "gem", "coin", "lock", "play", "pause",
        "back", "gear", "home", "retry", "check", "close", "bolt", "shield", "hex", "drop"
    };

    public static Sprite Get(string shape, int size = 64)
    {
        string key = shape + size;
        if (cache.TryGetValue(key, out Sprite found) && found != null) return found;
        Texture2D tex = BuildTexture(shape, size, 2);
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        sprite.name = "gen_" + shape;
        cache[key] = sprite;
        return sprite;
    }

    public static Texture2D BuildTexture(string shape, int size, int samples)
    {
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Bilinear;
        tex.wrapMode = TextureWrapMode.Clamp;
        Color32[] pixels = new Color32[size * size];
        float inv = 1f / (samples * samples);
        for (int py = 0; py < size; py++)
        {
            for (int px = 0; px < size; px++)
            {
                float sum = 0f;
                for (int sy = 0; sy < samples; sy++)
                    for (int sx = 0; sx < samples; sx++)
                    {
                        float x = ((px + (sx + 0.5f) / samples) / size) * 2f - 1f;
                        float y = ((py + (sy + 0.5f) / samples) / size) * 2f - 1f;
                        sum += Sample(shape, x, y);
                    }
                byte a = (byte)Mathf.RoundToInt(Mathf.Clamp01(sum * inv) * 255f);
                pixels[py * size + px] = new Color32(255, 255, 255, a);
            }
        }
        tex.SetPixels32(pixels);
        tex.Apply(false, false);
        return tex;
    }

    public static Texture2D BuildRoundRect(int size, int radius, int outline)
    {
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Bilinear;
        tex.wrapMode = TextureWrapMode.Clamp;
        Color32[] pixels = new Color32[size * size];
        float half = size * 0.5f;
        for (int py = 0; py < size; py++)
        {
            for (int px = 0; px < size; px++)
            {
                float outer = RoundBoxDistance(px + 0.5f - half, py + 0.5f - half, half, radius);
                float coverage = Mathf.Clamp01(0.5f - outer);
                if (outline > 0)
                {
                    float inner = RoundBoxDistance(px + 0.5f - half, py + 0.5f - half, half - outline, Mathf.Max(0, radius - outline));
                    coverage *= Mathf.Clamp01(inner + 0.5f);
                }
                pixels[py * size + px] = new Color32(255, 255, 255, (byte)Mathf.RoundToInt(coverage * 255f));
            }
        }
        tex.SetPixels32(pixels);
        tex.Apply(false, false);
        return tex;
    }

    static float RoundBoxDistance(float x, float y, float halfExtent, float radius)
    {
        float qx = Mathf.Abs(x) - (halfExtent - radius);
        float qy = Mathf.Abs(y) - (halfExtent - radius);
        float ox = Mathf.Max(qx, 0f), oy = Mathf.Max(qy, 0f);
        return Mathf.Sqrt(ox * ox + oy * oy) + Mathf.Min(Mathf.Max(qx, qy), 0f) - radius;
    }

    static readonly float[] PlayPoly = { -0.55f, 0.8f, -0.55f, -0.8f, 0.85f, 0f };
    static readonly float[] BoltPoly = { 0.15f, 0.95f, -0.55f, -0.05f, -0.05f, -0.05f, -0.2f, -0.95f, 0.55f, 0.1f, 0.05f, 0.1f };
    static readonly float[] ShieldPoly = { -0.8f, 0.85f, 0.8f, 0.85f, 0.8f, -0.2f, 0f, -0.9f, -0.8f, -0.2f };
    static readonly float[] HexPoly = { 0.9f, 0f, 0.45f, 0.78f, -0.45f, 0.78f, -0.9f, 0f, -0.45f, -0.78f, 0.45f, -0.78f };
    static readonly float[] RoofPoly = { -0.97f, 0.05f, 0f, 0.92f, 0.97f, 0.05f };
    static readonly float[] DropPoly = { 0f, 0.95f, 0.6f, -0.1f, -0.6f, -0.1f };
    static readonly float[] ArrowPoly = { 0.12f, 0.33f, 0.30f, 0.83f, 0.56f, 0.26f };
    static readonly float[] StarPoly = { 0.0f, 0.95f, -0.28f, 0.28f, -0.95f, 0.0f, -0.28f, -0.28f, 0.0f, -0.95f, 0.28f, -0.28f, 0.95f, 0.0f, 0.28f, 0.28f };
    static readonly float[] GemPoly = { 0f, 0.95f, -0.85f, 0f, 0f, -0.95f, 0.85f, 0f };

    static float B(bool value) => value ? 1f : 0f;

    static float Sample(string shape, float x, float y)
    {
        float ax = Mathf.Abs(x);
        float ay = Mathf.Abs(y);
        float r = Mathf.Sqrt(x * x + y * y);

        switch (shape)
        {
            case "circle":
                return B(ax <= 0.85f && ay <= 0.85f);

            case "ring":
                return B(ax <= 0.85f && ay <= 0.85f && !(ax <= 0.62f && ay <= 0.62f));

            case "square":
                return B(ax <= 0.88f && ay <= 0.88f);

            case "glow":
                return 0f;

            case "coin":
                return B(ax <= 0.85f && ay <= 0.85f && !(ax <= 0.55f && ay <= 0.55f && (ax >= 0.35f || ay >= 0.35f)));

            case "heart":
                {
                    bool topL = (x >= -0.75f && x <= -0.05f && y >= -0.05f && y <= 0.75f);
                    bool topR = (x >= 0.05f && x <= 0.75f && y >= -0.05f && y <= 0.75f);
                    bool bot = (ay <= 0.75f && y < 0f && (ax + Mathf.Abs(y)) <= 1.0f);
                    return B(topL || topR || bot);
                }

            case "star":
                return B(Poly(StarPoly, x, y));

            case "gem":
                return B(Poly(GemPoly, x, y));

            case "play":
                return B(Poly(PlayPoly, x, y));

            case "pause":
                return B(ay <= 0.75f && ((x >= -0.6f && x <= -0.18f) || (x >= 0.18f && x <= 0.6f)));

            case "bolt":
                return B(Poly(BoltPoly, x, y));

            case "shield":
                return B(Poly(ShieldPoly, x, y));

            case "hex":
                return B(Poly(HexPoly, x, y));

            case "drop":
                return B(Poly(DropPoly, x, y) || (ax <= 0.6f && ay <= 0.6f));

            case "lock":
                {
                    bool body = ax <= 0.65f && y >= -0.8f && y <= 0.1f;
                    bool shackle = ax <= 0.45f && ax >= 0.22f && y >= 0.1f && y <= 0.75f;
                    bool top = y >= 0.55f && y <= 0.75f && ax <= 0.45f;
                    return B(body || shackle || top);
                }

            case "back":
                {
                    bool shaft = y >= -0.18f && y <= 0.18f && x >= -0.4f && x <= 0.7f;
                    bool head = (x <= -0.05f && ax + ay <= 0.85f && x < 0.2f);
                    return B(shaft || head);
                }

            case "check":
                return B(Seg(x, y, -0.7f, 0f, -0.2f, -0.55f) <= 0.18f || Seg(x, y, -0.2f, -0.55f, 0.75f, 0.6f) <= 0.18f);

            case "close":
                return B(Seg(x, y, -0.65f, -0.65f, 0.65f, 0.65f) <= 0.18f || Seg(x, y, -0.65f, 0.65f, 0.65f, -0.65f) <= 0.18f);

            case "gear":
                {
                    bool hub = ax <= 0.65f && ay <= 0.65f && !(ax <= 0.28f && ay <= 0.28f);
                    bool teethH = ax <= 0.88f && ay <= 0.3f;
                    bool teethV = ax <= 0.3f && ay <= 0.88f;
                    return B(hub || teethH || teethV);
                }

            case "home":
                {
                    bool roof = Poly(RoofPoly, x, y);
                    bool body = ax <= 0.68f && y >= -0.85f && y <= 0.1f;
                    bool door = ax <= 0.22f && y <= -0.2f;
                    return B((roof || body) && !door);
                }

            case "retry":
                {
                    bool outer = ax <= 0.72f && ay <= 0.72f && !(ax <= 0.44f && ay <= 0.44f);
                    bool cut = x > 0f && y > 0.1f;
                    bool arrow = x >= 0.3f && x <= 0.85f && y >= 0.1f && y <= 0.65f;
                    return B((outer && !cut) || arrow);
                }

            default:
                return 0f;
        }
    }

    static bool Poly(float[] p, float x, float y)
    {
        bool inside = false;
        int n = p.Length / 2;
        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            float xi = p[2 * i], yi = p[2 * i + 1], xj = p[2 * j], yj = p[2 * j + 1];
            if (((yi > y) != (yj > y)) && (x < (xj - xi) * (y - yi) / (yj - yi) + xi)) inside = !inside;
        }
        return inside;
    }

    static float Seg(float px, float py, float ax, float ay, float bx, float by)
    {
        float vx = bx - ax, vy = by - ay, wx = px - ax, wy = py - ay;
        float t = Mathf.Clamp01((wx * vx + wy * vy) / (vx * vx + vy * vy));
        float dx = wx - t * vx, dy = wy - t * vy;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }
}
