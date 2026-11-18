using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopRow : MonoBehaviour
{
    public Image icon;
    public TMP_Text title;
    public TMP_Text detail;
    public TMP_Text extra;
    public UiButton button;
    public Image buttonBackground;
    public TMP_Text buttonLabel;

    public void SetState(string label, Color color, Color textColor, bool interactable)
    {
        button.SetLabel(label);
        button.SetRestColor(color);
        button.interactable = interactable;
        buttonLabel.color = textColor;
    }
}
