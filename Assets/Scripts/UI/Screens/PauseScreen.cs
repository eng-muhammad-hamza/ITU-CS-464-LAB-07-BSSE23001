using TMPro;
using UnityEngine;

public class PauseScreen : UIScreen
{
    public TMP_Text levelText;
    public RectTransform[] items;

    protected override void OnShowStart()
    {
        levelText.text = "LEVEL " + (GameData.SelectedLevel + 1) + "  " + Catalog.Levels[GameData.SelectedLevel].name.ToUpper();
    }

    protected override void OnShown() => Stagger(0f, items);
}
