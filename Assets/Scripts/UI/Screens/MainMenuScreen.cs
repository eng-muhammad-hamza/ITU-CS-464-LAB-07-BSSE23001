using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    public RectTransform logo;
    public RectTransform orbHolder;
    public Image orbBody;
    public RectTransform playWrapper;
    public RectTransform secondaryRow;
    public RectTransform footer;
    public RectTransform topBar;
    public TMP_Text progressText;

    protected override void OnShowStart()
    {
        OrbDef orb = Catalog.Orbs[GameData.SelectedOrb];
        if (orbBody != null) orbBody.color = orb.color;
        if (progressText != null) progressText.text = "Stars " + GameData.TotalStars() + " / " + (Catalog.Levels.Length * 3);
    }

    protected override void OnShown() => Stagger(0f, topBar, logo, orbHolder, playWrapper, secondaryRow, footer);
}
