using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : UIScreen
{
    public RectTransform viewport;
    public RectTransform content;
    public float scrollSpeed = 95f;

    float startY;
    float endY;

    protected override void OnShowStart()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        startY = -viewport.rect.height;
        endY = content.rect.height;
        content.anchoredPosition = new Vector2(0f, startY);
    }

    void Update()
    {
        if (!IsVisible) return;
        bool touching = Input.touchCount > 0 || Input.GetMouseButton(0);
        if (!touching) content.anchoredPosition += Vector2.up * (scrollSpeed * Time.unscaledDeltaTime);
        if (content.anchoredPosition.y > endY) content.anchoredPosition = new Vector2(0f, startY);
    }
}
