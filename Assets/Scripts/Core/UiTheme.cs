using UnityEngine;

public static class UiTheme
{
    public static readonly Color BgDeep = Hex("0B1026");
    public static readonly Color Bg = Hex("121A3D");
    public static readonly Color Surface = Hex("1B2554");
    public static readonly Color SurfaceHi = Hex("2A3678");
    public static readonly Color Outline = Hex("3B4A93");

    public static readonly Color Action = Hex("22D3EE");
    public static readonly Color Accent = Hex("FF4FA3");
    public static readonly Color Reward = Hex("FFC83D");
    public static readonly Color Danger = Hex("FF5C5C");
    public static readonly Color Success = Hex("3DDC84");

    public static readonly Color Text = Hex("F2F5FF");
    public static readonly Color TextMuted = Hex("9AA6D6");
    public static readonly Color TextOnBright = Hex("0B1026");
    public static readonly Color Locked = Hex("3A4170");

    public const float SizeDisplay = 132f;
    public const float SizeH1 = 76f;
    public const float SizeH2 = 54f;
    public const float SizeBody = 42f;
    public const float SizeCaption = 32f;

    public const float TouchMin = 150f;
    public const float Margin = 54f;
    public const float Gap = 36f;

    public static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString("#" + hex, out Color color);
        return color;
    }

    public static Color WithAlpha(Color color, float alpha) { color.a = alpha; return color; }
}
