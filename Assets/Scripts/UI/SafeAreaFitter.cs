using UnityEngine;

[ExecuteAlways]
public class SafeAreaFitter : MonoBehaviour
{
    Rect lastArea;
    Vector2Int lastSize;

    void OnEnable() => Apply();
    void Update() { if (Screen.safeArea != lastArea || lastSize.x != Screen.width || lastSize.y != Screen.height) Apply(); }

    void Apply()
    {
        RectTransform rt = (RectTransform)transform;
        Rect area = Screen.safeArea;
        lastArea = area;
        lastSize = new Vector2Int(Screen.width, Screen.height);
        if (Screen.width <= 0 || Screen.height <= 0) return;
        Vector2 min = area.position, max = area.position + area.size;
        min.x /= Screen.width; min.y /= Screen.height;
        max.x /= Screen.width; max.y /= Screen.height;
        rt.anchorMin = min;
        rt.anchorMax = max;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
